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
using System.Linq;
using System.Reflection;
using Atlas.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

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
            CurrentUser.AllLayers = Atlas3.Manager.AtlasManager.Instance.Map.Layers.Select(_ => _.LegendText).OrderBy(_ => _).ToList();


            Unloaded += MapView_Unloaded;
            Application.Current.MainWindow.Closing += MainWindow_Closing;

            MapControl.EditorEnabled = true;

            DataContextChanged += MapView_DataContextChanged;
            (DataContext as MapViewModel).InformationTypesChanged(new object(), new EventArgs());
            LayerSettings();
        }



        private void LayerSettings()
        {
            WBIS2Model model = new WBIS2Model();
            foreach(var l in MapControl.UxMap.Layers)
            {
                var layer = MapControl.GetLayer(l.LegendText);
                if (layer == null) continue;
                var et = model.Model.GetEntityTypes().FirstOrDefault(_ => _.GetTableName() == l.LegendText.ToLower()); //model.Model.FindEntityType(l.LegendText.ToLower());
                if (et != null)
                {
                    Type t = et.ClrType;
                    var ge = t.GetCustomAttribute(typeof(GeometryEdits));
                    if (ge != null)
                    {
                        if (!((GeometryEdits)ge).Locked)
                        {
                            layer.UseNotCommon = true;
                            ((PGFeatureSet)layer.DataSet).ExternalAppLayer = true;
                            ((PGFeatureSet)layer.DataSet).ExternalAppLayerSaving += MapView_ExternalAppLayerSaving;
                            continue;
                        }
                    }
                }
                layer.Locked = true;
            }
        }
        private void MapView_ExternalAppLayerSaving(object sender, EventArgs e)
        {
            var list = (List<IFeature>)sender;
            WBIS2Model model = new WBIS2Model();
            IEntityType et = model.Model.GetEntityTypes().FirstOrDefault(_ => _.GetTableName() == MapControl.ActiveLayer.LegendText.ToLower());
            string keyProp = GetKeyColumn(et);

            var updateProp = et.ClrType.GetProperties().FirstOrDefault(_ => _.Name == "UserModified");
            if (updateProp == null) return;

            bool UpdateUserLocation = false;
            if (et.ClrType == typeof(SiteCalling))
                UpdateUserLocation = MessageBox.Show("Would you like to set the user detections for this site calling record to the new location?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

            ApplicationUser user = model.ApplicationUsers.First(_=>_.Id == CurrentUser.User.Id);

            foreach (IFeature f in list)
            {              
                var record = model.Find(et.ClrType, f.DataRow[keyProp]);
                updateProp.SetValue(record, user);

                if (UpdateUserLocation)
                {
                    var detections = model.SiteCallingDetections
                        .Include(_ => _.SiteCalling)
                        .Include(_ => _.UserLocation)
                        .Where(_ => _.SiteCalling.Id == (Guid)f.DataRow[keyProp]).ToArray();
                    foreach(var detection in detections)
                    {
                        detection.UserLocation.Geometry = (NetTopologySuite.Geometries.Point)f.Geometry.Copy();
                    }
                }
            }
            model.SaveChanges();
        }

        private string GetKeyColumn(IEntityType et)
        {
            var keyProp = et.ClrType.GetProperties().First(_ => _.GetCustomAttributes().FirstOrDefault(_ => _.GetType() == typeof(KeyAttribute)) != null);
            var colAtt = keyProp.GetCustomAttributes().FirstOrDefault(_ => _.GetType() == typeof(ColumnAttribute));
            if (colAtt != null)
                return ((ColumnAttribute)colAtt).Name;
            else return keyProp.Name;
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
            LayersTree.SaveLayoutToXML();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
