using Atlas.Controls;
using Atlas.Data;
using Atlas.Symbology;
using Atlas3.Controls;
using DevExpress.Mvvm;
using WBIS_2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using WBIS_2.Modules.Views;
using Atlas.Controls.Atlas3.MapFunctions;
using NetTopologySuite.Geometries;

namespace WBIS_2.Modules.ViewModels
{
    public class MapViewModel
    {
        private bool _editorEnabled;
        public bool EditorEnabled
        {
            get
            {
                return _editorEnabled;
            }
            set
            {
                _editorEnabled = value;
                if (MapControl != null)
                {
                    MapControl.EditorEnabled = value;
                }
            }
        }
        public MapControl MapControl
        {
            get
            {
                return _mapControl;
            }
            set
            {
                _mapControl = value;
                _mapControl.UxMap.SelectionChanged += UxMap_SelectionChanged;
                _mapControl.UxMap.Layers.LayerAdded+= UxMap_LayerAdded;
                _mapControl.UxMap.Layers.LayerRemoved += UxMap_LayerAdded;
                _mapControl.EditorEnabled = _editorEnabled;
                CurrentUser.AllLayers = _mapControl.UxMap.Layers.Select(_=>_.LegendText).OrderBy(_=>_).ToList();
            }
        }


        private void UxMap_LayerAdded(object? sender, LayerEventArgs e)
        {
            CurrentUser.AllLayers = _mapControl.UxMap.Layers.Select(_ => _.LegendText).OrderBy(_ => _).ToList();
        }

        private void UxMap_SelectionChanged(object sender, EventArgs e)
        {
            ResetAFS();
            MapDataPasser.MapSelectionChanged(MapControl.Selection.ToFeatureList());
        }

        private MapControl _mapControl;
        public static MapViewModel Create(bool editorEnabled)
        {
            return new MapViewModel()
            {
                EditorEnabled = editorEnabled,
            };
        }

        protected MapViewModel()
        {
        }

        public MapViewModel(bool temp)
        {
            MapDataPasser.ZoomToLayerEvent += ZoomToLayer;
            MapDataPasser.ZoomToFeatureEvent += ZoomToSingle;
            MapDataPasser.RefreshLayersEvent += RefreshLayers;
            MapDataPasser.MapDrawFeatureEvent += DrawActivity;
            MapDataPasser.MapShowAFSEvent += MapDataPasser_MapShowAFSEvent;
            MapDataPasser.UserDistrictsChangedEvent += MapDataPasser_UserDistrictsChanged;
            MapDataPasser.InformationTypesChangedEvent += InformationTypesChanged;
        }

        public void ZoomToLayer(string layerName)
        {
            var layer = MapControl.GetLayer(layerName);
            layer.SelectionEnabled = true;
            ((IMapFeatureLayer)layer).SelectAll();
        }

        public void ZoomToLayer(object sender, EventArgs e)
        {
            if (MapControl == null)
            {
                return;
            }
            var layer = MapControl.GetLayer(MapDataPasser.ZoomLayerName);
            if (layer == null) return;
            layer.SelectionEnabled = true;
            if (MapDataPasser.ZoomKeyValues.Count == 0)
            {
                layer.ClearSelection();
                return;
            }
            MapDataPasser.SelectionFromGrid = true;
            var q = $"{MapDataPasser.ZoomKeyField} = {MapDataPasser.ZoomKeyValues[0]}";
            var allKeys = String.Join(",", MapDataPasser.ZoomKeyValues);
            var q2 = $"[{MapDataPasser.ZoomKeyField}] IN ({allKeys})";
            MapControl.ActiveLayer = layer;
            layer.SelectByAttribute(q2);
            if ((bool)sender) layer.ZoomToSelectedFeatures();
            MapDataPasser.SelectionFromGrid = false;
        }
        public void ZoomToSingle(object sender, EventArgs e)
        {
            if (MapControl == null)
            {
                return;
            }
            var layer = MapControl.GetLayer(MapDataPasser.ZoomLayerName);
            if (layer == null) return;
            layer.SelectionEnabled = true;
            if (MapDataPasser.ZoomKeyValueSingle == null)
                return;
            MapDataPasser.SelectionFromGrid = true;

            MapControl.ActiveLayer = layer;
            var f = layer.DataSet.Features.First(_ => (string)_.DataRow[MapDataPasser.ZoomKeyField] == MapDataPasser.ZoomKeyValueSingle);

            Extent extent = new Extent();
            foreach (Coordinate c in f.Geometry.Envelope.Coordinates)
            {
                extent.ExpandToInclude(c.X, c.Y);
            }
            MapControl.UxMap.ViewExtents = extent;
            MapControl.UxMap.MapFrame.Invalidate();

            MapDataPasser.SelectionFromGrid = false;
        }

