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
using WBIS_2.Modules.Tools;
using System.Reflection;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DevExpress.Data.ODataLinq.Helpers;
using NetTopologySuite.Geometries;

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public abstract class RecordImporterBase : BindableBase
    {
        public WBIS2Model Database = new WBIS2Model();
        public List<PropertyType> GetProperties(Type InfoType)
        {
            List<PropertyType> properties = new List<PropertyType>();
            var props = InfoType.GetProperties();
            GetProperties(props, ref properties,"");

            return properties.OrderByDescending(_ => _.Required).ThenBy(_ => _.PropertyName).ToList();
        }

        private void GetProperties(PropertyInfo[] props, ref List<PropertyType> properties, string dir)
        {
            foreach (var prop in props)
            {
                var att = prop.GetCustomAttributes(true).FirstOrDefault(_ => _.GetType() == typeof(ImportAttribute));
                if (att == null) continue;

                if (prop.PropertyType.GetInterfaces().Contains(typeof(IInformationType)))
                {
                    var subProps = prop.PropertyType.GetProperties();
                    GetProperties(subProps, ref properties,$"{dir}{prop.PropertyType.Name}.");
                }
                else
                {
                    string typeName = GetDataTypeString(prop.PropertyType);
                    properties.Add(new PropertyType() { PropertyName = $"{dir}{prop.Name}", TypeName = typeName, Required = ((ImportAttribute)att).Required });
                }
            }
        }


        public RecordImporterBase()
        {
            FileSelectCommand = new DelegateCommand(FileSelectClick);
            SaveCommand = new DelegateCommand(SaveClick);
            CloseCommand = new DelegateCommand(CloseClick);
            SaveSetupCommand = new DelegateCommand(SaveSetupClick);
            LoadSetupCommand = new DelegateCommand(LoadSetupClick);
        }
        public RecordImportHolderViewModel Holder { get; set; }


        public ICommand FileSelectCommand { get; set; }
        public abstract void FileSelectClick();

        public ICommand SaveCommand { get; set; }
        public abstract void SaveClick();
        public virtual void BuildAttributes<t>(ref t record, DataRow dataRow) where t : class
        {
            var attributes = PropertyCrosswalk.Where(_ => _.PropertyType != null);
            attributes = attributes.Where(_ => !_.PropertyType.PropertyName.Contains("."));

            foreach (var attribute in attributes)
            {
                var prop = typeof(SPI_GGOW).GetProperty(attribute.PropertyType.PropertyName);
                var val = ValueProcessors.GetParseValue(dataRow[attribute.Attribute], prop.PropertyType);
                prop.SetValue(record, val);
            }
        }

        /// <summary>
        /// Perfom chacks to see if records can be imported.
        /// </summary>
        public bool CheckSave()
        {
            if (ImportDataTable == null)
            {
                MessageBox.Show("There must be a file selected.");
                return false;
            }

            var missingFields = GetUnusedFields().Where(_ => _.Required).ToArray();
            if (missingFields.Length > 0)
            {
                string missingText = string.Join("\n", missingFields.Select(_ => _.PropertyName));
                MessageBox.Show($"All required fields must be used.\n{missingText}");
                return false;
            }

            int updateCount = GetUpdateCount();
            if (updateCount > 0)
            {
                if (MessageBox.Show($"The selected shapefile will update {updateCount.ToString("N0")} features. Do you wish to continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return false;
            }

            if (!CheckTpes())
            {
                if (MessageBox.Show("Not all attribute types match." +
                    "\nThe system will attempt to parse data to the correct type but can not garentee the accuracy of the transition. Would you like to continue? ", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return false;
            }


            var issues = RecordTypeSaveCheck();
            if (issues.Count > 0)
            {
                if (MessageBox.Show("There are issues with the import. Would you like to export a list of issues?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TextWriters.WriteTextList("TXT|*.txt", "-", issues);
                }
                return false;
            }

            return true;
        }
        /// <summary>
        /// Perfom checks specific to record types and return a list of issues
        /// </summary>
        public abstract List<string> RecordTypeSaveCheck();



        public abstract List<PropertyType> AvailibleFields { get; }
        public string RequiredFieldsList => string.Join('\n', AvailibleFields.Where(_ => _.Required).Select(_ => _.PropertyName));
        public List<PropertyType> GetUnusedFields() => AvailibleFields.Where(_ => !PropertyCrosswalk
            .Where(z => z.PropertyType != null)
            .Select(z => z.PropertyType).Any(z => z.PropertyName == _.PropertyName))
            .ToList();
        public List<PropertyCrosswalk> PropertyCrosswalk { get; set; }
        public bool OverwriteValues { get; set; } = false;


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

        public Shapefile ImportShapefile
        {
            get
            {
                return GetProperty(() => ImportShapefile);
            }
            set
            {
                SetProperty(() => ImportShapefile, value);
                if (ImportShapefile == null) ImportDataTable = null;
                else ImportDataTable = ImportShapefile.DataTable;
            }
        }
        public DataTable ImportDataTable
        {
            get
            {
                return GetProperty(() => ImportDataTable);
            }
            set
            {
                SetProperty(() => ImportDataTable, value);
                BuildAttributeTable();
            }
        }

        private void BuildAttributeTable()
        {
            PropertyCrosswalk = new List<PropertyCrosswalk>();
            List<string> attributes = new List<string>();
            if (ImportDataTable != null)
            {
                foreach (DataColumn col in ImportDataTable.Columns)
                {
                    var xWalk = new RecordImporters.PropertyCrosswalk()
                    {
                        Attribute = col.ColumnName,
                        DataType = GetDataTypeString(col.DataType),
                        AvailibleFields = AvailibleFields
                    };
                    xWalk.RefreshAvailible += XWalk_RefreshAvailible;
                    PropertyCrosswalk.Add(xWalk);
                }
            }
            IdOptions = attributes;
            RaisePropertiesChanged(nameof(IdOptions));
            RaisePropertiesChanged(nameof(PropertyCrosswalk));
        }

        private void XWalk_RefreshAvailible(object sender, EventArgs e)
        {
            foreach (var xWalk in PropertyCrosswalk)
            {
                xWalk.AvailibleFields = GetUnusedFields();
                RaisePropertyChanged(nameof(xWalk.AvailibleFields));
            }
        }
        private string GetDataTypeString(Type type)
        {
            if (type == typeof(int) || type == typeof(long) || type == typeof(double))
                return "Numeric";
            else return type.Name;
        }

        public bool RepositoryData { get; set; } = false;       

        public List<string> IdOptions { get; set; }
        public string IdAttribute { get; set; }
        public ICommand CloseCommand { get; set; }
        public void CloseClick()
        {
            Holder.RemoveImportControl(this);
        }

        /// <summary>
        /// Check that properties that can only accept certain values are offered the correct values.
        /// </summary>
        public List<string> CheckAvailibleOptions(Type type)
        {
            List<string> issues = new List<string>();
            var choosenAttributes = PropertyCrosswalk.Where(_ => _.PropertyType != null).ToList();
            foreach (var xWalk in choosenAttributes)
            {
                var p = type.GetProperty(xWalk.PropertyType.PropertyName);
                if (p != null)
                {
                    object? options = p.GetValue(null, null);
                    if (options != null)
                    {
                        if (!CheckAvailibleOptions((string[])options, xWalk.PropertyType.PropertyName, xWalk.Attribute))
                        {
                            issues.Add($"'{xWalk.Attribute}' contains invalid values for '{xWalk.PropertyType.PropertyName}'. Valid values are \n\t" +
                                string.Join("\n\t", (string[])options));
                        }
                    }
                }
            }
            return issues;
        }
        public bool CheckAvailibleOptions(string[] options, string propertyName, string attributeName)
        {
            foreach (DataRow r in ImportDataTable.Rows)
            {
                if (r[attributeName] is DBNull) return false;
                if (!options.Select(_ => _.ToUpperInvariant()).Contains(r[attributeName].ToString().ToUpper())) return false;
            }
            return true;
        }

        /// <summary>
        /// Check that the data types between the selected atributes and properties are the same.
        /// The type should be the information type
        /// </summary>
        //public List<string> CheckTpes(Type type)
        //{
        //    List<string> issues = new List<string>();
        //    var choosenAttributes = PropertyCrosswalk.Where(_ => _.PropertyType != null).ToList();
        //    foreach (var xWalk in choosenAttributes)
        //    {
        //        var p = type.GetProperty(xWalk.PropertyType.PropertyName);

        //        if (xWalk.PropertyType.TypeName != "'String'")
        //        {
        //            if (!xWalk.PropertyType.TypeName.Contains(xWalk.DataType))
        //                issues.Add($"'{xWalk.Attribute}' has a data type of '{xWalk.DataType}' and '{xWalk.PropertyType.PropertyName}' must be {xWalk.PropertyType.TypeName}.");
        //        }
        //    }
        //    return issues;
        //}

        public bool CheckTpes()
        {
            List<string> issues = new List<string>();
            var choosenAttributes = PropertyCrosswalk.Where(_ => _.PropertyType != null).ToList();
            foreach (var xWalk in choosenAttributes)
            {
                if (xWalk.PropertyType.TypeName != "'String'")
                {
                    if (!xWalk.PropertyType.TypeName.Contains(xWalk.DataType)) return false;
                }
            }
            return true; ;
        }


        public abstract int GetUpdateCount();
        public abstract string CheckDupIds();
           
        public ICommand SaveSetupCommand { get; set; }
        public void SaveSetupClick()
        {
            if (ImportDataTable == null)
            {
                MessageBox.Show("No file has been selected.");
                return;
            }

            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}ImportSetup";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = path;
            sfd.Filter = "CSV|*.csv";
            if (!sfd.ShowDialog().Value) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName))
            {
                sw.WriteLine(("Property,Attribute"));
                foreach (var xWalk in PropertyCrosswalk.Where(_ => _.PropertyType != null))
                {
                    sw.WriteLine($"{xWalk.PropertyType.PropertyName},{xWalk.Attribute}");
                }
            }
            MessageBox.Show("Setup has been saved.");
        }
        public ICommand LoadSetupCommand { get; set; }
        public void LoadSetupClick()
        {
            if (ImportDataTable == null)
            {
                MessageBox.Show("No file has been selected.");
                return;
            }

            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}ImportSetup";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = path;
            ofd.Filter = "CSV|*.csv";
            if (!ofd.ShowDialog().Value) return;

            LoadSetup(ofd.FileName);
        }
        public void LoadSetup(string fileName)
        {
            foreach (var xWalk in PropertyCrosswalk)
            {
                xWalk.PropertyType = null;
                RaisePropertyChanged(nameof(xWalk.PropertyType));
            }

            using (StreamReader sr = new StreamReader(fileName))
            {
                string txt = sr.ReadToEnd();
                var lines = txt.Split('\n');
                for (int i = 1; i < lines.Length; i++)
                {
                    if (lines[i] == "") continue;
                    var vals = lines[i].Split(',');
                    var prop = vals[0].Replace("\r", "").Replace("\n", "");
                    var att = vals[1].Replace("\r", "").Replace("\n", "");

                    if (AvailibleFields.Any(_ => _.PropertyName == prop))
                    {
                        var xWalk = PropertyCrosswalk.FirstOrDefault(_ => _.Attribute == att);
                        if (xWalk != null)
                        {
                            xWalk.PropertyType = xWalk.AvailibleFields.First(_ => _.PropertyName == prop);
                            RaisePropertyChanged(nameof(xWalk.PropertyType));
                        }
                    }
                }
            }
        }




        public t BuildAttributes<t>(DataRow dataRow, t record) where t : class
        {
            var attributes = PropertyCrosswalk.Where(_ => _.PropertyType != null);

            foreach (var attribute in attributes)
            {
                PropertyInfo prop;
                if (!attribute.PropertyType.PropertyName.Contains("."))
                    prop = typeof(t).GetProperty(attribute.PropertyType.PropertyName);
                else
                {
                    prop = typeof(t).GetProperty(attribute.PropertyType.PropertyName);
                }

                object val;
                if (!prop.PropertyType.GetInterfaces().Contains(typeof(IInformationType)))
                    val = ValueProcessors.GetParseValue(dataRow[attribute.Attribute], prop.PropertyType);
                else
                    val = GetRecordEntity(prop.PropertyType, dataRow);

                prop.SetValue(record, val);
            }
            return record;
        }
        public object GetRecordEntity(Type propertyType, DataRow dataRow)
        {
            object returnVal = null;

            if (propertyType == typeof(ApplicationUser))
                returnVal = GetRecordEntity<ApplicationUser>(dataRow);
            else if (propertyType == typeof(BirdSpecies))
                returnVal = GetRecordEntity<BirdSpecies>(dataRow);
            else if (propertyType == typeof(WildlifeSpecies))
                returnVal = GetRecordEntity<WildlifeSpecies>(dataRow);
            else if (propertyType == typeof(AmphibianSpecies))
                returnVal = GetRecordEntity<AmphibianSpecies>(dataRow);
            else if (propertyType == typeof(PlantSpecies))
                returnVal = GetRecordEntity<PlantSpecies>(dataRow);
            else if (propertyType == typeof(THP_Area))
                returnVal = GetRecordEntityThpArea(dataRow);

            return returnVal;
        }

        public THP_Area GetRecordEntityThpArea(DataRow dataRow) 
        {
            string thpCol = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == "THP_Area.THPName").Attribute;
            string thpName = dataRow[thpCol].ToString();

            var thp = DbHelp.ThpExistance(Database, thpName);
            if (thp == null)
            {
                Expression<Func<THP_Area, bool>> a = _ => _.THPName == thpName;
                thp = GetHolderEntity<THP_Area>(a);
            }

            if (thp == null)
            {
                thp = new THP_Area()
                {
                    THPName = thpName
                };
                Database.THP_Areas.Add(thp);
                AddNewElement<THP_Area>(thp);
            }
            return thp;
        }

        public t GetRecordEntity<t>(DataRow dataRow) where t : class
        {
            Dictionary<PropertyInfo, object> propertyInfos = GetPropertyValues<t>(dataRow);
            var expression = GetEntityExpression<t>(propertyInfos);

            var returnVal = Holder.Database.Set<t>().FirstOrDefault(expression);

            if (returnVal == null)
                returnVal = GetHolderEntity<t>(expression);

            if (returnVal == null)
            {
                returnVal = Activator.CreateInstance<t>();

                if (returnVal is ApplicationUser returnVal2)
                    returnVal2.ApplicationGroup = Holder.Database.ApplicationGroups.FirstOrDefault(_ => _.GroupName == "Unknown");

                foreach (var valKey in propertyInfos)
                {
                    PropertyInfo pi = valKey.Key;
                    object val = valKey.Value;

                    pi.SetValue(returnVal, val);
                }
                if (typeof(t).GetInterfaces().Contains(typeof(IPlaceHolder)))
                    ((IPlaceHolder)returnVal).PlaceHolder = true;

                Holder.Database.Set<t>().Add(returnVal);
                AddNewElement<t>(returnVal);
            }
            return returnVal;
        }
        private Dictionary<PropertyInfo, object> GetPropertyValues<t>(DataRow dataRow)
        {
            Dictionary<PropertyInfo, object> propertyInfos = new Dictionary<PropertyInfo, object>();
            string className = typeof(t).Name;
            var propNames = PropertyCrosswalk.Where(_ => _.PropertyType != null).Where(_ => _.PropertyType.PropertyName.StartsWith(className));

            foreach (var prop in propNames)
            {
                PropertyInfo pi = typeof(t).GetProperty(prop.PropertyType.PropertyName.Replace($"{className}.", ""));
                string colName = PropertyCrosswalk.Where(_ => _.PropertyType != null).First(_ => _.PropertyType.PropertyName == prop.PropertyType.PropertyName).Attribute;
                propertyInfos.Add(pi, dataRow[colName]);
            }
            return propertyInfos;
        }
        public Expression<Func<t, bool>> GetEntityExpression<t>(Dictionary<PropertyInfo, object> propertyInfos)
        {
            Expression<Func<t, bool>> a;
            System.Linq.Expressions.Expression returnVal = null;
            foreach (var valKey in propertyInfos)
            {
                PropertyInfo pi = valKey.Key;
                object val = valKey.Value;
                a = _ => pi.GetValue(_) == val;

                if (returnVal == null)
                    returnVal = a;
                else
                    returnVal = System.Linq.Expressions.Expression.AndAlso(returnVal, a);
            }

            return returnVal as Expression<Func<t, bool>>;
        }
        public t? GetHolderEntity<t>(Expression<Func<t, bool>> expression)
        {
            if (Holder.NewListElements.ContainsKey(typeof(t)))
            {
                var EnityList = Holder.NewListElements[typeof(t)].Cast<t>();
                return EnityList.AsQueryable().FirstOrDefault(expression);
            }
            return default(t);
        }

        private void AddNewElement<t>(object newElement)
        {
            if (Holder.NewListElements.ContainsKey(typeof(t)))
                Holder.NewListElements.Add(typeof(t), new List<object>());
            Holder.NewListElements[typeof(t)].Add(newElement);
        }
        public abstract string HelperText { get; }
    }
    public class PropertyCrosswalk : BindableBase
    {
        public event EventHandler RefreshAvailible;

        public string Attribute { get; set; }
        public string DataType { get; set; }
        public PropertyType PropertyType
        {
            get
            {
                return GetProperty(() => PropertyType);
            }
            set
            {
                // if (value != null)
                SetProperty(() => PropertyType, value);
                RefreshAvailible?.Invoke(new object(), new EventArgs());
            }
        }
        public List<PropertyType> AvailibleFields
        {
            get
            {
                return GetProperty(() => AvailibleFields);
            }
            set
            {
                SetProperty(() => AvailibleFields, value);
            }
        }
    }
    public class PropertyType : BindableBase
    {
        public string PropertyName { get; set; }
        public string TypeName { get; set; }
        public bool Required
        {
            get
            {
                return GetProperty(() => Required);
            }
            set
            {
                SetProperty(() => Required, value);
                if (Required)
                    ShowRequired = FontWeights.Bold;
                else ShowRequired = FontWeights.Normal;
            }
        }
        public FontWeight ShowRequired { get; set; }
    }
}
