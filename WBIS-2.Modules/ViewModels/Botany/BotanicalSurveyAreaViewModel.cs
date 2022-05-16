using Atlas.Data;
using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WBIS_2.DataModel;
using WBIS_2.Modules.Interfaces;
using WBIS_2.Modules.Tools;
using System.Reflection;

namespace WBIS_2.Modules.ViewModels
{
    public class BotanicalSurveyAreaViewModel : DetailAndChildrenViewModelBase, IDocumentContent, IDetailView
    {
        public static bool AddSingle => true;
        public BotanicalSurveyArea SurveyArea
        {
            get { return GetProperty(() => SurveyArea); }
            set
            {
                SetProperty(() => SurveyArea, value);
                Record = SurveyArea;
            }
        }


        public string[] ThpNames { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(1000, MinimumLength = 1)]
        public string ThpName
        {
            get { return GetProperty(() => ThpName); }
            set
            {
                SetProperty(() => ThpName, value);

                    CanConnectScoping = Database.BotanicalScopings
                    .Include(_=>_.THP_Area).Any(_=>_.THP_Area == DbHelp.ThpExistance(Database, ThpName));
                RaisePropertiesChanged(nameof(CanConnectScoping));
                if (!CanConnectScoping)
                {
                    ConnectScoping = false;
                    RaisePropertiesChanged(nameof(ConnectScoping));
                }
            }
        }


        public bool ConnectScoping { get; set; } = false;
        public bool CanConnectScoping { get; set; } = false;


        public object Title => $"Area: {ThpName}:{SurveyArea.AreaName}{ChangedSign}";


        public static BotanicalSurveyAreaViewModel Create(Guid guid)
        {
            return ViewModelSource.Create(() => new BotanicalSurveyAreaViewModel(guid));
        }

        public BotanicalSurveyAreaViewModel(Guid guid)
        {           
            ThpNames = Database.THP_Areas.Select(_=>_.THPName).OrderBy(_=>_).ToArray();

            SurveyArea = Database.BotanicalSurveyAreas
                   .Include(_ => _.BotanicalScoping)
                   .Include(_ => _.THP_Area)
                   .Include(_ => _.Watersheds)
                   .Include(_ => _.District)
                   .Include(_ => _.Quad75s)
                   .FirstOrDefault(_ => _.Guid == guid);

            if (SurveyArea != null)
            {           
                if (SurveyArea.THP_Area != null) ThpName = SurveyArea.THP_Area.THPName;
            }
            else
            {
                SurveyArea = new BotanicalSurveyArea();
                SurveyArea.Guid = guid;
            }

            RaisePropertyChanged(nameof(ThpName));


            SurveyTypes = MultiChoiceClassBuilder(SurveyArea, "SurveyType");
            Aspects = MultiChoiceClassBuilder(SurveyArea, "Aspect");
            Slopes = MultiChoiceClassBuilder(SurveyArea, "Slope");
            Canopies = MultiChoiceClassBuilder(SurveyArea, "Canopy");
            RockOutcrops = MultiChoiceClassBuilder(SurveyArea, "RockOutcrops");
            Boulders = MultiChoiceClassBuilder(SurveyArea, "Boulders");
            UnderstoryVegs = MultiChoiceClassBuilder(SurveyArea, "UnderstoryVegetation");
            GenHabs = MultiChoiceClassBuilder(SurveyArea, "GeneralHabitat");


        ParentType = SurveyArea;
            RaisePropertyChanged(nameof(ParentType));

            if (SurveyArea.BotanicalScoping != null)
            {
                ConnectScoping = true;
                RaisePropertiesChanged(nameof(ConnectScoping));
            }
        }
        public override void Records_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            if (CurrentChild == null) return;
            if (ParentType == null) return;

            e.QueryableSource = CurrentChild.Manager.GetQueryable(new object[] { SurveyArea }, ParentType.GetType(), Database);
        }






        public override void CloseForm()
        {
            throw new NotImplementedException();
        }

