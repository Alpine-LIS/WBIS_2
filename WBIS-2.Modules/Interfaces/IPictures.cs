using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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
    public interface IPictures
    {
        ObservableCollection<ImageView> Pictures { get; set; }
        void FillPictures();
        public bool PictureUploadEnabled { get; set; }    
        ICommand UploadCommand => new DelegateCommand(Upload);
        void Upload();
        public ICommand SaveSingleCommand => new DelegateCommand(SaveSingle);
        void SaveSingle();
        public ICommand SaveAllCommand => new DelegateCommand(SaveAll);
        public void  SaveAll();
        public ImageView SelectedImage { get; set; }  
        
        public Picture CreatePicture()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.jpg";
            if (!ofd.ShowDialog().Value) return null;

            System.Drawing.Image image = System.Drawing.Image.FromFile(ofd.FileName);

            foreach (var prop in image.PropertyItems)
            {
                if (prop.Id == 0x112)
                {
                    var val = prop.Value[0];
                    if (val == 3 || val == 4)
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else if (val == 5 || val == 6)
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    else if (val == 7 || val == 8)
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
            }

            var imageData = converterDemo(image);
            var previewData = converterDemo(new Bitmap(image, new System.Drawing.Size(image.Width / 8, image.Height / 8)));

            Picture picture = new Picture()
            {
                DateTime = DateTime.Now,
                ImageData = imageData,
                PreviewData = previewData
            };
            return picture;
        }
        byte[] converterDemo(System.Drawing.Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
    }
}
