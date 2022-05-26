using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WBIS_2.Common;
using System.Collections.ObjectModel;
using System.Collections;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.Views.Wildlife;
using WBIS_2.Modules.ViewModels.Wildlife;
using System.IO;
using DevExpress.Mvvm.ModuleInjection;
using Npgsql;
using DevExpress.Mvvm.POCO;
using System.Windows.Controls;
using WBIS_2.Modules.Views.UserControls;

namespace WBIS_2.Modules.ViewModels
{

    [POCOViewModel]
    public abstract class ListViewModelBase : WBISViewModelBase, IMapNavigation
    {
        protected ListViewModelBase()
        {
            RefreshDataSource();

            //CurrentUser.CurrentUserChanged += CurrentUserChanged;

            SelectedItems = new ObservableCollection<IInformationType>();
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;

            ShowDetailsCommand = new DelegateCommand(ShowDetails);
            ShowChildrenCommand  = new DelegateCommand(ShowChildren);
            AddRecordCommand = new DelegateCommand(AddRecord);
            ImportRecordsCommand = new DelegateCommand(ImportRecordsExecute);
            DeleteRecordCommand = new DelegateCommand(DeleteRecords);
            RestoreRecordCommand = new DelegateCommand(RestoreRecords);
            RecordsRefreshCommand = new DelegateCommand(RecordsRefresh);

            ViewActiveCommand = new DelegateCommand(ViewActive);
            ClearActiveListCommand = new DelegateCommand(ClearActiveList);
        }

        public override void WBISViewModelBase_ViewModelRemoving(object? sender, ViewModelRemovingEventArgs e)
        {
            CurrentUser.AddRemoveInfoType(ListManager.DisplayName, false);
            base.WBISViewModelBase_ViewModelRemoving(sender, e);
        }

        public IInformationType CurrentRecord { get; set; }
        public EntityInstantFeedbackSource Records { get; set; }
        internal virtual void RefreshDataSource()
        {
            Records = new EntityInstantFeedbackSource
            {
                AreSourceRowsThreadSafe = true,
                KeyExpression = $"Guid",
            };
            Records.GetQueryable += Records_GetQueryable;
            Records.Refresh();
            RaisePropertyChanged(nameof(Records));
        }
        public abstract void Records_GetQueryable(object sender, GetQueryableEventArgs e);

        public override void CurrentUserChanged(object sender, EventArgs e)
        {
            RefreshDataSource();
            base.CurrentUserChanged(sender, e);
        }


        private bool _viewDeleted = CurrentUser.ViewDeleted;
        public bool ViewDeleted
        {
            get { return _viewDeleted; }
            set
            {
                if (_viewDeleted != value)
                {
                    _viewDeleted = value;
                    CurrentUser.ViewDeleted = value;
                    RefreshDataSource();
                }
            }
        }

        private bool _ViewRepository = CurrentUser.ViewRepository;
        public bool ViewRepository
        {
            get { return _ViewRepository; }
            set
            {
                if (_ViewRepository != value)
                {
                    _ViewRepository = value;
                    CurrentUser.ViewRepository = value;
                    RefreshDataSource();
                }
            }
        }


        public IInfoTypeManager ListManager
        {
            get
            {
                return GetProperty(() => ListManager);
            }
            set
            {
                SetProperty(() => ListManager, value);
                //if (ListManager == null) return;
                SetRecordOptions();
            }
        }

        public string TableName => ListManager == null? "" : ListManager.DisplayName;


        private void SetRecordOptions()
        {
            if (ListManager == null) return;

            DetailsViewModel = Type.GetType("WBIS_2.Modules.ViewModels." + ListManager.DisplayName.Replace(" ", "") + "ViewModel");
            ShowDetailsEnabled = DetailsViewModel != null;
            ShowChildrenEnabled = ListManager.AvailibleChildren.Count() > 0;
            if (DetailsViewModel != null)
                AddSingleRecord = (bool)DetailsViewModel.GetProperty("AddSingle").GetValue(null);

            ImportView = Type.GetType("WBIS_2.Modules.Views.RecordImporters." + ListManager.DisplayName.Replace(" ", "") + "ImportView");
            ImportRecords = ImportView != null;

            ActiveUnitMenuVisable = ListManager.CanSetActive;
            DeleteRestoreRecordsEnabled = ListManager.DeleteRestoreRecord;

            RaisePropertyChanged(nameof(ActiveUnitMenuVisable));
            RaisePropertyChanged(nameof(ImportRecords));
            RaisePropertyChanged(nameof(AddSingleRecord));
            RaisePropertyChanged(nameof(DeleteRestoreRecordsEnabled));
            RaisePropertyChanged(nameof(ShowDetailsEnabled));
            RaisePropertyChanged(nameof(ShowChildrenEnabled));

            if (ListManager.CanSetActive && CurrentUser.AutoFilterActiveUnits) ViewActive();

            AddRequiredPassesAvailible = ListManager.InformationType == typeof(Hex160);
            RaisePropertyChanged(nameof(AddRequiredPassesAvailible));
        }



