﻿using Atlas.Data;
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
    public class BotanicalPlantOfInterestViewModel : DetailAndChildrenViewModelBase, IDocumentContent, IDetailView, IPictures
    {
        public static bool AddSingle => false;
        public BotanicalElement element
        {
            get { return GetProperty(() => element); }
            set
            {
                SetProperty(() => element, value);
                Record = element;
            }
        }
        public BotanicalPlantOfInterest plantOfInterest
        {
            get { return GetProperty(() => plantOfInterest); }
            set
            {
                SetProperty(() => plantOfInterest, value);
            }
        }



        public object Title => $"{plantOfInterest.PlantSpecies.SpeciesCode}{ChangedSign}";


        public static BotanicalPlantOfInterestViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new BotanicalPlantOfInterestViewModel(guid));
        }

        public BotanicalPlantOfInterestViewModel(Guid guid)
        {
            element = Database.BotanicalElements
                .Include(_=>_.BotanicalPlantOfInterest).ThenInclude(_=>_.PlantSpecies)
                .Include(_=>_.User)
                .Include(_=>_.Pictures)
                .First(_ => _.Guid == guid);
            plantOfInterest = element.BotanicalPlantOfInterest;

            SetDateValues();
         
            ParentType = element;
            RaisePropertyChanged(nameof(ParentType));

            PlantSpecies = Database.PlantSpecies.Where(_=>!_.PlaceHolder || _.Guid == plantOfInterest.PlantSpecies.Guid).ToArray();

            SciName = plantOfInterest.PlantSpecies.SciName;
            ComName = plantOfInterest.PlantSpecies.ComName;
            Family = plantOfInterest.PlantSpecies.Family;

            SciNames = PlantSpecies.Select(_ => _.ComName).Distinct().ToArray();
            ComNames = PlantSpecies.Select(_ => _.ComName).Distinct().ToArray();
            Families = PlantSpecies.Select(_ => _.Family).Distinct().ToArray();

            User = element.User.UserName;
        }
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (CurrentChild == null) return;
            if (ParentType == null) return;

            e.QueryableSource = Database.BotanicalElements
                .Include(_ => _.BotanicalPlantList).ThenInclude(_ => _.PlantSpecies)
                .Include(_ => _.BotanicalPlantList).ThenInclude(_ => _.BotanicalPlantOfInterest)
                .Where(_ => _.BotanicalPlantList != null)
                .Where(_ => _.BotanicalPlantList.BotanicalPlantOfInterest != null)
                .Where(_ => _.BotanicalPlantList.BotanicalPlantOfInterest.Guid == plantOfInterest.Guid);

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

            if (HasErrors() || plantOfInterest.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var ps = Database.PlantSpecies.Include(_ => _.BotanicalPlantsOfInterest)
                .First(_ => _.SciName == SciName && _.ComName == ComName && _.Family == Family);
            plantOfInterest.PlantSpecies = ps;

            GetDateValues();

            Database.BotanicalElements.Update(element);
            Database.BotanicalPlantsOfInterest.Update(plantOfInterest);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        public string[] SiteQualities => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(BotanicalPlantOfInterest)) && _.Property == "site_quality").Select(_ => _.SelectionText).ToArray();
        public string[] LandUses => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(BotanicalPlantOfInterest)) && _.Property == "land_use").Select(_ => _.SelectionText).ToArray();
        public string[] Users => Database.ApplicationUsers.Where(_ => (_.Botany && !_._delete && !_.PlaceHolder) || _.UserName == User ).Select(_ => _.UserName).OrderBy(_=>_).ToArray();
        [Required]
        public string User { get; set; }

        [Required]
        public DateTime foundDate { get; set; } = DateTime.MinValue;
        [Required, Range(0,23,ErrorMessage ="Please enter a value between 0 and 23.")]
        public int foundHour { get; set; } =0;
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int foundMinute { get; set; } = 0;
              

        private void SetDateValues()
        {
            foundDate = plantOfInterest.DateTime.Date;
            foundHour = plantOfInterest.DateTime.Hour;
            foundMinute = plantOfInterest.DateTime.Minute;
        }
        private void GetDateValues()
        {
            plantOfInterest.DateTime = new DateTime(foundDate.Year, foundDate.Month, foundDate.Day, foundHour, foundMinute, 0);
        }



        PlantSpecies[] PlantSpecies = new PlantSpecies[0];
        [Required]
        public string SciName
        {
            get { return GetProperty(()=>SciName); }
            set 
            {
                SetProperty(()=>SciName, value);
                FillFromSciName();
            }
        }
        public string[] SciNames{get;set;}
        public string ComName
        {
            get { return GetProperty(() => ComName); }
            set
            {
                SetProperty(() => ComName, value);
                FillFromComName();
            }
        }
        public string[] ComNames { get; set; }
        public string Family
        {
            get { return GetProperty(() => Family); }
            set
            {
                SetProperty(() => Family, value);
                FillFromFamily();
            }
        }
        public string[] Families { get; set; }

        private void FillFromSciName()
        {
            ComNames = PlantSpecies.Where(_=>_.SciName == SciName).Select(_=>_.ComName).Distinct().ToArray();
            Families = PlantSpecies.Where(_ => _.SciName == SciName).Select(_ => _.Family).Distinct().ToArray();
            RaisePropertyChanged(nameof(ComNames));
            RaisePropertyChanged(nameof(Families));
        }
        private void FillFromComName()
        {
            SciNames = PlantSpecies.Where(_ => _.ComName == ComName).Select(_ => _.SciName).Distinct().ToArray();
            Families = PlantSpecies.Where(_ => _.ComName == ComName).Select(_ => _.Family).Distinct().ToArray();
            RaisePropertyChanged(nameof(SciNames));
            RaisePropertyChanged(nameof(Families));
        }
        private void FillFromFamily()
        {
            SciNames = PlantSpecies.Where(_ => _.Family == Family).Select(_ => _.SciName).Distinct().ToArray();
            ComNames = PlantSpecies.Where(_ => _.Family == Family).Select(_ => _.ComName).Distinct().ToArray();
            RaisePropertyChanged(nameof(SciNames));
            RaisePropertyChanged(nameof(ComNames));
        }






        //public PicturesViewModel PictureDisplay => new PicturesViewModel() { UploadEnabled = true };
        public ObservableCollection<ImageView> Pictures { get; set; }
        public void FillPictures()
        {
            var pictureData = element.Pictures;

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
            element.Pictures.Add(picture);
            Changed = true;
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
