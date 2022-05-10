using DevExpress.Xpf.Grid;
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
using WBIS_2.Modules.ViewModels;

namespace WBIS_2.Modules.Views.Botany
{
    /// <summary>
    /// Interaction logic for BotanicalScopingPlantView.xaml
    /// </summary>
    public partial class BotanicalScopingPlantView : UserControl
    {
        public BotanicalScopingPlantView()
        {
            InitializeComponent();
            //PlantView.CustomCellAppearance += PlantView_CustomCellAppearance;
            PlantView.CustomRowAppearance += MyView_CustomRowAppearance;
        }

        //private void PlantView_CustomCellAppearance(object? sender, CustomCellAppearanceEventArgs e)
        //{
        //    var id = PlantGridControl.GetCellValue(e.RowHandle, "Guid");
        //    if (e.Property.Name == "Foreground")
        //    {
        //        if (((BotanicalScopingViewModel)DataContext).ChangedSpecies.Contains((Guid)id))
        //        {
        //            e.Result = FontWeights.Bold;
        //            e.Handled = true;
        //        }
        //        else e.Handled = false;
        //    }
        //    else e.Handled = false;
        //}

        private void MyView_CustomRowAppearance(object sender, CustomRowAppearanceEventArgs e)
        {
            //https://docs.devexpress.com/WPF/116513/controls-and-libraries/data-grid/conditional-formatting/conditional-formats/formatting-focused-cells-and-rows

            //var id = PlantGridControl.GetCellValue(e.RowHandle, "Guid");
            var exclude = PlantGridControl.GetCellValue(e.RowHandle, "Exclude");
            var excludeReport = PlantGridControl.GetCellValue(e.RowHandle, "ExcludeReport");

            if (e.Property.Name == "Background")
            {
                if ((bool)excludeReport)
                {
                    e.Result = Brushes.LightSalmon;
                    e.Handled = true;
                }
                else if ((bool)exclude)
                {
                    e.Result = Brushes.LightGray;
                    e.Handled = true;
                }
                else e.Handled = false;
            }
            //else if (e.Property.Name == "Foreground")
            //{
            //    if (((BotanicalScopingViewModel)DataContext).ChangedSpecies.Contains((Guid)id))
            //    {                    
            //        e.Result = FontWeights.Bold;
            //        e.Handled = true;
            //    }
            //    else e.Handled = false;
            //}
            else e.Handled = false;

            //if (excludeReport == null)
            //    e.Handled = false;
            //else if ((bool)excludeReport)
            //{
            //    if (e.Property.Name == "Background")
            //    {
            //        e.Result = Brushes.LightSalmon;
            //        e.Handled = true;
            //    }
            //    else if (e.Property.Name == "Foreground")
            //    {
            //        if (((BotanicalScopingViewModel)DataContext).ChangedSpecies.Contains((Guid)id))
            //            e.Result = Brushes.Black;
            //        else
            //            e.Result = Brushes.Black;
            //        e.Handled = true;
            //    }
            //    else e.Handled = false;
            //}
            //else if (exclude == null)
            //    e.Handled = false;
            //else if ((bool)exclude)
            //{
            //    if (e.Property.Name == "Background")
            //    {
            //        e.Result = Brushes.LightGray;
            //        e.Handled = true;
            //    }
            //    else if (e.Property.Name == "Foreground")
            //    {
            //        if (((BotanicalScopingViewModel)DataContext).ChangedSpecies.Contains((Guid)id))
            //            e.Result = Brushes.Black;
            //        else
            //            e.Result = Brushes.Black;
            //        e.Handled = true;
            //    }
            //    else e.Handled = false;
            //}
            //else
            //    e.Handled = false;
        }
    }
}
