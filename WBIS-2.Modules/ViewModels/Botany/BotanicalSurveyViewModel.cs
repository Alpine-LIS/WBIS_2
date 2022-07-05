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
    public class BotanicalSurveyViewModel : DetailAndChildrenViewModelBase, IDocumentContent, IDetailView, IPictures
    {
        public static bool AddSingle => false;
        public BotanicalSurvey Survey
        {
            get { return GetProperty(() => Survey); }
            set
            {
                SetProperty(() => Survey, value);
                Record = Survey;
            }
        }


        public string[] ThpNames { get; set; }
        [Required]
        public string ThpName
        {
            get { return GetProperty(() => ThpName); }
            set
            {
                SetProperty(() => ThpName, value);

                AreaNames = Database.BotanicalSurveyAreas
                    .Include(_ => _.THP_Area)
                    .Where(_ => _.THP_Area.THPName == ThpName).Select(_ => _.AreaName)
                    .Where(_ => _ != null).Where(_ => _ != "").OrderBy(_ => _).ToArray();
                RaisePropertyChanged(nameof(AreaNames));
            }
        }

        public string[] AreaNames { get; set; }
        [Required]
        public string AreaName
        {
            get { return GetProperty(() => AreaName); }
            set { SetProperty(() => AreaName, value); }
        }


        public object Title => $"Survey: {ThpName}: {AreaName} {StartDate.ToShortDateString()}{ChangedSign}";


        public static BotanicalSurveyViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new BotanicalSurveyViewModel(guid));
        }

        public BotanicalSurveyViewModel(Guid guid)
        {
            ThpNames = Database.THP_Areas
                .Include(_=>_.BotanicalSurveyAreas).Where(_=>_.BotanicalSurveyAreas != null)
                .Select(_=>_.THPName).OrderBy(_=>_).ToArray();

            Survey = Database.BotanicalSurveys
                   .Include(_ => _.BotanicalScoping)
                     .Include(_ => _.BotanicalSurveyArea)
                 .Include(_ => _.THP_Area)
                   .Include(_ => _.Watersheds)
                   .Include(_ => _.District)
                   .Include(_ => _.Quad75s)
                   .FirstOrDefault(_ => _.Id == guid);

            if (Survey != null)
            {
                if (Survey.BotanicalSurveyArea.THP_Area != null) ThpName = Survey.BotanicalSurveyArea.THP_Area.THPName;
                if (Survey.BotanicalSurveyArea.AreaName != null) AreaName = Survey.BotanicalSurveyArea.AreaName;

                SetDateValues();
            }
            else
            {
                Survey = new BotanicalSurvey();
                Survey.Id = guid;
            }

            ParentType = Survey;
            RaisePropertyChanged(nameof(ParentType));
        }
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (CurrentChild == null) return;
            if (ParentType == null) return;

            e.QueryableSource = CurrentChild.Manager.GetQueryable(new object[] { Survey }, ParentType.GetType(), Database, showDelete: ViewDeleted, showRepository: ViewRepository); 

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

            if (HasErrors() || Survey.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var area = Database.BotanicalSurveyAreas
                .Include(_=>_.THP_Area)
                .Include(_=>_.BotanicalScoping)
                .First(_=>_.THP_Area.THPName == ThpName && _.AreaName == AreaName);
            Survey.BotanicalSurveyArea = area;
            Survey.THP_Area = area.THP_Area;
            Survey.BotanicalScoping = area.BotanicalScoping;

            GetDateValues();

            if (!Database.BotanicalSurveys.Contains(Survey))
                Database.BotanicalSurveys.Add(Survey);
            else Database.BotanicalSurveys.Update(Survey);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        public string[] SurveyTypes => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(BotanicalSurvey)) && _.Property == "survey_type").Select(_ => _.SelectionText).ToArray();

        [Required]
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        [Required, Range(0,23,ErrorMessage ="Please enter a value between 0 and 23.")]
        public int StartHour { get; set; } =0;
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int StartMinute { get; set; } = 0;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int EndHour { get; set; } = 0;
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int EndMinute { get; set; } = 0;

        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int SpentHour { get; set; } = 0;
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int SpentMinute { get; set; } = 0;
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int SpentSecond { get; set; } = 0;


        private void SetDateValues()
        {
            StartDate = Survey.StartTime.Date;
            StartHour = Survey.StartTime.Hour;
            StartMinute = Survey.StartTime.Minute;

            EndDate = Survey.EndTime.Date;
            EndHour = Survey.EndTime.Hour;
            EndMinute = Survey.EndTime.Minute;

            SpentHour = Survey.TimeSpent.Hours;
            SpentMinute = Survey.TimeSpent.Minutes;
            SpentSecond = Survey.TimeSpent.Seconds;
        }
        private void GetDateValues()
        {
            Survey.StartTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartHour, StartMinute, 0);
            Survey.EndTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndHour, EndMinute, 0);
            Survey.TimeSpent = new TimeSpan(SpentHour, SpentMinute, SpentSecond);
        }












        //public PicturesViewModel PictureDisplay => new PicturesViewModel() { UploadEnabled = true };
        public ObservableCollection<ImageView> Pictures { get; set; }
        public void FillPictures()
        {
            var pictureData = Database.Pictures
                .Include(_ => _.BotanicalElement).ThenInclude(_ => _.BotanicalSurvey)
                .Where(_ => _.BotanicalElement.BotanicalSurvey.Id == Survey.Id);

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
