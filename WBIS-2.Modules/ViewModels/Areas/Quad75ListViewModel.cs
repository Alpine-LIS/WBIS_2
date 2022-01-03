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
using Microsoft.Win32;
using Atlas.Data;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace WBIS_2.Modules.ViewModels
{
    public class Quad75sListViewModel : ReadOnlyListViewModel<Quad75>
    {
        public static Quad75sListViewModel Create(string caption, string content, List<Guid> changedRecords = null)
        {
            return ViewModelSource.Create(() => new Quad75sListViewModel(changedRecords)
            {
                Caption = caption,
                Content = content
            });
        }
        List<Guid> ChangedRecords;
        protected Quad75sListViewModel(List<Guid> changedRecords)
        {
            ChangedRecords = changedRecords;
            LockedRecord = !CurrentUser.AdminPrivileges;
            SetListType(typeof(Quad75));
            Privileges();
            SelectionUpdated += UpdateChildren;
        }


        public ChildrenListViewModel ChildrenView { get; set; }
        private void UpdateChildren(object sender, EventArgs e)
        {
            if (ChildrenView != null)
            {
                ChildrenView.ParentQuery = SelectedItems.Cast<Quad75>().ToArray();
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
            IDocument document = service.FindDocumentById("Quad75 Children");
            if (document == null)
            {
                ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<Quad75>().ToArray(), new Quad75());
                document = service.CreateDocument("ChildrenListView", ChildrenView, "Quad75 Children", this);
                document.Id = "Quad75 Children";
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
                Database.Quad75s.Remove(CurrentRecord);
                Database.SaveChanges();
                Records.Refresh();
            }
        }

        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (ChangedRecords != null)
            {
                e.QueryableSource = Database.Set<Quad75>().Where(_ => ChangedRecords.Contains(_.Guid));//.AsTracking();

                return;
            }

            e.QueryableSource = Database.Set<Quad75>();
            e.Tag = Database;
        }

        public override void Save()
        {

        }

        public override void CloseForm()
        {
            IModuleManager Manager = ModuleManager.DefaultManager;
            Manager.Remove(Common.Regions.Documents, typeof(Quad75).Name);
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

            foreach (Quad75 district in Database.Quad75s)
            {
                Database.Quad75s.Remove(district);
            }
            Database.SaveChanges();

            Shapefile shapefile = new PolygonShapefile(ofd.FileName);
            foreach (var feat in shapefile.Features)
            {
                Quad75 quad;// = Database.Quad75s.FirstOrDefault(_ => _.QuadCode == feat.DataRow["Name"].ToString());
                //if (quad == null)
                //{
                    quad = new Quad75()
                    {
                        QuadCode = feat.DataRow["QUADCODE"].ToString(),
                        UsgsCode = feat.DataRow["USGSCODE"].ToString(),
                        CNPSCode = feat.DataRow["CNPSCODE"].ToString(),
                        QuadName = feat.DataRow["QUADNAME"].ToString(),
                        QuadSize = feat.DataRow["QUADSIZE"].ToString(),
                        Q24Year = TryIntParse(feat.DataRow["Q24YEAR"].ToString()),
                        Q100Name = feat.DataRow["Q100NAME"].ToString(),
                        Border = feat.DataRow["BORDER"].ToString(),
                        UTMZone = feat.DataRow["UTMZONE"].ToString(),
                        B_M = feat.DataRow["B_M"].ToString(),
                        Sensitive = feat.DataRow["SENSITIVE"].ToString(),
                        Perimeter = (double)feat.DataRow["PERIMETER"],
                        Area = (double)feat.DataRow["AREA"]
                    };

                    quad.Geometry = (Polygon)feat.Geometry;
                    if (quad.Geometry.SRID == 0) quad.Geometry.SRID = 26710;

                    Database.Quad75s.Add(quad);
                //}
                //else
                //{
                //    quad.Geometry = (Polygon)feat.Geometry;
                //    if (quad.Geometry.SRID == 0) quad.Geometry.SRID = 26710;

                //    Database.Quad75s.Update(quad);
                //}
            }
            Database.SaveChanges();
            UpdateConnections();
        }
        private int TryIntParse(string val)
        {
            int returnVal = 0;
            int.TryParse(val, out returnVal);
            return returnVal; 
        }

        private void UpdateConnections()
        {
            var quads = Database.Quad75s
                .Include(_ => _.Hex160s)
                .Include(_ => _.CNDDBOccurrences)
                .Include(_ => _.CDFW_SpottedOwls);

            var Hex160s = Database.Hex160s
                   .Include(_ => _.Quad75s).ToList();
            var CNDDBOccurrences = Database.CNDDBOccurrences
                .Include(_ => _.Quad75s).ToList();
            var CDFW_SpottedOwls = Database.CDFW_SpottedOwls
                .Include(_ => _.Quad75s).ToList();

            foreach (var quad in quads)
            {
                quad.Hex160s = new List<Hex160>();
                quad.CNDDBOccurrences = new List<CNDDBOccurrence>();
                quad.CDFW_SpottedOwls = new List<CDFW_SpottedOwl>();
               
                quad.Hex160s = Hex160s
                    .Where(_ => _.Geometry.IsWithinDistance(quad.Geometry, 1)).ToList();
                quad.CNDDBOccurrences = CNDDBOccurrences
                    .Where(_ => _.Geometry.IsWithinDistance(quad.Geometry, 1)).ToList();
                quad.CDFW_SpottedOwls = CDFW_SpottedOwls
                    .Where(_ => _.Geometry.IsWithinDistance(quad.Geometry, 1)).ToList();

                Database.Quad75s.Update(quad);
            }
            Database.SaveChanges();
        }
    }
}
