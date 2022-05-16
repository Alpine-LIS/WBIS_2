﻿using Atlas.Controls;
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
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WBIS_2.Modules.Tools;

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
                _mapControl.UxMap.KeyDown += UxMap_SelectionMade;
                _mapControl.UxMap.Layers.LayerAdded+= UxMap_LayerAdded;
                _mapControl.UxMap.Layers.LayerRemoved += UxMap_LayerAdded;
                _mapControl.EditorEnabled = _editorEnabled;
            }
        }


        private void UxMap_KeyPress()
        {
            throw new NotImplementedException();
        }

        private void UxMap_LayerAdded(object? sender, LayerEventArgs e)
        {
            CurrentUser.AllLayers = _mapControl.UxMap.Layers.Select(_ => _.LegendText).OrderBy(_ => _).ToList();
        }

        private void UxMap_SelectionChanged(object sender, EventArgs e)
        {
            ResetAFS();
            MapDataPasser.ZoomLayerName = MapControl.ActiveLayer.LegendText;
            MapDataPasser.MapSelectionChanged(MapControl.Selection.ToFeatureList());
        }

        private void UxMap_SelectionMade(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != System.Windows.Forms.Keys.Enter) return;
            MapDataPasser.ZoomLayerName = MapControl.ActiveLayer.LegendText;
            MapDataPasser.MapSelectionMade(MapControl.Selection.ToFeatureList());
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
            //MapDataPasser.UserDistrictsChangedEvent += MapDataPasser_UserDistrictsChanged;
            MapDataPasser.InformationTypesChangedEvent += InformationTypesChanged;
            MapDataPasser.SetActiveLayerEvent += MapDataPasser_SetActiveLayerEvent;
        }

        private void MapDataPasser_SetActiveLayerEvent(object? sender, EventArgs e)
        {
            var layer = MapControl.GetLayer((string)sender);
            if (layer == null) return;
            MapControl.UxMap.SetActiveLayer(layer);
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
            var allKeys = String.Join("','", MapDataPasser.ZoomKeyValues);
            var q2 = $"CONVERT([{MapDataPasser.ZoomKeyField}],'System.String') IN ('{allKeys}')";
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
            var f = layer.DataSet.Features.First(_ => (Guid)_.DataRow[MapDataPasser.ZoomKeyField] == MapDataPasser.ZoomKeyValueSingle);

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
                var f = AfsLayer.Key.DataSet.Features.First(_ => (Guid)_.DataRow[MapDataPasser.ZoomKeyField] == key);
                var a = (PolygonCategory)AfsLayer.Key.GetCategory(f.Fid).Clone();
                a.Symbolizer.SetFillColorEx(System.Drawing.Color.FromArgb(50, 255, 243,13));
                a.Symbolizer.SetOutline(System.Drawing.Color.Yellow, 1);
                a.SelectionSymbolizer.SetFillColorEx(System.Drawing.Color.FromArgb(50, 244, 233, 0));
                a.SelectionSymbolizer.SetOutline(System.Drawing.Color.Yellow, 1);
                PreAfsCategory.Add(key, AfsLayer.Key.GetCategory(f.Fid));
                AfsLayer.Key.SetCategory(f.Fid, a);
            }
            MapControl.UxMap.MapFrame.Invalidate();
        }

        KeyValuePair<IMapFeatureLayer, string> AfsLayer = new KeyValuePair<IMapFeatureLayer, string>();
        //IMapFeatureLayer AfsLayer;
        Dictionary<Guid, IFeatureCategory> PreAfsCategory = new Dictionary<Guid, IFeatureCategory>();
        private void ResetAFS()
        {
            foreach (KeyValuePair<Guid, IFeatureCategory> keyValuePair in PreAfsCategory)
            {
                var f = AfsLayer.Key.DataSet.Features.First(_ => (Guid)_.DataRow[AfsLayer.Value] == keyValuePair.Key);
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

            List<string> layers = (List<string>)sender;

            foreach (string startingStr in layers)
            {
                string lyrStr = startingStr;
                
                var layer = MapControl.GetLayer(lyrStr);
                if (layer == null) continue;
                //MapControl.UxMap.ActiveLayer = layer;
                PGFeatureSet pGFeatureSet = (PGFeatureSet)layer.DataSet;
                //MapControl.UxMap.ActiveLayer.DataSet = new PGFeatureSet(pGFeatureSet.Server, pGFeatureSet.Table, "Geometry", "Guid");
                //MapControl.UxMap.ActiveLayer.Selection.Configure();
                layer.DataSet = new PGFeatureSet(pGFeatureSet.Server, pGFeatureSet.Table, "geometry", "guid");
                layer.Selection.Configure();
                layer.AssignFastDrawnStates();
            }

            MapControl.UxMap.MapFrame.Initialize();
            InformationTypesChanged(new object(), new EventArgs());
        }





        private void MapDataPasser_UserMapOptionsChanged(string layerStr)
        {
            var p = typeof(WBIS2Model).GetProperties().FirstOrDefault(_ => MapDataPasser.SimpleLayerStr(_.Name) == MapDataPasser.SimpleLayerStr(layerStr));
            if (p == null) return;
            var trueType = p.PropertyType.GenericTypeArguments.Single();
           // var runTimeType = p.PropertyType.GenericTypeArguments.Single();
            //var trueType = Type.GetType(runTimeType.FullName);

            //Is the layer a sub element
            var sub = trueType.GetCustomAttributes(typeof(SubElement), true).FirstOrDefault();//.FirstOrDefault(_ => _.AttributeType == typeof(SubElement));
            Type parentType = null; 
            string parent = "";
            string joinParent = "";
            if (sub != null)
            {
                WBIS2Model model = new WBIS2Model();

                parentType = ((SubElement)sub).ParentType;
                var entityType = model.Model.FindEntityType(parentType);
                var schema = entityType.GetSchema();
                parent = entityType.GetTableName().ToLower();

                joinParent = $" INNER JOIN {parent} ON {layerStr.ToLower()}.guid = {parent}.guid";
            }

            string delete = GetDeleteQuery(layerStr, parent, parentType, trueType);
            string repository = GetRepositoryQuery(layerStr, parent, parentType, trueType);
            string districts = GetDistrictQuery(layerStr, parent, parentType, trueType);

            string boolQuery = string.Join(" AND ", new string[] { delete, repository, districts });
            while (boolQuery.StartsWith(" AND "))
                boolQuery = boolQuery.Remove(0, 5);

            string query = $"SELECT {layerStr.ToLower()}.guid, {boolQuery} from {layerStr.ToLower()} {joinParent} ORDER BY {layerStr.ToLower()}.guid";

            DataTable dt = new DataTable();
            using (NpgsqlDataAdapter filler = new NpgsqlDataAdapter(query, WBIS2Model.GetRDSConnectionString()))
                filler.Fill(dt);


            var layer = MapControl.GetLayer(layerStr);
            bool misteap = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1] is DBNull)
                {
                    layer.SetVisible(i, false);
                    MapDataPasser.HiddenFeatures[layerStr].Add((Guid)dt.Rows[i][0]);
                }
                else if (!(bool)dt.Rows[i][1])
                {
                    layer.SetVisible(i, false);
                    MapDataPasser.HiddenFeatures[layerStr].Add((Guid)dt.Rows[i][0]);
                }
                else
                    layer.SetVisible(i, true);
                                
                if ((Guid)dt.Rows[i][0] != (Guid)layer.DataSet.DataTable.Rows[i]["guid"])
                    misteap = true;
            }
            if (misteap)
                MessageBox.Show($"There was a misalignment displaying layer {layerStr}");
        }

        private string GetDistrictQuery(string layerStr, string parent, Type parentType, Type trueType)
        {
            string districts = "";
            //Is the district connection one to one or many to many
            if (parentType == null)
            {
                if (trueType.GetProperty("District") != null)
                    districts = $" {layerStr.ToLower()}.district_id IN ({DistrictGuids})";
                else
                    districts = $" {layerStr.ToLower()}.guid IN (SELECT {DbHelp.GetDbString(trueType)}_id FROM {layerStr.ToLower()}_districts WHERE district_id IN ({DistrictGuids}))";
            }
            else
            {
                if (parentType.GetProperty("District") != null)
                    districts = $" {parent}.district_id IN ({DistrictGuids})";
                else
                    districts = $" {layerStr.ToLower()}.guid IN (SELECT {DbHelp.GetDbString(parentType)}_id FROM {parent}_districts WHERE district_id IN ({DistrictGuids}))";
            }
            return districts;
        }
        private string GetRepositoryQuery(string layerStr, string parent, Type parentType, Type trueType)
        {
            string repository = "";
            if (!CurrentUser.ViewRepository)
            {
                if (parent == "")
                {
                    if (trueType.GetProperties().Any(_ => _.Name == "Repository"))
                        repository = $"{layerStr.ToLower()}.repository = FALSE";
                }
                else
                {
                    if (Type.GetType(parent).GetProperties().Any(_ => _.Name == "Repository"))
                        repository = $"{parent}.repository = FALSE";
                }
            }
            return repository;
        }
        private string GetDeleteQuery(string layerStr, string parent, Type parentType, Type trueType)
        {
            string delete = "";
            if (!CurrentUser.ViewDeleted)
            {
                if (parent == "")
                {
                    if (trueType.GetProperties().Any(_ => _.Name == "_delete"))
                        delete = $"{layerStr.ToLower()}._delete = FALSE";
                }
                else
                {
                    if (Type.GetType(parent).GetProperties().Any(_ => _.Name == "_delete"))
                        delete = $"{parent}._delete = FALSE";
                }
            }
            return delete;
        }


       
        string DistrictGuids = $"'{string.Join("','", CurrentUser.Districts.Select(_ => _.Guid))}'";
        public void InformationTypesChanged(object sender, EventArgs e)
        {
            MapDataPasser.HiddenFeatures = new Dictionary<string, List<Guid>>();
            foreach (var layer in MapControl.UxMap.Layers)
            {
                if (CurrentUser.VisibleLayers.Contains(MapDataPasser.CleanLayerStr(layer.LegendText)))
                {
                    layer.IsVisible = true;
                    MapDataPasser.HiddenFeatures.Add(layer.LegendText, new List<Guid>()); ;
                    MapDataPasser_UserMapOptionsChanged(layer.LegendText);
                }
                else
                {
                    layer.IsVisible = false;
                }
            }
            MapControl.UxMap.MapFrame.Invalidate();
        }
    }
}
