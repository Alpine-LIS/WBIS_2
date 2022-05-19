using DevExpress.Mvvm;
using System.Linq;
using System.Windows;
using System;
using System.Windows.Media.Imaging;
using System.IO;
using DevExpress.Xpf.LayoutControl;
using WBIS_2.DataModel;
using System.Drawing;

namespace WBIS_2.Modules.ViewModels
{
    public class CustomFlowLayoutControl : FlowLayoutControl
    {
        #region Dependency Properties
        public bool ChildAddedByUser
        {
            get { return (bool)GetValue(ChildAddedByUserProperty); }
            set { SetValue(ChildAddedByUserProperty, value); }
        }
        public static readonly DependencyProperty ChildAddedByUserProperty = DependencyProperty.Register("ChildAddedByUser", typeof(bool), typeof(CustomFlowLayoutControl), new PropertyMetadata(false));
        public Object SelectedObject
        {
            get { return (Object)GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }
        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(Object), typeof(CustomFlowLayoutControl), new PropertyMetadata(null, selectedItemChanged));
        #endregion
        #region Overrides
        protected override void OnChildAdded(FrameworkElement child)
        {
            base.OnChildAdded(child);
            GroupBox box = child as GroupBox;
            box.StateChanged += box_StateChanged;
            box.MouseLeftButtonUp += box_MouseLeftButtonUp;
            if (ChildAddedByUser)
            {
                if (box != null)
                {
                    box.State = GroupBoxState.Maximized;
                }
            }
        }
        #endregion
        #region Private Methods
        private static void selectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomFlowLayoutControl c = d as CustomFlowLayoutControl;
            if (c != null)
            {
                var child = c.GetLogicalChildren(false).OfType<FrameworkElement>().FirstOrDefault(f => f.DataContext == c.SelectedObject);
                var children = c.GetLogicalChildren(false).OfType<FrameworkElement>().Where(f => f.DataContext is ImageView);
                foreach (var cc in children)
                {
                    (cc.DataContext as ImageView).IsPreview = true;
                }
                DevExpress.Xpf.LayoutControl.GroupBox box = child as DevExpress.Xpf.LayoutControl.GroupBox;
                if (box != null)
                {
                    box.State = GroupBoxState.Maximized;
                    (box.DataContext as ImageView).IsPreview = false;
                }
                c.UpdateLayout();
            }
        }
        private void box_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var groupBox = (DevExpress.Xpf.LayoutControl.GroupBox)sender;
            if (groupBox.State == GroupBoxState.Normal)
            {
                groupBox.State = GroupBoxState.Maximized;
            }
            else
            {
                groupBox.State = GroupBoxState.Normal;
            }
        }
        private void box_StateChanged(object sender, DevExpress.Xpf.Core.ValueChangedEventArgs<GroupBoxState> e)
        {
            DevExpress.Xpf.LayoutControl.GroupBox box = sender as DevExpress.Xpf.LayoutControl.GroupBox;
            if (e.NewValue == GroupBoxState.Maximized)
            {
                if (SelectedObject != null && SelectedObject != box.DataContext)
                {
                    (box.DataContext as ImageView).IsPreview = true;
                }
                SelectedObject = box.DataContext;
                (box.DataContext as ImageView).IsPreview = false;
            }
            else
            {
                if (!this.GetLogicalChildren(false).OfType<DevExpress.Xpf.LayoutControl.GroupBox>().Any(b => b.State == GroupBoxState.Maximized))
                {
                    SelectedObject = null;
                }
            }
        }
        #endregion
    }
    public class ImageView : BindableBase
    {
        private bool _isPreview;
        public bool IsPreview
        {
            get
            {
                return _isPreview;
            }
            set
            {
                _isPreview = value;
                RaisePropertyChanged(nameof(PreviewImage));
                RaisePropertyChanged(nameof(Width));
                RaisePropertyChanged(nameof(Height));
            }
        }
        public Picture Picture { get; set; }
        public BitmapImage PreviewImage
        {
            get
            {
                if (IsPreview)
                {
                    MemoryStream stream = new MemoryStream(Picture.PreviewData);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
                else
                {
                    MemoryStream stream = new MemoryStream(Picture.ImageData);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
            }
        }
        public BitmapImage FullImage
        {
            get
            {
                
                    MemoryStream stream = new MemoryStream(Picture.ImageData);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
            }
        }

        public ImageView(Picture picture)
        {
            Picture = picture;
            IsPreview = true;
        }

        public string PictureDate
        {
            get
            {
                return Picture.DateTime.ToString();
            }
        }

        public int Width
        {
            get
            {
                return PreviewImage.PixelWidth;
            }
        }

        public int Height
        {
            get
            {
                return PreviewImage.PixelHeight;
            }
        }

    }
}

//    public class CustomFlowLayoutControl : FlowLayoutControl
//    {
//        #region Dependency Properties
//        public bool ChildAddedByUser
//        {
//            get { return (bool)GetValue(ChildAddedByUserProperty); }
//            set { SetValue(ChildAddedByUserProperty, value); }
//        }
//        public static readonly DependencyProperty ChildAddedByUserProperty = DependencyProperty.Register("ChildAddedByUser", typeof(bool), typeof(CustomFlowLayoutControl), new PropertyMetadata(false));
//        public Object SelectedObject
//        {
//            get { return (Object)GetValue(SelectedObjectProperty); }
//            set { SetValue(SelectedObjectProperty, value); }
//        }
//        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(Object), typeof(CustomFlowLayoutControl), new PropertyMetadata(null, selectedItemChanged));
//        #endregion
//        #region Overrides
//        protected override void OnChildAdded(FrameworkElement child)
//        {
//            base.OnChildAdded(child);
//            GroupBox box = child as GroupBox;
//            box.StateChanged += box_StateChanged;
//            box.MouseLeftButtonUp += box_MouseLeftButtonUp;
//            if (ChildAddedByUser)
//            {
//                if (box != null)
//                {
//                    box.State = GroupBoxState.Maximized;
//                }
//            }
//        }
//        #endregion
//        #region Private Methods
//        private static void selectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            CustomFlowLayoutControl c = d as CustomFlowLayoutControl;
//            if (c != null)
//            {
//                var child = c.GetLogicalChildren(false).OfType<FrameworkElement>().FirstOrDefault(f => f.DataContext == c.SelectedObject);
//                var children = c.GetLogicalChildren(false).OfType<FrameworkElement>().Where(f => f.DataContext is ImageView);
//                foreach (var cc in children)
//                {
//                    (cc.DataContext as ImageView).IsPreview = true;
//                }
//                DevExpress.Xpf.LayoutControl.GroupBox box = child as DevExpress.Xpf.LayoutControl.GroupBox;
//                if (box != null)
//                {
//                    box.State = GroupBoxState.Maximized;
//                    (box.DataContext as ImageView).IsPreview = false;
//                }
//                c.UpdateLayout();
//            }
//        }
//        private void box_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {
//            var groupBox = (DevExpress.Xpf.LayoutControl.GroupBox)sender;
//            if (groupBox.State == GroupBoxState.Normal)
//            {
//                groupBox.State = GroupBoxState.Maximized;
//            }
//            else
//            {
//                groupBox.State = GroupBoxState.Normal;
//            }
//        }
//        private void box_StateChanged(object sender, DevExpress.Xpf.Core.ValueChangedEventArgs<GroupBoxState> e)
//        {
//            DevExpress.Xpf.LayoutControl.GroupBox box = sender as DevExpress.Xpf.LayoutControl.GroupBox;
//            if (e.NewValue == GroupBoxState.Maximized)
//            {
//                if (SelectedObject != null && SelectedObject != box.DataContext)
//                {
//                    (box.DataContext as ImageView).IsPreview = true;
//                }
//                SelectedObject = box.DataContext;
//                (box.DataContext as ImageView).IsPreview = false;
//            }
//            else
//            {
//                if (!this.GetLogicalChildren(false).OfType<DevExpress.Xpf.LayoutControl.GroupBox>().Any(b => b.State == GroupBoxState.Maximized))
//                {
//                    SelectedObject = null;
//                }
//            }
//        }
//        #endregion
//    }
//    public class ImageView : BindableBase
//    {
//        private bool _isPreview;
//        public bool IsPreview
//        {
//            get
//            {
//                return _isPreview;
//            }
//            set
//            {
//                _isPreview = value;
//                PreviewImage = SetPreviewImage();
//                RaisePropertyChanged(nameof(PreviewImage));
//                RaisePropertyChanged(nameof(Width));
//                RaisePropertyChanged(nameof(Height));
//            }
//        }
//        public Picture Picture { get; set; }
//        //public BitmapSource PreviewImage
//        //{
//        //    get
//        //    {
//        //        if (IsPreview)
//        //        {
//        //            MemoryStream stream = new MemoryStream(Picture.PreviewData);
//        //            BitmapImage image = new BitmapImage();
//        //            image.BeginInit();
//        //            image.StreamSource = stream;
//        //            image.EndInit();
//        //            return image;
//        //        }
//        //        else
//        //        {
//        //            MemoryStream stream = new MemoryStream(Picture.ImageData);
//        //            BitmapImage image = new BitmapImage();
//        //            image.BeginInit();
//        //            image.StreamSource = stream;
//        //            image.EndInit();
//        //            return image;
//        //        }
//        //    }
//        //}


//        public BitmapSource PreviewImage
//        {
//            get { return GetProperty(() => PreviewImage); }
//            set { SetProperty(() => PreviewImage, value); }
//        }

//        private BitmapSource SetPreviewImage()
//        {
//            if (IsPreview)
//            {
//                //MemoryStream stream = new MemoryStream(Picture.ImageData);
//                MemoryStream stream = new MemoryStream(Picture.PreviewData);
//                BitmapImage image = new BitmapImage();
//                image.BeginInit();
//                image.StreamSource = stream;
//                image.EndInit();



//                //using (var fileStream = new FileStream($@"C:\Users\user\Desktop\Phhotos\ToUplaod\test2.jpg", FileMode.Create))
//                //{
//                //    BitmapEncoder encoder = new PngBitmapEncoder();
//                //    //encoder.Frames.Add(BitmapFrame.Create(image));
//                //    encoder.Frames.Add(BitmapFrame.Create(image));
//                //    encoder.Save(fileStream);
//                //}

//                return image;
//            }
//            else
//            {
//                MemoryStream stream = new MemoryStream(Picture.ImageData);
//                BitmapImage image = new BitmapImage();
//                image.BeginInit();
//                image.StreamSource = stream;
//                image.EndInit();
//                return image;
//            }
//        }

//        public ImageView(Picture picture)
//        {
//            Picture = picture;
//            IsPreview = true;
//        }

//        public string PictureDate
//        {
//            get
//            {
//                return Picture.DateTime.ToString();
//            }
//        }

//        public int Width
//        {
//            get
//            {
//                return PreviewImage.PixelWidth;
//            }
//        }

//        public int Height
//        {
//            get
//            {
//                return PreviewImage.PixelHeight;
//            }
//        }
//    }
//}
