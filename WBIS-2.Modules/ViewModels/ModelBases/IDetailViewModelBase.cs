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
        bool HasEditableGeometry { get; }
        PropertyInfo GeoProperty { get; }

        ICommand SaveCommand => new DelegateCommand(Save);
        abstract void Save();
        abstract void GeoChanged();

        ICommand FeatureExternalCommand => new DelegateCommand(FeatureExternal);
        abstract void FeatureExternal();
        ICommand FeatureRemoveCommand => new DelegateCommand(FeatureRemove);
        abstract void FeatureRemove();


        ICommand FeatureDrawCommand => new DelegateCommand(FeatureDraw);
        abstract void FeatureDraw();
        abstract void MapDataPasser_DrawnEvent(object sender, EventArgs e);
    }
}

