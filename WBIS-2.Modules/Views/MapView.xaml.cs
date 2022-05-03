using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;
using Atlas3.Manager;
using WBIS_2.Modules.ViewModels;
using WBIS_2.DataModel;
using Atlas3.Controls.ViewModels;
using System.Threading.Tasks;

namespace WBIS_2.Modules.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
            DataContext = new MapViewModel(true);
            (DataContext as MapViewModel).MapControl = MapControl;

            AtlasManager.Instance.Map = MapControl.UxMap;
            AtlasManager.Instance.Map.AppManager.LoadExtensions();

            Loaded += MapView_Loaded;

            if (DataContext == null)
            {
                LayersTree.DataContext = new Atlas3.Controls.ViewModels.LegendTreeExControlViewModel();
            }
            var appFolder = AppContext.BaseDirectory;
            var shapesFolder = Path.Combine(appFolder, "MapData");
            var projectFile = Path.Combine(shapesFolder, $"WBIS2.geo3");// "RMS.geo3");
            if (File.Exists(projectFile))
            {
                Atlas3.Manager.AtlasManager.Instance.LoadProject(projectFile);
                Atlas3.Manager.AtlasManager.Instance.Map.FunctionMode = Atlas.Controls.FunctionMode.Select;
                Atlas3.Manager.AtlasManager.Instance.Map.FunctionMode = Atlas.Controls.FunctionMode.ZoomPan;
            }


            Unloaded += MapView_Unloaded;
            Application.Current.MainWindow.Closing += MainWindow_Closing;

            MapControl.EditorEnabled = true;

            DataContextChanged += MapView_DataContextChanged;
            (DataContext as MapViewModel).InformationTypesChanged(new object(), new EventArgs());
        }


        private void MapView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //(DataContext as MapViewModel).MapControl = MapControl;
        }

        private void MapView_Unloaded(object sender, RoutedEventArgs e)
        {
            var appFolder = AppContext.BaseDirectory;
            var shapesFolder = Path.Combine(appFolder, "MapData");
            var projectFile = Path.Combine(shapesFolder, $"WBIS2.geo3");// "RMS.geo3");


            //Atlas3.Manager.AtlasManager.Instance.SaveProject();
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var appFolder = AppContext.BaseDirectory;
            var shapesFolder = Path.Combine(appFolder, "MapData");
            var projectFile = Path.Combine(shapesFolder, $"WBIS2.geo3");// "RMS.geo3");
            Atlas3.Manager.AtlasManager.Instance.SaveProject();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
