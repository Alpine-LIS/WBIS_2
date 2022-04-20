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
    public abstract class RecordImporterBase : BindableBase
    {
        public List<string> GetProperties(Type InfoType, bool requiredFields)
        {
            List<string> properties = new List<string>();
            var props = InfoType.GetProperties();
            //.Where(_ => _.GetCustomAttributesData().Any(_ => _.GetType() == typeof(ImportAttribute)));
            foreach (var prop in props)
            {
                var attributes = prop.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType() == typeof(ImportAttribute))
                    {
                        if (((ImportAttribute)attribute).Required == requiredFields)
                            properties.Add(prop.Name);
                        break;
                    }
                }
            }
            return properties;
        }


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
        public bool CheckSave()
        {
            if (ImportShapefile == null) return false;
            return PropertyCrosswalk.Select(x => x.Property).Intersect(RequiredFields).Count() == RequiredFields.Count();
        }



        public abstract List<string> RequiredFields { get; }
        public string RequiredFieldsList => string.Join('\n', RequiredFields);
        public abstract List<string> OptionalFields { get; }
        public List<string> AvailibleFields => GetSortedList();
        public List<PropertyCrosswalk> PropertyCrosswalk { get;set;}
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
        private void BuildAttributeTable()
        {
            PropertyCrosswalk = new List<PropertyCrosswalk>();
            List<string> attributes = new List<string>();
            if (ImportShapefile != null)
            {
                foreach (DataColumn col in ImportShapefile.DataTable.Columns)
                {
                    PropertyCrosswalk.Add(new RecordImporters.PropertyCrosswalk()
                    {
                        Attribute = col.ColumnName,
                        DataType = col.DataType.Name,
                        AvailibleFields = AvailibleFields
                    });
                }
            }
            IdOptions = attributes;
            RaisePropertiesChanged(nameof(IdOptions));
            RaisePropertiesChanged(nameof(PropertyCrosswalk));
        }





        public List<string> IdOptions { get; set; }
        public string IdAttribute { get; set; }
        public bool ConnectSpacially { get; set; }
        public bool ConnectId { get; set; }
        public ICommand CloseCommand { get; set; }
        public void CloseClick()
        {
            Holder.RemoveImportControl(this);
        }





        public bool IsWbisProperty(Type property)
        {
            return property.Namespace == "WBIS_2.DataModel";
        }
    }
    public class PropertyCrosswalk
    {
        public string Attribute { get; set; }
        public string DataType { get; set; }
        public string Property { get; set; }
        public List<string> AvailibleFields { get; set; }
    }
}
