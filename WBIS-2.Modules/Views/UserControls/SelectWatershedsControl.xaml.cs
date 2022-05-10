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

namespace WBIS_2.Modules.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SelectWatershedsControl.xaml
    /// </summary>
    public partial class SelectWatershedsControl : UserControl
    {
        WBIS2Model Database = new WBIS2Model();
        List<WatershedSelection> WatershedSelections = new List<WatershedSelection>();
        public SelectWatershedsControl(Guid[] currentSelection)
        {
            InitializeComponent();
            IInfoTypeManager manager = new InformationTypeManager<Watershed>();
            foreach (Watershed watershed in manager.GetQueryable(CurrentUser.Districts.ToArray(), typeof(District), Database))
                WatershedSelections.Add(new WatershedSelection()
                {
                    WatershedID = watershed.WatershedID,
                    WatershedName = watershed.WatershedName,
                    Hydrologic = watershed.Hydrologic,
                    Select = currentSelection.Contains(watershed.Guid),
                    guid = watershed.Guid
                });
            GridControl.ItemsSource = WatershedSelections;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {

        }
        private void DeselectAllClick(object sender, RoutedEventArgs e)
        {

        }
        private void SelectTouchingClick(object sender, RoutedEventArgs e)
        {
            if (GridControl.SelectedItem == null) return;

            var geo = Database.Watersheds.First(_ => _.Guid == ((WatershedSelection)GridControl.SelectedItem).guid).Geometry;
            var watersheds = Database.Watersheds.Where(_ => _.Geometry.Touches(geo));
            foreach(var watershed in watersheds)
            {
                var w = WatershedSelections.FirstOrDefault(_=>_.guid == watershed.Guid);
                if (w != null)
                    w.Select = true;
            }
        }





        public class WatershedSelection
        {
            public string WatershedID { get; set; }
            public string WatershedName { get; set; }
            public string Hydrologic { get; set; }
            public bool Select { get; set; }
            public Guid guid { get; set; }
        }

        private void GridControl_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (GridControl.SelectedItem == null)
            {
                BtnSelectTouching.Content = "";
                BtnSelectTouching.IsEnabled = false;
            }
            else
            {
                BtnSelectTouching.Content = "Select watersheds touching " + ((WatershedSelection)GridControl.SelectedItem).WatershedName;
                BtnSelectTouching.IsEnabled = true;
            }
        }
    }
}