        private void MapDataPasser_MapShowAFSEvent(object sender, EventArgs e)
        {
            if (MapControl == null)
            {
                return;
            }

            ResetAFS();
            AfsLayer = new KeyValuePair<IMapFeatureLayer, string>(MapControl.GetLayer(MapDataPasser.ZoomLayerName), MapDataPasser.ZoomKeyField);
            if (AfsLayer.Key == null) return;
            AfsLayer.Key.SelectionEnabled = true;
            PreAfsCategory.Clear();

            //IFeatureCategory newCat = new PolygonCategory(System.Drawing.Color.FromArgb(150,244,233,0), System.Drawing.Color.Yellow, 2);
            foreach (var key in MapDataPasser.ZoomKeyValues)
            {
                var f = AfsLayer.Key.DataSet.Features.First(_ => (string)_.DataRow[MapDataPasser.ZoomKeyField] == (string)key);
                var a = (PolygonCategory)AfsLayer.Key.GetCategory(f.Fid).Clone();
                a.Symbolizer.SetFillColorEx(System.Drawing.Color.FromArgb(250, 244, 233, 0));
                a.Symbolizer.SetOutline(System.Drawing.Color.Yellow, 1);
                a.SelectionSymbolizer.SetFillColorEx(System.Drawing.Color.FromArgb(50, 244, 233, 0));
                a.SelectionSymbolizer.SetOutline(System.Drawing.Color.Yellow, 1);
                PreAfsCategory.Add((string)key, AfsLayer.Key.GetCategory(f.Fid));
                AfsLayer.Key.SetCategory(f.Fid, a);
            }
            MapControl.UxMap.MapFrame.Invalidate();
        }

        KeyValuePair<IMapFeatureLayer, string> AfsLayer = new KeyValuePair<IMapFeatureLayer, string>();
        //IMapFeatureLayer AfsLayer;
        Dictionary<string, IFeatureCategory> PreAfsCategory = new Dictionary<string, IFeatureCategory>();
        private void ResetAFS()
        {
            foreach (KeyValuePair<string, IFeatureCategory> keyValuePair in PreAfsCategory)
            {
                var f = AfsLayer.Key.DataSet.Features.First(_ => (string)_.DataRow[AfsLayer.Value] == keyValuePair.Key);
                AfsLayer.Key.SetCategory(f.Fid, keyValuePair.Value);
            }
            MapControl.UxMap.MapFrame.Invalidate();
        }


        private Guid GuidDrawing;
        private void DrawActivity(object sender, EventArgs e)
        {
            GuidDrawing = (Guid)sender;
            var lyr = MapControl.GetLayer("ACTIVITIES");
            MapControl.UxMap.ActiveLayer = lyr;

            IMapFunction function = MapControl.UxMap.MapFunctionsManager.MapFunctions.FirstOrDefault(_ => _.Name == "AddPolygonMapFunction");
            if (function == null)
            {
                function = new AddPolygonMapFunction(MapControl.UxMap, new Atlas.Data.Feature());
                function.Name = "AddPolygonMapFunction";
                MapControl.UxMap.MapFunctionsManager.Add(function);
            }
            MapControl.UxMap.FunctionMode = FunctionMode.Select;
            function.Activate();

            function.FunctionDeactivated -= FunctionDeactivated;
            function.FunctionDeactivated += FunctionDeactivated;

            MapControl.UxMap.OnActiveLayerChanged -= UxMap_OnActiveLayerChanged;
            MapControl.UxMap.OnActiveLayerChanged += UxMap_OnActiveLayerChanged;
            MapControl.ActiveLayer.DataSet.Features.FeatureAdded -= FeatureAdded;
            MapControl.ActiveLayer.DataSet.Features.FeatureAdded += FeatureAdded;
        }

        private void UxMap_OnActiveLayerChanged(object sender, ActiveLayerEventArgs e)
        {
            MapControl.UxMap.OnActiveLayerChanged -= UxMap_OnActiveLayerChanged;
            MapControl.UxMap.FunctionMode = FunctionMode.Select;
        }

