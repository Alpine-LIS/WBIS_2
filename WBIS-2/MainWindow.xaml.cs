using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WBIS_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Assembly thisAssem = typeof(MainWindow).Assembly;
            AssemblyName thisAssemName = thisAssem.GetName();
            Version ver = thisAssemName.Version;
            this.Title = $@"WBIS {ver}";
            new WBIS_2.DataModel.WBIS2Model().Database.EnsureCreated();
        }

        private void ThemedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void ThemedWindow_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