        public event EventHandler ViewActiveEvent;
        public bool ActiveUnitMenuVisable { get; set; }
        public ICommand ViewActiveCommand { get; set; }
        public void ViewActive()
        {
            ViewActiveEvent?.Invoke(new object(), new EventArgs()); 
        }
        public ICommand ClearActiveListCommand { get; set; }
        public void ClearActiveList()
        {
            RemoveActiveUnits(true);
            Records.Refresh();
        }


        public bool AddSingleRecord { get; set; } = false;
        public ICommand AddRecordCommand { get; set; }
        public void AddRecord()
        {
            AddSingle();
        }
        public bool ImportRecords { get; set; } = false;
        public ICommand ImportRecordsCommand { get; set; }
        public Type ImportView { get; set; }
        public void ImportRecordsExecute()
        {
            Views.RecordImporters.RecordImportHolderView recordImportHolderView = new Views.RecordImporters.RecordImportHolderView((UserControl)Activator.CreateInstance(ImportView));
            CustomControlWindow window = new CustomControlWindow(recordImportHolderView);
            if (window.DialogResult)
            {
                RefreshDataSource();
                RaisePropertyChanged(nameof(Records));
            }
        }


       

        public bool DeleteRestoreRecordsEnabled { get; set; }
        public ICommand DeleteRecordCommand { get; set; }
        public void DeleteRecords()
        {
            if (SelectedItems.Count == 0) return;
            var children = ListManager.AvailibleChildren
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(IUserRecords)))
                    .Select(_ => _.Manager.DisplayName);
            if (children.Count() > 0)
            {                
                if (MessageBox.Show($"Are you sure you want to delete {SelectedItems.Count.ToString("N0")} records?" +
                  $"\n\nThis will also delete all related children " +
                  $"({string.Join(", ", children)}" +
                  $" and any potential sub-children.", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            }
            else
            { if (MessageBox.Show($"Are you sure you want to delete {SelectedItems.Count.ToString("N0")} records?", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return; }

            new DeleteRestoreAndRepository(SelectedItems.ToArray(), "_delete").DeleteRecords();
        }

        public ICommand RestoreRecordCommand { get; set; }
        public void RestoreRecords()
        {
            if (SelectedItems.Count == 0) return;
            var children = ListManager.PossibleParents
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(IUserRecords)))
                    .Select(_ => _.Manager.DisplayName);
            if (children.Count() > 0)
            {
                if (MessageBox.Show($"Are you sure you want to restore {SelectedItems.Count.ToString("N0")} records?" +
                  $"\n\nThis will also restore potential parents " +
                  $"({string.Join(", ", children)}.", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            }
            else
            { if (MessageBox.Show($"Are you sure you want to restore {SelectedItems.Count.ToString("N0")} records?", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return; }

            new DeleteRestoreAndRepository(SelectedItems.ToArray(),"_delete").RestoreRecords();
        }







        public void RepositoryStoreRecords()
        {
            if (SelectedItems.Count == 0) return;
            var children = ListManager.AvailibleChildren
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(IUserRecords)))
                    .Select(_ => _.Manager.DisplayName);
            if (children.Count() > 0)
            {
                if (MessageBox.Show($"Are you sure you want to store {SelectedItems.Count.ToString("N0")} records to the repository?" +
                  $"\n\nThis will also store all related children " +
                  $"({string.Join(", ", children)}" +
                  $" and any potential sub-children.", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            }
            else
            { if (MessageBox.Show($"Are you sure you want to store {SelectedItems.Count.ToString("N0")} records to the repository?", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return; }

            new DeleteRestoreAndRepository(SelectedItems.ToArray(), "Repository").DeleteRecords();
        }
        public void RepositoryReviveRecords()
        {
            if (SelectedItems.Count == 0) return;
            var children = ListManager.PossibleParents
                    .Where(_ => _.Manager.InformationType.GetInterfaces().Contains(typeof(IUserRecords)))
                    .Select(_ => _.Manager.DisplayName);
            if (children.Count() > 0)
            {
                if (MessageBox.Show($"Are you sure you want to revive {SelectedItems.Count.ToString("N0")} records from the repository?" +
                  $"\n\nThis will also revive potential parents " +
                  $"({string.Join(", ", children)}.", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            }
            else
            { if (MessageBox.Show($"Are you sure you want to revive {SelectedItems.Count.ToString("N0")} records from the repository?", "", MessageBoxButton.YesNo) == MessageBoxResult.No) return; }

            new DeleteRestoreAndRepository(SelectedItems.ToArray(), "Repository").RestoreRecords();
        }








        public ICommand RecordsRefreshCommand { get; set; }
        public void RecordsRefresh()
        {
            Records.Refresh();
            RaisePropertyChanged(nameof(Records));
        }









        public bool ShowChildrenEnabled { get; set; }
        public ICommand ShowChildrenCommand { get; set; }
        public void ShowChildren()
        {
            if (SelectedItems.Count == 0)
            {
                return;
            }

            if (ListManager.AvailibleChildren.Count() > 0)
            {
                IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
                IDocument document = service.FindDocumentById(ListManager.DisplayName + " Children");
                if (document == null)
                {
                    ChildrenListViewModel ChildrenView = ChildrenListViewModel.Create(SelectedItems.ToArray(), (IInformationType)Activator.CreateInstance(ListManager.InformationType));

                    document = service.CreateDocument("ChildrenListView", ChildrenView, ListManager.DisplayName + " Children", this);
                    document.Id = ListManager.DisplayName + " Children";
                }
                document.Show();
            }
        }




        public Type DetailsViewModel { get; set; }
        public ICommand ShowDetailsCommand { get; set; }
        public bool ShowDetailsEnabled { get; set; }
        public void ShowDetails()
        {
            if (CurrentRecord == null) return;
            if (DetailsViewModel == null)
            {
                ShowChildren();
                return;
            }

            if (SelectedItems.Count > 1)
            {
                ShowChildren();
                return;
            }

            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocumentById(CurrentRecord.Guid);
            if (document == null)
            {
                var viewModel = DetailsViewModel.GetMethod("Create").Invoke(new object(), new object[] { CurrentRecord.Guid });
                if (viewModel is BotanicalElementViewModel vm)
                    document = service.CreateDocument(vm.ViewName, vm.ViewModel.GetMethod("Create").Invoke(new object(), new object[] { CurrentRecord.Guid }), CurrentRecord.Guid, this);
                else
                    document = service.CreateDocument(ListManager.DisplayName.Replace(" ", "") + "View", viewModel, CurrentRecord.Guid, this);
                document.Id = CurrentRecord.Guid;
            }
            document.Show();
        }
        private void AddSingle()
        {
            if (DetailsViewModel == null) return;

            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            Guid newGuid = Guid.NewGuid();
            IDocument document = service.FindDocumentById(newGuid);
            if (document == null)
            {
                var viewModel = DetailsViewModel.GetMethod("Create").Invoke(new object(), new object[] { newGuid });
                document = service.CreateDocument(ListManager.DisplayName.Replace(" ", "") + "View", viewModel, newGuid, this);
                document.Id = newGuid;
            }
            document.Show();
        }

        public bool ToggleAutoZoom { get; set; } = true;


        public ObservableCollection<IInformationType> SelectedItems { get; set; }
        private void SelectedItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectionChanged();
        }
        public abstract void SelectionChanged();


        public ICommand ExportToXlsxCommand { get; set; }
        public event EventHandler ExportToXlsxEvent;
        public void ExportToXlsxClick()
        {
            ExportToXlsxEvent?.Invoke(new object(), new EventArgs());
        }

        public ICommand FilterFromGridSelectionCommand { get; set; }
              
        public event EventHandler FilterFromGridSelection;
        private void FilterFromGridSelectionClick()
        {
            FilterFromGridSelection?.Invoke(new object(), new EventArgs());
        }



        public event EventHandler SaveGridLayoutEvent;
        public void SaveGridLayout()
        {
            //if (DontSaveLayout || Tracker.ChangesSaving) return;
            //SaveGridLayoutEvent?.Invoke(new object(), new EventArgs());
        }


        public string TableKeyField => "guid";

        public string LayerKeyField => "guid";

        public string LayerName => ListManager.GetLayerName();

        public void ZoomToLayer()
        {
            List<Guid> guids = new List<Guid>();
            if (ListManager.SubstituteLayer == null)
                guids = SelectedItems.Select(_ => _.Guid).ToList();
            else
            {
                var prop = ListManager.InformationType.GetProperties()
                    .FirstOrDefault(_ => _.PropertyType == ListManager.SubstituteLayer.SubLayer);
                if (prop == null)
                {
                    var props = ListManager.InformationType.GetProperties()
                        .Where(_ => _.PropertyType.GetGenericArguments().Count() > 0);
                    prop = props.FirstOrDefault(_ => _.PropertyType.GetGenericArguments().Single() == ListManager.SubstituteLayer.SubLayer);
                }
                    
                if (prop == null) return;
                foreach (var item in SelectedItems)
                {
                    var t = prop.GetValue(item);
                    if (t.GetType().GetInterfaces().Contains(typeof(IInformationType)))
                        guids.Add(((IInformationType)t).Guid);
                    else
                    {                        
                        foreach (IInformationType i in Enumerable.ToArray<IInformationType>((IEnumerable<IInformationType>)t))
                            guids.Add(i.Guid);
                    }
                }
            }
            MapDataPasser.ZoomToLayer(LayerName, LayerKeyField, guids, ToggleAutoZoom);
        }

        public void ZoomToFeature(IInformationType ZoomObject)
        {
            MapDataPasser.ZoomToFeature(LayerName, LayerKeyField, ZoomObject.Guid);
        }

        public void MapShowAFS(Dictionary<Guid, Guid> selection)
        {
            MapDataPasser.MapShowAFS(LayerName, LayerKeyField, selection.Values.Cast<Guid>().Distinct().ToList());
        }


        public void RemoveActiveUnits(bool removeAll)
        {
            WBIS2Model model = new WBIS2Model();
            var entityType = model.Model.FindEntityType(ListManager.InformationType);
            string tableName  = $"active_{entityType.GetTableName()}";

            Guid queryGuid = CurrentUser.User.Guid;
            if (CurrentUser.MobileUserActiveUnits)
                queryGuid = CurrentUser.MobileUserGuid;

            var conn = new NpgsqlConnection(WBIS2Model.GetRDSConnectionString());
            conn.Open();
            using (var cmd = new NpgsqlCommand("", conn))
            {
                using (var transaction = conn.BeginTransaction())
                {
                    if (removeAll)
                    {
                        cmd.CommandText = $"DELETE FROM \"{tableName}\" WHERE \"application_user_id\" = '{queryGuid}'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (IInformationType item in SelectedItems)
                        {
                            cmd.CommandText = $"DELETE FROM \"{tableName}\" WHERE \"application_user_id\" = '{queryGuid}' " +
                                $"AND \"unit_id\" = '{item.Guid}'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
            conn.Close();
        }

        public void AddAppendActiveUnits(bool newList)
        {
            WBIS2Model model = new WBIS2Model();
            var entityType = model.Model.FindEntityType(ListManager.InformationType);
            string tableName = $"active_{entityType.GetTableName()}";

            Guid queryGuid = CurrentUser.User.Guid;
            if (CurrentUser.MobileUserActiveUnits)
                queryGuid = CurrentUser.MobileUserGuid;

            var conn = new NpgsqlConnection(WBIS2Model.GetRDSConnectionString());
            conn.Open();
            using (var cmd = new NpgsqlCommand("", conn))
            {
                using (var transaction = conn.BeginTransaction())
                {
                    if (newList)
                    {
                        cmd.CommandText = $"DELETE FROM \"{tableName}\" WHERE \"application_user_id\" = '{queryGuid}'";
                        cmd.ExecuteNonQuery();
                    }

                    var qualifiedTable = $"\"{tableName}\"";
                    var query_copy = $"COPY {qualifiedTable}(\"application_user_id\",\"unit_id\") FROM STDIN (FORMAT BINARY)";

                    using var writer = conn.BeginBinaryImport(query_copy);
                    foreach (IInformationType item in SelectedItems)
                    {
                        writer.StartRow();
                        writer.Write(queryGuid);
                        writer.Write(item.Guid);
                    }
                    writer.Complete();
                    writer.Close();
                    transaction.Commit();
                }
            }
            conn.Close();
        }


        public bool AddRequiredPassesAvailible { get; set; } = false;
        public ICommand AddRequiredPassesCommand => new DelegateCommand(AddRequiredPassesClick);
        private void AddRequiredPassesClick()
        {
            if (SelectedItems.Count == 0)
            {
                MessageBox.Show("There must be records selected.");
                return;
            }
            SetRequiredPassesControl SetRequiredPassesControl = new SetRequiredPassesControl(SelectedItems.Cast<Hex160>().ToArray());
            CustomControlWindow window = new CustomControlWindow(SetRequiredPassesControl);
        }
    }
}

