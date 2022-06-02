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
    public class OwlBandingViewModel : DetailViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => false;
        public OwlBanding Banding
        {
            get { return GetProperty(() => Banding); }
            set
            {
                SetProperty(() => Banding, value);
                Record = Banding;
            }
        }
              
        public object Title => $"{Banding.RecordType}: {Banding.BirdSpecies.Species}{ChangedSign}";


        public static OwlBandingViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new OwlBandingViewModel(guid));
        }

        public OwlBandingViewModel(Guid guid)
        {
            Banding = Database.OwlBandings
                .Include(_ => _.User)
                .Include(_ => _.BirdSpecies)
                .Include(_=>_.ProtectionZone)
                .First(_ => _.Guid == guid);

            SpeciesSites = Database.ProtectionZones
              .Where(_ => (_.Geometry.IsWithinDistance(Banding.Geometry, 3218.69) && !_._delete && !_.Repository) || _ == Banding.ProtectionZone).ToArray();
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

            if (HasErrors() || Banding.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            if (!CheckSave()) return;

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            GetDateValues();
           
            if (!Database.OwlBandings.Contains(Banding))
                Database.OwlBandings.Add(Banding);
            else Database.OwlBandings.Update(Banding);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        private bool CheckSave()
        {
           
            return true;
        }


        [Required]
        public DateTime StartDate { get { return GetProperty(() => StartDate); } set { SetProperty(() => StartDate, value); } }
        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int StartHour
        {
            get { return GetProperty(() => StartHour); }
            set { SetProperty(() => StartHour, value);}
        }
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int StartMinute
        {
            get { return GetProperty(() => StartMinute); }
            set {SetProperty(() => StartMinute, value);}
        }

        [Required]
        public DateTime EndDate { get { return GetProperty(() => EndDate); } set { SetProperty(() => EndDate, value); } }
        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int EndHour
        {
            get { return GetProperty(() => EndHour); }
            set { SetProperty(() => EndHour, value); }
        }
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int EndMinute
        {
            get { return GetProperty(() => EndMinute); }
            set { SetProperty(() => EndMinute, value); }
        }
        private void SetDateValues()
        {
            StartDate = Banding.StartTime.Date;
            StartHour = Banding.StartTime.Hour;
            StartMinute = Banding.StartTime.Minute;

            EndDate = Banding.EndTime.Date;
            EndHour = Banding.EndTime.Hour;
            EndMinute = Banding.EndTime.Minute;
        }
        private void GetDateValues()
        {
            Banding.StartTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartHour, StartMinute, 0);
            Banding.EndTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndHour, EndMinute, 0);
        }

        public void OnDestroy()
        {
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public BirdSpecies[] AvailibleSpecies => Database.BirdSpecies.Where(_=>_.BandingSpecies).ToArray();
        public ProtectionZone[] SpeciesSites { get; set; }
        public string[] Sexs => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(OwlBanding)) && _.Property == "sex").Select(_ => _.SelectionText).ToArray();
        public string[] AgeClasses => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(OwlBanding)) && _.Property == "age_class").Select(_ => _.SelectionText).ToArray();
        public string[] CaptureMethods => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(OwlBanding)) && _.Property == "capture_method").Select(_ => _.SelectionText).ToArray();
        public string[] USFWS_BandColors => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(OwlBanding)) && _.Property == "usfws_band_color").Select(_ => _.SelectionText).ToArray();
        public string[] Bands => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(OwlBanding)) && _.Property == "bands").Select(_ => _.SelectionText).ToArray();
        public string[] TrapCodes => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(OwlBanding)) && _.Property == "trap_code").Select(_ => _.SelectionText).ToArray();

        public void BandingClick()
        {
           BandingPatternControl bandingPatternControl = new BandingPatternControl(Banding.BandingLeg + ":" + Banding.BandingPattern, false);
            CustomControlWindow window = new CustomControlWindow(bandingPatternControl);
            if (window.DialogResult)
            {
                var vals = bandingPatternControl.ReturnValue.ToString().Split(':');
                Banding.BandingLeg = vals[0];
                if (vals.Length > 1) Banding.BandingPattern = vals[1];
                else Banding.BandingPattern = "";
                RaisePropertyChanged(nameof(Banding));
            }
        }
    }
}
