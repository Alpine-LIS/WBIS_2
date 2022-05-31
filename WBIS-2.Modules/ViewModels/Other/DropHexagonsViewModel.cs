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
    public class DropHexagonsViewModel : BindableBase
    {
        WBIS2Model Database = new WBIS2Model();
      Hex160[] Hex160s { get; set; }
        public DropHexagonsViewModel(Hex160[] hex160s)
        {
            Hex160s = hex160s;
        }
        public BirdSpecies[] AvailibleSpecies => Database.BirdSpecies.Where(_ => _.IsSurveyable).ToArray();
        [Required]
        public BirdSpecies SelectedSpecies { get; set; }
        public string Comments { get; set; } = "DROPPED: Owls nesting within 3/4 mile radius.";
        public bool ParialDrop { get; set; } = false;

        [Required]
        public string SurveyType1 { get; set; }
        public string[] SurveyType1s => Database.DropdownOptions.Where(_ => _.Entity == "site_calling" && _.Property == "survey_type1").Select(_ => _.SelectionText).ToArray();
        [Required]
        public string SurveyType2 { get; set; }
        public string[] SurveyType2s => Database.DropdownOptions.Where(_=>_.Entity == "site_calling" && _.Property == "survey_type2").Select(_=>_.SelectionText).ToArray();


        public bool DropPasses()
        {
            if (!InitialCheck()) return false;

            string drop = "Drop";
            if (ParialDrop) drop = "Partial-Drop";


            foreach (var h in Hex160s)
            {
                var hex160 = Database.Hex160s
                    .Include(_=>_.Districts)
                    .First(_=>_.Guid == h.Guid);

                SiteCalling siteCalling = new SiteCalling()
                {
                    RecordType = drop,
                    SurveySpecies = SelectedSpecies,
                    Hex160 = hex160,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    SurveyType1 = SurveyType1,
                    SurveyType2 = SurveyType2,
                    Geometry = hex160.Geometry.Centroid,
                    SPOW_OccupancyStatus = "No owls"
                };
                Database.SiteCallings.Add(siteCalling);
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

            if (SurveyType1 == null)
            {
                MessageBox.Show("SurveyType1 must be selected.");
                return false;
            }
            if (SurveyType1 == "")
            {
                MessageBox.Show("SurveyType1 must be selected.");
                return false;
            }

            if (SurveyType2 == null)
            {
                MessageBox.Show("SurveyType2 must be selected.");
                return false;
            }
            if (SurveyType2  == "")
            {
                MessageBox.Show("SurveyType2 must be selected.");
                return false;
            }
            return true;
        }
    }
}