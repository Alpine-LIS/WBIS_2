using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.ViewModels.Wildlife
{
    public class ManageRequiredPassesViewModel
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


        public ManageRequiredPassesViewModel()
        {
            AddEditPassesCommand = new DelegateCommand(AddEditPassesClick);
            RemovePassesCommand = new DelegateCommand(RemovePassesClick);
        }
        public ICommand AddEditPassesCommand { get; set; }
        public ICommand RemovePassesCommand { get; set; }

        public void AddEditPassesClick()
        {

        }
        public void RemovePassesClick()
        {

        }
    }
}
