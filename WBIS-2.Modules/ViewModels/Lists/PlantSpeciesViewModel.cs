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
    public class PlantSpeciesViewModel : WBISViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => true;
        public PlantSpecies CurrentSpecies { get; set; }
        public object Title => $"{CurrentSpecies.SciName}";

       
        //Sci name outside of plant species because it needs to be required, but can be empty in the database
        [Required(AllowEmptyStrings = false), StringLength(1000, MinimumLength = 1)]
        public string SciName { get; set; }


        public static PlantSpeciesViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new PlantSpeciesViewModel(guid));
        }

        public PlantSpeciesViewModel(Guid guid)
        {
            CurrentSpecies = Database.PlantSpecies
                .Include(_ => _.FloweringTimelines)
                .Include(_ => _.RegionalPlantLists).ThenInclude(_=>_.Region)
                .Include(_ => _.PlantProtectionSummaries).ThenInclude(_=>_.District)
                .FirstOrDefault(_=>_.Guid == guid);

            if (CurrentSpecies != null)
            {
                FillProtectionSummaries();
                SciName = CurrentSpecies.SciName;
            }
            else
            {
                CurrentSpecies = new PlantSpecies() 
                {
                RegionalPlantLists = new List<RegionalPlantList>(),
                PlantProtectionSummaries = new List<PlantProtectionSummary>()
                };
                FillProtectionSummariesEmpty();
            }
            FillRegions();

            RaisePropertyChanged(nameof(ProtectionSummaries));
            RaisePropertyChanged(nameof(RegionSelections));
        }


        private void FillProtectionSummaries()
        {
            ProtectionSummaries = new PlantProtectionSummary[0];
            var blankProtection = CurrentSpecies.PlantProtectionSummaries.FirstOrDefault(_ => _.District == null);
            string protection = "";
            if (blankProtection != null)
                protection = blankProtection.Summary;
            ProtectionSummaries = ProtectionSummaries.Append(new PlantProtectionSummary()
            {
                District = null,
                Summary = protection
            }).ToArray();
            foreach (var d in Database.Districts)
            {
                blankProtection = CurrentSpecies.PlantProtectionSummaries.FirstOrDefault(_ => _.District == d);
                protection = "";
                if (blankProtection != null)
                    protection = blankProtection.Summary;
                ProtectionSummaries = ProtectionSummaries.Append(new PlantProtectionSummary()
                {
                    District = d,
                    Summary = protection
                }).ToArray();
            }
            RaisePropertyChanged(nameof(ProtectionSummaries));
        }
        private void FillProtectionSummariesEmpty()
        {
            ProtectionSummaries = new PlantProtectionSummary[0];
            ProtectionSummaries = ProtectionSummaries.Append(new PlantProtectionSummary()
            {
                District = null,
                Summary = ""
            }).ToArray();
            foreach (var d in Database.Districts)
            {
                ProtectionSummaries = ProtectionSummaries.Append(new PlantProtectionSummary()
                {
                    District = d,
                    Summary = ""
                }).ToArray();
            }
            RaisePropertyChanged(nameof(ProtectionSummaries));
        }

        private void FillRegions()
        {
            RegionSelections = new RegionSelection[0];
            foreach (var region in Database.Regions.Where(_=>_.RegionName != "Master List"))
            {
                RegionSelections = RegionSelections.Append(new RegionSelection()
                {
                    Region = region,
                    Select = CurrentSpecies.RegionalPlantLists.Select(_=>_.Region.Guid).Contains(region.Guid)
                }).ToArray();
            }
            RaisePropertyChanged(nameof(RegionSelections));
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

            if (HasErrors() || CurrentSpecies.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            if (Database.PlantSpecies.Any(_=>_.SciName.ToUpper().Trim() == SciName.ToUpper().Trim() && _.Guid != CurrentSpecies.Guid))
            {
                MessageBox.Show("A species with this name already exists.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            CurrentSpecies.SciName = SciName;

            if (!Database.PlantSpecies.Contains(CurrentSpecies))
                Database.PlantSpecies.Add(CurrentSpecies);
            else Database.PlantSpecies.Update(CurrentSpecies);

            Database.FloweringTimelines.RemoveRange(Database.FloweringTimelines.Include(_=>_.PlantSpecies).Where(_=>_.PlantSpecies.Guid == CurrentSpecies.Guid));
            Database.PlantProtectionSummaries.RemoveRange(Database.PlantProtectionSummaries.Include(_ => _.PlantSpecies).Where(_ => _.PlantSpecies.Guid == CurrentSpecies.Guid));
            Database.RegionalPlantLists.RemoveRange(Database.RegionalPlantLists.Include(_ => _.PlantSpecies).Where(_ => _.PlantSpecies.Guid == CurrentSpecies.Guid));

            Database.FloweringTimelines.Add(new FloweringTimeline()
            {
                PlantSpecies = CurrentSpecies,
                ActiveFrom = ActiveFrom,
                ActiveTo = ActiveTo
            });

            foreach(var rs in RegionSelections.Where(_=>_.Select))
            {
                Database.RegionalPlantLists.Add(new RegionalPlantList()
                {
                    Region = rs.Region,
                    PlantSpecies = CurrentSpecies
                });
            }

            foreach(var ps in ProtectionSummaries.Where(_=>_.Summary.Length > 0))
            {
                Database.PlantProtectionSummaries.Add(new PlantProtectionSummary()
                {
                    District = ps.District,
                    PlantSpecies = CurrentSpecies,
                    Summary = ps.Summary
                });
            }

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
            //throw new NotImplementedException();
        }



        public string ActiveFrom { get; set; } = "N/A";
        public string ActiveTo { get; set; } = "N/A";
        public string[] AbailibleMonths => new string[] 
        {
        "N/A",
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
        };

        public PlantProtectionSummary[] ProtectionSummaries { get; set; }

        public RegionSelection[] RegionSelections { get; set; }
        public class RegionSelection
        {
            public Region Region { get; set; }
            public bool Select { get; set; } = false;
        }




               
        public string[] Familys => Database.PlantSpecies.Select(_=>_.Family).Where(_=>_ != "").Distinct().ToArray();
        public string[] RPlantRanks => Database.PlantSpecies.Select(_ => _.RPlantRank).Where(_ => _ != "").Distinct().ToArray();
        public string[] FedLists => Database.PlantSpecies.Select(_ => _.FedList).Where(_ => _ != "").Distinct().ToArray();
        public string[] CalLists => Database.PlantSpecies.Select(_ => _.CalList).Where(_ => _ != "").Distinct().ToArray();
        public string[] GRanks => Database.PlantSpecies.Select(_ => _.GRank).Where(_ => _ != "").Distinct().ToArray();
        public string[] SRanks => Database.PlantSpecies.Select(_ => _.SRank).Where(_ => _ != "").Distinct().ToArray();
        public string[] TaxonGroups => Database.PlantSpecies.Select(_ => _.TaxonGroup).Where(_ => _ != "").Distinct().ToArray();
    }
}
