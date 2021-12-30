using System;
using System.Collections.Generic;

namespace WBIS_2.DataModel
{
    public static class MapDataPasser
    {
        public static string ZoomLayerName { get; set; }
        public static string ZoomKeyField { get; set; }
        public static List<object> ZoomKeyFields { get; set; }
        public static event EventHandler ZoomToLayerEvent;
        public static void ZoomToLayer(string layerName, string keyField, List<object> keyFields)
        {
            ZoomLayerName = layerName;
            ZoomKeyField = keyField;
            ZoomKeyFields = keyFields;
            ZoomToLayerEvent?.Invoke(new object(), new EventArgs());
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
    }
}
