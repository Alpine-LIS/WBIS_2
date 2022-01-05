using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WBIS_2.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace WBIS_2.Modules.ViewModels.Wildlife
{
    public class ManageRequiredPassesViewModel:BindableBase
    {
        protected WBIS2Model _database { get; set; }
        public WBIS2Model Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new WBIS2Model();
                }
                return _database;
            }
        }
        public BirdSpecies[] BirdSpecies { get { return Database.BirdSpecies.ToArray(); } }
        [Required]
        public BirdSpecies SelectedSpecies { get; set; } 

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0.")]
        public int RequiredPasses { get; set; } = 1;

        IList<Hex160> Hex160s { get; set; }
        public ManageRequiredPassesViewModel(IList<Hex160> hex160s)
        {
            AddEditPassesCommand = new DelegateCommand(AddEditPassesClick);
            RemovePassesCommand = new DelegateCommand(RemovePassesClick);
            Hex160s = hex160s;
            SelectedSpecies = BirdSpecies[0];
            RaisePropertyChanged(nameof(SelectedSpecies));
        }
        public ICommand AddEditPassesCommand { get; set; }
        public ICommand RemovePassesCommand { get; set; }

        public void AddEditPassesClick()
        {
            bool replace = false;

            var exisitngHexs = Database.Hex160RequiredPasses
                .Include(_ => _.Hex160)
                .Include(_ => _.BirdSpecies)
                .Where(_ => Hex160s.Contains(_.Hex160)
                    && _.BirdSpecies == SelectedSpecies).Select(_=>_.Hex160);
            if (exisitngHexs.Count() > 0)
            {
                replace = MessageBox.Show("Would you like to replace existing required passes for the same species with the new pass number?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            }

            foreach (Hex160 hex160 in Hex160s)
            {
                if (exisitngHexs.Contains(hex160))
                {
                    if (!replace) continue;

                    Hex160RequiredPass exisitingRequiredPass = Database.Hex160RequiredPasses
                        .Include(_ => _.Hex160)
                        .Include(_ => _.BirdSpecies)
                        .First(_ => _.Hex160 == hex160
                            && _.BirdSpecies == SelectedSpecies);
                    exisitingRequiredPass.RequiredPasses = RequiredPasses;
                    Database.Hex160RequiredPasses.Update(exisitingRequiredPass);
                }
                else
                {
                    Hex160RequiredPass hex160RequiredPass = new Hex160RequiredPass()
                    {
                        Hex160 = Database.Hex160s.Include(_ => _.SiteCallings).First(_ => _ == hex160),
                        BirdSpecies = SelectedSpecies,
                        RequiredPasses = RequiredPasses,
                        User = Database.ApplicationUsers.FirstOrDefault(_ => _ == CurrentUser.User)
                    };
                    hex160RequiredPass.CurrentPasses = 0;
                    Database.Hex160RequiredPasses.Add(hex160RequiredPass);
                }
            }
            Database.SaveChanges();
            MessageBox.Show("Operation complete.");
        }
        public void RemovePassesClick()
        {
            Hex160RequiredPass[] hex160RequiredPasses = Database.Hex160RequiredPasses
                .Include(_=>_.Hex160)
                .Include(_=>_.BirdSpecies)
                .Where(_ => Hex160s.Contains(_.Hex160) 
                    && _.BirdSpecies == SelectedSpecies 
                    && _.RequiredPasses == RequiredPasses).ToArray();
            foreach (Hex160RequiredPass hex160RequiredPass in hex160RequiredPasses)
                Database.Hex160RequiredPasses.Remove(hex160RequiredPass);
            Database.SaveChanges();
            MessageBox.Show("Operation complete.");
        }
    }
}
