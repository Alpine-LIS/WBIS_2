//using DevExpress.Data.Linq;
//using DevExpress.Mvvm;
//using DevExpress.Mvvm.ModuleInjection;
//using DevExpress.Mvvm.POCO;
//using Microsoft.EntityFrameworkCore;
//using WBIS_2.DataModel;
//using WBIS_2.Modules.Views;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Input;
//using NetTopologySuite.Geometries;
//using Atlas.Data;
//using Microsoft.Win32;
//using Point = NetTopologySuite.Geometries.Point;
//using Atlas.Projections;
//using System.Data;
//using WBIS_2.Modules.Interfaces;
//using System.Collections.ObjectModel;

//namespace WBIS_2.Modules.ViewModels
//{
//    public class CdfwOwlsListViewModel : ReadOnlyListViewModel<CDFW_SpottedOwl>, IMapNavigation
//    {
//        public static CdfwOwlsListViewModel Create(string caption, string content, List<Guid> changedRecords = null)
//        {
//            return ViewModelSource.Create(() => new CdfwOwlsListViewModel(changedRecords)
//            {
//                Caption = caption,
//                Content = content,
//                SelectedItems = new ObservableCollection<CDFW_SpottedOwl>(),
//            });
//        }
//        List<Guid> ChangedRecords;

//        public string LayerName => "CDFW_SPOTTED_OWLS";
//        public string LayerKeyField => "obsid";
//        public string TableKeyField => "obsid";

//        protected CdfwOwlsListViewModel(List<Guid> changedRecords)
//        {
//            ChangedRecords = changedRecords;
//            LockedRecord = !CurrentUser.AdminPrivileges;
//            SetListType(typeof(CDFW_SpottedOwl));
//            Privileges();
//        }

//        public override void ShowDetails()
//        {
//            if (CurrentRecord == null)
//            {
//                return;
//            }
//            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
//            IDocument document = service.FindDocumentById(CurrentRecord.Guid);
//            if (document == null)
//            {
//                //document = service.CreateDocument("Hex160DetailsView", Hex160ChildrenViewModel.Create(CurrentRecord), CurrentRecord, this);
//                //document.Id = CurrentRecord.Guid;
//            }
//            document.Show();
//        }
              
//        public override void DeleteRecord()
//        {
//            if (CurrentRecord == null)
//            {
//                return;
//            }

//            if (MessageBox.Show("Are you sure you want to delete this record?", "Record Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
//            {
//                Database.CDFW_SpottedOwls.Remove(CurrentRecord);
//                Database.SaveChanges();
//                Records.Refresh();
//            }
//        }

//        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
//        {
//            if (ChangedRecords != null)
//            {
//                e.QueryableSource = Database.Set<CDFW_SpottedOwl>().Where(_ => ChangedRecords.Contains(_.Guid));//.AsTracking();

//                return;
//            }

//            e.QueryableSource = Database.Set<CDFW_SpottedOwl>();
//            e.Tag = Database;
//        }

//        public override void Save()
//        {

//        }

//        public override void CloseForm()
//        {
//            IModuleManager Manager = ModuleManager.DefaultManager;
//            Manager.Remove(Common.Regions.Documents, typeof(CDFW_SpottedOwl).Name);
//        }

//        public void OnClose(CancelEventArgs e)
//        {
//        }

//        public void OnDestroy()
//        {
//        }
//        public override void ViewEdits()
//        {
//        }


//        public override void AddRecord()
//        {
//            //var a = Database.CNDDBOccurrences.Include(_ => _.Districts).Where(_ => _.Districts == null).ToArray();
//            var b = Database.CNDDBOccurrences.Include(_ => _.Districts).Where(_ => _.Districts.Count == 0).ToArray();

//            //var c = Database.CDFW_SpottedOwls.Include(_ => _.Districts).Where(_ => _.Districts == null).ToArray();
//            var d = Database.CDFW_SpottedOwls.Include(_ => _.District).Where(_ => _.District != null).ToArray();






//            OpenFileDialog ofd = new OpenFileDialog();
//            ofd.Filter = "SHP|*.shp";
//            if (!ofd.ShowDialog().Value) return;
//            Shapefile shapefile = new PointShapefile(ofd.FileName);


//            OpenFileDialog ofd2 = new OpenFileDialog();
//            ofd2.Filter = "SHP|*.shp";
//            if (!ofd2.ShowDialog().Value) return;
//            Shapefile diagramShape = new LineShapefile(ofd2.FileName);


