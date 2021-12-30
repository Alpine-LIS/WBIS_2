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
using System.Data;
using Atlas.Projections;
using WBIS_2.Modules.Interfaces;
using System.Collections.ObjectModel;

namespace WBIS_2.Modules.ViewModels
{
    public class CnddbsListViewModel : ReadOnlyListViewModel<CNDDBOccurrence>, IMapNavigation
    {
        public static CnddbsListViewModel Create(string caption, string content, List<Guid> changedRecords = null)
        {
            return ViewModelSource.Create(() => new CnddbsListViewModel(changedRecords)
            {
                Caption = caption,
                Content = content,
                SelectedItems = new ObservableCollection<CNDDBOccurrence>(),
            });
        }
        List<Guid> ChangedRecords;


        public string LayerName => "CNDDB_OCCURRENCES";
        public string LayerKeyField => "eondx";
        public string TableKeyField => "eondx";

        protected CnddbsListViewModel(List<Guid> changedRecords)
        {
            ChangedRecords = changedRecords;
            LockedRecord = !CurrentUser.AdminPrivileges;
            SetListType(typeof(CNDDBOccurrence));
            Privileges();
        }

        public override void ShowDetails()
        {
            if (CurrentRecord == null)
            {
                return;
            }
            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(CurrentRecord.Guid);
            if (document == null)
            {
                //document = service.CreateDocument("CnddbsListView", CnddbsListViewModel.Create(CurrentRecord), CurrentRecord, this);
                //document.Id = CurrentRecord.Guid;
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
                Database.CNDDBOccurrences.Remove(CurrentRecord);
                Database.SaveChanges();
                Records.Refresh();
            }
        }

        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (ChangedRecords != null)
            {
                e.QueryableSource = Database.Set<CNDDBOccurrence>().Where(_ => ChangedRecords.Contains(_.Guid));//.AsTracking();

                return;
            }

            e.QueryableSource = Database.Set<CNDDBOccurrence>();
            e.Tag = Database;
        }

        public override void Save()
        {

        }

