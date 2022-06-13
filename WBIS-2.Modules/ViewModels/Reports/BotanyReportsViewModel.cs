using Atlas.Data;
using Atlas.Projections;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;
using NetTopologySuite.Geometries;
using DevExpress.Mvvm.POCO;
using System.Windows.Input;
using System.IO.Compression;
using WBIS_2.Modules.Tools;
using Microsoft.EntityFrameworkCore;

namespace WBIS_2.Modules.ViewModels.Reports
{
    public class BotanyReportsViewModel : WBISViewModelBase, IDocumentContent
    {
        public object Title => $"Botanical Reports";
      
        public THP_Area[] ThpAreas=> Database.THP_Areas.OrderBy(_=>_.THPName).ToArray();
        public THP_Area SelectedThp
        {
            get { return GetProperty(() => SelectedThp); }
            set
            {
                SetProperty(() => SelectedThp, value);
                ScopingCount = Database.BotanicalScopings
                    .Include(_=>_.THP_Area)
                    .Where(_ => _.THP_Area == SelectedThp && !_._delete && !_.Repository).Count().ToString("N0");
                AreaCount = Database.BotanicalSurveyAreas
                    .Include(_ => _.THP_Area)
                    .Where(_ => _.THP_Area == SelectedThp && !_._delete && !_.Repository).Count().ToString("N0");
                SurveyCount = Database.BotanicalSurveys
                    .Include(_ => _.THP_Area)
                    .Where(_ => _.THP_Area == SelectedThp && !_._delete && !_.Repository).Count().ToString("N0");

                RaisePropertyChanged(nameof(ScopingCount));
                RaisePropertyChanged(nameof(AreaCount));
                RaisePropertyChanged(nameof(SurveyCount));

                ReportsAvailible = ScopingCount != "0";
                RaisePropertyChanged(nameof(ReportsAvailible));
            }
        }

        public bool ReportsAvailible { get; set; } = false;
        public string ScopingCount { get; set; }
        public string AreaCount { get; set; }
        public string SurveyCount { get; set; }

        public BotanyReportsViewModel()
        {

        }

        public static BotanyReportsViewModel Create()
        {
            return ViewModelSource.Create(() => new BotanyReportsViewModel()
            { Caption = "Botanical Reports", 
            });
        }

        public ICommand BotanicalScopingCommand => new DelegateCommand(BotanicalScopingClick);
        private void BotanicalScopingClick()
        {
            if (SelectedThp == null)
            {
                MessageBox.Show("There is no THP selected.");
                return;
            }
            new BotanicalScopingReport(SelectedThp);
        }

        public ICommand BotanicalSurveyCommand => new DelegateCommand(BotanicalSurveyClick);
        private void BotanicalSurveyClick()
        {
            if (SelectedThp == null)
            {
                MessageBox.Show("There is no THP selected.");
                return;
            }
            new BotanicalSurveyReport(SelectedThp);
        }

        public ICommand ThpBotanicalSurveyCommand => new DelegateCommand(ThpBotanicalSurveyClick);
        private void ThpBotanicalSurveyClick()
        {
            if (SelectedThp == null)
            {
                MessageBox.Show("There is no THP selected.");
                return;
            }
            new THPBotanicalSurveyReport(SelectedThp);
        }









        public override void Tracker_ChangesSaved(object sender, IEnumerable<EntityEntry> e)
        {
        }

        public override void CloseForm()
        {
        }

        public void OnDestroy()
        {
        }
    }   
}
