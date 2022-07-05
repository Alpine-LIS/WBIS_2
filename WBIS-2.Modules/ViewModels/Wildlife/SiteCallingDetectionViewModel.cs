using Atlas.Data;
using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using WBIS_2.Modules.Tools;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Diagnostics;
using WBIS_2.Modules.Views.UserControls;

namespace WBIS_2.Modules.ViewModels
{
    public class SiteCallingDetectionViewModel : DetailViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => false;
        public SiteCallingDetection Detection
        {
            get { return GetProperty(() => Detection); }
            set
            {
                SetProperty(() => Detection, value);
                Record = Detection;
            }
        }
              
        public object Title => $"{Detection.DetectionMethod}: {Detection.SpeciesFound.Species}{ChangedSign}";


        public static SiteCallingDetectionViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new SiteCallingDetectionViewModel(guid));
        }

        public SiteCallingDetectionViewModel(Guid guid)
        {
            Detection = Database.SiteCallingDetections
                .Include(_ => _.User)
                .Include(_ => _.SpeciesFound)
                .First(_ => _.Id == guid);

            SpeciesSites = Database.ProtectionZones
              .Where(_ => (_.Geometry.IsWithinDistance(Detection.Geometry, 3218.69) && !_._delete && !_.Repository)).Select(_=>_.PZ_ID).ToArray();
            SetDateValues();
        }

        public override void CloseForm()
        {
            throw new NotImplementedException();
        }

        public override void OnClose(CancelEventArgs e)
        {
            if (Changed)
            {
                var result = ThemedMessageBox.Show(title: "Confirmation", text: UnsavedMessageText, messageBoxButtons: MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        public override void Save()
        {
            if (!this.Changed) return;

            if (HasErrors() || Detection.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            if (!CheckSave()) return;

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            GetDateValues();
           
            if (!Database.SiteCallingDetections.Contains(Detection))
                Database.SiteCallingDetections.Add(Detection);
            else Database.SiteCallingDetections.Update(Detection);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        private bool CheckSave()
        {
            if (new string[] {"NSOW","CSOW","SPOW+BDOW" }.Contains(Detection.SpeciesFound.Species))
            {
                if(Detection.Sex.Contains("Male"))
                {
                    if (Detection.MaleBandingLeg == null)
                    {
                        MessageBox.Show("Male spotted owls must have banding information filled.");
                        return false;
                    }
                    if (Detection.MaleBandingLeg == "")
                    {
                        MessageBox.Show("Male spotted owls must have banding information filled.");
                        return false;
                    }
                }
                if (Detection.Sex.Contains("Female"))
                {
                    if (Detection.FemaleBandingLeg == null)
                    {
                        MessageBox.Show("Female spotted owls must have banding information filled.");
                        return false;
                    }
                    if (Detection.FemaleBandingLeg == "")
                    {
                        MessageBox.Show("Male spotted owls must have banding information filled.");
                        return false;
                    }
                }
            }
            return true;
        }


        [Required]
        public DateTime DetectionDate { get { return GetProperty(() => DetectionDate); } set { SetProperty(() => DetectionDate, value); } }
        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int DetectionHour
        { get { return GetProperty(() => DetectionHour); } 
            set { SetProperty(() => DetectionHour, value); } }
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int DetectionMinute
        { get { return GetProperty(() => DetectionMinute); } 
            set { SetProperty(() => DetectionMinute, value); } }

        private void SetDateValues()
        {
            DetectionDate = Detection.DetectionTime.Date;
            DetectionHour = Detection.DetectionTime.Hour;
            DetectionMinute = Detection.DetectionTime.Minute;
        }
        private void GetDateValues()
        {
            Detection.DetectionTime = new DateTime(DetectionDate.Year, DetectionDate.Month, DetectionDate.Day, DetectionHour, DetectionMinute, 0);
        }

        public void OnDestroy()
        {
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public BirdSpecies[] AvailibleSpecies => Database.BirdSpecies.Where(_=>_.IsFindable).ToArray();
        public string[] SpeciesSites { get; set; }
        public string[] Sexs => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCallingDetection)) && _.Property == "sex").Select(_ => _.SelectionText).ToArray();
        public string[] Ages => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCallingDetection)) && _.Property == "age").Select(_ => _.SelectionText).ToArray();
        public string[] DetectionMethods => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCallingDetection)) && _.Property == "detection_method").Select(_ => _.SelectionText).ToArray();

        //public ICommand MaleBandingLegCommand => new DelegateCommand(MalBandingClick);
        //public ICommand MaleBandingPatternCommand => new DelegateCommand(MalBandingClick);
        public void MalBandingClick()
        {
           BandingPatternControl bandingPatternControl = new BandingPatternControl(Detection.MaleBandingLeg + ":" +Detection.MaleBandingPattern);
            CustomControlWindow window = new CustomControlWindow(bandingPatternControl);
            if (window.DialogResult)
            {
                var vals = bandingPatternControl.ReturnValue.ToString().Split(':');
                Detection.MaleBandingLeg = vals[0];
                if (vals.Length > 1) Detection.MaleBandingPattern = vals[1];
                else Detection.MaleBandingPattern = "";
                RaisePropertyChanged(nameof(Detection));
            }
        }

        //public ICommand FemaleBandingLegCommand => new DelegateCommand(FemalBandingClick);
        //public ICommand FemaleBandingPatternCommand => new DelegateCommand(FemalBandingClick);
        public void FemalBandingClick()
        {
            BandingPatternControl bandingPatternControl = new BandingPatternControl(Detection.FemaleBandingLeg + ":" + Detection.FemaleBandingPattern);
            CustomControlWindow window = new CustomControlWindow(bandingPatternControl);
            if (window.DialogResult)
            {
                var vals = bandingPatternControl.ReturnValue.ToString().Split(':');
                Detection.FemaleBandingLeg = vals[0];
                if (vals.Length > 1) Detection.FemaleBandingPattern = vals[1];
                else Detection.FemaleBandingPattern = "";
                RaisePropertyChanged(nameof(Detection));
            }
        }
    }
}
