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
using WBIS_2.Modules.ViewModels.RecordImporters;

namespace WBIS_2.Modules.Views.RecordImporters
{
    /// <summary>
    /// Interaction logic for SiteCallingImportView.xaml
    /// </summary>
    public partial class BotanicalSurveyImportView : UserControl
    {
        public BotanicalSurveyImportView()
        {
            InitializeComponent();
            DataContext = new BotanicalSurveyImportViewModel();
        }
    }
}
