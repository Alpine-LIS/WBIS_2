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
using System.Data;
using System.IO;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace WBIS_2.Modules.ViewModels
{
    public class WatershedsListViewModel : ReadOnlyListViewModel<Watershed>
    {
        public static WatershedsListViewModel Create(string caption, string content, List<Guid> changedRecords = null)
        {
            return ViewModelSource.Create(() => new WatershedsListViewModel(changedRecords)
            {
                Caption = caption,
                Content = content
            });
        }
        List<Guid> ChangedRecords;
        protected WatershedsListViewModel(List<Guid> changedRecords)
        {
            ChangedRecords = changedRecords;
            LockedRecord = !CurrentUser.AdminPrivileges;
            SetListType(typeof(Watershed));
            Privileges();
            SelectionUpdated += UpdateChildren;
        }


        public ChildrenListViewModel ChildrenView { get; set; }
        private void UpdateChildren(object sender, EventArgs e)
        {
            if (ChildrenView != null)
            {
                ChildrenView.ParentQuery = SelectedItems.Cast<Watershed>().ToArray();
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
            IDocument document = service.FindDocumentById("Watershed Children");
            if (document == null)
            {
                ChildrenView = ChildrenListViewModel.Create(SelectedItems.Cast<Watershed>().ToArray(), new Watershed());
                document = service.CreateDocument("ChildrenListView", ChildrenView, "Watershed Children", this);
                document.Id = "Watershed Children";
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
                Database.Watersheds.Remove(CurrentRecord);
                Database.SaveChanges();
                Records.Refresh();
            }
        }

        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (ChangedRecords != null)
            {
                e.QueryableSource = Database.Set<Watershed>().Where(_ => ChangedRecords.Contains(_.Guid));//.AsTracking();

                return;
            }

            e.QueryableSource = Database.Set<Watershed>();
            e.Tag = Database;
        }

        public override void Save()
        {

        }

        public override void CloseForm()
        {
            IModuleManager Manager = ModuleManager.DefaultManager;
            Manager.Remove(Common.Regions.Documents, typeof(Watershed).Name);
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
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "CSV|*.csv";
            if (!ofd1.ShowDialog().Value) return;

            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(ofd1.FileName))
            {
                string txt = sr.ReadToEnd();
                var lines = txt.Split('\n');
                string[] cols = lines[0].Split(',');
                foreach (string col in cols)
                {
                    dt.Columns.Add(col);
                }
                for (int i = 1; i < lines.Count(); i++)
                {
                    DataRow r = dt.NewRow();
                    string[] vals = lines[i].Split(',');
                    for (int t = 0; t < vals.Count(); t++)
                    {
                        r[t] = vals[t];
                    }
                    dt.Rows.Add(r);
                }
            }

            foreach (Watershed district in Database.Watersheds)
            {
                Database.Watersheds.Remove(district);
            }
            Database.SaveChanges();

            //Shapefile shapefile = new PolygonShapefile(ofd.FileName);
            foreach (DataRow DataRow in dt.Rows)
            {
                Watershed watershed;// = Database.Quad75s.FirstOrDefault(_ => _.QuadCode == feat.DataRow["Name"].ToString());
                                    //if (quad == null)
                                    //{
                watershed = new Watershed()
                {
                    WatershedName = DataRow["WSHD_NAME"].ToString(),
                    WatershedID = DataRow["WSHD_ID"].ToString().Trim(),
                    MouthTRS = DataRow["MOUTH_TRS"].ToString(),
                    MouthLat = TryIntParse(DataRow["MOUTH_LAT"].ToString()),
                    MouthLon = TryIntParse(DataRow["MOUTH_LON"].ToString()),
                    ElevationMin = TryIntParse(DataRow["ELEV_MIN"].ToString()),
                    ElevationMax = TryIntParse(DataRow["ELEV_MAX"].ToString()),
                    BasinLength = TryIntParse(DataRow["BASIN_LEN"].ToString()),
                    Perim001 = TryIntParse(DataRow["PERIM_001"].ToString()),
                    VallyLength = TryIntParse(DataRow["VALLEY_LEN"].ToString()),
                    ChanelLength = TryIntParse(DataRow["CHAN_LEN"].ToString()),
                    ChanelOrientation = DataRow["CHAN_ORIEN"].ToString(),
                    WS_Order = TryIntParse(DataRow["WS_ORDER"].ToString()),
                    HumintPop = DataRow["HUMINT_POP"].ToString(),
                    HumintRec = DataRow["HUMINT_REC"].ToString(),
                    HumintVis = DataRow["HUMINT_VIS"].ToString(),
                    Geology = DataRow["GEOLOGY"].ToString(),
                    BasinMType = TryIntParse(DataRow["BASINMTYPE"].ToString()),
                    RiverName = DataRow["RIVER_NAME"].ToString(),
                    DownStrWshd = DataRow["DOWNSTRWSH"].ToString(),
                    HydroReg = DataRow["HYDRO_REG"].ToString(),
                    RWQCB = DataRow["RWQCB"].ToString(),
                    Hydrologic = DataRow["HYDROLOGIC"].ToString(),
                    HydroArea = DataRow["HYDRO_AREA"].ToString(),
                    HydroSuba = DataRow["HYDRO_SUBA"].ToString(),
                    SuperPlan = DataRow["SUPER_PLAN"].ToString(),
                    Threatend = DataRow["THREATENED"].ToString(),
                    ASP_UP = DataRow["ASP_UP"].ToString(),
                    TOC = TryBoolParse(DataRow["TOC"].ToString()),
                    ESU = TryBoolParse(DataRow["ESU"].ToString()),
                    D303 = DataRow["D303"].ToString(),
                    WshdAcres = TryIntParse(DataRow["WSHD_ACRES"].ToString()),
                    SPIAcres = TryIntParse(DataRow["SPI_ACRES"].ToString()),
                    Perimeter = TryIntParse(DataRow["PERIMETER"].ToString())
                };

                Database.Watersheds.Add(watershed);
            }
            Database.SaveChanges();

            OpenFileDialog ofd2 = new OpenFileDialog();
            ofd2.Filter = "SHP|*.shp";
            if (!ofd2.ShowDialog().Value) return;

            Shapefile shapefile = new PolygonShapefile(ofd2.FileName);
            foreach (var feat in shapefile.Features)
            {
                Watershed watershed = Database.Watersheds.FirstOrDefault(_ => _.WatershedID == feat.DataRow["WSHD_ID"].ToString().Trim());
                if (watershed == null) continue;

                if (feat.Geometry is NetTopologySuite.Geometries.Polygon) watershed.Geometry = new MultiPolygon(new Polygon[] { (Polygon)feat.Geometry });
                else watershed.Geometry = (MultiPolygon)feat.Geometry;
                if (watershed.Geometry.SRID == 0) watershed.Geometry.SRID = 26710;

                Database.Watersheds.Update(watershed);
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
        private double TryDoubleParse(string val)
        {
            double returnVal = 0d;
            double.TryParse(val, out returnVal);
            return returnVal;
        }
            private bool? TryBoolParse(string val)
            {
                bool returnVal = false;
            if (               bool.TryParse(val, out returnVal))
                return returnVal;
            else
                return null;
            }

        private void UpdateConnections()
        {
            var watersheds = Database.Watersheds
                .Include(_ => _.Districts)
                .Include(_ => _.Hex160s)
                .Include(_ => _.CNDDBOccurrences)
                .Include(_ => _.CDFW_SpottedOwls);

            var Districts = Database.Districts
                .Include(_ => _.Watersheds).ToList();
            var Hex160s = Database.Hex160s
                .Include(_ => _.Watersheds).ToList();
            var CNDDBOccurrences = Database.CNDDBOccurrences
                .Include(_ => _.Watersheds).ToList();
            var CDFW_SpottedOwls = Database.CDFW_SpottedOwls
                .Include(_ => _.Watersheds).ToList();

            foreach (var watershed in watersheds)
            {
                watershed.Districts = new List<District>();
                watershed.Hex160s = new List<Hex160>();
                watershed.CNDDBOccurrences = new List<CNDDBOccurrence>();
                watershed.CDFW_SpottedOwls = new List<CDFW_SpottedOwl>();

                watershed.Districts = Districts
                    .Where(_ => _.Geometry.IsWithinDistance(watershed.Geometry, 1)).ToList();
                watershed.Hex160s = Hex160s
                    .Where(_ => _.Geometry.IsWithinDistance(watershed.Geometry, 1)).ToList();
                watershed.CNDDBOccurrences = CNDDBOccurrences
                    .Where(_ => _.Geometry.IsWithinDistance(watershed.Geometry, 1)).ToList();
                watershed.CDFW_SpottedOwls = CDFW_SpottedOwls
                    .Where(_ => _.Geometry.IsWithinDistance(watershed.Geometry, 1)).ToList();

                Database.Watersheds.Update(watershed);
            }
            Database.SaveChanges();
        }
    }
}
