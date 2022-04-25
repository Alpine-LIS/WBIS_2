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

                        string typeName = subProp.PropertyType.Name;
                        if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(double))
                            typeName = "Numeric";
                        properties.Add(new PropertyType() { PropertyName = $"{prop.PropertyType.Name}.{subProp.Name}", TypeName = typeName, Required = ((ImportAttribute)subAtt).Required });
                    }
                }
                else
                {                   
                    string typeName = prop.PropertyType.Name;
                    if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(double))
                        typeName = "Numeric";
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

            int a = PropertyCrosswalk.Where(_ => _.PropertyType != null).Count(_ => _.PropertyType.Required);
            int b = AvailibleFields.Count(_ => _.Required);

            if (PropertyCrosswalk.Where(_ => _.PropertyType != null)
                .Count(_ => _.PropertyType.Required) != AvailibleFields.Count(_ => _.Required))
            {
                MessageBox.Show("All required fields must be used.");
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
                        DataType = col.DataType.Name,
                        AvailibleFields = AvailibleFields
                    };
                    if (col.DataType == typeof(int) || col.DataType == typeof(double))
                        xWalk.DataType = "Numeric";
                    PropertyCrosswalk.Add(xWalk);
                }
            }
            IdOptions = PropertyCrosswalk.Select(_=>_.Attribute).ToList();
            RaisePropertiesChanged(nameof(IdOptions));
            RaisePropertiesChanged(nameof(PropertyCrosswalk));
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

        //public object GetRecordEntity(string neededValue, Type propertyType)
        //{
        //    object returnVal = null;

        //    if (propertyType == typeof(ApplicationUser))
        //        returnVal= GetRecordEntityApplicationUser<ApplicationUser>(neededValue);

        //    return returnVal;
        //}
        //public ApplicationUser GetRecordEntityApplicationUser<t>(string userName) where t : ApplicationUser
        //{
        //    var user = Database.ApplicationUsers.FirstOrDefault(x => x.UserName == userName);
        //    if (user == null)
        //    {
        //        if (NewListElements.ContainsKey(typeof(t))) 
        //            user = (t)NewListElements[typeof(t)].FirstOrDefault(x => ((t)x).UserName == userName);
        //    }                

        //    if (user == null)
        //    {
        //        user = new ApplicationUser()
        //        {
        //            UserName = userName,
        //            ApplicationGroup = Database.ApplicationGroups.FirstOrDefault(_ => _.GroupName == "Unknown"),
        //            PlaceHolder = true
        //        };
        //        Database.ApplicationUsers.Add(user);
        //        AddNewElement<t>(user);
        //    }
        //    return user;
        //}
        //public BirdSpecies GetRecordEntityBirdSpecies<t>(string neededValue) where t : BirdSpecies
        //{
        //    var element = Database.BirdSpecies.FirstOrDefault(x => x.Species == neededValue);
        //    if (element == null)
        //    {
        //        if (NewListElements.ContainsKey(typeof(t)))
        //            element = (t)NewListElements[typeof(t)].FirstOrDefault(x => ((t)x).Species == neededValue);
        //    }

        //    if (element == null)
        //    {
        //        element = new BirdSpecies()
        //        {
        //            Species = neededValue,
        //            PlaceHolder = true
        //        };
        //        Database.BirdSpecies.Add(element);
        //        AddNewElement<t>(element);
        //    }
        //    return element;
        //}
        //public WildlifeSpecies GetRecordEntityWildlifeSpecies<t>(string neededValue) where t : WildlifeSpecies
        //{
        //    var element = Database.WildlifeSpecies.FirstOrDefault(x => x.Species == neededValue);
        //    if (element == null)
        //    {
        //        if (NewListElements.ContainsKey(typeof(t)))
        //            element = (t)NewListElements[typeof(t)].FirstOrDefault(x => ((t)x).Species == neededValue);
        //    }

        //    if (element == null)
        //    {
        //        element = new WildlifeSpecies()
        //        {
        //            Species = neededValue,
        //            PlaceHolder = true
        //        };
        //        Database.WildlifeSpecies.Add(element);
        //        AddNewElement<t>(element);
        //    }
        //    return element;
        //}
        //public AmphibianSpecies GetRecordEntityAmphibianSpecies<t>(string neededValue) where t : AmphibianSpecies
        //{
        //    var element = Database.AmphibianSpecies.FirstOrDefault(x => x.SpeciesCode == neededValue);
        //    if (element == null)
        //    {
        //        if (NewListElements.ContainsKey(typeof(t)))
        //            element = (t)NewListElements[typeof(t)].FirstOrDefault(x => ((t)x).Species == neededValue);
        //    }

        //    if (element == null)
        //    {
        //        element = new AmphibianSpecies()
        //        {
        //            Species = neededValue,
        //            PlaceHolder = true
        //        };
        //        Database.AmphibianSpecies.Add(element);
        //        AddNewElement<t>(element);
        //    }
        //    return element;
        //}
        //public PlantSpecies GetRecordEntityPlantSpecies<t>(string neededValue) where t : PlantSpecies
        //{
        //    var element = Database.PlantSpecies.FirstOrDefault(x => x.Species == neededValue);
        //    if (element == null)
        //    {
        //        if (NewListElements.ContainsKey(typeof(t)))
        //            element = (t)NewListElements[typeof(t)].FirstOrDefault(x => ((t)x).Species == neededValue);
        //    }

        //    if (element == null)
        //    {
        //        element = new PlantSpecies()
        //        {
        //            Species = neededValue,
        //            PlaceHolder = true
        //        };
        //        Database.PlantSpecies.Add(element);
        //        AddNewElement<t>(element);
        //    }
        //    return element;
        //}

        private void AddNewElement<t>(object newElement)
        {
            if (Holder.NewListElements.ContainsKey(typeof(t)))
                Holder.NewListElements.Add(typeof(t), new List<object>());
            Holder.NewListElements[typeof(t)].Add(newElement);
        }
    }
    public class PropertyCrosswalk : BindableBase
    {
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
                if (value != null)
                    SetProperty(() => PropertyType, value);
            }
        }
        public List<PropertyType> AvailibleFields { get; set; }
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
