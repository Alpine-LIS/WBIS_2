using System;
using System.Collections.Generic;

namespace WBIS_2.DataModel
{
    public static class MapDataPasser
    {
        public static string ZoomLayerName { get; set; }
        public static string ZoomKeyField { get; set; }
        public static string ZoomKeyValueSingle { get; set; }
        public static List<object> ZoomKeyValues { get; set; }
        public static event EventHandler ZoomToLayerEvent;
        public static event EventHandler ZoomToFeatureEvent;
        public static void ZoomToLayer(string layerName, string keyField, List<object> keyFields, bool PerformZoom)
        {
            ZoomLayerName = layerName;
            ZoomKeyField = keyField;
            ZoomKeyValues = keyFields;
            ZoomToLayerEvent?.Invoke(PerformZoom, new EventArgs());
        }
        public static void ZoomToFeature(string layerName, string keyField, string value)
        {
            ZoomLayerName = layerName;
            ZoomKeyField = keyField;
            ZoomKeyValueSingle = value;
            ZoomToFeatureEvent?.Invoke(new object(), new EventArgs());
        }

        public static event EventHandler RefreshLayersEvent;
        public static void RefreshLayers(List<string> lyrNames)
        {
            RefreshLayersEvent?.Invoke(lyrNames, new EventArgs());
        }
        public static void RefreshLayers()
        {
            RefreshLayersEvent?.Invoke(new object(), new EventArgs());
        }

        public static event EventHandler MapSelectionChangedEvent;
        public static void MapSelectionChanged(object featureList)
        {
            MapSelectionChangedEvent?.Invoke(featureList, new EventArgs());
        }

        public static event EventHandler MapDrawFeatureEvent;
        public static void MapDrawFeature(Guid guid)
        {
            ActivityDrawnEvent = null;
            MapDrawFeatureEvent?.Invoke(guid, new EventArgs());
        }
        public static event EventHandler ActivityDrawnEvent;
        public static void ActivityDrawn(NetTopologySuite.Geometries.Geometry geometry)
        {
            ActivityDrawnEvent?.Invoke(geometry, new EventArgs());
            ActivityDrawnEvent = null;
        }
        public static void ActivityNotDrawn()
        {
            ActivityDrawnEvent = null;
        }


        public static bool SelectionFromGrid = false;




        public static event EventHandler MapShowAFSEvent;
        public static void MapShowAFS(string layerName, string keyField, List<object> keyFields)
        {
            ZoomLayerName = layerName;
            ZoomKeyField = keyField;
            ZoomKeyValues = keyFields;
            MapShowAFSEvent?.Invoke(new object(), new EventArgs());
        }

        public static event EventHandler UserDistrictsChangedEvent;
        public static void UserDistrictsChanged()
        {
            UserDistrictsChangedEvent?.Invoke(new object(), new EventArgs());
        }
        public static bool ViewFuelTreatments = false;


        /// <summary>
        /// When the opened information types are changed, or their default visable layers are updated.
        /// </summary>
        public static EventHandler InformationTypesChangedEvent;
        public static void InformationTypesChanged()
        {
            InformationTypesChangedEvent?.Invoke(new object(), new EventArgs());
        }
        public static string CleanLayerStr(string value)
        {
            return value.Replace(" ", "_").ToUpper();
            //return value.Replace(" ", "").Replace("_", "").ToUpper();
        }
    }
}
