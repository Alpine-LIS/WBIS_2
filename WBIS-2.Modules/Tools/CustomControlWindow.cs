using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WBIS_2.Modules.Tools
{
    public class CustomControlWindow
    {
        public bool DialogResult { get; set; }
        public CustomControlWindow(UserControl control, string uid = "")
        {
            DXWindow window = new DXWindow()
            {
                Content = control,
                WindowStyle = WindowStyle.None,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                Uid = uid
            };

            DialogResult = window.ShowDialog().Value;
        }
        public CustomControlWindow(UserControl control, bool topmost)
        {
            DXWindow window = new DXWindow()
            {
                Content = control,
                WindowStyle = WindowStyle.None,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                Topmost = topmost
            };

            DialogResult = window.ShowDialog().Value;
        }
    }
}
