using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WBIS_2.Modules.ViewModels.Wildlife;

namespace WBIS_2.Modules.Views.Wildlife
{
    /// <summary>
    /// Interaction logic for ManageRequiredPassesView.xaml
    /// </summary>
    public partial class ManageRequiredPassesView : UserControl
    {
        public ManageRequiredPassesView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }
    }
}
