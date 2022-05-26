using DevExpress.Mvvm;
using System.Linq;
using System.Windows;
using System;
using System.Windows.Media.Imaging;
using System.IO;
using DevExpress.Xpf.LayoutControl;
using WBIS_2.DataModel;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WBIS_2.Modules.ViewModels
{
    public class SetRequiredPassesViewModel : BindableBase
    {
        WBIS2Model Database = new WBIS2Model();
      Hex160[] Hex160s { get; set; }
        public SetRequiredPassesViewModel(Hex160[] hex160s)
        {
            Hex160s = hex160s;
        }

        [Required, Range(0,999)]
        public int RequiredPasses { get; set; } = 0;
        public BirdSpecies[] AvailibleSpecies => Database.BirdSpecies.Where(_ => _.IsSurveyable).ToArray();
        [Required]
        public BirdSpecies SelectedSpecies { get; set; }

        public bool SetPasses()
        {
            if (!InitialCheck()) return false;
            var replace = CheckExistingPasses();
            if (replace == MessageBoxResult.Cancel) return false;
           
            foreach(var h in Hex160s)
            {
                var hex160 = Database.Hex160s
                    .Include(_=>_.Districts)
                    .First(_=>_.Guid == h.Guid);
                var rp = Database.Hex160RequiredPasses
                           .Include(_ => _.Hex160)
                           .Include(_ => _.BirdSpecies)
                           .FirstOrDefault(_ => _.BirdSpecies == SelectedSpecies && _.Hex160 == hex160 && !_._delete && !_.Repository);
                if (rp != null && replace == MessageBoxResult.Yes)
                {
                    rp.RequiredPasses = RequiredPasses;
                }
                else
                {
                    rp = new Hex160RequiredPass()
                    {
                        Hex160 = hex160,
                        Districts = hex160.Districts,
                        BirdSpecies = SelectedSpecies,
                        Dropped = false,
                        _delete = false,
                        Repository = false,
                        RequiredPasses = RequiredPasses,
                        Geometry = hex160.Geometry
                    };
                    Database.Hex160RequiredPasses.Add(rp);
                }
            }
            Database.SaveChanges();

            return true;
        }
        private bool InitialCheck()
        {
            if (SelectedSpecies == null)
            {
                MessageBox.Show("There must be a species selected.");
                return false;
            }
            if (RequiredPasses <= 0)
            {
                MessageBox.Show("Required passes must be greater than 0.");
                return false;
            }
            return true;
        }
        private MessageBoxResult CheckExistingPasses()
        {
            var z = Database.Hex160RequiredPasses
                           .Include(_ => _.Hex160)
                           .Include(_ => _.BirdSpecies)
                           .Where(_ => _.BirdSpecies == SelectedSpecies && !_._delete && !_.Repository)
                           .Select(_ => _.Hex160);
            if (z.Any(_ => Hex160s.Contains(_)))
            {
               return MessageBox.Show($"Some of the selected hexagons already have required passes for {SelectedSpecies.Species}." +
                   $" Would you like to replace these passes?","",MessageBoxButton.YesNoCancel);
            }
            else return MessageBoxResult.Yes;
        }
    }
}