        private void FunctionDeactivated(object sender, EventArgs e)
        {
            MapControl.UxMap.ActiveLayer.DataSet.Features.FeatureAdded -= FeatureAdded;
            MapDataPasser.ActivityNotDrawn();
            MapControl.UxMap.FunctionMode = FunctionMode.Select;
            MapControl.UxMap.MapFrame.Suspended = false;
        }
        private void FeatureAdded(object sender, FeatureEventArgs e)
        {
            MapControl.UxMap.ActiveLayer.DataSet.Features.FeatureAdded -= FeatureAdded;
            var g = e.Feature.Geometry.Copy();

            if (e.Feature.ParentFeatureSet.DataTable.Columns.Contains("REGENGUID"))
            {
                e.Feature.ParentFeatureSet.DataTable.Columns.Remove("REGENGUID");
                e.Feature.ParentFeatureSet.DataTable.Columns.Add("regenguid", typeof(Guid));
            }
            if (e.Feature.ParentFeatureSet.DataTable.Columns.Contains("FUELBREAKGUID"))
            {
                e.Feature.ParentFeatureSet.DataTable.Columns.Remove("FUELBREAKGUID");
                e.Feature.ParentFeatureSet.DataTable.Columns.Add("fuelbreakguid", typeof(Guid));
            }
            if (e.Feature.ParentFeatureSet.DataTable.Columns.Contains("GUID"))
            {
                e.Feature.ParentFeatureSet.DataTable.Columns.Remove("GUID");
                e.Feature.ParentFeatureSet.DataTable.Columns.Add("guid", typeof(Guid));
                e.Feature.DataRow["guid"] = GuidDrawing;// Guid.NewGuid();
            }

            MapDataPasser.ActivityDrawn(g);
        }



        private void RefreshLayers(object sender, EventArgs e)
        {

            //List<string> layers = (List<string>)sender;

            //foreach (string startingStr in layers)
            //{
            //    string lyrStr = startingStr;
            //    if (startingStr.ToUpper().Contains("REGEN")) lyrStr = "REGENS";
            //    else if (startingStr.ToUpper().Contains("FUELBREAK")) lyrStr = "FUELBREAKS";
            //    else if (startingStr.ToUpper().Contains("DISTRICT")) lyrStr = "DISTRICTS";
            //    else if (startingStr.ToUpper().Contains("PLANTING")) lyrStr = "ACTIVITIES";
            //    else if (startingStr.ToUpper().Contains("SPRAYING")) lyrStr = "ACTIVITIES";
            //    else if (startingStr.ToUpper().Contains("EVALUATION")) lyrStr = "ACTIVITIES";
            //    else if (startingStr.ToUpper().Contains("MISCELLANEOUS")) lyrStr = "ACTIVITIES";
            //    else if (startingStr.ToUpper().Contains("COMMERCIAL")) lyrStr = "ACTIVITIES";
            //    else if (startingStr.ToUpper().Contains("SITE")) lyrStr = "ACTIVITIES";

            //    var layer = MapControl.GetLayer(lyrStr);
            //    if (layer == null) continue;
            //    //MapControl.UxMap.ActiveLayer = layer;
            //    PGFeatureSet pGFeatureSet = (PGFeatureSet)layer.DataSet;
            //    //MapControl.UxMap.ActiveLayer.DataSet = new PGFeatureSet(pGFeatureSet.Server, pGFeatureSet.Table, "Geometry", "Guid");
            //    //MapControl.UxMap.ActiveLayer.Selection.Configure();
            //    layer.DataSet = new PGFeatureSet(pGFeatureSet.Server, pGFeatureSet.Table, "Geometry", "Guid");
            //    layer.Selection.Configure();
            //    layer.AssignFastDrawnStates();
            //}

            //MapControl.UxMap.MapFrame.Initialize();
            //MapDataPasser_UserDistrictsChanged(new object(), new EventArgs());
        }


        private void MapDataPasser_UserDistrictsChanged(object sender, EventArgs e)
        {
            //if (MapControl == null) return;

            //Guid[] districtGuids = CurrentUser.Districts.Select(_ => _.Guid).ToArray();

            ////string[] layers = new string[] { "REGENS" };
            //string[] layers = new string[] { "REGENS", "FUELBREAKS" };
            //foreach (string layer in layers)
            //{
            //    var featureLayer = MapControl.GetLayer(layer);
            //    foreach (var f in featureLayer.DataSet.Features)
            //    {
            //        //featureLayer.SetVisible(f, (string)f.DataRow["DISTRICT"] == "Redding");
            //        if (layer == "FUELBREAKS")
            //            featureLayer.SetVisible(f, districtGuids.Contains((Guid)f.DataRow["DistrictGuid"]) &&
            //                (!(bool)f.DataRow["FuelTreatment"] || (bool)f.DataRow["FuelTreatment"] == MapDataPasser.ViewFuelTreatments));
            //        else
            //            featureLayer.SetVisible(f, districtGuids.Contains((Guid)f.DataRow["DistrictGuid"]));
            //    }
            //}
            //MapControl.UxMap.MapFrame.Invalidate();
        }

        public void InformationTypesChanged(object sender, EventArgs e)
        {
            foreach (var layer in MapControl.UxMap.Layers)
                layer.IsVisible = CurrentUser.VisibleLayers.Contains(MapDataPasser.CleanLayerStr(layer.LegendText));
            MapControl.UxMap.MapFrame.Invalidate();
        }
    }
}
