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
using WBIS_2.Modules.ViewModels.Reports;

namespace WBIS_2.Modules.Views.Reports
{
    /// <summary>
    /// Interaction logic for ReportBuilderControl.xaml
    /// </summary>
    public partial class ReportBuilderControl : UserControl
    {
        public ReportBuilderControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ReportBuilderViewModel).MyPivotGridControl = ReportPivotGrid;
            //(this.DataContext as ReportBuilderViewModel).LoadReportClick();
        }
    }
}