//            foreach (CDFW_SpottedOwl district in Database.CDFW_SpottedOwls)
//            {
//                Database.CDFW_SpottedOwls.Remove(district);
//            }
//            foreach (CDFW_SpottedOwlDiagram district in Database.CDFW_SpottedOwlDiagrams)
//            {
//                Database.CDFW_SpottedOwlDiagrams.Remove(district);
//            }
//            Database.SaveChanges();

           
//            shapefile.Reproject(ProjectionInfo.FromEpsgCode(26710));
//            shapefile.SaveAs(ofd.FileName, true);
//            diagramShape.Reproject(ProjectionInfo.FromEpsgCode(26710));
//            diagramShape.SaveAs(ofd2.FileName, true);

//            var owlList = FilterFromCountyOwl(shapefile);
//            shapefile.SaveAs(ofd.FileName, true);
//            FilterFromCountyOwlDiagram(diagramShape, owlList);
//            diagramShape.SaveAs(ofd2.FileName, true);
            
//            shapefile = new PointShapefile(ofd.FileName);
//            diagramShape = new LineShapefile(ofd2.FileName);


//            foreach (var feat in shapefile.Features)
//            {
//                CDFW_SpottedOwl owl;// = Database.Quad75s.FirstOrDefault(_ => _.QuadCode == feat.DataRow["Name"].ToString());
//                                    //if (quad == null)
//                                    //{
//                owl = new CDFW_SpottedOwl()
//                {
//                    SNAME = feat.DataRow["SNAME"].ToString(),
//                    CNAME = feat.DataRow["CNAME"].ToString(),
//                    OBSID = TryIntParse( feat.DataRow["OBSID"].ToString()),
//                    MASTEROWL = feat.DataRow["MASTEROWL"].ToString(),
//                    TYPEOBS = feat.DataRow["TYPEOBS"].ToString(),
//                    OBSERVER = feat.DataRow["OBSERVER"].ToString(),
//                    DATEOBS = feat.DataRow["DATEOBS"].ToString(),
//                    TIMEOBS = feat.DataRow["TIMEOBS"].ToString(),
//                    NUMADOBS = TryIntParse(feat.DataRow["NUMADOBS"].ToString()),
//                    AGESEX = feat.DataRow["AGESEX"].ToString(),
//                    PAIR = feat.DataRow["PAIR"].ToString(),
//                    NEST = feat.DataRow["NEST"].ToString(),
//                    NUMYOUNG = feat.DataRow["NUMYOUNG"].ToString(),
//                    SUBSPECIES = feat.DataRow["SUBSPECIES"].ToString(),
//                    LONDD_N83 = TryIntParse(feat.DataRow["LONDD_N83"].ToString()),
//                    LATDD_N83 = TryIntParse(feat.DataRow["LATDD_N83"].ToString()),
//                    COORDSRC = feat.DataRow["COORDSRC"].ToString(),
//                    DOCID = feat.DataRow["DOCID"].ToString(),
//                    COMMENTS = feat.DataRow["COMMENTS"].ToString(),
//                    MTRS = feat.DataRow["MTRS"].ToString(),
//                    HIGHESTUSE = feat.DataRow["HIGHESTUSE"].ToString(),
//                    SYMBOLOGY = feat.DataRow["SYMBOLOGY"].ToString(),
//                };


//                owl.Geometry = (Point)feat.Geometry;
//                if (owl.Geometry.SRID == 0) owl.Geometry.SRID = 26710;

//                Database.CDFW_SpottedOwls.Add(owl);
//                //}
//                //else
//                //{
//                //    quad.Geometry = (Polygon)feat.Geometry;
//                //    if (quad.Geometry.SRID == 0) quad.Geometry.SRID = 26710;

//                //    Database.Quad75s.Update(quad);
//                //}
//            }

//            foreach (var feat in diagramShape.Features)
//            {
//                CDFW_SpottedOwlDiagram owl = new CDFW_SpottedOwlDiagram();

//                if (feat.Geometry.ToString() != "MULTILINESTRING EMPTY")
//                {
//                    if (feat.Geometry is NetTopologySuite.Geometries.MultiLineString) owl.Geometry = (LineString)((MultiLineString)feat.Geometry).Geometries[0];
//                    else owl.Geometry = (LineString)feat.Geometry;
//                    if (owl.Geometry.SRID == 0) owl.Geometry.SRID = 26710;
//                }
//                Database.CDFW_SpottedOwlDiagrams.Add(owl);
//            }

