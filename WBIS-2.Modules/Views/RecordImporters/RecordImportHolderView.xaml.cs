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
using System.Linq;
using WBIS_2.Modules.ViewModels.RecordImporters;

namespace WBIS_2.Modules.Views.RecordImporters
{
    /// <summary>
    /// Interaction logic for RecordImportHolderView.xaml
    /// </summary>
    public partial class RecordImportHolderView : UserControl
    {
        public RecordImportHolderView(UserControl _startingRecordImport)
        {
            InitializeComponent();
            this.DataContext = new RecordImportHolderViewModel(_startingRecordImport, this);
        }
        List<UserControl> UserControls = new List<UserControl>();
        public void AddRecordImporterControl(UserControl userControl)
        {
            GridContent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto)});
            Grid.SetRow(userControl, GridContent.RowDefinitions.Count - 1);
            GridContent.Children.Add(userControl);
            UserControls.Add(userControl);
        }
        public void RemoveRecordImporterControl(object RemoveViewModel)
        {
            int index = UserControls.FindIndex(_=>_.DataContext.GetType() == RemoveViewModel.GetType());
            GridContent.RowDefinitions.RemoveAt(index);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!((RecordImportHolderViewModel)DataContext).SaveClick())
                return;
            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }
    }
}
