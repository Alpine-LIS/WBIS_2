using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WBIS_2.Common;
using System.Collections.ObjectModel;
using System.Collections;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.Views.Wildlife;
using WBIS_2.Modules.ViewModels.Wildlife;
using System.IO;
using DevExpress.Mvvm.ModuleInjection;
using Npgsql;
using DevExpress.Mvvm.POCO;
using System.Windows.Controls;
using System.Reflection;
using NetTopologySuite.Geometries;

namespace WBIS_2.Modules.ViewModels
{
    [POCOViewModel]
    public abstract class DetailAndChildrenViewModelBase : ChildrenListViewModel, IDetailViewModelBase
    {
        public IInformationType Record { get; set; }
        public bool HasEditableGeometry => Record.Manager.InformationType.GetProperty("Geometry") != null && ReplacebleGeometry;
        public PropertyInfo GeoProperty => Record.Manager.InformationType.GetProperty("Geometry");
        public virtual bool ReplacebleGeometry => true;

        public abstract void Save();
        public void GeoChanged()
        {
            this.Changed = true;
            if (MessageBox.Show("The geometry has been updated, the new geometry won't be shown until the record is saved. Press ‘OK’ to save or ‘Cancel’ to continue editing.",
                      "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Save();
            }
        }

        public void FeatureExternal()
        {
            Geometry geo = new RecordFeatureBuilder().ExternalFeature(Record.Manager.InformationType.GetProperty("Geometry"));
            if (geo != null)
            {
                GeoProperty.SetValue(Record, geo);
                GeoChanged();
            }
        }
        public void FeatureRemove()
        {            
            var geometry = GeoProperty.GetValue(Record);
            if (geometry != null)
            {
                GeoProperty.SetValue(Record, null);
                GeoChanged();
            }
        }


        public void FeatureDraw()
        {
            MapDataPasser.MapDrawFeature(Record.Id, Record.Manager.GetTableName(Database));
            MapDataPasser.ActivityDrawnEvent += MapDataPasser_DrawnEvent;
        }
        public void MapDataPasser_DrawnEvent(object sender, EventArgs e)
        {
            MapDataPasser.ActivityDrawnEvent -= MapDataPasser_DrawnEvent;
            Geometry geo;
            if (GeoProperty.PropertyType == typeof(MultiPolygon))
            {
                if (sender is Polygon) geo = new MultiPolygon(new Polygon[] { (Polygon)sender });
                else geo = (MultiPolygon)sender;
            }
            else if (GeoProperty.PropertyType == typeof(Polygon))
            {
                if (sender is MultiPolygon) geo = ((MultiPolygon)sender).First();
                else geo = (Polygon)sender;
            }
            else if (GeoProperty.PropertyType == typeof(NetTopologySuite.Geometries.Point))
            {
                if (sender is MultiPoint) geo = ((MultiPoint)sender).First();
                else geo = (NetTopologySuite.Geometries.Point)sender;
            }
            else if(GeoProperty.PropertyType == typeof(MultiPoint))
            {
                if (sender is NetTopologySuite.Geometries.Point) geo = new MultiPoint(new NetTopologySuite.Geometries.Point[] { (NetTopologySuite.Geometries.Point)sender });
                else geo = (MultiPolygon)sender;
            }
            else if (GeoProperty.PropertyType == typeof(MultiLineString))
            {
                if (sender is LineString) geo = new MultiLineString(new LineString[] { (LineString)sender });
                else geo = (MultiLineString)sender;
            }
            else// if (GeoProperty.PropertyType == typeof(LineString))
            {
                if (sender is MultiLineString) geo = ((MultiLineString)sender).First();
                else geo = (LineString)sender;
            }

            geo.SRID = 26710;
            GeoProperty.SetValue(Record, geo);
            GeoChanged();
        }
    }
}