//            Database.SaveChanges();
//            UpdateConnections();
//        }


//        private int TryIntParse(string val)
//        {
//            int returnVal = 0;
//            int.TryParse(val, out returnVal);
//            return returnVal;
//        }

//        private void UpdateConnections()
//        {
//            var owls = Database.CDFW_SpottedOwls
//                .Include(_ => _.Watershed)
//                .Include(_ => _.District)
//                .Include(_ => _.Quad75);

//            var Watersheds = Database.Watersheds
//                    .Include(_ => _.CDFW_SpottedOwls).ToList();
//            var Districts = Database.Districts
//                    .Include(_ => _.CDFW_SpottedOwls).ToList();
//            var Quad75s = Database.Quad75s
//                    .Include(_ => _.CDFW_SpottedOwls).ToList();

//            foreach (var owl in owls)
//            {
//                //owl.Watersheds = new List<Watershed>();
//                //owl.Districts = new List<District>();
//                //owl.Quad75s = new List<Quad75>();

//                //owl.Watersheds = Watersheds
//                //    .Where(_ => _.Geometry.IsWithinDistance(owl.Geometry, 1)).ToList();
//                //owl.Districts = Districts
//                //    .Where(_ => _.Geometry.IsWithinDistance(owl.Geometry, 1)).ToList();
//                //owl.Quad75s = Quad75s
//                //    .Where(_ => _.Geometry.IsWithinDistance(owl.Geometry, 1)).ToList();

//                Database.CDFW_SpottedOwls.Update(owl);
//            }
//            Database.SaveChanges();
//        }

//        private List<string> FilterFromCountyOwl(Shapefile owlPointLayer)
//        {
//            // Shapefile quadLayer = new PolygonShapefile(AppDomain.CurrentDomain.BaseDirectory + "\\MapShapes\\Quad75.shp");
//            var districts = Database.Districts.ToArray();

//            DataTable dataTable = owlPointLayer.DataTable;

//            List<int> removeFeatures = new List<int>();
//            List<string> TouchingOwl = new List<string>();


//            for (int i = 0; i < owlPointLayer.Features.Count; i++)
//            {
//                string Owl = dataTable.Rows[i]["MASTEROWL"].ToString();

//                var point = owlPointLayer.Features[i].Geometry;

//                if (point != null)
//                {
//                    //if (!districts.Any(_ => _.Geometry.Contains(point)))
//                    //{
//                    //    removeFeatures.Add(i);
//                    //}
//                    //else
//                    //{
//                    //    if (Owl.Length > 3) { TouchingOwl.Add(Owl); }
//                    //}
//                }
//                else { removeFeatures.Add(i); }
//            }

//            int subtract = 0;
//            for (int i = 0; i < removeFeatures.Count; i++)
//            {
//                string Owl = dataTable.Rows[removeFeatures[i] - subtract]["MASTEROWL"].ToString();
//                if (!TouchingOwl.Contains(Owl))
//                {
//                    owlPointLayer.Features.RemoveAt(removeFeatures[i] - subtract);
//                    subtract++;
//                }
//            }
//            return TouchingOwl;
//        }
//        private static void FilterFromCountyOwlDiagram(Shapefile layerDiagram, List<string> owlList)
//        {
//            owlList = owlList.Distinct().ToList();
//            List<int> featureList = new List<int>();
//            DataTable dataTable = layerDiagram.DataTable;
//            for (int i = 0; i < dataTable.Rows.Count; i++)
//            {
//                if (!owlList.Contains(dataTable.Rows[i]["MASTEROWL"].ToString())) featureList.Add(i);
//            }

//            for (int i = 0; i < featureList.Count; i++)
//            {
//                layerDiagram.Features.RemoveAt(featureList[i] - i);
//            }
//        }

//        public ObservableCollection<CDFW_SpottedOwl> SelectedItems
//        {
//            get
//            {
//                return GetProperty(() => SelectedItems);
//                //this.Caption = $"Regens {SelectedItems.Count}";
//            }
//            set { SetProperty(() => SelectedItems, value); }
//        }
//        public void ZoomToLayer()
//        {
//            var selectedKeys = new List<object>();
//            if (this.SelectedItems.Count > 0)
//            {
//                foreach (var i in SelectedItems)
//                {
//                    selectedKeys.Add($"'{i.OBSID}'");
//                }
//            }
//            MapDataPasser.ZoomToLayer(LayerName, LayerKeyField, selectedKeys);
//        }
//    }
//}
