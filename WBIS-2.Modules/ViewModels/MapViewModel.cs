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
//using Atlas3.Controls.Compatibility.MapFunctions;
using WBIS_2.Modules.Views;
using Atlas.Controls.Atlas3.MapFunctions;

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
                _mapControl.EditorEnabled = _editorEnabled;
            }
        }


        private void UxMap_SelectionChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
            Messenger.Default.Register<FeatureEditedMessage>(this, OnFeatureEdited);

        }

        public MapViewModel(bool temp)
        {
            Messenger.Default.Register<FeatureEditedMessage>(this, OnFeatureEdited);
            MapDataPasser.ZoomToLayerEvent += ZoomToLayer;
            MapDataPasser.RefreshLayersEvent += RefreshLayers;
            MapDataPasser.MapDrawFeatureEvent += DrawActivity;
        }

        public void ZoomToLayer(string layerName)
        {
            var layer = MapControl.GetLayer(layerName);
            layer.SelectionEnabled = true;
            ((IMapFeatureLayer)layer).SelectAll();
        }


        private void OnFeatureEdited(FeatureEditedMessage obj)
        {
            if (obj.Feature.DataRow.Table.Columns.Contains("Guid"))
            {
                string tbl = obj.Feature.ParentFeatureSet.Name;
                //if (tbl)
                //tbl = tbl.Substring(0, tbl.Length - 1);

                NetTopologySuite.Geometries.MultiPolygon geo = null;
                var database = new WBIS_2.DataModel.WBIS2Model();
                //if (tbl == "Regens")
                //{
                //    tbl = "Regen";
                //    geo = database.Regens.First(_ => _.Guid == (Guid)obj.Feature.DataRow["Guid"]).Geometry;
                //}
                //else if (tbl == "Fuelbreaks")
                //{
                //    tbl = "Fuelbreak";
                //    geo = database.Fuelbreaks.First(_ => _.Guid == (Guid)obj.Feature.DataRow["Guid"]).Geometry;
                //}
                //else if (tbl == "Activities")
                //{
                //    tbl = "Activity";
                //    geo = database.Activities.First(_ => _.Guid == (Guid)obj.Feature.DataRow["Guid"]).Geometry;
                //}
                //database.InsertRecordChange((Guid)obj.Feature.DataRow["Guid"], tbl, "Geometry", geo, obj.Feature.Geometry);
            }
        }

        public void ZoomToLayer(string layerName, string keyField, List<object> keyFields)
        {
            if (MapControl == null)
            {
                return;
            }
            var layer = MapControl.GetLayer(layerName);
            layer.SelectionEnabled = true;
            if (keyFields.Count == 0)
            {
                return;
            }
            var q = $"{keyField} = {keyFields[0]}";
            //layer.SelectByAttribute(q);
            var allKeys = String.Join(",", keyFields);
            var q2 = $"[{keyField}] IN ({allKeys})";
            MapControl.ActiveLayer = layer;
            layer.SelectByAttribute(q2);
            layer.ZoomToSelectedFeatures();
            //((IMapFeatureLayer)layer).SelectAll();
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
            if (MapDataPasser.ZoomKeyFields.Count == 0)
            {
                return;
            }
            MapDataPasser.SelectionFromGrid = true;
            var q = $"{MapDataPasser.ZoomKeyField} = {MapDataPasser.ZoomKeyFields[0]}";
            //layer.SelectByAttribute(q);
            var allKeys = String.Join(",", MapDataPasser.ZoomKeyFields);
            var q2 = $"[{MapDataPasser.ZoomKeyField}] IN ({allKeys})";
            MapControl.ActiveLayer = layer;
            layer.SelectByAttribute(q2);
            layer.ZoomToSelectedFeatures();
            MapDataPasser.SelectionFromGrid = false;
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

            //.Parse("TEMP0000-0000-0000-0000-000000000000");

            //MapControl.UxMap.ActiveLayer.DataSet.DataTable.Rows.RemoveAt(MapControl.UxMap.ActiveLayer.DataSet.DataTable.Rows.Count - 1);
            //MapControl.UxMap.ActiveLayer.DataSet.Features.Remove(e.Feature);
            //MapControl.UxMap.ActiveLayer.Save();

            MapDataPasser.ActivityDrawn(g);


            //MapControl.UxMap.FunctionMode = FunctionMode.Select;
            //PGFeatureSet
            //e.Feature.DataRow[0] = "1";
            //MapControl.UxMap.ActiveLayer.DataSet.Edit(MapControl.UxMap.ActiveLayerFeaturesCount(), e.Feature.DataRow);
            //MapControl.UxMap.ActiveLayer.Save();

            //string tempPath = ODOS.Models.Parent.GettempPath();
            //ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(tempPath + "\\TempShapes\\temp.shp");
            //shapeFileFeatureLayer.RequireIndex = false;
            //shapeFileFeatureLayer.Open();
            //newGeometry = e.Feature.Geometry;
            //newFeature = shapeFileFeatureLayer.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns)[0];
            //shapeFileFeatureLayer.Close();
        }



        private void RefreshLayers(object sender, EventArgs e)
        {

            List<string> layers = (List<string>)sender;

            foreach (string startingStr in layers)
            {
                string lyrStr = startingStr;
                if (startingStr.ToUpper().Contains("REGEN")) lyrStr = "REGENS";
                else if (startingStr.ToUpper().Contains("FUELBREAK")) lyrStr = "FUELBREAKS";
                else if (startingStr.ToUpper().Contains("DISTRICT")) lyrStr = "DISTRICTS";
                else if (startingStr.ToUpper().Contains("PLANTING")) lyrStr = "ACTIVITIES";
                else if (startingStr.ToUpper().Contains("SPRAYING")) lyrStr = "ACTIVITIES";
                else if (startingStr.ToUpper().Contains("EVALUATION")) lyrStr = "ACTIVITIES";
                else if (startingStr.ToUpper().Contains("MISCELLANEOUS")) lyrStr = "ACTIVITIES";
                else if (startingStr.ToUpper().Contains("COMMERCIAL")) lyrStr = "ACTIVITIES";
                else if (startingStr.ToUpper().Contains("SITE")) lyrStr = "ACTIVITIES";

                var layer = MapControl.GetLayer(lyrStr);
                if (layer == null) return;
                //MapControl.UxMap.ActiveLayer = layer;
                PGFeatureSet pGFeatureSet = (PGFeatureSet)layer.DataSet;
                //MapControl.UxMap.ActiveLayer.DataSet = new PGFeatureSet(pGFeatureSet.Server, pGFeatureSet.Table, "Geometry", "Guid");
                //MapControl.UxMap.ActiveLayer.Selection.Configure();
                layer.DataSet = new PGFeatureSet(pGFeatureSet.Server, pGFeatureSet.Table, "Geometry", "Guid");
                layer.Selection.Configure();
            }


            MapControl.UxMap.MapFrame.Initialize();
        }
    }
}
