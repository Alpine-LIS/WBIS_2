using Atlas.Data;
using DevExpress.Data.Linq;
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
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Diagnostics;

namespace WBIS_2.Modules.ViewModels
{
    public class BotanicalPlantListViewModel : DetailViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => false;
        public BotanicalElement element
        {
            get { return GetProperty(() => element); }
            set
            {
                SetProperty(() => element, value);
                Record = element;
            }
        }
        public BotanicalPlantList plantList
        {
            get { return GetProperty(() => plantList); }
            set
            {
                SetProperty(() => plantList, value);
            }
        }



        public object Title =>  $"{plantList.PlantSpecies.SpeciesCode}{ChangedSign}";


        public static BotanicalPlantListViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new BotanicalPlantListViewModel(guid));
        }

        public BotanicalPlantListViewModel(Guid guid)
        {
            element = Database.BotanicalElements
                .Include(_=>_.BotanicalPlantList).ThenInclude(_=>_.PlantSpecies)
                .Include(_=>_.User)
                .Include(_=>_.Pictures)
                .First(_ => _.Id == guid);

            plantList = element.BotanicalPlantList;

                PlantSpecies = Database.PlantSpecies.Where(_ => !_.PlaceHolder || _.Id == plantList.PlantSpecies.Id).ToArray();
            SciNames = PlantSpecies.Select(_ => _.ComName).Distinct().OrderBy(_ => _).ToArray();
            ComNames = PlantSpecies.Select(_ => _.ComName).Distinct().OrderBy(_ => _).ToArray();
            Families = PlantSpecies.Select(_ => _.Family).Distinct().OrderBy(_ => _).ToArray();

            if (plantList.PlantSpecies != null)
                SciName = plantList.PlantSpecies.SciName;            
        }


        public override void CloseForm()
        {
            throw new NotImplementedException();
        }

        public override void OnClose(CancelEventArgs e)
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

        
        public override void Save()
        {
            if (!this.Changed) return;

            if (HasErrors() || plantList.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

         
            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            var ps = Database.PlantSpecies.Include(_ => _.BotanicalPlantsOfInterest)
                .First(_ => _.SciName == SciName && _.ComName == ComName && _.Family == Family);
            plantList.PlantSpecies = ps;

           
                Database.BotanicalElements.Update(element);
                Database.BotanicalPlantsList.Update(plantList);
           

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

      

        PlantSpecies[] PlantSpecies = new PlantSpecies[0];
        [Required]
        public string SciName
        {
            get { return GetProperty(()=>SciName); }
            set 
            {
                if (SciName == value) return;
                SetProperty(()=>SciName, value);
                FillFromSciName();
            }
        }
        public string[] SciNames{get;set;}
        public string ComName
        {
            get { return GetProperty(() => ComName); }
            set
            {
                if (ComName == value) return;
                SetProperty(() => ComName, value);
                FillFromComName();
            }
        }
        public string[] ComNames { get; set; }
        public string Family
        {
            get { return GetProperty(() => Family); }
            set
            {
                if (Family == value) return;
                SetProperty(() => Family, value);
                FillFromFamily();
            }
        }
        public string[] Families { get; set; }

        private void FillFromSciName()
        {
            if (SciName == null) return;
            var ps = PlantSpecies.FirstOrDefault(_ => _.SciName == SciName);
            if (ps == null)
            {
                ComName ="";
                Family = "";
            }
            else
            {
                ComName = ps.ComName;
                Family = ps.Family;
            }
            ComName = ps.ComName;
            Family = ps.Family;
            RaisePropertyChanged(nameof(ComNames));
            RaisePropertyChanged(nameof(Families));
        }
        private void FillFromComName()
        {
            if (SciName == null) return;
            var ps = PlantSpecies.FirstOrDefault(_ => _.ComName == ComName);
            if (ps == null)
            {
                SciName = "";
                Family = "";
            }
            else
            {
                SciName = ps.SciName;
                Family = ps.Family;
            }
            RaisePropertyChanged(nameof(SciNames));
            RaisePropertyChanged(nameof(Families));
        }
        private void FillFromFamily()
        {
            if (Family == null) return;
            SciNames = PlantSpecies.Where(_ => _.Family == Family).Select(_ => _.SciName).Distinct().OrderBy(_ => _).ToArray();
            ComNames = PlantSpecies.Where(_ => _.Family == Family).Select(_ => _.ComName).Distinct().OrderBy(_=>_).ToArray();
            RaisePropertyChanged(nameof(SciNames));
            RaisePropertyChanged(nameof(ComNames));
        }


        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public void OnDestroy()
        {
        }
    }
}