        public override void CloseForm()
        {
            IModuleManager Manager = ModuleManager.DefaultManager;
            Manager.Remove(Common.Regions.Documents, typeof(CNDDBOccurrence).Name);
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

            foreach (CNDDBOccurrence district in Database.CNDDBOccurrences)
            {
                Database.CNDDBOccurrences.Remove(district);
            }
            Database.SaveChanges();

            Shapefile shapefile = new PolygonShapefile(ofd.FileName);
            FilterFromCountyCnddb(shapefile);
            shapefile.SaveAs(ofd.FileName, true);
                       

            shapefile.Reproject(ProjectionInfo.FromEpsgCode(26710));
            shapefile.SaveAs(ofd.FileName, true);
            shapefile = new PolygonShapefile(ofd.FileName);


            foreach (IFeature feat in shapefile.Features)
            {
                Geometry geometry = feat.Geometry;
                geometry = NetTopologySuite.Simplify.DouglasPeuckerSimplifier.Simplify(geometry, 5d * 0.3048);//Simplify by 5 feet
                feat.Geometry = geometry;
            }
            shapefile.SaveAs(ofd.FileName, true);


            foreach (var feat in shapefile.Features)
            {
                CNDDBOccurrence cNDDB;// = Database.Quad75s.FirstOrDefault(_ => _.QuadCode == feat.DataRow["Name"].ToString());
                                      //if (quad == null)
                                      //{
                cNDDB = new CNDDBOccurrence()
                {
                    SNAME = feat.DataRow["SNAME"].ToString(),
                    CNAME = feat.DataRow["CNAME"].ToString(),
                    ELMCODE = feat.DataRow["ELMCODE"].ToString(),
                    OCCNUMBER = TryIntParse(feat.DataRow["OCCNUMBER"].ToString()),
                    MAPNDX = feat.DataRow["MAPNDX"].ToString(),
                    EONDX = TryIntParse(feat.DataRow["EONDX"].ToString()),
                    KEYQUAD = feat.DataRow["KEYQUAD"].ToString(),
                    KQUADNAME = feat.DataRow["KQUADNAME"].ToString(),
                    KEYCOUNTY = feat.DataRow["KEYCOUNTY"].ToString(),
                    PLSS = feat.DataRow["PLSS"].ToString(),
                    ELEVATION = TryIntParse(feat.DataRow["ELEVATION"].ToString()),
                    PARTS = TryIntParse(feat.DataRow["PARTS"].ToString()),
                    ELMTYPE = TryIntParse(feat.DataRow["ELMTYPE"].ToString()),
                    TAXONGROUP = feat.DataRow["TAXONGROUP"].ToString(),
                    EOCOUNT = TryIntParse(feat.DataRow["EOCOUNT"].ToString()),
                    ACCURACY = feat.DataRow["ACCURACY"].ToString(),
                    PRESENCE = feat.DataRow["PRESENCE"].ToString(),
                    OCCTYPE = feat.DataRow["OCCTYPE"].ToString(),
                    OCCRANK = feat.DataRow["OCCRANK"].ToString(),
                    SENSITIVE = feat.DataRow["SENSITIVE"].ToString(),
                    SITEDATE = feat.DataRow["SITEDATE"].ToString(),
                    ELMDATE = feat.DataRow["ELMDATE"].ToString(),
                    OWNERMGT = feat.DataRow["OWNERMGT"].ToString(),
                    FEDLIST = feat.DataRow["FEDLIST"].ToString(),
                    CALLIST = feat.DataRow["CALLIST"].ToString(),
                    GRANK = feat.DataRow["GRANK"].ToString(),
                    SRANK = feat.DataRow["SRANK"].ToString(),
                    RPLANTRANK = feat.DataRow["RPLANTRANK"].ToString(),
                    CDFWSTATUS = feat.DataRow["CDFWSTATUS"].ToString(),
                    OTHRSTATUS = feat.DataRow["OTHRSTATUS"].ToString(),
                    LOCATION = feat.DataRow["LOCATION"].ToString(),
                    LOCDETAILS = feat.DataRow["LOCDETAILS"].ToString(),
                    ECOLOGICAL = feat.DataRow["ECOLOGICAL"].ToString(),
                    GENERAL = feat.DataRow["GENERAL"].ToString(),
                    THREAT = feat.DataRow["THREAT"].ToString(),
                    THREATLIST = feat.DataRow["THREATLIST"].ToString(),
                    LASTUPDATE = feat.DataRow["LASTUPDATE"].ToString(),
                    AREA = TryIntParse(feat.DataRow["AREA"].ToString()),
                    PERIMETER = TryIntParse(feat.DataRow["PERIMETER"].ToString()),
                    AVLCODE = TryIntParse(feat.DataRow["AVLCODE"].ToString()),
                    Symbology = TryIntParse(feat.DataRow["Symbology"].ToString()),
                    SymbologyText = CnddbSymbolParser(feat.DataRow["Symbology"].ToString())

                };


                if (feat.Geometry is NetTopologySuite.Geometries.Polygon) cNDDB.Geometry = new MultiPolygon(new Polygon[] { (Polygon)feat.Geometry });
                else cNDDB.Geometry = (MultiPolygon)feat.Geometry;
                if (cNDDB.Geometry.SRID == 0) cNDDB.Geometry.SRID = 26710;

                Database.CNDDBOccurrences.Add(cNDDB);
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



        private static void FilterFromCountyCnddb(Shapefile cnddbLayer)
        {
            List<string> counties = GetCounties();
            List<int> featureList = new List<int>();

            DataTable dt = cnddbLayer.DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!counties.Contains(dt.Rows[i]["KEYCOUNTY"].ToString())) { featureList.Add(i); }
            }

            for (int i = 0; i < featureList.Count; i++)
            {
                cnddbLayer.ShapeIndices.RemoveAt(featureList[i] - i);
                cnddbLayer.DataTable.Rows.RemoveAt(featureList[i] - i);
            }
        }
        private static List<string> GetCounties()
        {
            List<string> counties = new List<string>
            {
                "ALP",
                "AMA",
                "BUT",
                "CAL",
                "ELD",
                "HUM",
                "LAS",
                "MOD",
                "NEV",
                "PLA",
                "PLU",
                "SHA",
                "SIE",
                "SIS",
                "TEH",
                "TRI",
                "TUO",
                "YUB"
            };
            return counties;
        }
        private string CnddbSymbolParser(string val)
        {
            if (val.Substring(0, 1) == "1")
            {
                if (val.Substring(2, 1) == "1") { return "Plant (80m)"; }
                else if (val.Substring(2, 1) == "2") { return "Plant (Specific)"; }
                else if (val.Substring(2, 1) == "3") { return "Plant (Non-Specific)"; }
                else { return "Plant (Circular)"; }
            }
            else if (val.Substring(0, 1) == "2")
            {
                if (val.Substring(2, 1) == "1") { return "Animal (80m)"; }
                else if (val.Substring(2, 1) == "2") { return "Animal (Specific)"; }
                else if (val.Substring(2, 1) == "3") { return "Animal (Non-Specific)"; }
                else { return "Animal (Circular)"; }
            }
            else if (val.Substring(0, 1) == "3")
            {
                if (val.Substring(2, 1) == "1") { return "Terrestial (80m)"; }
                else if (val.Substring(2, 1) == "2") { return "Terrestial (Specific)"; }
                else if (val.Substring(2, 1) == "3") { return "Terrestial (Non-Specific)"; }
                else { return "Terrestial (Circular)"; }
            }
            else if (val.Substring(0, 1) == "4")
            {
                if (val.Substring(2, 1) == "1") { return "Aquatic (80m)"; }
                else if (val.Substring(2, 1) == "2") { return "Aquatic (Specific)"; }
                else if (val.Substring(2, 1) == "3") { return "Aquatic (Non-Specific)"; }
                else { return "Aquatic (Circular)"; }
            }
            else if (val.Substring(0, 1) == "8")
            {
                if (val.Substring(2, 1) == "1") { return "Multiple (80m)"; }
                else if (val.Substring(2, 1) == "2") { return "Multiple (Specific)"; }
                else if (val.Substring(2, 1) == "3") { return "Multiple (Non-Specific)"; }
                else { return "Multiple (Circular)"; }
            }
            else { return "Sensitive EO"; }
        }
        private int TryIntParse(string val)
        {
            int returnVal = 0;
            int.TryParse(val, out returnVal);
            return returnVal;
        }


