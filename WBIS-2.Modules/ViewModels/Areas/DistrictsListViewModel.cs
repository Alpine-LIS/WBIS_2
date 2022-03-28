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
//using Microsoft.Win32;
//using Atlas.Data;
//using NetTopologySuite.Geometries;
//using System.Collections.ObjectModel;

//namespace WBIS_2.Modules.ViewModels
//{
//    public class DistrictsListViewModel : ReadOnlyListViewModel<District>
//    {
//        public static DistrictsListViewModel Create(string caption, string content, List<Guid> changedRecords = null)
//        {
//            return ViewModelSource.Create(() => new DistrictsListViewModel(changedRecords)
//            {
//                Caption = caption,
//                Content = content
//            });
//    }
//        List<Guid> ChangedRecords;
//        protected DistrictsListViewModel(List<Guid> changedRecords)
//        {
//            ChangedRecords = changedRecords;
//            LockedRecord = !CurrentUser.AdminPrivileges;
//            SetListType(typeof(District));
//            Privileges();
//            SelectionUpdated += UpdateChildren;
//        }



//        private void UpdateChildren(object sender, EventArgs e)
//        {
//                IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
//                IDocument document = service.FindDocumentById("District Children");
//                if (document != null)
//                {
//                    var ChildrenView = (ChildrenListViewModel)document.Content;
//                    ChildrenView.ParentQuery = SelectedItems.Cast<District>().ToArray();
//                    RaisePropertyChanged(nameof(ChildrenView));
//                }
//        }

//        public override void ShowDetails()
//        {
//            if (CurrentRecord == null)
//            {
//                return;
//            }
//            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
//            IDocument document = service.FindDocumentById("District Children");
//            if (document == null)
//            {
//                var ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<District>().ToArray(), new District());
//                document = service.CreateDocument("ChildrenListView", ChildrenView, "District Children", this);
//                document.Id = "District Children";
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
//                Database.Districts.Remove(CurrentRecord);
//                Database.SaveChanges();
//                Records.Refresh();
//            }
//        }

//        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
//        {
//            if (ChangedRecords != null)
//            {
//                e.QueryableSource = Database.Set<District>().Where(_ => ChangedRecords.Contains(_.Guid));//.AsTracking();

//                return;
//            }

//            e.QueryableSource = Database.Set<District>();//.AsTracking();
//            e.Tag = Database;
//        }

//        public override void Save()
//        {

//        }

//        public override void CloseForm()
//        {
//            IModuleManager Manager = ModuleManager.DefaultManager;
//            Manager.Remove(Common.Regions.Documents, typeof(District).Name);
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
//            OpenFileDialog ofd = new OpenFileDialog();
//            ofd.Filter = "SHP|*.shp";
//            if (!ofd.ShowDialog().Value) return;

//            foreach(District district in Database.Districts)
//            {
//                Database.Districts.Remove(district);
//            }
//            Database.SaveChanges();

//            Shapefile shapefile = new PolygonShapefile(ofd.FileName);
//            foreach (var feat in shapefile.Features)
//            {
//                District district = Database.Districts.FirstOrDefault(_ => _.DistrictName == feat.DataRow["Name"].ToString());
//                if (district == null)
//                {
//                    district = new District() { DistrictName = feat.DataRow["Name"].ToString(), ManagementArea = GetManagmentArea(feat.DataRow["Name"].ToString()) };

//                    //if (feat.Geometry is NetTopologySuite.Geometries.Polygon) district.Geometry = new MultiPolygon(new Polygon[] { (Polygon)feat.Geometry });
//                    //else district.Geometry = (MultiPolygon)feat.Geometry;
//                    //if (district.Geometry.SRID == 0) district.Geometry.SRID = 26710;

//                    Database.Districts.Add(district);
//                }
//                else
//                {
//                    //if (feat.Geometry is NetTopologySuite.Geometries.Polygon) district.Geometry = new MultiPolygon(new Polygon[] { (Polygon)feat.Geometry });
//                    //else district.Geometry = (MultiPolygon)feat.Geometry;
//                    //if (district.Geometry.SRID == 0) district.Geometry.SRID = 26710;

//                    Database.Districts.Update(district);
//                }
//            }
//            Database.SaveChanges();
//            UpdateConnections();
//        }
//        private string GetManagmentArea(string districtName)
//        {
//            if (districtName == "Lassen")
//                return "Northern Sierra Area";
//            else if (districtName == "Martell")
//                return "Southern Sierra Area";
//            else if (districtName == "Burney")
//                return "Cascade Area";
//            else if (districtName == "Redding")
//                return "Cascade Area";
//            else if (districtName == "Tahoe")
//                return "Northern Sierra Area";
//            else if (districtName == "Coast")
//                return "Cascade Area";
//            else if (districtName == "Sonora")
//                return "Southern Sierra Area";
//            else if (districtName == "Weaverville")
//                return "Cascade Area";
//            else if (districtName == "Stirling")
//                return "Northern Sierra Area";
//            else if (districtName == "Camino")
//                return "Southern Sierra Area";
//            else if (districtName == "Almanor")
//                return "Northern Sierra Area";
//            else
//                return "";
//        }

//        private void UpdateConnections()
//        {
//            var districts = Database.Districts
//                .Include(_=>_.Watersheds)
//                .Include(_=>_.Hex160s)
//                .Include(_ => _.CNDDBOccurrences)
//                .Include(_ => _.CDFW_SpottedOwls).ToArray();

//            var Watersheds = Database.Watersheds
//                    .Include(_ => _.Districts).ToList();
//            //var Hex160s = Database.Hex160s
//            //        .Include(_ => _.District).ToList();
//            var CNDDBOccurrences = Database.CNDDBOccurrences
//                    .Include(_ => _.Districts).ToList();
//            var CDFW_SpottedOwls = Database.CDFW_SpottedOwls
//                    .Include(_ => _.District).ToList();

//            foreach (var district in districts)
//            {
//                district.Watersheds = new List<Watershed>();
//                district.Hex160s = new List<Hex160>();
//                district.CNDDBOccurrences = new List<CNDDBOccurrence>();
//                district.CDFW_SpottedOwls = new List<CDFW_SpottedOwl>();

//                //district.Watersheds = Watersheds
//                //    .Where(_ => _.Geometry.IsWithinDistance(district.Geometry, 1)).ToList();
//                ////district.Hex160s = Hex160s
//                ////    .Where(_ => _.Geometry.IsWithinDistance(district.Geometry, 1)).ToList();
//                //district.CNDDBOccurrences = CNDDBOccurrences
//                //    .Where(_ => _.Geometry.IsWithinDistance(district.Geometry, 1)).ToList();
//                //district.CDFW_SpottedOwls = CDFW_SpottedOwls
//                //    .Where(_ => _.Geometry.IsWithinDistance(district.Geometry, 1)).ToList();

//                Database.Districts.Update(district);
//            }
//            Database.SaveChanges();
//        }
//    }
//}
