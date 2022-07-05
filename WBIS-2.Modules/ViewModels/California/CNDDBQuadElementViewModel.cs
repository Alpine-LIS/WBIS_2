using Atlas.Data;
using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using WBIS_2.Modules.Tools;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Diagnostics;

namespace WBIS_2.Modules.ViewModels
{
    public class CNDDBQuadElementViewModel : DetailViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => false;
        public override bool ReplacebleGeometry => false;
        public CNDDBQuadElement Element
        {
            get { return GetProperty(() => Element); }
            set
            {
                SetProperty(() => Element, value);
                Record = Element;
            }
        }

        public object Title => $"{Element.ElmCode}{ChangedSign}";


        public static CNDDBQuadElementViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new CNDDBQuadElementViewModel(guid));
        }

        public CNDDBQuadElementViewModel(Guid guid)
        {
            Element = Database.CNDDBQuadElements
                   .FirstOrDefault(_ => _.Id == guid);
        }
       

        public override void CloseForm()
        {
            throw new NotImplementedException();
        }

       
        public override void Save()
        {
           
        }

       


        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public void OnDestroy()
        {
        }
    }
}
