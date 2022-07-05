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
    public class WildlifeSpeciesViewModel : DetailViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => true;
        public WildlifeSpecies CurrentSpecies
        {
            get { return GetProperty(() => CurrentSpecies); }
            set
            {
                SetProperty(() => CurrentSpecies, value);
                Record = CurrentSpecies;
            }
        }
        public object Title => $"{CurrentSpecies.AlphaCode}{Changed}";


        [Required(AllowEmptyStrings = false), StringLength(1000, MinimumLength = 4)]
        public string AlphaCode { get; set; }


        public static WildlifeSpeciesViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new WildlifeSpeciesViewModel(guid));
        }

        public WildlifeSpeciesViewModel(Guid guid)
        {
            CurrentSpecies = Database.WildlifeSpecies
                .FirstOrDefault(_ => _.Id == guid);

            if (CurrentSpecies != null)
            {
                AlphaCode = CurrentSpecies.AlphaCode;
            }
            else
            {
                CurrentSpecies = new WildlifeSpecies();
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

            if (Database.WildlifeSpecies.Any(_ => _.AlphaCode.ToUpper().Trim() == AlphaCode.ToUpper().Trim() && _.Id != CurrentSpecies.Id))
            {
                MessageBox.Show("A species with this name already exists.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();

            CurrentSpecies.AlphaCode = AlphaCode;

            if (!Database.WildlifeSpecies.Contains(CurrentSpecies))
                Database.WildlifeSpecies.Add(CurrentSpecies);
            else Database.WildlifeSpecies.Update(CurrentSpecies);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }

        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
            //throw new NotImplementedException();
        }

        public string[] Classes => Database.WildlifeSpecies.Select(_ => _.Class).Where(_ => _ != "").Distinct().ToArray();
        public string[] Orders => Database.WildlifeSpecies.Select(_ => _.Order).Where(_ => _ != "").Distinct().ToArray();
        public string[] Families => Database.WildlifeSpecies.Select(_ => _.Family).Where(_ => _ != "").Distinct().ToArray();
        public string[] Genuses => Database.WildlifeSpecies.Select(_ => _.Genus).Where(_ => _ != "").Distinct().ToArray();
        public string[] Species => Database.WildlifeSpecies.Select(_ => _.Species).Where(_ => _ != "").Distinct().ToArray();
        public string[] SubSpecies => Database.WildlifeSpecies.Select(_ => _.SubSpecies).Where(_ => _ != "").Distinct().ToArray();
    }
}
