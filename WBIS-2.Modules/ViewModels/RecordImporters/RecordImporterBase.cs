using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBIS_2.DataModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Diagnostics;
using DevExpress.Mvvm;
using System.Windows.Input;
using Atlas.Data;

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public abstract class RecordImporterBase: BindableBase
    {
        public RecordImporterBase()
        {
            FileSelectCommand = new DelegateCommand(FileSelectClick);
            SaveCommand = new DelegateCommand(SaveClick);
            CloseCommand = new DelegateCommand(CloseClick);
        }
        public RecordImportHolderViewModel Holder { get; set; }


        public ICommand FileSelectCommand { get; set; }
        public abstract void FileSelectClick();

        public ICommand SaveCommand { get; set; }
        public abstract void SaveClick();
        public abstract bool CheckSave();



        public abstract List<string> RequiredFields { get; }
        public abstract List<string> OptionalFields { get; }
        public List<string> AvailibleFields => GetSortedList();
        private List<string> GetSortedList()
        {
            var returnVal = RequiredFields.Concat(OptionalFields).ToList();
            returnVal.Sort();
            return returnVal;
        }

        public bool RepositoryData { get; set; } = false;


        public bool IsSubElement
        {
            get
            {
                return GetProperty(() => IsSubElement);
            }
            set
            {
                SetProperty(() => IsSubElement, value);
                if (IsSubElement) IsSubElementVis = Visibility.Visible;
                else IsSubElementVis = Visibility.Hidden;
                RaisePropertyChanged(nameof(IsSubElementVis));
            }
        }
        public Visibility IsSubElementVis { get; set; } = Visibility.Hidden;
        public abstract IInformationType ReturnRecordId(string link);
        public abstract IInformationType ReturnRecordSpacial(Atlas.Data.Feature link);

        public Shapefile ImportShapefile
        {
            get
            {
                return GetProperty(() => ImportShapefile);
            }
            set
            {
                SetProperty(() => ImportShapefile, value);
                BuildAttributeTable();
            }
        }
        public DataTable AttributeTable { get; set; }
        private void BuildAttributeTable()
        { 
            AttributeTable = new DataTable();
            List<string> attributes = new List<string>();
            if (ImportShapefile != null)
            {
                AttributeTable.Columns.Add("Attribute", typeof(string));
                AttributeTable.Columns.Add("Type", typeof(string));
                AttributeTable.Columns.Add("Property", typeof(string));
                foreach (DataColumn col in ImportShapefile.DataTable.Columns)
                {
                    DataRow r = AttributeTable.NewRow();
                    r[0] = col.ColumnName;
                    attributes.Add(col.ColumnName);
                    r[1] = col.DataType.Name;
                    AttributeTable.Rows.Add(r);
                }
            }
            IdOptions = attributes;
            RaisePropertiesChanged(nameof(IdOptions));
            RaisePropertiesChanged(nameof(AttributeTable));
        }





    public List<string> IdOptions{ get; set; }
    public string IdAttribute { get; set; }
        public bool ConnectSpacially { get; set; }
        public bool ConnectId { get; set; }
public ICommand CloseCommand { get; set; }
        public void CloseClick()
        {
            Holder.RemoveImportControl(this);
        }








        /*
        Site Calling Shape
            Correct shape type
            Required fields
            Optional Fields
            Link To Track
            Link tp device info
            Link to detections

        Track
            Correct shape type

        DeviceInfo
            Correct shape type

        Detections
            Correct shape type
            Required fields
            Optional Fields
            Link to UserLocation

        UserLocation
            Correct shape type
        */
    }
}
