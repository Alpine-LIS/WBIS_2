using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.ModuleInjection;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace WBIS_2.Modules.ViewModels
{
    public class PlantSpeciesListViewModel : ParentListViewModel
    {
        public static PlantSpeciesListViewModel Create(IInformationType parentType)
        {
            return ViewModelSource.Create(() => new PlantSpeciesListViewModel()
            {
                Caption = $"{parentType.Manager.DisplayName}",
                Content = $"{parentType.Manager.DisplayName}",
                ParentType = parentType,
            });
        }
        public PlantSpeciesListViewModel()
        {
            Regions = Database.Regions.ToArray();
            CurrentRegion = Database.Regions.First(_ => _.RegionName == "Master List");
        }

        public Region CurrentRegion
        {
            get { return GetProperty(() => CurrentRegion); }
            set
            {
                SetProperty(() => CurrentRegion, value);
                RefreshDataSource();
            }
        }
        public Region[] Regions { get; set; }


        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            var query = (IQueryable<PlantSpecies>)ParentType.Manager.GetQueryable(Database, ForceInclude: new List<string>() { "RegionalPlantLists" });
            if (CurrentRegion.RegionName != "Master List")
                query = query.Where(_=>_.RegionalPlantLists.Select(_=>_.Region).Contains(CurrentRegion));

            e.QueryableSource = query;
        }
    }
}
