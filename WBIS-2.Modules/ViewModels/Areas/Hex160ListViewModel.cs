using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NetTopologySuite.Geometries;
using Atlas.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace WBIS_2.Modules.ViewModels
{
    public class Hex160sListViewModel : ReadOnlyListViewModel<Hex160>
    {
        public static Hex160sListViewModel Create(string caption, string content, List<Guid> changedRecords = null)
        {
            return ViewModelSource.Create(() => new Hex160sListViewModel(changedRecords)
            {
                Caption = caption,
                Content = content
            });
        }
        List<Guid> ChangedRecords;
        protected Hex160sListViewModel(List<Guid> changedRecords)
        {
            ChangedRecords = changedRecords;
            LockedRecord = !CurrentUser.AdminPrivileges;
            SetListType(typeof(Hex160));
            Privileges();
            SelectionUpdated += UpdateChildren;
        }


        public Hex160ChildrenViewModel ChildrenView { get; set; }
        private void UpdateChildren(object sender, EventArgs e)
        {
            if (ChildrenView != null)
            {
                ChildrenView.ParentQuery = SelectedItems.Cast<Hex160>().ToArray();
                RaisePropertyChanged(nameof(ChildrenView));
            }
        }
        public override void ShowDetails()
        {
            if (CurrentRecord == null)
            {
                return;
            }
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById("Hex160 Children");
            if (document == null)
            {
                ChildrenView = Hex160ChildrenViewModel.Create(SelectedItems.Cast<Hex160>().ToArray());
                document = service.CreateDocument("Hex160ChildrenView", ChildrenView, "Hex160 Children", this);
                document.Id = "Hex160 Children";
            }
            document.Show();
        }
              
        public override void DeleteRecord()
        {
            if (CurrentRecord == null)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Record Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Database.Hex160s.Remove(CurrentRecord);
                Database.SaveChanges();
                Records.Refresh();
            }
        }

        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (ChangedRecords != null)
            {
                e.QueryableSource = Database.Set<Hex160>().Where(_ => ChangedRecords.Contains(_.Guid));//.AsTracking();

                return;
            }

            e.QueryableSource = Database.Set<Hex160>();
            e.Tag = Database;
        }

        public override void Save()
        {

        }

        public override void CloseForm()
        {
            IModuleManager Manager = ModuleManager.DefaultManager;
            Manager.Remove(Common.Regions.Documents, typeof(Hex160).Name);
        }

        public void OnClose(CancelEventArgs e)
        {
        }

        public void OnDestroy()
        {
        }
        public override void ViewEdits()
        {
        }


        public override void AddRecord()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            if (!ofd.ShowDialog().Value) return;

            foreach (Hex160 hex in Database.Hex160s)
            {
                Database.Hex160s.Remove(hex);
            }
            Database.SaveChanges();

            Shapefile shapefile = new PolygonShapefile(ofd.FileName);
            foreach (var feat in shapefile.Features)
            {
                Hex160 hex;// = Database.Hex160s.FirstOrDefault(_ => _.Hex160ID == feat.DataRow["HEX_160_ID"].ToString());
                //if (hex == null)
                //{
                    hex = new Hex160() { Hex160ID = feat.DataRow["HEX_160_ID"].ToString() };

                     hex.Geometry = (Polygon)feat.Geometry;
                    if (hex.Geometry.SRID == 0) hex.Geometry.SRID = 26710;

                    Database.Hex160s.Add(hex);
                //}
                //else
                //{
                //    hex.Geometry = (Polygon)feat.Geometry;
                //    if (hex.Geometry.SRID == 0) hex.Geometry.SRID = 26710;

                //    Database.Hex160s.Update(hex);
                //}
            }
            Database.SaveChanges();
            UpdateConnections();
        }
        private void UpdateConnections()
        {
            var hex160s = Database.Hex160s
                .Include(_ => _.Watersheds)
                .Include(_ => _.Districts)
                .Include(_ => _.Quad75s).ToArray();

            var Watersheds = Database.Watersheds
                   .Include(_ => _.Hex160s).ToList();
            var Districts = Database.Districts
                .Include(_ => _.Hex160s).ToList();
            var Quad75s = Database.Quad75s
                .Include(_ => _.Hex160s).ToList();

            foreach (var hex in hex160s)
            {
                hex.Watersheds = new List<Watershed>();
                hex.Districts = new List<District>();
                hex.Quad75s = new List<Quad75>();

                hex.Watersheds = Watersheds
                    .Where(_ => _.Geometry.IsWithinDistance(hex.Geometry, 1)).ToList();
                hex.Districts = Districts
                    .Where(_ => _.Geometry.IsWithinDistance(hex.Geometry, 1)).ToList();
                hex.Quad75s = Quad75s
                    .Where(_ => _.Geometry.IsWithinDistance(hex.Geometry, 1)).ToList();

                Database.Hex160s.Update(hex);
            }
            Database.SaveChanges();
        }
    }
}
