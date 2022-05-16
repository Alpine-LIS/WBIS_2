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
using NetTopologySuite.Geometries;
using System.Reflection;

namespace WBIS_2.Modules.ViewModels
{
    public interface IDetailViewModelBase
    {
        IInformationType Record { get; set; }
        bool HasEditableGeometry  => Record.Manager.InformationType.GetProperty("Geometry") != null;
        PropertyInfo GeoProperty => Record.Manager.InformationType.GetProperty("Geometry");

        ICommand SaveCommand => new DelegateCommand(Save);
        abstract void Save();
        abstract void GeoChanged();

        public ICommand ActivityFeatureExternalCommand => new DelegateCommand(ActivityFeatureExternal);
        private void ActivityFeatureExternal()
        {
            Geometry geo = new RecordFeatureBuilder().ExternalFeature(Record.Manager.InformationType.GetProperty("Geometry"));
            if (geo != null)
            {
                GeoProperty.SetValue(Record,geo);
                GeoChanged();               
            }
        }
        public ICommand ActivityFeatureRemoveCommand => new DelegateCommand(ActivityFeatureRemove);
        private void ActivityFeatureRemove()
        {
            var geometry = GeoProperty.GetValue(Record);
            if (geometry != null)
            {
                GeoProperty.SetValue(Record, null);
                GeoChanged();
            }
        }


        public ICommand ActivityFeatureDrawCommand => new DelegateCommand(ActivityFeatureDraw);
        private void ActivityFeatureDraw()
        {
            MapDataPasser.MapDrawFeature(Record.Guid);
            MapDataPasser.ActivityDrawnEvent += MapDataPasser_ActivityDrawnEvent;
        }
        private void MapDataPasser_ActivityDrawnEvent(object sender, EventArgs e)
        {
            MapDataPasser.ActivityDrawnEvent -= MapDataPasser_ActivityDrawnEvent;
            Geometry geo;
            if (sender is Polygon) geo = new MultiPolygon(new Polygon[] { (Polygon)sender });
            else geo = (MultiPolygon)sender;
            geo.SRID = 26710;
            GeoProperty.SetValue(Record, geo);
            GeoChanged();
        }
    }
}

