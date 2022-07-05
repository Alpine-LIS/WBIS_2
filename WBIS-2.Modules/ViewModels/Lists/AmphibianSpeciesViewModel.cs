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
    public class AmphibianSpeciesViewModel : DetailViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => true;
        public AmphibianSpecies CurrentSpecies
        {
            get { return GetProperty(() => CurrentSpecies); }
            set
            {
                SetProperty(() => CurrentSpecies, value);
                Record = CurrentSpecies;
            }
        }
        public object Title => $"{CurrentSpecies.SpeciesName}{Changed}";

       
        [Required(AllowEmptyStrings = false), StringLength(1000, MinimumLength = 1)]
        public string SpeciesName { get; set; }


        public static AmphibianSpeciesViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new AmphibianSpeciesViewModel(guid));
        }

        public AmphibianSpeciesViewModel(Guid guid)
        {
            CurrentSpecies = Database.AmphibianSpecies
                .FirstOrDefault(_=>_.Id == guid);

            if (CurrentSpecies != null)
            {
                SpeciesName = CurrentSpecies.SpeciesName;
            }
            else
            {
                CurrentSpecies = new AmphibianSpecies() ;
                CurrentSpecies.Id = guid;
            }
        }
              
     

        public override void CloseForm()
        {
            throw new NotImplementedException();
        }
        public void OnDestroy()
        {
        }

        public override void Save()
        {
            if (!this.Changed) return;

            if (HasErrors() || CurrentSpecies.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            if (Database.AmphibianSpecies.Any(_=>_.SpeciesName.ToUpper().Trim() == SpeciesName.ToUpper().Trim() && _.Id != CurrentSpecies.Id))
            {
                MessageBox.Show("A species with this name already exists.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            CurrentSpecies.SpeciesName = SpeciesName;

            if (!Database.AmphibianSpecies.Contains(CurrentSpecies))
                Database.AmphibianSpecies.Add(CurrentSpecies);
            else Database.AmphibianSpecies.Update(CurrentSpecies);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
            //throw new NotImplementedException();
        }
    }
}
