using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WBIS_2.Modules.ViewModels
{
    public abstract class ReadOnlyListViewModel<T> : WBISViewModelBase where T : class, new()
    {
        //public EntityInstantFeedbackSource Records { get; set; }
        public T CurrentRecord { get; set; }


        //public bool IsIMapNavigation
        //{
        //    get
        //    {
        //        if (this is IMapNavigation)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}
        protected ReadOnlyListViewModel()
        {
            //RefreshDataSource();
            //Tracker.ChangesSaved += Tracker_ChangesSaved;
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
            if (e.Any(_ => _.State != EntityState.Unchanged && _.Entity is T))
            {
                RefreshDataSource();
            }
        }
    }
}
