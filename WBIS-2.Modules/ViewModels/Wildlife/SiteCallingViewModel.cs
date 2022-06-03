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

namespace WBIS_2.Modules.ViewModels
{
    public class SiteCallingViewModel : DetailAndChildrenViewModelBase, IDocumentContent, IDetailView, IPictures
    {
        public static bool AddSingle => false;
        public SiteCalling Calling
        {
            get { return GetProperty(() => Calling); }
            set
            {
                SetProperty(() => Calling, value);
                Record = Calling;
            }
        }
              
        public object Title => $"{Calling.RecordType}: {Calling.SurveySpecies.Species}{ChangedSign}";


        public static SiteCallingViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new SiteCallingViewModel(guid));
        }

        public SiteCallingViewModel(Guid guid)
        {
            Calling = Database.SiteCallings
                .Include(_ => _.User)
                .Include(_ => _.SurveySpecies)
                .Include(_ => _.Hex160)
                .Include(_ => _.ProtectionZone)
                .First(_ => _.Guid == guid);
            ParentType = Calling;
            RaisePropertyChanged(nameof(ParentType));  
            
            ProtectionZones = Database.ProtectionZones
            .Where(_ => (_.Geometry.IsWithinDistance(Calling.Geometry, 3218.69) && !_._delete && !_.Repository) || _ == Calling.ProtectionZone).ToArray();
            SetDateValues();
            PassNumber = Calling.PassNumber;
            //RefreshOtherWildlifeRecords();
           // RefreshOtherWildlifeRecords();
        }
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (CurrentChild == null) return;
            if (ParentType == null) return;

            e.QueryableSource = CurrentChild.Manager.GetQueryable(new object[] { Calling }, ParentType.GetType(), Database, ViewDeleted, ViewRepository);

            FillPictures();
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

        public override void OnDestroy()
        {
            //throw new NotImplementedException();
        }

        public override void Save()
        {
            if (!this.Changed) return;

            if (HasErrors() || Calling.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }
            if (new string[] { "SPOW", "NSOW", "SPOW+BDOW"}.Contains(Calling.SurveySpecies.Species) && Calling.SPOW_OccupancyStatus == null)
            {
                MessageBox.Show("The SPOW occupancy status must be filled when the survey species is a spotted owl.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            GetDateValues();

            if (PassNumber != Calling.PassNumber)
            {
                Calling.ManualPassChanged = true;
                Calling.PassNumber = PassNumber;
            }

            if (!Database.SiteCallings.Contains(Calling))
                Database.SiteCallings.Add(Calling);
            else Database.SiteCallings.Update(Calling);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }




        public string[] Type2Surveys => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "survey_type2").Select(_ => _.SelectionText).ToArray();
        public string[] Type1Surveys => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "survey_type1").Select(_ => _.SelectionText).ToArray();
        public string[] Precips => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "precipitation").Select(_ => _.SelectionText).ToArray();
        public string[] Winds => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "wind").Select(_ => _.SelectionText).ToArray();
        public string[] NestingOptions => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "nesting_status").Select(_ => _.SelectionText).ToArray();
        public string[] ReproductiveOptions => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "reproductive_status").Select(_ => _.SelectionText).ToArray();
        public string[] OccupancyOptions => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "spow_occupancy_status").Select(_ => _.SelectionText).ToArray();
        public string[] NestTypes => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "nest_type").Select(_ => _.SelectionText).ToArray();
        public string[] TreeSpesies => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(SiteCalling)) && _.Property == "tree_species").Select(_ => _.SelectionText).ToArray();


        [Required]
        public DateTime StartDate { get { return GetProperty(() => StartDate); } set { SetProperty(() => StartDate, value); UpdateTimeSpent(); } }
        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int StartHour { get { return GetProperty(() => StartHour); } 
            set { SetProperty(() => StartHour, value);
                UpdateTimeSpent();
            } }
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int StartMinute { get { return GetProperty(() => StartMinute); } 
            set { SetProperty(() => StartMinute, value); 
                UpdateTimeSpent();
            } }

        [Required]
        public DateTime EndDate { get { return GetProperty(() => EndDate); } set { SetProperty(() => EndDate, value); UpdateTimeSpent(); } }
        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int EndHour { get { return GetProperty(() => EndHour); } 
            set { SetProperty(() => EndHour, value);
                UpdateTimeSpent();
            } }
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int EndMinute { get { return GetProperty(() => EndMinute); } 
            set { SetProperty(() => EndMinute, value);
                UpdateTimeSpent();
            } }
        private void SetDateValues()
        {
            StartDate = Calling.StartTime.Date;
            StartHour = Calling.StartTime.Hour;
            StartMinute = Calling.StartTime.Minute;

            EndDate = Calling.EndTime.Date;
            EndHour = Calling.EndTime.Hour;
            EndMinute = Calling.EndTime.Minute;
        }
        private void GetDateValues()
        {
            Calling.StartTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartHour, StartMinute, 0);
            Calling.EndTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndHour, EndMinute, 0);
        }
        private void UpdateTimeSpent()
        {
            try
            {
                var start = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartHour, StartMinute, 0);
                var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndHour, EndMinute, 0);
                TimeSpent = end.Subtract(start).TotalMinutes.ToString("N2");
                RaisePropertyChanged(nameof(TimeSpent));

                var conn = new Npgsql.NpgsqlConnection(WBIS2Model.GetRDSConnectionString());
                conn.Open();
                string a = $"SELECT RISE_SET_TIME(ST_Y(ST_Transform(geometry, 4267)), ST_X(ST_Transform(geometry, 4267)), FALSE, '{start}') at time zone 'America/Los_Angeles' + (INTERVAL '1 day')" +
                    $"FROM site_callings WHERE guid = '{Calling.Guid}' limit 1";
                var cmd = new Npgsql.NpgsqlCommand(a, conn);
                var test = cmd.ExecuteScalar();
                if (!(test is DBNull))
                {
                    Calling.SunsetTime = (DateTime)test;
                    RaisePropertyChanged(nameof(Calling));
                }
                conn.Close();
            }
            catch (Exception)
            {

            }
        }
        public string TimeSpent { get; set; }


        public BirdSpecies[] AvailibleSpecies => Database.BirdSpecies.Where(_=>_.IsSurveyable).ToArray();
        public ProtectionZone[] ProtectionZones { get; set; }

        public int PassNumber { get; set; } = 0;





        //public EntityInstantFeedbackSource OtherWildlifeRecords { get; set; }
        //internal virtual void RefreshOtherWildlifeRecords()
        //{
        //    OtherWildlifeRecords = new EntityInstantFeedbackSource
        //    {
        //        AreSourceRowsThreadSafe = true,
        //        KeyExpression = $"Guid",
        //    };
        //    OtherWildlifeRecords.GetQueryable += RecordsOtherWildlifeRecords;
        //    OtherWildlifeRecords.Refresh();
        //    RaisePropertyChanged(nameof(OtherWildlifeRecords));
        //}
        //public void RecordsOtherWildlifeRecords(object sender, GetQueryableEventArgs e)
        //{
        //    e.QueryableSource = Database.OtherWildlifeRecords
        //        .Include(_ => _.SiteCalling)
        //        .Where(_ => _.SiteCalling == Calling);
        //    //e.QueryableSource = Database.SiteCallings
        //    //    .Include(_ => _.OtherWildlifeRecords)
        //    //    .Where(_ => _.Guid == Calling.Guid)
        //    //    .Select(_=>_.OtherWildlifeRecords);
        //}





        public ObservableCollection<ImageView> Pictures { get; set; }
        public void FillPictures()
        {
            var pictureData = Database.Pictures
                .Include(_ => _.SiteCalling)
                .Where(_ => _.SiteCalling.Guid == Calling.Guid);

            Pictures = new ObservableCollection<ImageView>();
            foreach (var p in pictureData)
            {
                Pictures.Add(new ImageView(p));
            }
            RaisePropertyChanged(nameof(Pictures));
        }

        public void Upload()
        {
            Picture picture = ((IPictures)this).CreatePicture();
            if (picture == null) return;

            Pictures.Add(new ImageView(picture));
            RaisePropertiesChanged(nameof(Pictures));
        }
       

        public void SaveSingle()
        {
            if (SelectedImage == null) return;
            string fileName = TextWriters.SaveFileName("JPG|*.jpg");
            if (fileName == null) return;

            MemoryStream stream = new MemoryStream( SelectedImage.Picture.ImageData);
            Image image = Image.FromStream(stream);
            image.Save(fileName);
            if (MessageBox.Show("Would you like to open your photo?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                new Process { StartInfo = new ProcessStartInfo(fileName) { UseShellExecute = true } }.Start();
        }

        public void SaveAll()
        {
            string fileName = TextWriters.SaveDirectoryName();
            if (fileName == null) return;

            Directory.CreateDirectory(fileName);
            int counter = 0;
            foreach(var pic in Pictures)
            {
                MemoryStream stream = new MemoryStream(pic.Picture.ImageData);
                Image image = Image.FromStream(stream);
                image.Save($@"{fileName}\{counter.ToString("0000")}.jpg");
                counter++;
            }
            MessageBox.Show($"Photos have been saved to {fileName}");
        }

        public ImageView SelectedImage
        {
            get { return GetProperty(() => SelectedImage);}
            set { SetProperty(() => SelectedImage, value);}
        }
        public bool PictureUploadEnabled { get; set; } = false;
    }
}