        private void UpdateConnections()
        {
            var cnddbs = Database.CNDDBOccurrences
                .Include(_ => _.Watersheds)
                .Include(_ => _.Districts)
                .Include(_ => _.Quad75s);

            var Watersheds = Database.Watersheds
                    .Include(_ => _.CNDDBOccurrences).ToList();
            var Districts = Database.Districts
                    .Include(_ => _.CNDDBOccurrences).ToList();
            var Quad75s = Database.Quad75s
                    .Include(_ => _.CNDDBOccurrences).ToList();

            foreach (var cnddb in cnddbs)
            {
                cnddb.Watersheds = new List<Watershed>();
                cnddb.Districts = new List<District>();
                cnddb.Quad75s = new List<Quad75>();

                cnddb.Watersheds = Watersheds
                    .Where(_ => _.Geometry.IsWithinDistance(cnddb.Geometry, 1)).ToList();
                cnddb.Districts = Districts
                    .Where(_ => _.Geometry.IsWithinDistance(cnddb.Geometry, 1)).ToList();
                cnddb.Quad75s = Quad75s
                    .Where(_ => _.Geometry.IsWithinDistance(cnddb.Geometry, 1)).ToList();

                Database.CNDDBOccurrences.Update(cnddb);
            }
            Database.SaveChanges();
        }

        public ObservableCollection<CNDDBOccurrence> SelectedItems
        {
            get
            {
                return GetProperty(() => SelectedItems);
                //this.Caption = $"Regens {SelectedItems.Count}";
            }
            set { SetProperty(() => SelectedItems, value); }
        }
        public void ZoomToLayer()
        {
            var selectedKeys = new List<object>();
            if (this.SelectedItems.Count > 0)
            {
                foreach (var i in SelectedItems)
                {
                    selectedKeys.Add($"'{i.EONDX}'");
                }
            }
            MapDataPasser.ZoomToLayer(LayerName, LayerKeyField, selectedKeys);
        }
    }
}
