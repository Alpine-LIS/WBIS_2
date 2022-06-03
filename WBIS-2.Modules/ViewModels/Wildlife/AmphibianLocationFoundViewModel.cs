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
    public class AmphibianLocationFoundViewModel : DetailViewModelBase, IDocumentContent, IDetailView, IPictures
    {
        public static bool AddSingle => false;
        public AmphibianElement element
        {
            get { return GetProperty(() => element); }
            set
            {
                SetProperty(() => element, value);
                Record = element;
            }
        }
        public AmphibianLocationFound locationFound
        {
            get { return GetProperty(() => locationFound); }
            set
            {
                SetProperty(() => locationFound, value);
            }
        }



        public object Title => $"{locationFound.AmphibianSpecies.SpeciesCode}{ChangedSign}";


        public static AmphibianLocationFoundViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new AmphibianLocationFoundViewModel(guid));
        }

        public AmphibianLocationFoundViewModel(Guid guid)
        {
            element = Database.AmphibianElements
                .Include(_=>_.AmphibianLocationFound).ThenInclude(_=>_.AmphibianSpecies)
                .Include(_=>_.User)
                .Include(_=>_.Pictures)
                .First(_ => _.Guid == guid);

            locationFound = element.AmphibianLocationFound;

                SetDateValues();     
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

        
        public override void Save()
        {
            if (!this.Changed) return;

            if (HasErrors() || locationFound.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();
        
            GetDateValues();

            Database.AmphibianElements.Update(element);
            Database.AmphibianLocationsFound.Update(locationFound);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

      
        public AmphibianSpecies[] AmphibianSpecies => Database.AmphibianSpecies.Where(_=>!_.PlaceHolder).ToArray();



        [Required]
        public DateTime foundDate { get; set; } = DateTime.MinValue;
        [Required, Range(0,23,ErrorMessage ="Please enter a value between 0 and 23.")]
        public int foundHour { get; set; } =0;
        [Required, Range(0, 59, ErrorMessage = "Please enter a value between 0 and 59.")]
        public int foundMinute { get; set; } = 0;
              

        private void SetDateValues()
        {
            foundDate = element.DateTime.Date;
            foundHour = element.DateTime.Hour;
            foundMinute = element.DateTime.Minute;
        }
        private void GetDateValues()
        {
            element.DateTime = new DateTime(foundDate.Year, foundDate.Month, foundDate.Day, foundHour, foundMinute, 0);
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

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
           
        }

        public void OnDestroy()
        {
        }

        public ImageView SelectedImage
        {
            get { return GetProperty(() => SelectedImage);}
            set { SetProperty(() => SelectedImage, value);}
        }
        public bool PictureUploadEnabled { get; set; } = false;




        public ICommand PlantOfInterstCommane => new DelegateCommand(PlantOfInterstClick);
        private void PlantOfInterstClick()
        {
            //var be = Database.BotanicalElements
            //     .Include(_ => _.BotanicalScoping)
            //     .Include(_ => _.BotanicalSurveyArea)
            //     .Include(_ => _.BotanicalSurvey)
            //     .AsNoTracking()
            //    .First(_ => _.Guid == element.Guid);
            //    be.Guid = Guid.NewGuid();
            //be.Pictures = new List<Picture>();
            //be.RecordType = "Plants of Interest";
            //be.BotanicalPlantOfInterest = new BotanicalPlantOfInterest()
            //{
            //    Guid = be.Guid,
            //    DateTime = pointOfInterest.DateTime,
            //    Radius = pointOfInterest.Radius,
            //    SpeciesFound = true
            //};

            //IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();

            //var viewModel = BotanicalPlantOfInterestViewModel.CreateNew(be, element.User.UserName);
            //    IDocument document = service.CreateDocument("BotanicalPlantOfInterestView", viewModel, be.Guid, this);
            //    document.Id = be.Guid;
        }
    }
}
