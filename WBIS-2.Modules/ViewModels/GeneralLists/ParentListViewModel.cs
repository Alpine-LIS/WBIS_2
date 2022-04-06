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
    public class ParentListViewModel : WBISViewModelBase, IDocumentContent, IMapNavigation
    {
        public object Title
        {
            get { return $"{ParentType.DisplayName}"; }
        }
        public IInformationType ParentType { get; set; }

        public static ParentListViewModel Create(IInformationType parentType)
        {
            return ViewModelSource.Create(() => new ParentListViewModel()
            {
                Caption = $"{(parentType).DisplayName}",
                Content = $"{(parentType).DisplayName}",
                ParentType = parentType,
            });
        }

        public override void CloseForm()
        {
            DocumentOwner.Close(this);
        }

        public void OnClose(CancelEventArgs e)
        {
            if (Changed)
            {
                var result = ThemedMessageBox.Show(title: "Confirmation", text: UnsavedMessageText, messageBoxButtons: MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        public void OnDestroy()
        {

        }

        protected ParentListViewModel()
        {
            //Tracker.ChangesSaved += Tracker_ChangesSaved;
            Privileges();
        }
              
        public override void ShowDetails()
        {
            if (CurrentRecord == null)
            {
                return;
            }

            if (ParentType.AvailibleChildren.Count() > 0)
            {
                IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
                IDocument document = service.FindDocumentById(ParentType.DisplayName + " Children");
                if (document == null)
                {
                    ChildrenListViewModel ChildrenView = ChildrenListViewModel.Create(SelectedItems.ToArray(), ParentType);

                    document = service.CreateDocument("ChildrenListView", ChildrenView, ParentType.DisplayName + " Children", this);
                    document.Id = ParentType.DisplayName + " Children";
                }
                document.Show();
            }
            //else
            //{ }



        }



        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
            
        }


       
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            e.QueryableSource = ((IQueryStuff)ParentType).GetQueryable(Database.Districts.ToArray(), ParentType.GetType(), Database);

            //if (ParentType is District)
            //    e.QueryableSource = Database.Set<District>()
            //        .AsNoTracking();
            //else if (ParentType is Watershed)
            //    e.QueryableSource = Database.Set<Watershed>()
            //        .AsNoTracking();
            //else if (ParentType is Quad75)
            //    e.QueryableSource = Database.Set<Quad75>()
            //        .AsNoTracking();
            //else if (ParentType is Hex160)
            //    e.QueryableSource = Database.Set<Hex160>()
            //        .AsNoTracking();
        }



        public override void Save()
        {
           
        }

        public object CurrentRecord { get; set; }
      
        public override void AddRecord()
        {
            
        }
        public override void DeleteRecord()
        {
           
        }


        public string FilterExpression
        {
            get
            {
                return "";
            }
            set
            {

            }
        }
        public bool ClosingFormEnabled = true;
     
        public string TableKeyField { get { return ""; } }

        public string LayerKeyField { get { return ""; } }

        public string LayerName { get { return ""; } }

        public void ZoomToLayer()
        {
        }

        public void ZoomToFeature(object ZoomObject)
        {
            //if (CurrentUser.RegenUser)
            //    MapDataPasser.ZoomToFeature(LayerName, LayerKeyField, ((Regen)ZoomObject).RegenID);
            //else
            //    MapDataPasser.ZoomToFeature(LayerName, LayerKeyField, ((Fuelbreak)ZoomObject).FuelbreakID);
        }
        public void MapShowAFS(Dictionary<Guid, string> selection)
        {
            MapDataPasser.MapShowAFS(LayerName, LayerKeyField, selection.Values.Cast<object>().Distinct().ToList());
            // MapDataPasser.MapShowAFS(LayerName, "Guid", selection.Cast<object>().ToList());
        }

        public override void SelectionChanged()
        {
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(ParentType.DisplayName + " Children");
            if (document != null)
            {
                ((ChildrenListViewModel)document.Content).UpdateParentQuery(SelectedItems.ToArray());
            }
        }
    }
}
