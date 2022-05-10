using Atlas.Data;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.ViewModels
{
    public class BotanicalScopingViewModel : WBISViewModelBase, IDocumentContent
    {
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
                SpeciesCount = $"Species: {SpeciesList.Count().ToString("N0")} " +
                    $"| Excluded: {SpeciesList.Count(_=>_.Exclude).ToString("N0")} " +
                    $"| Excluded from report: {SpeciesList.Count(_=>_.ExcludeReport).ToString("N0")}";
                RaisePropertyChanged(nameof(SpeciesCount));
            }
        }
        public string SpeciesCount { get; set; }
        public string SpeciesRank { get; set; } = "Rare Plant Rank:";

        public Watershed CurrentWatershed { get; set; }
        public Watershed[] WatershedList { get; set; }
        public BotanicalScoping Scoping { get; set; }
        public string[] ThpNames { get; set; }
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

            ThpNames = Database.THP_Areas.Select(_=>_.THPName).OrderBy(_=>_).ToArray();
                Foresters = Database.BotanicalScopings.Select(_=>_.Forester).Distinct().OrderBy(_ => _).ToArray();
            Regions = Database.Regions.ToArray();

            if (guid != Guid.Empty)
            {
                Scoping = Database.BotanicalScopings
                    .Include(_ => _.THP_Area)
                    .Include(_ => _.Region)
                    .Include(_ => _.Watersheds)
                    .First(_=>_.Guid == guid);

                WatershedList = Scoping.Watersheds.ToArray();
                SpeciesList = Database.BotanicalScopingSpecies
                    .Include(_=>_.PlantSpecies)
                    .Include(_=>_.BotanicalScoping).Where(_=>_.BotanicalScoping == Scoping).ToArray();
                if (Scoping.THP_Area != null) ThpName = Scoping.THP_Area.THPName;
            }
            else
            {
                Scoping = new BotanicalScoping();
                Scoping.Guid = Guid.Empty;
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
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
            //throw new NotImplementedException();
        }






        public ICommand AllowMapSelectionCommand { get; set; }
        private void AllowMapSelection()
        {
            MapDataPasser.MapSelectionMadeEvent += MapDataPasser_MapSelectionChangedEvent;
        }

        private void MapDataPasser_MapSelectionChangedEvent(object? sender, EventArgs e)
        {
            MapDataPasser.MapSelectionMadeEvent -= MapDataPasser_MapSelectionChangedEvent;
            if (!MapDataPasser.ZoomLayerName.ToUpper().Contains("WATERSHED")) return;
            if (MessageBox.Show("Would you like to use these watersheds for the scoping?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            List<IFeature> features = (List<IFeature>)sender;
            WatershedList = Database.Watersheds.Where(_=>features.Select(x=>x.DataRow["guid"]).Cast<Guid>().Contains(_.Guid)).ToArray();
            RaisePropertyChanged(nameof(WatershedList));
        }


        public ICommand WatershedListSelectionCommand { get; set; }
        private void WatershedListSelection()
        {
            
        }
    }
}