        public override void OnClose(CancelEventArgs e)
        {
            if (Changed)
            {
                var result = ThemedMessageBox.Show(title: "Confirmation", text: UnsavedMessageText, messageBoxButtons: MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        public override void OnDestroy()
        {
            //throw new NotImplementedException();
        }

        public override void Save()
        {
            if (!this.Changed) return;

            if (HasErrors() || SurveyArea.HasErrors())
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();
                      
            THP_Area tHP_Area = DbHelp.ThpExistance(Database, ThpName);
            if (tHP_Area == null)
            {
                tHP_Area = new THP_Area() { THPName = ThpName };
                Database.THP_Areas.Add(tHP_Area);
            }
            SurveyArea.THP_Area = tHP_Area;

            if (ConnectScoping)
                SurveyArea.BotanicalScoping = Database.BotanicalScopings
                    .Include(_=>_.THP_Area)
                    .Include(_=>_.BotanicalSurveyAreas)
                    .FirstOrDefault(_=>_.THP_Area == SurveyArea.THP_Area);

            if (!Database.BotanicalSurveyAreas.Contains(SurveyArea))
                Database.BotanicalSurveyAreas.Add(SurveyArea);
            else Database.BotanicalSurveyAreas.Update(SurveyArea);

            Database.SaveChanges();
            this.Changed = false;
            w.Stop();
        }



        public MultiChoiceClass SurveyTypes { get; set; }
        public MultiChoiceClass Aspects { get; set; }
        public MultiChoiceClass Slopes { get; set; }
        public MultiChoiceClass Canopies { get; set; }
        public MultiChoiceClass RockOutcrops { get; set; }
        public MultiChoiceClass Boulders { get; set; }
        public string[] Substrates => Database.DropdownOptions.Where(_ => _.Entity == DbHelp.GetDbString(typeof(BotanicalSurveyArea)) && _.Property == "substrate").Select(_ => _.SelectionText).ToArray();
        public MultiChoiceClass UnderstoryVegs { get; set; }
        public MultiChoiceClass GenHabs { get; set; }



        public MultiChoiceClass MultiChoiceClassBuilder(IInformationType informationType, string propertyName)
        {
            PropertyInfo property = informationType.GetType().GetProperty(propertyName);
            string currentVal = "";
            object test = property.GetValue(informationType);
            if (test != null) currentVal = test.ToString();
            DropdownOption[] dropDownOptions = Database.DropdownOptions.Where(_=>_.Entity == DbHelp.GetDbString(informationType.GetType())
                && _.Property == DbHelp.GetDbString(propertyName)).ToArray();
            return new MultiChoiceClass(informationType, property, dropDownOptions, currentVal);
        }

        public class MultiChoiceClass : BindableBase
        {
            public MultiChoiceClass(IInformationType parent, PropertyInfo property, DropdownOption[] dropDownOptions, string currentVal)
            {
                Parent = parent;
                Property = property;
                foreach (var optionVals in dropDownOptions)
                {
                    var option = new Option()
                    {
                        Displaytext = optionVals.FullText,
                        SelectionText = optionVals.SelectionText,
                        Select = currentVal.Contains(optionVals.SelectionText)                        
                    };
                    option.SelectionChanged += SelectionChanged;
                    Options = Options.Append(option).ToArray();
                }
                SelectionChanged(new object(), new EventArgs());
            }

            IInformationType Parent;
            PropertyInfo Property;

            public string ConcatText { get; set; }
            public Option[] Options{get;set;} = new Option[0];

            private void SelectionChanged(object sender, EventArgs e)
            {
                if (Options.Length == 0)
                    ConcatText = "";
                else ConcatText = String.Join(",", Options.Where(_ => _.Select).Select(_=>_.SelectionText));
                RaisePropertyChanged(nameof(ConcatText));

                Property.SetValue(Parent, ConcatText);
            }
        }
        public class Option:BindableBase
        {
            public string Displaytext { get; set; }
            public string SelectionText { get; set; }
            public EventHandler SelectionChanged;
            public bool Select
            {
                get { return GetProperty(() => Select); }
                set
                {
                    SetProperty(() => Select, value);
                    SelectionChanged?.Invoke(new object(), new EventArgs());
                }
            }
        }
    }
}
