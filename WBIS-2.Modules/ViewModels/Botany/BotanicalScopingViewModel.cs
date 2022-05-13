using Atlas.Data;
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

namespace WBIS_2.Modules.ViewModels
{
    public class BotanicalScopingViewModel : WBISViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => true;

        private BotanicalScopingSpecies CheckSpecies = null;
        public BotanicalScopingSpecies CurrentSpecies
        {
            get
            {
                return GetProperty(() => CurrentSpecies);
            }
            set
            {
                CheckChangedSpecies();
                SetProperty(() => CurrentSpecies, value);
                if (CurrentSpecies == null)
                {
                    SpeciesRank = $"Rare Plant Rank:";
                    RaisePropertyChanged(nameof(SpeciesRank));
                    return;
                }

                SpeciesRank = $"Rare Plant Rank: {CurrentSpecies.PlantSpecies.RPlantRank}";
                RaisePropertyChanged(nameof(SpeciesRank));
                CheckSpecies = new BotanicalScopingSpecies()
                {
                    HabitatDescription = CurrentSpecies.HabitatDescription,
                    SpiHabitatDescription = CurrentSpecies.SpiHabitatDescription,
                    NddbHabitatDescription = CurrentSpecies.NddbHabitatDescription,
                    ProtectionSummary = CurrentSpecies.ProtectionSummary,
                    Exclude = CurrentSpecies.Exclude,
                    ExcludeText = CurrentSpecies.ExcludeText,
                    ExcludeReport = CurrentSpecies.ExcludeReport
                };
                UpdateSpeciesCount();
            }
        }

        public List<Guid> ChangedSpecies = new List<Guid>();
        private void CheckChangedSpecies()
        {
            if (CheckSpecies == null) return;
            if (CurrentSpecies == null) return;
            if (CheckSpecies.HabitatDescription != CurrentSpecies.HabitatDescription
                || CheckSpecies.SpiHabitatDescription != CurrentSpecies.SpiHabitatDescription
                || CheckSpecies.NddbHabitatDescription != CurrentSpecies.NddbHabitatDescription
                || CheckSpecies.ProtectionSummary != CurrentSpecies.ProtectionSummary
                || CheckSpecies.Exclude != CurrentSpecies.Exclude
                || CheckSpecies.ExcludeText != CurrentSpecies.ExcludeText
                || CheckSpecies.ExcludeReport != CurrentSpecies.ExcludeReport)
            {
                ChangedSpecies.Add(CurrentSpecies.Guid);
                Changed = true;
            }
        }




        public BotanicalScopingSpecies[] SpeciesList
        {
            get { return GetProperty(() => SpeciesList); }
            set
            {
                SetProperty(()=>SpeciesList, value);
                UpdateSpeciesCount();
            }
        }
        public ObservableCollection<BotanicalScopingSpecies> SelectedSpecies { get; set; } = new ObservableCollection<BotanicalScopingSpecies>();

        private void UpdateSpeciesCount()
        {
            SpeciesCount = $"Species: {SpeciesList.Count().ToString("N0")} " +
                                $"| Excluded: {SpeciesList.Count(_ => _.Exclude).ToString("N0")} " +
                                $"| Excluded from report: {SpeciesList.Count(_ => _.ExcludeReport).ToString("N0")}";
            RaisePropertyChanged(nameof(SpeciesCount));
        }
        public string SpeciesCount { get; set; }
        public string SpeciesRank { get; set; } = "Rare Plant Rank:";

        public Watershed CurrentWatershed { get; set; }
        public Watershed[] WatershedList
        {
            get { return GetProperty(() => WatershedList); }
            set
            {
                SetProperty(() => WatershedList, value);
                if (WatershedList.Count() > 0)
                {
                    Scoping.WshdElevationMax = Convert.ToInt32(WatershedList.Max(_ => _.ElevationMax));
                    Scoping.WshdElevationMin = Convert.ToInt32(WatershedList.Min(_ => _.ElevationMin));
                }
                else
                {
                    Scoping.WshdElevationMax = 0;
                    Scoping.WshdElevationMin = 0;
                }
                RaisePropertyChanged(nameof(Scoping));
            }
        }
        public ObservableCollection<Watershed> SelectedWatersheds { get; set; } = new ObservableCollection<Watershed>();

