using WBIS_2.Modules.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WBIS_2.Modules.Views.Reports
{
    /// <summary>
    /// Interaction logic for ReportLayoutSaverView.xaml
    /// </summary>
    public partial class ReportLayoutSaverView : UserControl
    {
        public ReportLayoutSaverView()
        {
            InitializeComponent();
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!((ReportLayoutSaverViewModel)DataContext).SaveLayout()) return;
            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }

        private void SimpleButton_Click_1(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }
    }
}
