using DevExpress.Mvvm;
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

namespace WBIS_2.Modules.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ParentReportControl.xaml
    /// </summary>
    public partial class ParentReportControl : UserControl
    {
        public string[] ReturnTypes { get; set; }
        List<InfoTypeChooser> options = new List<InfoTypeChooser>();
        public ParentReportControl(IInformationType[] informationTypes)
        {
            InitializeComponent();

            foreach (var t in informationTypes)
                options.Add(new InfoTypeChooser() { InfoTypeName = t.Manager.DisplayName });
            LbxOptions.ItemsSource = options;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (!options.Any(_=>_.Selected))
            {
                MessageBox.Show("There are no tables selected.");
                return;
            }

            ReturnTypes = options.Where(_=>_.Selected).Select(_=>_.InfoTypeName).ToArray();

            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }


        public class InfoTypeChooser : BindableBase
        {
            public bool Selected { get; set; } = true;
            public string InfoTypeName { get; set; }
        }
    }
}
