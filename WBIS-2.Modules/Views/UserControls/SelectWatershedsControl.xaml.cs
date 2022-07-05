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

namespace WBIS_2.Modules.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SelectWatershedsControl.xaml
    /// </summary>
    public partial class SelectWatershedsControl : UserControl
    {
        WBIS2Model Database = new WBIS2Model();
        public List<WatershedSelection> WatershedSelections = new List<WatershedSelection>();
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
                    Select = currentSelection.Contains(watershed.Id),
                    guid = watershed.Id
                });
            GridControl.ItemsSource = WatershedSelections;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
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
        private void DeselectAllClick(object sender, RoutedEventArgs e)
        {
            foreach (var w in WatershedSelections)
                w.SetSelect(false);
        }
        private void SelectTouchingClick(object sender, RoutedEventArgs e)
        {
            if (GridControl.SelectedItem == null) return;

            var geo = Database.Watersheds.First(_ => _.Id == ((WatershedSelection)GridControl.SelectedItem).guid).Geometry;
            var watersheds = Database.Watersheds.Where(_ => _.Geometry.Touches(geo)).AsNoTracking();
            foreach (var watershed in watersheds)
            {
                var w = WatershedSelections.FirstOrDefault(_ => _.guid == watershed.Id);
                if (w != null)
                    w.SetSelect(true);
            }
            ((WatershedSelection)GridControl.SelectedItem).SetSelect(true);
        }





        public class WatershedSelection : BindableBase
        {
            public string WatershedID { get; set; }
            public string WatershedName { get; set; }
            public string Hydrologic { get; set; }
            public bool Select { get; set; }
            public Guid guid { get; set; }

            public void SetSelect (bool select)
            {
                Select = select;
                RaisePropertyChanged(nameof(Select));
            }
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
