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

namespace WBIS_2.Modules.ViewModels.RecordImporters
{
    public abstract class RecordImporterBase : BindableBase
    {
        public List<PropertyType> GetProperties(Type InfoType)
        {
            List<PropertyType> properties = new List<PropertyType>();
            var props = InfoType.GetProperties();
            //.Where(_ => _.GetCustomAttributesData().Any(_ => _.GetType() == typeof(ImportAttribute)));
            foreach (var prop in props)
            {
                var att = prop.GetCustomAttributes(true).FirstOrDefault(_ => _.GetType() == typeof(ImportAttribute));
                if (att == null) continue;

                if (prop.PropertyType.GetInterfaces().Contains(typeof(IInformationType)))
                {
                    var subProps = prop.PropertyType.GetProperties();
                    foreach(var subProp in subProps)
                    {
                        var subAtt = subProp.GetCustomAttributes(true).FirstOrDefault(_ => _.GetType() == typeof(ImportAttribute));
                        if (subAtt == null) continue;

                        string typeName = GetDataTypeString(subProp.PropertyType);                        
                        properties.Add(new PropertyType() { PropertyName = $"{prop.PropertyType.Name}.{subProp.Name}", TypeName = typeName, Required = ((ImportAttribute)subAtt).Required, FullClassName = prop.PropertyType.FullName });
                    }
                }
                else
                {                   
                    string typeName = GetDataTypeString(prop.PropertyType);
                    properties.Add(new PropertyType() { PropertyName = prop.Name, TypeName = typeName, Required = ((ImportAttribute)att).Required });
                }


            }
            return properties.OrderByDescending(_ => _.Required).ThenBy(_ => _.PropertyName).ToList();
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
        public abstract void SaveClick(List<object> ExcludeIds);
        public abstract List<object> GenerateChild();
        public abstract object BuildAttributes(object unit, DataRow dataRow);

        /// <summary>
        /// Perfom checks to see if records can be imported.
        /// If record is a sub item don't produce a list of erros. This will be aquired through it's parent 'ListSaveCheck'
        /// </summary>
        public bool CheckSave()
        {
            if (ImportShapefile == null)
            {
                MessageBox.Show("There must be a shapefile selected.");
                return false;
            }
            if (IdAttribute == null)
            {
                MessageBox.Show("Please select an attribute to use as a linking field.");
                return false;
            }

            var missingFields = GetUnusedFields().Where(_ => _.Required).ToArray();
            if (missingFields.Length > 0)
            {
                string missingText = string.Join("\n", missingFields.Select(_ => _.PropertyName));
                MessageBox.Show($"All required fields must be used.\n{missingText}");
                return false;
            }

            BoolSaveCheck();
            if (!IsSubElement)
            {
                var issues = ListSaveCheck();
                if (issues.Count > 0)
                {
                    if (MessageBox.Show("There are issues with the import. Would you like to export a list of issues?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        TextWriters.WriteTextList("TXT|*.txt", "-", issues);
                    }
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Perfom checks specific to record types and return a list of issues.
        /// Also gets a list of issues for it's children
        /// </summary>
        public abstract List<string> ListSaveCheck();
        /// <summary>
        /// Used for the simple boolean element of the 'CheckSave' routine
        /// </summary>
        public abstract bool BoolSaveCheck();



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
            IdOptions = PropertyCrosswalk.Select(_=>_.Attribute).ToList();
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




        public List<string> IdOptions { get; set; }
        public string IdAttribute { get; set; }
        public bool ImportUnconnected { get; set; } = false;
        public bool ConnectSpacially { get; set; } = false;
        public bool ConnectId { get; set; } = true;
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
            foreach (DataRow r in ImportShapefile.DataTable.Rows)
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
        public List<string> CheckTpes(Type type)
        {
            List<string> issues = new List<string>();
            var choosenAttributes = PropertyCrosswalk.Where(_ => _.PropertyType != null).ToList();
            foreach (var xWalk in choosenAttributes)
            {
                var p = type.GetProperty(xWalk.PropertyType.PropertyName);

                if (xWalk.PropertyType.TypeName != "String")
                {
                    if (!xWalk.PropertyType.TypeName.Contains(xWalk.DataType))
                        issues.Add($"'{xWalk.Attribute}' has a data type of '{xWalk.DataType}' and '{xWalk.PropertyType.PropertyName}' must be {xWalk.PropertyType.TypeName}.");
                }
            }
            return issues;
        }
        

        public ICommand SaveSetupCommand { get; set; }
        public void SaveSetupClick()
        {
            if (ImportShapefile == null)
            {
                MessageBox.Show("No shapefile has been selected.");
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
            if (ImportShapefile == null)
            {
                MessageBox.Show("No shapefile has been selected.");
                return;
            }

            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}ImportSetup";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = path;
            ofd.Filter = "CSV|*.csv";
            if (!ofd.ShowDialog().Value) return;
            using (StreamReader sr = new StreamReader(ofd.FileName))
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


        public abstract IInformationType ReturnRecordId(string link);

        public abstract IInformationType ReturnRecordSpacial(Feature link);

      

        private void AddNewElement<t>(object newElement)
        {
            if (Holder.NewListElements.ContainsKey(typeof(t)))
                Holder.NewListElements.Add(typeof(t), new List<object>());
            Holder.NewListElements[typeof(t)].Add(newElement);
        }






        //public object GetRecordEntity(Type propertyType, DataRow dataRow)
        //{
        //    object returnVal = null;

        //    //if (propertyType == typeof(ApplicationUser))
        //    //    returnVal = GetRecordEntityApplicationUser<ApplicationUser>(dataRow);
        //    //else if (propertyType == typeof(BirdSpecies))
        //    //        returnVal = GetRecordEntityBirdSpecies<BirdSpecies>(dataRow);
        //    //else if (propertyType == typeof(WildlifeSpecies))
        //    //    returnVal = GetRecordEntityWildlifeSpecies<WildlifeSpecies>(dataRow);
        //    //else if (propertyType == typeof(AmphibianSpecies))
        //    //    returnVal = GetRecordEntityAmphibianSpecies<AmphibianSpecies>(dataRow);
        //    //else if (propertyType == typeof(PlantSpecies))
        //    //    returnVal = GetRecordEntityPlantSpecies<PlantSpecies>(dataRow);

        //    return returnVal;
        //}
        private Dictionary<PropertyInfo, object> GetPropertyValues<t>(DataRow dataRow)
        {
            Dictionary<PropertyInfo, object> propertyInfos = new Dictionary<PropertyInfo, object>();
            string className = typeof(t).Name;
            var propNames = PropertyCrosswalk.Where(_ => _.PropertyType != null).Where(_ => _.PropertyType.PropertyName.StartsWith(className));

            foreach(var prop in propNames)
            {
                PropertyInfo pi = typeof(t).GetProperty(prop.PropertyType.PropertyName.Replace($"{className}.", ""));
                string colName = PropertyCrosswalk.Where(_=>_.PropertyType != null).First(_=>_.PropertyType.PropertyName == prop.PropertyType.PropertyName).Attribute;
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

        public t GetRecordEntityApplicationUser<t>(DataRow dataRow) where t : class
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
                ((IPlaceHolder)returnVal).PlaceHolder = true;

                Holder.Database.Set<t>().Add(returnVal);
                AddNewElement<t>(returnVal);
            }
            return returnVal;
        }
        //public BirdSpecies GetRecordEntityBirdSpecies<t>(DataRow dataRow) where t : BirdSpecies
        //{
        //    Dictionary<PropertyInfo, object> propertyInfos = GetPropertyValues<t>(dataRow);
        //    var expression = (Expression<Func<BirdSpecies, bool>>)GetEntityExpression<t>(propertyInfos);
        //    var returnVal = Holder.Database.BirdSpecies.FirstOrDefault(expression);

        //    if (returnVal == null)
        //    {
        //        if (Holder.NewListElements.ContainsKey(typeof(t)))
        //        {
        //            var EnityList = Holder.NewListElements[typeof(t)].Cast<t>();
        //            returnVal = EnityList.AsQueryable().FirstOrDefault(expression);
        //        }
        //    }

        //    if (returnVal == null)
        //    {
        //        returnVal = new BirdSpecies();
        //        foreach (var valKey in propertyInfos)
        //        {
        //            PropertyInfo pi = valKey.Key;
        //            object val = valKey.Value;

        //            pi.SetValue(returnVal, val);
        //        }
        //        returnVal.PlaceHolder = true;

        //        Holder.Database.BirdSpecies.Add(returnVal);
        //        AddNewElement<t>(returnVal);
        //    }
        //    return returnVal;
        //}
        //public WildlifeSpecies GetRecordEntityWildlifeSpecies<t>(DataRow dataRow) where t : WildlifeSpecies
        //{
        //    Dictionary<PropertyInfo, object> propertyInfos = GetPropertyValues<t>(dataRow);
        //    var expression = (Expression<Func<WildlifeSpecies, bool>>)GetEntityExpression<t>(propertyInfos);
        //    var returnVal = Holder.Database.WildlifeSpecies.FirstOrDefault(expression);

        //    if (returnVal == null)
        //    {
        //        if (Holder.NewListElements.ContainsKey(typeof(t)))
        //        {
        //            var EnityList = Holder.NewListElements[typeof(t)].Cast<t>();
        //            returnVal = EnityList.AsQueryable().FirstOrDefault(expression);
        //        }
        //    }

        //    if (returnVal == null)
        //    {
        //        returnVal = new WildlifeSpecies();
        //        foreach (var valKey in propertyInfos)
        //        {
        //            PropertyInfo pi = valKey.Key;
        //            object val = valKey.Value;

        //            pi.SetValue(returnVal, val);
        //        }
        //        returnVal.PlaceHolder = true;

        //        Holder.Database.WildlifeSpecies.Add(returnVal);
        //        AddNewElement<t>(returnVal);
        //    }
        //    return returnVal;
        //}
        //public AmphibianSpecies GetRecordEntityAmphibianSpecies<t>(DataRow dataRow) where t : AmphibianSpecies
        //{
        //    Dictionary<PropertyInfo, object> propertyInfos = GetPropertyValues<t>(dataRow);
        //    var expression = (Expression<Func<AmphibianSpecies, bool>>)GetEntityExpression<t>(propertyInfos);
        //    var returnVal = Holder.Database.AmphibianSpecies.FirstOrDefault(expression);

        //    if (returnVal == null)
        //    {
        //        if (Holder.NewListElements.ContainsKey(typeof(t)))
        //        {
        //            var EnityList = Holder.NewListElements[typeof(t)].Cast<t>();
        //            returnVal = EnityList.AsQueryable().FirstOrDefault(expression);
        //        }
        //    }

        //    if (returnVal == null)
        //    {
        //        returnVal = new AmphibianSpecies();
        //        foreach (var valKey in propertyInfos)
        //        {
        //            PropertyInfo pi = valKey.Key;
        //            object val = valKey.Value;

        //            pi.SetValue(returnVal, val);
        //        }
        //        returnVal.PlaceHolder = true;

        //        Holder.Database.AmphibianSpecies.Add(returnVal);
        //        AddNewElement<t>(returnVal);
        //    }
        //    return returnVal;
        //}
        //public PlantSpecies GetRecordEntityPlantSpecies<t>(DataRow dataRow) where t : PlantSpecies
        //{
        //    Dictionary<PropertyInfo, object> propertyInfos = GetPropertyValues<t>(dataRow);
        //    var expression = (Expression<Func<PlantSpecies, bool>>)GetEntityExpression<t>(propertyInfos);
        //    var returnVal = Holder.Database.PlantSpecies.FirstOrDefault(expression);

        //    if (returnVal == null)
        //    {
        //        if (Holder.NewListElements.ContainsKey(typeof(t)))
        //        {
        //            var EnityList = Holder.NewListElements[typeof(t)].Cast<t>();
        //            returnVal = EnityList.AsQueryable().FirstOrDefault(expression);
        //        }
        //    }

        //    if (returnVal == null)
        //    {
        //        returnVal = new PlantSpecies();
        //        foreach (var valKey in propertyInfos)
        //        {
        //            PropertyInfo pi = valKey.Key;
        //            object val = valKey.Value;

        //            pi.SetValue(returnVal, val);
        //        }
        //        returnVal.PlaceHolder = true;

        //        Holder.Database.PlantSpecies.Add(returnVal);
        //        AddNewElement<t>(returnVal);
        //    }
        //    return returnVal;
        //}

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
        public string FullClassName { get; set; }
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
