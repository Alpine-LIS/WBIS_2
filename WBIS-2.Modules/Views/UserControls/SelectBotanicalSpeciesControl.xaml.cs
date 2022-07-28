using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SelectBotanicalSpeciesControl.xaml
    /// </summary>
    public partial class SelectBotanicalSpeciesControl : UserControl
    {
        WBIS2Model Database = new WBIS2Model();
        public List<SpeciesSelection> SpeciesSelections = new List<SpeciesSelection>();

        List<object> ParentObjects = new List<object>();
        List<object> UseParentObjects = new List<object>();
        Type ParentType = null;

        public SelectBotanicalSpeciesControl(List<object> parentObjects, Type parentType, string region)
        {
            InitializeComponent();

                ParentObjects = parentObjects;
            UseParentObjects = parentObjects;
            ParentType = parentType;


            if (ParentType == typeof(Quad75))
            {
                GridControl.Columns.First(_=>_.FieldName == "QuadCode").Visible = true;
                GridControl.Columns.First(_ => _.FieldName == "QuadName").Visible = true;
            }
            else if (ParentType == typeof(Quad75))
            {
                GridControl.Columns.First(_ => _.FieldName == "WshdId").Visible = true;
                GridControl.Columns.First(_ => _.FieldName == "WshdName").Visible = true;
            }

            CbxRegions.ItemsSource = Database.Regions.ToList();
            CbxRegions.SelectedItem = Database.Regions.First(_ => _.RegionName == region);           
            CbxRegions.SelectedIndexChanged += CbxRegions_SelectedIndexChanged;


            FillSpeciesList();
        }

        private void CbxRegions_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            FillSpeciesList();
        }

        private void FillSpeciesList()
        {
            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            SpeciesSelections = new List<SpeciesSelection>();
            if (ParentType == null)
            {
                var records = Database.PlantSpecies
                    .Include(_=>_.RegionalPlantLists)
                    .Where(_ => !_.PlaceHolder).AsNoTracking();
                if (((Region)CbxRegions.SelectedItem).RegionName != "Master List")
                    records = records.Where(_ => _.RegionalPlantLists.Select(x => x.Region).Contains((Region)CbxRegions.SelectedItem));

                foreach (var record in records)
                {
                    var s = new SpeciesSelection()
                    {
                        SciName = record.SciName,
                        ComName = record.ComName,
                        RPlantRank = record.RPlantRank,
                        CalList = record.CalList,
                        FedList = record.FedList,
                        guid = record.Id
                    };

                    SpeciesSelections.Add(s);
                }
            }
            else
            {
                if (ChbxSpiPoint.IsChecked.Value)
                    FillSpiPoint();
                if (ChbxSpiPoly.IsChecked.Value)
                    FillSpiPoly();
                if (ChbxPoI.IsChecked.Value)
                    FillPoI();
                if (ChbxCnddbQuad.IsChecked.Value)
                    FillCnddbOcc();
                if (ParentType == typeof(Quad75) && ChbxCnddbQuad.IsChecked.Value) 
                    FillCnddbQuad();
            }
            GridControl.ItemsSource = SpeciesSelections;
            GridControl.RefreshData();
            w.Stop();
        }

        private void FillSpiPoint()
        {
            var records = ((IQueryable<SPIPlantPoint>)new SPIPlantPoint().Manager.GetQueryable(UseParentObjects.ToArray(), ParentType, Database, ForceInclude: new List<string>() { "PlantSpecies.RegionalPlantLists" }, showDelete: false));
            if (((Region)CbxRegions.SelectedItem).RegionName != "Master List")
                records = records.Where(_ => _.PlantSpecies.RegionalPlantLists.Select(x=>x.Region).Contains((Region)CbxRegions.SelectedItem));

            foreach (var record in records)
            {
                var s = SpeciesSelections.FirstOrDefault(_=>_.guid == record.PlantSpecies.Id);
                if (s != null)
                    s.SpiPoint = true;
                else
                {
                    s = new SpeciesSelection()
                    {
                        SciName = record.PlantSpecies.SciName,
                        ComName = record.PlantSpecies.ComName,
                        RPlantRank = record.PlantSpecies.RPlantRank,
                        CalList = record.PlantSpecies.CalList,
                        FedList = record.PlantSpecies.FedList,
                        guid = record.PlantSpecies.Id,
                        SpiPoint = true
                    };

                    if (ParentType == typeof(Quad75))
                    {
                        s.QuadCode = record.Quad75.QuadCode;
                        s.QuadName = record.Quad75.QuadName;
                    }
                    else
                    {
                        s.WshdId = record.Watershed.WatershedID;
                        s.WshdName = record.Watershed.WatershedName;
                    }

                    SpeciesSelections.Add(s);
                }
            }
        }
        private void FillSpiPoly()
        {
            foreach (var p in UseParentObjects)
            {
                var records = ((IQueryable<SPIPlantPolygon>)new SPIPlantPolygon().Manager.GetQueryable(new object[] { p }, ParentType, Database, ForceInclude: new List<string>() { "PlantSpecies.RegionalPlantLists" }, showDelete: false));
                if (((Region)CbxRegions.SelectedItem).RegionName != "Master List")
                    records = records.Where(_ => _.PlantSpecies.RegionalPlantLists.Select(x => x.Region).Contains((Region)CbxRegions.SelectedItem));

                foreach (var record in records)
                {
                    var s = SpeciesSelections.FirstOrDefault(_ => _.guid == record.PlantSpecies.Id);
                    if (s != null)
                        s.SpiPoly = true;
                    else
                    {
                        s = new SpeciesSelection()
                        {
                            SciName = record.PlantSpecies.SciName,
                            ComName = record.PlantSpecies.ComName,
                            RPlantRank = record.PlantSpecies.RPlantRank,
                            CalList = record.PlantSpecies.CalList,
                            FedList = record.PlantSpecies.FedList,
                            guid = record.PlantSpecies.Id,
                            SpiPoly = true
                        };

                        if (ParentType == typeof(Quad75))
                        {
                            s.QuadCode = ((Quad75)p).QuadCode;
                            s.QuadName = ((Quad75)p).QuadName;
                        }
                        else
                        {
                            s.WshdId = ((Watershed)p).WatershedID;
                            s.WshdName = ((Watershed)p).WatershedName;
                        }

                        SpeciesSelections.Add(s);
                    }
                }
            }
        }
        private void FillPoI()
        {
            var records = ((IQueryable<BotanicalElement>)new BotanicalElement().Manager.GetQueryable(UseParentObjects.ToArray(), ParentType, Database, ForceInclude: new List<string>() { "BotanicalPlantOfInterest.PlantSpecies.RegionalPlantLists" }))
                .Where(_=>_.BotanicalPlantOfInterest != null);
            if (((Region)CbxRegions.SelectedItem).RegionName != "Master List")
                records = records.Where(_ => _.BotanicalPlantOfInterest.PlantSpecies.RegionalPlantLists.Select(x => x.Region).Contains((Region)CbxRegions.SelectedItem));

            foreach (var record in records)
            {
                var s = SpeciesSelections.FirstOrDefault(_ => _.guid == record.BotanicalPlantOfInterest.PlantSpecies.Id);
                if (s != null)
                    s.PoI = true;
                else
                {
                    s = new SpeciesSelection()
                    {
                        SciName = record.BotanicalPlantOfInterest.PlantSpecies.SciName,
                        ComName = record.BotanicalPlantOfInterest.PlantSpecies.ComName,
                        RPlantRank = record.BotanicalPlantOfInterest.PlantSpecies.RPlantRank,
                        CalList = record.BotanicalPlantOfInterest.PlantSpecies.CalList,
                        FedList = record.BotanicalPlantOfInterest.PlantSpecies.FedList,
                        guid = record.BotanicalPlantOfInterest.PlantSpecies.Id,
                        PoI = true
                    };

                    if (ParentType == typeof(Quad75))
                    {
                        s.QuadCode = record.Quad75.QuadCode;
                        s.QuadName = record.Quad75.QuadName;
                    }
                    else
                    {
                        s.WshdId = record.Watershed.WatershedID;
                        s.WshdName = record.Watershed.WatershedName;
                    }

                    SpeciesSelections.Add(s);
                }
            }
        }
        private void FillCnddbOcc()
        {
            foreach (var p in UseParentObjects)
            {
                var records = ((IQueryable<CNDDBOccurrence>)new CNDDBOccurrence().Manager.GetQueryable(new object[] { p }, ParentType, Database, ForceInclude: new List<string>() { "PlantSpecies.RegionalPlantLists" }))
                    .Where(_=>_.PlantSpecies != null);
                if (((Region)CbxRegions.SelectedItem).RegionName != "Master List")
                    records = records.Where(_ => _.PlantSpecies.RegionalPlantLists.Select(x => x.Region).Contains((Region)CbxRegions.SelectedItem));

                foreach (var record in records)
                {
                    var s = SpeciesSelections.FirstOrDefault(_ => _.guid == record.PlantSpecies.Id);
                    if (s != null)
                        s.CnddbOcc = true;
                    else
                    {
                        s = new SpeciesSelection()
                        {
                            SciName = record.PlantSpecies.SciName,
                            ComName = record.PlantSpecies.ComName,
                            RPlantRank = record.PlantSpecies.RPlantRank,
                            CalList = record.PlantSpecies.CalList,
                            FedList = record.PlantSpecies.FedList,
                            guid = record.PlantSpecies.Id,
                            CnddbOcc = true
                        };

                        if (ParentType == typeof(Quad75))
                        {
                            s.QuadCode = ((Quad75)p).QuadCode;
                            s.QuadName = ((Quad75)p).QuadName;
                        }
                        else
                        {
                            s.WshdId = ((Watershed)p).WatershedID;
                            s.WshdName = ((Watershed)p).WatershedName;
                        }

                        SpeciesSelections.Add(s);
                    }
                }
            }
        }
        private void FillCnddbQuad()
        {
            var records = ((IQueryable<CNDDBQuadElement>)new CNDDBQuadElement().Manager.GetQueryable(UseParentObjects.ToArray(), ParentType, Database, ForceInclude: new List<string>() { "PlantSpecies.RegionalPlantLists" }))
                .Where(_=>_.PlantSpecies != null);
            if (((Region)CbxRegions.SelectedItem).RegionName != "Master List")
                records = records.Where(_ => _.PlantSpecies.RegionalPlantLists.Select(x => x.Region).Contains((Region)CbxRegions.SelectedItem));

            foreach (var record in records)
            {
                var s = SpeciesSelections.FirstOrDefault(_ => _.guid == record.PlantSpecies.Id);
                if (s != null)
                    s.CnddbQuad = true;
                else
                {
                    s = new SpeciesSelection()
                    {
                        SciName = record.PlantSpecies.SciName,
                        ComName = record.PlantSpecies.ComName,
                        RPlantRank = record.PlantSpecies.RPlantRank,
                        CalList = record.PlantSpecies.CalList,
                        FedList = record.PlantSpecies.FedList,
                        guid = record.PlantSpecies.Id,
                        CnddbQuad = true
                    };
                     
                    s.QuadCode = record.Quad75.QuadCode;
                    s.QuadName = record.Quad75.QuadName;

                    SpeciesSelections.Add(s);
                }
            }
        }



        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }
        private void DeselectAllClick(object sender, RoutedEventArgs e)
        {
            foreach (var w in SpeciesSelections)
                w.SetSelect(false);
        }
        private void SelectAllClick(object sender, RoutedEventArgs e)
        {
            foreach (var w in SpeciesSelections)
                w.SetSelect(true);
        }
        private void SelectTouchingClick(object sender, RoutedEventArgs e)
        {
            //if (GridControl.SelectedItem == null) return;

            //var geo = Database.Watersheds.First(_ => _.Guid == ((SpeciesSelection)GridControl.SelectedItem).guid).Geometry;
            //var watersheds = Database.Watersheds.Where(_ => _.Geometry.Touches(geo));
            //foreach (var watershed in watersheds)
            //{
            //    var w = WatershedSelections.FirstOrDefault(_ => _.guid == watershed.Guid);
            //    if (w != null)
            //        w.SetSelect(true);
            //}
            //((WatershedSelection)GridControl.SelectedItem).SetSelect(true);
        }





        public class SpeciesSelection : BindableBase
        {
            public string SciName { get; set; }
            public string ComName { get; set; }
            public string FedList { get; set; }
            public string CalList { get; set; }
            public string RPlantRank { get; set; }


            public bool SpiPoint { get; set; } = false;
            public bool SpiPoly { get; set; } = false;
            public bool PoI { get; set; } = false;
            public bool CnddbOcc { get; set; } = false;
            public bool CnddbQuad { get; set; } = false;


            public string QuadCode { get; set; }
            public string QuadName { get; set; }
            public string WshdId { get; set; }
            public string WshdName { get; set; }


            public bool Select { get; set; } = true;
            public Guid guid { get; set; }

            public void SetSelect (bool select)
            {
                Select = select;
                RaisePropertyChanged(nameof(Select));
            }
        }

        private void CheckEditChanged(object sender, RoutedEventArgs e)
        {
            FillSpeciesList();
        }

        private void CheckEditChangedAdjacent(object sender, RoutedEventArgs e)
        {
            if (!ChbxAdjacent.IsChecked.Value)
                UseParentObjects = ParentObjects;
            else
            {
                UseParentObjects = new List<object>();
                if (ParentType == typeof(Quad75))
                    foreach (var obj in ParentObjects.Cast<Quad75>())
                        UseParentObjects.AddRange(Database.Quad75s.Where(_ => _.Geometry.Touches(obj.Geometry)).AsNoTracking());
                else
                    foreach (var obj in ParentObjects.Cast<Watershed>())
                        UseParentObjects.AddRange(Database.Watersheds.Where(_ => _.Geometry.Touches(obj.Geometry)).AsNoTracking());
            }
            FillSpeciesList();
        }


    }
}