        public BotanicalScoping Scoping { get; set; }
        public string[] ThpNames { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(1000, MinimumLength = 1)]
        public string ThpName { get; set; }
        public string[] Foresters { get; set; }
        public string[] EcoUnits => new string[] { "Outer North Coast Ranges",
            "High Inner North Coast Ranges",
            "Inner Coast Ranges",
            "Klamath Ranges",
            "Cascade Range Foothills",
            "High Cascade Range",
            "Northern Sierra Nevada Foothills",
            "Central Sierra Nevada Foothills",
            "Northern High Sierra Nevada",
            "Central High Sierra Nevada",
            "Modoc Plateau",
            "(Unknown)" };
        public Region[] Regions { get; set; }

        public object Title => $"Scoping: {ThpName}{ChangedSign}";


        public static BotanicalScopingViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new BotanicalScopingViewModel(guid));
        }

        public BotanicalScopingViewModel(Guid guid)
        {
            AllowMapSelectionCommand = new DelegateCommand(AllowMapSelection);
            WatershedListSelectionCommand = new DelegateCommand(WatershedListSelection);
            WatershedRemoveSelectedCommand = new DelegateCommand(WatershedRemoveSelected);


            SpeciesFromWatershedMapCommand = new DelegateCommand(SpeciesFromWatershedMap);
            SpeciesFromQuad75MapCommand = new DelegateCommand(SpeciesFromQuad75Map);
            SpeciesFromWatershedListCommand = new DelegateCommand(SpeciesFromWatershedList);
            RemoveSelectedSpeciesCommand = new DelegateCommand(RemoveSelectedSpecies);
            FloweringTimelineCommand = new DelegateCommand(FloweringTimeline);
            AddSpeciesCommand = new DelegateCommand(AddSpecies);



            ThpNames = Database.THP_Areas.Select(_=>_.THPName).OrderBy(_=>_).ToArray();
                Foresters = Database.BotanicalScopings.Select(_=>_.Forester).Distinct().OrderBy(_ => _).ToArray();
            Regions = Database.Regions.ToArray();

            Scoping = Database.BotanicalScopings
                   .Include(_ => _.THP_Area)
                   .Include(_ => _.Region)
                   .Include(_ => _.Watersheds)
                   .Include(_ => _.Districts)
                   .Include(_ => _.Quad75s)
                   .FirstOrDefault(_ => _.Guid == guid);

            if (Scoping != null)
            {
                WatershedList = Database.Watersheds
                    .Include(_ => _.Districts)
                    .Include(_=>_.BotanicalScopings)
                    .Where(_=>_.BotanicalScopings.Contains(Scoping))
                    .AsNoTracking().ToArray();
                SpeciesList = Database.BotanicalScopingSpecies
                    .Include(_=>_.PlantSpecies)
                    .Include(_=>_.BotanicalScoping).Where(_=>_.BotanicalScoping == Scoping)
                    .AsNoTracking().ToArray();
                if (Scoping.THP_Area != null) ThpName = Scoping.THP_Area.THPName;
            }
            else
            {
                Scoping = new BotanicalScoping();
                Scoping.Guid = guid;
                Scoping.Region = Regions.First(_=>_.RegionName == "Master List");
                SpeciesList = new BotanicalScopingSpecies[0];
                WatershedList = new Watershed[0];
            }

            RaisePropertyChanged(nameof(ThpName));
        }
              
        public override void CloseForm()
        {
            throw new NotImplementedException();
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
            //throw new NotImplementedException();
        }

        public override void Save()
        {
            if (!this.Changed) return;

            if (HasErrors() || Scoping.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            if (WatershedList.Length == 0)
            {
                MessageBox.Show("A botanical scoping must have watersheds.");
                return;
            }

            foreach (var bs in SpeciesList)
            {
                if (bs.Exclude && bs.ExcludeText == "")
                {
                    MessageBox.Show("There are excluded species that are missing a justification.");
                    return;
                }
            }

            

            if (Database.BotanicalScopings
                    .Include(_ => _.THP_Area).Any(_ => _.THP_Area == DbHelp.ThpExistance(Database, ThpName)
                    && _.Guid != Scoping.Guid))
            {
                MessageBox.Show($"There is already a scoping for the thp {ThpName}.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            Database.BotanicalScopingSpecies.RemoveRange(
                Database.BotanicalScopingSpecies.Include(_ => _.BotanicalScoping)
                .Where(_ => _.BotanicalScoping.Guid == Scoping.Guid));

            Scoping.BotanicalScopingSpecies = new List<BotanicalScopingSpecies>();
            Scoping.Watersheds = new List<Watershed>();
            Scoping.Districts = new List<District>();
            Scoping.Quad75s = new List<Quad75>();
                       
            foreach(var bs in SpeciesList)
            {
                BotanicalScopingSpecies botanicalScopingSpecies = new BotanicalScopingSpecies()
                {
                    PlantSpecies = Database.PlantSpecies.First(_=>_.Guid == bs.PlantSpecies.Guid),
                    Exclude = false,
                    ExcludeReport = false,
                    HabitatDescription = bs.HabitatDescription,
                    NddbHabitatDescription = bs.NddbHabitatDescription,
                    SpiHabitatDescription = bs.SpiHabitatDescription,
                    ProtectionSummary = bs.ProtectionSummary,
                    BotanicalScoping = Scoping
                };
                Scoping.BotanicalScopingSpecies.Add(botanicalScopingSpecies);
                Database.BotanicalScopingSpecies.Add(botanicalScopingSpecies);
            }

            Scoping.Watersheds = Database.Watersheds
                .Include(_ => _.Districts)
                .Include(_ => _.Quad75s)
                .Where(_ => WatershedList.Select(z => z.Guid).Contains(_.Guid)).ToList();

            foreach(var water in Scoping.Watersheds)
            {
                ((List<District>)Scoping.Districts).AddRange(water.Districts);
                ((List<Quad75>)Scoping.Quad75s).AddRange(water.Quad75s);
            }

            Scoping.Districts = Scoping.Districts.Distinct().ToList();
            Scoping.Quad75s = Scoping.Quad75s.Distinct().ToList();

            if (Scoping.Districts == null)
                Scoping.Districts = new List<District>() { Database.Districts.First(_=>_.DistrictName == "N/A")};
            if (Scoping.Districts.Count == 0)
                Scoping.Districts = new List<District>() { Database.Districts.First(_ => _.DistrictName == "N/A") };

            THP_Area tHP_Area = DbHelp.ThpExistance(Database, ThpName);
            if (tHP_Area == null)
            {
                tHP_Area = new THP_Area() { THPName = ThpName };
                Database.THP_Areas.Add(tHP_Area);
            }
            Scoping.THP_Area = tHP_Area;

            if (!Database.BotanicalScopings.Contains(Scoping))
                Database.BotanicalScopings.Add(Scoping);
            else Database.BotanicalScopings.Update(Scoping);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
            //throw new NotImplementedException();
        }






        public ICommand AllowMapSelectionCommand { get; set; }
        private void AllowMapSelection()
        {
            MapDataPasser.SetActiveLayer("watersheds");
            MapDataPasser.MapSelectionMadeEvent += MapDataPasser_MapSelectionChangedEvent;
        }

        private void MapDataPasser_MapSelectionChangedEvent(object? sender, EventArgs e)
        {
            MapDataPasser.MapSelectionMadeEvent -= MapDataPasser_MapSelectionChangedEvent;
            if (!MapDataPasser.ZoomLayerName.ToUpper().Contains("WATERSHED")) return;
            if (MessageBox.Show("Would you like to use these watersheds for the scoping?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            List<IFeature> features = (List<IFeature>)sender;
            WatershedList = Database.Watersheds
                .Include(_ => _.Districts)
                .Where(_=>features.Select(x=>x.DataRow["guid"]).Cast<Guid>().Contains(_.Guid))
                .AsNoTracking().ToArray();
            RaisePropertyChanged(nameof(WatershedList));
            this.Changed = true;
        }


        public ICommand WatershedListSelectionCommand { get; set; }
        private void WatershedListSelection()
        {
            var selectWatershedsControl = new Views.UserControls.SelectWatershedsControl(WatershedList.Select(_=>_.Guid).ToArray());
            CustomControlWindow window = new CustomControlWindow(selectWatershedsControl);
            if (window.DialogResult)
            {
                Guid[] newGuids = selectWatershedsControl.WatershedSelections.Where(_=>_.Select).Select(_=>_.guid).ToArray();
                WatershedList = Database.Watersheds
                    .Include(_=>_.Districts)
                    .Where(_ => newGuids.Contains(_.Guid))
                    .AsNoTracking().ToArray();
                RaisePropertyChanged(nameof(WatershedList));
                this.Changed = true;
            }
        }

        public ICommand WatershedRemoveSelectedCommand { get; set; }
        private void WatershedRemoveSelected()
        {
            if (SelectedWatersheds == null) return;
            if (SelectedWatersheds.Count == 0) return;

            WatershedList = WatershedList.Except(SelectedWatersheds).ToArray();
            RaisePropertyChanged(nameof(WatershedList));
            this.Changed = true;
        }







        public ICommand AddSpeciesCommand { get; set; }
        private void AddSpecies()
        {
            var selectBotanicalSpeciesControl = new Views.UserControls.SelectBotanicalSpeciesControl(null, null, Scoping.Region.RegionName);
            CustomControlWindow window = new CustomControlWindow(selectBotanicalSpeciesControl);
            if (window.DialogResult)
            {
                ApplySelectedSpecies(selectBotanicalSpeciesControl);
            }
        }


        public ICommand SpeciesFromWatershedMapCommand { get; set; }
        private void SpeciesFromWatershedMap()
        {
            MapDataPasser.SetActiveLayer("watersheds");
            MapDataPasser.MapSelectionMadeEvent += MapDataPasser_MapSelectionSpeciesEvent;
        }
        public ICommand SpeciesFromQuad75MapCommand { get; set; }
        private void SpeciesFromQuad75Map()
        {
            MapDataPasser.SetActiveLayer("quad75s");
            MapDataPasser.MapSelectionMadeEvent += MapDataPasser_MapSelectionSpeciesEvent;
        }

        private void MapDataPasser_MapSelectionSpeciesEvent(object? sender, EventArgs e)
        {
            MapDataPasser.MapSelectionMadeEvent -= MapDataPasser_MapSelectionSpeciesEvent;
            if (!MapDataPasser.ZoomLayerName.ToUpper().Contains("WATERSHED")
                && !MapDataPasser.ZoomLayerName.ToUpper().Contains("QUAD")) return;
            if (MessageBox.Show("Would you like to use this selection to select plant species?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;


            List<IFeature> features = (List<IFeature>)sender;
            Views.UserControls.SelectBotanicalSpeciesControl selectBotanicalSpeciesControl;
            if (MapDataPasser.ZoomLayerName.ToUpper().Contains("WATERSHED"))
            {
                var list = Database.Watersheds.Where(_ => features.Select(x => x.DataRow["guid"]).Cast<Guid>().Contains(_.Guid))
                    .AsNoTracking().ToList<object>();
                selectBotanicalSpeciesControl = new Views.UserControls.SelectBotanicalSpeciesControl(list, typeof(Watershed), "Master List");
            }
            else
            {
                var list = Database.Quad75s.Where(_ => features.Select(x => x.DataRow["guid"]).Cast<Guid>().Contains(_.Guid))
                    .AsNoTracking().ToList<object>();
                selectBotanicalSpeciesControl = new Views.UserControls.SelectBotanicalSpeciesControl(list, typeof(Quad75), "Master List");
            }
            CustomControlWindow window = new CustomControlWindow(selectBotanicalSpeciesControl);
            if (window.DialogResult)
            {
                ApplySelectedSpecies(selectBotanicalSpeciesControl);
            }
        }


        public ICommand SpeciesFromWatershedListCommand { get; set; }
        private void SpeciesFromWatershedList()
        {
            var selectBotanicalSpeciesControl = new Views.UserControls.SelectBotanicalSpeciesControl(WatershedList.Cast<object>().ToList(), typeof(Watershed), "Master List");
            CustomControlWindow window = new CustomControlWindow(selectBotanicalSpeciesControl);
            if (window.DialogResult)
            {
                ApplySelectedSpecies(selectBotanicalSpeciesControl);
            }
        }

        private void ApplySelectedSpecies(Views.UserControls.SelectBotanicalSpeciesControl selectBotanicalSpeciesControl)
        {
            bool replace = MessageBox.Show("Replace the current plant list?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            bool spiDesc = MessageBox.Show("Botanical scoping plant species use the CNDDB habitat description by default. Would you like the use the SPI habitat description?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

                Guid[] newGuids = selectBotanicalSpeciesControl.SpeciesSelections.Where(_ => _.Select).Select(_ => _.guid).ToArray();
            var species = Database.PlantSpecies.Where(_ => newGuids.Contains(_.Guid)).AsNoTracking().ToArray();
            List<BotanicalScopingSpecies> botanicalScopingSpecies = new List<BotanicalScopingSpecies>();
            foreach (var s in species)
            {
                if (!SpeciesList.Any(_ => _.PlantSpecies.Guid == s.Guid) || replace)
                {
                    var b = new BotanicalScopingSpecies()
                    {
                        PlantSpecies = s,
                        Exclude = false,
                        ExcludeReport = false,
                        HabitatDescription = s.GenHabitat,
                        NddbHabitatDescription = s.GenHabitat,
                        SpiHabitatDescription = s.SpiHabitat
                    };
                    if (spiDesc) b.HabitatDescription = b.SpiHabitatDescription;
                    b.ProtectionSummary = GetProtectionSummary(s);

                    botanicalScopingSpecies.Add(b);
                }
            }

            if (replace) SpeciesList = botanicalScopingSpecies.ToArray();
            else SpeciesList = SpeciesList.Concat(botanicalScopingSpecies).ToArray();

            RaisePropertyChanged(nameof(SpeciesList));
            this.Changed = true;
        }

        private string GetProtectionSummary(PlantSpecies plantSpecies)
        {
            if (WatershedList.Length > 0)
            {
                foreach(Watershed w in WatershedList)
                {
                    var ps = Database.PlantProtectionSummaries
                                .Include(_ => _.PlantSpecies)
                                .Include(_ => _.District)
                                .FirstOrDefault(_ => w.Districts.Contains(_.District) && _.PlantSpecies == plantSpecies);
                    if (ps != null) return ps.Summary;
                }                
            }

            if (Scoping.Region.RegionName != "Master List")
            {
                var ps = Database.PlantProtectionSummaries
                                .Include(_ => _.PlantSpecies)
                                .Include(_ => _.District)
                                .FirstOrDefault(_ => Scoping.Region.RegionName.Contains(_.District.DistrictName) && _.PlantSpecies == plantSpecies);
                if (ps != null) return ps.Summary;
            }

            if (WatershedList.Length > 0)
            {
                var ps = Database.PlantProtectionSummaries
                                .Include(_ => _.PlantSpecies)
                                .Include(_ => _.District)
                                .FirstOrDefault(_ => CurrentUser.Districts.Contains(_.District) && _.PlantSpecies == plantSpecies);
                if (ps != null) return ps.Summary;
            }

            if (WatershedList.Length > 0)
            {
                var ps = Database.PlantProtectionSummaries
                                .Include(_ => _.PlantSpecies)
                                .FirstOrDefault(_ => _.PlantSpecies == plantSpecies);
                if (ps != null) return ps.Summary;
            }


            return "";
        }


        public ICommand RemoveSelectedSpeciesCommand { get; set; }
        private void RemoveSelectedSpecies()
        {
            if (SelectedSpecies == null) return;
            if (SelectedSpecies.Count == 0) return;

            SpeciesList = SpeciesList.Except(SelectedSpecies).ToArray();
            RaisePropertyChanged(nameof(SpeciesList));
            this.Changed = true;
        }


        public ICommand FloweringTimelineCommand { get; set; }
        private void FloweringTimeline()
        {
            var floweringTimeline = new Views.UserControls.FloweringTimelineControl(SpeciesList, ThpName, Scoping.ElevationMin, Scoping.ElevationMax);
            CustomControlWindow window = new CustomControlWindow(floweringTimeline);
            if (window.DialogResult)
            {
            }
        }
    }
}
