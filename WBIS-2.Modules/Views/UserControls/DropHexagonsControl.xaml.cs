using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
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
using WBIS_2.DataModel;
using WBIS_2.Modules.ViewModels;

namespace WBIS_2.Modules.Views.UserControls
{
    /// <summary>
    /// Interaction logic for DropHexagonsControl.xaml
    /// </summary>
    public partial class DropHexagonsControl : UserControl
    {
        WBIS2Model Database = new WBIS2Model();
        public DropHexagonsControl(Hex160[] hex160s)
        {
            InitializeComponent();
            this.DataContext = new DropHexagonsViewModel(hex160s);
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!((DropHexagonsViewModel)DataContext).DropPasses())
                return;
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }
    }
}
