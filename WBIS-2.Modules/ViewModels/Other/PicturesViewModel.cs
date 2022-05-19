using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.ViewModels
{
    public class PicturesViewModel : BindableBase
    {
        WBIS2Model Database = new WBIS2Model();

        public void FillPictures(List<IInformationType> Query, Type QueryType)
        {
            var parameterExp = Expression.Parameter(typeof(Picture), "type");
            var propertyExp = Expression.Property(parameterExp, typeof(Picture).GetProperty(QueryType.Name));
            MethodInfo method = Query.GetType().GetMethod("Contains", new[] { QueryType });
            var someValue = Expression.Constant(Query);
            var containsMethodExp = Expression.Call(someValue, method, propertyExp);
            var a = Expression.Lambda<Func<Picture, bool>>(containsMethodExp, parameterExp);


            //var pictureData = Database.Pictures
            //    .Include(QueryType.Name)
            //    .Where(a);
            var pictureData = Database.Pictures.Take(5);

            Pictures = new ObservableCollection<ImageView>();
            foreach (var p in pictureData)
            {
                Pictures.Add(new ImageView(p));
            }
            RaisePropertyChanged(nameof(Pictures));



            LogoImage = Database.Pictures.First().PreviewData;
            RaisePropertyChanged(nameof(LogoImage));

            NewStream = new MemoryStream(Database.Pictures.First().PreviewData);
            RaisePropertyChanged(nameof(NewStream));


            Image image = Image.FromStream(new MemoryStream(pictureData.First().PreviewData));
                //image.Save("output.jpg", ImageFormat.Jpeg);
            RaisePropertyChanged(nameof(image));


            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create((BitmapSource)e.ImageSource));
            //encoder.

            //MemoryStream stream = new MemoryStream(pictureData.First().PreviewData);
            //BitmapImage image = new BitmapImage();
            //image.BeginInit();
            //image.StreamSource = stream;
            //image.EndInit();
            //PreviewImage = image;
            //RaisePropertiesChanged(nameof(PreviewImage));



            //BitmapImage btm;
            //using (var ms = new MemoryStream(pictureData.First().PreviewData))
            //{
            //    btm = new BitmapImage();
            //    btm.BeginInit();
            //    btm.StreamSource = ms;
            //    btm.CacheOption = BitmapCacheOption.OnLoad;
            //    btm.EndInit();
            //    btm.Freeze();
            //}
            //PreviewImage = btm;
            //RaisePropertiesChanged(nameof(PreviewImage));
        }
        public ObservableCollection<ImageView> Pictures { get; set; }


       // public BitmapImage PreviewImage { get;set;}
        public byte[] LogoImage
        {
            get { return GetProperty(()=>LogoImage); }
            set { SetProperty(()=>LogoImage, value);}
        }
        public Stream NewStream { get; set; }
        public Image image { get; set; }




        public ICommand SaveSingleCommand => new DelegateCommand(SaveSingleClick);
        private void SaveSingleClick()
        {

        }

        public ICommand SaveAllCommand => new DelegateCommand(SaveAllClick);
        private void SaveAllClick()
        {

        }


        public bool UploadEnabled { get; set; }= false;
        public ICommand UploadCommand => new DelegateCommand(UploadClick);
        private void UploadClick()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog().Value)
            {
                var bitmap = new Bitmap(ofd.FileName);
                var imageData = converterDemo(bitmap);
                var previewData = converterDemo(new Bitmap(bitmap, new System.Drawing.Size(bitmap.Width / 8, bitmap.Height / 8)));

                Picture picture = new Picture() { DateTime = DateTime.Now, ImageData = imageData, PreviewData = previewData, Guid = Guid.NewGuid() };
                               

                Pictures = new ObservableCollection<ImageView>();
                Pictures.Add(new ImageView(picture));
                
                RaisePropertyChanged(nameof(Pictures));
            }
        }
        public static byte[] converterDemo(System.Drawing.Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
    }
}
