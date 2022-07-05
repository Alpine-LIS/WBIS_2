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
    public class AmphibianSurveyViewModel : DetailAndChildrenViewModelBase, IDocumentContent, IDetailView, IPictures
    {
        public static bool AddSingle => false;
        public AmphibianSurvey Survey
        {
            get { return GetProperty(() => Survey); }
            set
            {
                SetProperty(() => Survey, value);
                Record = Survey;
            }
        }
              
        public object Title => $"{Survey.SiteID}: {Survey.DateTime}{ChangedSign}";


        public static AmphibianSurveyViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new AmphibianSurveyViewModel(guid));
        }

        public AmphibianSurveyViewModel(Guid guid)
        {
            Survey = Database.AmphibianSurveys
                .Include(_ => _.User)
                .First(_ => _.Id == guid);
            ParentType = Survey;
            RaisePropertyChanged(nameof(ParentType));  
            
            SetDateValues();
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

            GetDateValues();

            if (!Database.AmphibianSurveys.Contains(Survey))
                Database.AmphibianSurveys.Add(Survey);
            else Database.AmphibianSurveys.Update(Survey);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }





        [Required]
        public DateTime SurveyDate { get { return GetProperty(() => SurveyDate); } set { SetProperty(() => SurveyDate, value); } }
        [Required, Range(0, 23, ErrorMessage = "Please enter a value between 0 and 23.")]
        public int SurveyHour
        { get { return GetProperty(() => SurveyHour); } 
            set { SetProperty(() => SurveyHour, value);
            } }
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int SurveyMinute
        { get { return GetProperty(() => SurveyMinute); } 
            set { SetProperty(() => SurveyMinute, value); 
            } }

     
        private void SetDateValues()
        {
            SurveyDate = Survey.DateTime.Date;
            SurveyHour = Survey.DateTime.Hour;
            SurveyMinute = Survey.DateTime.Minute;
        }
        private void GetDateValues()
        {
            Survey.DateTime = new DateTime(SurveyDate.Year, SurveyDate.Month, SurveyDate.Day, SurveyHour, SurveyMinute, 0);
        }



        public string[] WaterTypes => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(AmphibianSurvey)) && _.Property == "water_type").Select(_ => _.SelectionText).ToArray();
        public string[] FlowSeasonality => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(AmphibianSurvey)) && _.Property == "seasonality_of_flow").Select(_ => _.SelectionText).ToArray();
        public string[] Counties => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(AmphibianSurvey)) && _.Property == "county").Select(_ => _.SelectionText).ToArray();
        public string[] Weathers => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(AmphibianSurvey)) && _.Property == "weather").Select(_ => _.SelectionText).ToArray();
        public string[] Winds => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(AmphibianSurvey)) && _.Property == "wind").Select(_ => _.SelectionText).ToArray();
        public string[] Flows => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(AmphibianSurvey)) && _.Property == "flow").Select(_ => _.SelectionText).ToArray();





        public ObservableCollection<ImageView> Pictures { get; set; }
        public void FillPictures()
        {
            var pictureData = Database.Pictures
                .Include(_ => _.AmphibianElement).ThenInclude(_=>_.AmphibianSurvey)
                .Where(_ => _.AmphibianElement.AmphibianSurvey.Id == Survey.Id);

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
