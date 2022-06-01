using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using System.ComponentModel;
using WBIS_2.ViewModels;
using WBIS_2.DataModel;
using WBIS_2.Views;
using WBIS_2.Common;
using AppModules = WBIS_2.Common.Modules;
using WBIS_2.DataModel;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.IO;
using System;
using WBIS_2.Modules.Views;
using WBIS_2.Modules.ViewModels;
using WBIS_2.Modules;

namespace WBIS_2
{
    public class Bootstrapper
    {
        const string StateVersion = "3.1";
        protected static IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        public virtual void Run()
        {
            GdalConfigurationRMS.ConfigureFull();
            Atlas3.Data.Exchange.GdalConfigurationAtlas3.ConfigureFull();
            ConfigureTypeLocators();
            RegisterModules();
            //if (!RestoreState())
            //{
                InjectModules();
           // }
            ConfigureNavigation();
            ShowMainWindow();
            //RestoreState();
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
        }

        protected virtual void ConfigureTypeLocators()
        {
            var mainAssembly = typeof(MainViewModel).Assembly;
            var modulesAssembly = typeof(ParentListView).Assembly;
            var assemblies = new[] { mainAssembly, modulesAssembly };
            ViewModelLocator.Default = new ViewModelLocator(assemblies);
            ViewLocator.Default = new ViewLocator(assemblies);
        }

        protected virtual void RegisterModules()
        {
            Manager.Register(Regions.MainWindow, new Module(AppModules.Main, MainViewModel.Create, typeof(MainView)));
            RegisterData();
        }
        private static void RegisterData()
        {
            Manager.Register(Regions.NavigationUser, new Module(AppModules.ModuleUser, () => new NavigationItem("User")));
            Manager.Register(Regions.NavigationAdminUser, new Module(AppModules.ModuleAdminUser, () => new NavigationItem("Users")));
            //Manager.Register(Regions.NavigationAdminUser, new Module(AppModules.ModuleAdminUser, () => new NavigationItem("Users")));

            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleDistricts, () => new NavigationItem("Districts")));
            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleWatersheds, () => new NavigationItem("Watersheds")));
            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleQuad75s, () => new NavigationItem("Quad75s")));
            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleHex160s, () => new NavigationItem("Hex160s")));

            Manager.Register(Regions.NavigationCalifornia, new Module(AppModules.ModuleCnddbOccurrences, () => new NavigationItem("CNDDB Occurrences")));
            Manager.Register(Regions.NavigationCalifornia, new Module(AppModules.ModuleCnddbQuadElements, () => new NavigationItem("CNDDB Quad Elements")));
            Manager.Register(Regions.NavigationCalifornia, new Module(AppModules.ModuleCdfwSpottedOwls, () => new NavigationItem("CDFW Spotted Owls")));


            Manager.Register(Regions.NavigationSupport, new Module(AppModules.ModulePermanentCallStation, () => new NavigationItem("Permanent Call Stations")));
            Manager.Register(Regions.NavigationSupport, new Module(AppModules.ModuleProtectionZone, () => new NavigationItem("Protection Zones")));
            Manager.Register(Regions.NavigationSupport, new Module(AppModules.ModuleWildlifeSpeciesList, () => new NavigationItem("Wildlife Species List")));
            Manager.Register(Regions.NavigationSupport, new Module(AppModules.ModuleAmphibianSpeciesList, () => new NavigationItem("Amphibian Species List")));
            Manager.Register(Regions.NavigationSupport, new Module(AppModules.ModuleBirdSpeciesList, () => new NavigationItem("Bird Species List")));
            Manager.Register(Regions.NavigationSupport, new Module(AppModules.ModulePlantSpeciesList, () => new NavigationItem("Plant Species List")));

            Manager.Register(Regions.NavigationBotany, new Module(AppModules.ModuleBotanicalScoping, () => new NavigationItem("Botanical Scopings")));
            Manager.Register(Regions.NavigationBotany, new Module(AppModules.ModuleBotanicalSurveyArea, () => new NavigationItem("Botanical Survey Areas")));
            Manager.Register(Regions.NavigationBotany, new Module(AppModules.ModuleBotanicalSurvey, () => new NavigationItem("Botanical Surveys")));
            Manager.Register(Regions.NavigationBotany, new Module(AppModules.ModuleBotanicalSurveyElement, () => new NavigationItem("Botanical Survey Elements")));


            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleHex160RequiredPasses, () => new NavigationItem("Hex160 Required Passes")));
            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleSiteCallings, () => new NavigationItem("Site Callings")));
            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleSiteCallingDetections, () => new NavigationItem("Site Calling Detections")));

            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleAmphibianSurveys, () => new NavigationItem("Amphibian Surveys")));
            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleAmphibianElements, () => new NavigationItem("Amphibian Elements")));
            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleBDOWSightings, () => new NavigationItem("BDOW Sightings")));
            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleDOMonitoring, () => new NavigationItem("DO Monitoring")));
            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleForestCarnivoreCameraStations, () => new NavigationItem("Forest Carnivore Camera Stations")));
            Manager.Register(Regions.NavigationWildlife, new Module(AppModules.ModuleRanchPhotoPoints, () => new NavigationItem("Ranch Photo Points")));


            Manager.Register(Regions.Documents, new Module(AppModules.ModuleUser,
               () => ApplicationUserViewModel.Create(CurrentUser.User.Guid), typeof(ApplicationUserView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleAdminUser,
               () => ParentListViewModel.Create(new ApplicationUser()), typeof(ParentListView)));

            Manager.Register(Regions.Documents, new Module(AppModules.ModuleDistricts,
                () => ParentListViewModel.Create(new District()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleWatersheds,
                () => ParentListViewModel.Create(new Watershed()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleHex160s,
                () => ParentListViewModel.Create(new Hex160()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleQuad75s,
                () => ParentListViewModel.Create(new Quad75()), typeof(ParentListView)));

            Manager.Register(Regions.Documents, new Module(AppModules.ModuleCnddbOccurrences,
                () => ParentListViewModel.Create(new CNDDBOccurrence()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleCnddbQuadElements,
                () => ParentListViewModel.Create(new CNDDBQuadElement()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleCdfwSpottedOwls,
                () => ParentListViewModel.Create(new CDFW_SpottedOwl()), typeof(ParentListView)));


            Manager.Register(Regions.Documents, new Module(AppModules.ModuleBotanicalScoping,
                () => ParentListViewModel.Create(new BotanicalScoping()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleBotanicalSurveyArea,
                () => ParentListViewModel.Create(new BotanicalSurveyArea()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleBotanicalSurvey,
                () => ParentListViewModel.Create(new BotanicalSurvey()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleBotanicalSurveyElement,
                () => ParentListViewModel.Create(new BotanicalElement()), typeof(ParentListView)));

            
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleHex160RequiredPasses,
                () => ParentListViewModel.Create(new Hex160RequiredPass()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleSiteCallings,
               () => ParentListViewModel.Create(new SiteCalling()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleSiteCallingDetections,
              () => ParentListViewModel.Create(new SiteCallingDetection()), typeof(ParentListView)));

            Manager.Register(Regions.Documents, new Module(AppModules.ModuleAmphibianSurveys,
               () => ParentListViewModel.Create(new AmphibianSurvey()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleAmphibianElements,
               () => ParentListViewModel.Create(new AmphibianElement()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleBDOWSightings,
               () => ParentListViewModel.Create(new BDOWSighting()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleDOMonitoring,
               () => ParentListViewModel.Create(new DOMonitoring()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleForestCarnivoreCameraStations,
               () => ParentListViewModel.Create(new ForestCarnivoreCameraStation()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleRanchPhotoPoints,
               () => ParentListViewModel.Create(new RanchPhotoPoint()), typeof(ParentListView)));


            Manager.Register(Regions.Documents, new Module(AppModules.ModulePermanentCallStation,
                () => ParentListViewModel.Create(new PermanentCallStation()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleProtectionZone,
                () => ParentListViewModel.Create(new ProtectionZone()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModulePlantSpeciesList,
               () => PlantSpeciesListViewModel.Create(new PlantSpecies()), typeof(PlantSpeciesListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleAmphibianSpeciesList,
               () => ParentListViewModel.Create(new AmphibianSpecies()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleBirdSpeciesList,
               () => ParentListViewModel.Create(new BirdSpecies()), typeof(ParentListView)));
            Manager.Register(Regions.Documents, new Module(AppModules.ModuleWildlifeSpeciesList,
               () => ParentListViewModel.Create(new WildlifeSpecies()), typeof(ParentListView)));
            
        }


        protected virtual bool RestoreState()
        {
            //return false;
            if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\appsettings.json"))
                return false;

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            if (config["StateVersion"] == StateVersion)
            {
                return Manager.Restore(config["LogicalState"], config["VisualState"]);
            }
            return false;
            //return false;
            /*
#if !DEBUG
            if (Settings.Default.StateVersion != StateVersion) return false;
            return Manager.Restore(Settings.Default.LogicalState, Settings.Default.VisualState);
#else
            return false;
#endif
*/
        }

        protected virtual void InjectModules()
        {
            Manager.Inject(Regions.MainWindow, AppModules.Main);

            Manager.Inject(Regions.NavigationUser, AppModules.ModuleUser);
            Manager.Inject(Regions.NavigationAdminUser, AppModules.ModuleAdminUser);

            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleDistricts);
            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleWatersheds);
            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleQuad75s);
            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleHex160s);

            Manager.Inject(Regions.NavigationCalifornia, AppModules.ModuleCnddbOccurrences);
            Manager.Inject(Regions.NavigationCalifornia, AppModules.ModuleCnddbQuadElements);
            Manager.Inject(Regions.NavigationCalifornia, AppModules.ModuleCdfwSpottedOwls);


            Manager.Inject(Regions.NavigationBotany, AppModules.ModuleBotanicalScoping);
            Manager.Inject(Regions.NavigationBotany, AppModules.ModuleBotanicalSurveyArea);
            Manager.Inject(Regions.NavigationBotany, AppModules.ModuleBotanicalSurvey);
            Manager.Inject(Regions.NavigationBotany, AppModules.ModuleBotanicalSurveyElement);


            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleHex160RequiredPasses);
            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleSiteCallings);
            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleSiteCallingDetections);

            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleAmphibianSurveys);
            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleAmphibianElements);
            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleBDOWSightings);
            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleDOMonitoring);
            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleForestCarnivoreCameraStations);
            Manager.Inject(Regions.NavigationWildlife, AppModules.ModuleRanchPhotoPoints);


            Manager.Inject(Regions.NavigationSupport, AppModules.ModuleProtectionZone);
            Manager.Inject(Regions.NavigationSupport, AppModules.ModulePermanentCallStation);
            Manager.Inject(Regions.NavigationSupport, AppModules.ModulePlantSpeciesList);
            Manager.Inject(Regions.NavigationSupport, AppModules.ModuleWildlifeSpeciesList);
            Manager.Inject(Regions.NavigationSupport, AppModules.ModuleBirdSpeciesList);
            Manager.Inject(Regions.NavigationSupport, AppModules.ModuleAmphibianSpeciesList);

        }

        protected virtual void ConfigureNavigation()
        {
            Manager.GetEvents(Regions.NavigationUser).Navigation += OnNavigation;
            Manager.GetEvents(Regions.NavigationAdminUser).Navigation += OnNavigation;
            Manager.GetEvents(Regions.NavigationAreas).Navigation += OnNavigation;
            Manager.GetEvents(Regions.NavigationCalifornia).Navigation += OnNavigation;
            Manager.GetEvents(Regions.NavigationWildlife).Navigation += OnNavigation;
            Manager.GetEvents(Regions.NavigationBotany).Navigation += OnNavigation;
            Manager.GetEvents(Regions.NavigationSupport).Navigation += OnNavigation;
            //Manager.GetEvents(Regions.NavigationOption).Navigation += OnNavigation;
            Manager.GetEvents(Regions.NavigationReports).Navigation += OnNavigation;
            Manager.GetEvents(Regions.Documents).Navigation += OnDocumentsNavigation;
        }

        void OnNavigation(object sender, NavigationEventArgs e)
        {
            if (e.NewViewModelKey == null) return;
            Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
        }

        void OnDocumentsNavigation(object sender, NavigationEventArgs e)
        {
            Manager.Navigate(Regions.NavigationAreas, e.NewViewModelKey);
        }

        void OnClosing(object sender, CancelEventArgs e)
        {
            App.Current.MainWindow = new MainWindow();
            //App.Current.MainWindow.Show();

            //return;

           // if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\appsettings.json"))
           //     File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}\\appsettings.json");

           //File.Create($"{AppDomain.CurrentDomain.BaseDirectory}\\appsettings.json").Close();

           // Manager.Save(out string logicalState, out string visualState);

           // //var config = new ConfigurationBuilder()
           // //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           // //    .AddJsonFile("appsettings.json").Build();
           // //config["StateVersion"] = StateVersion;
           // //config["LogicalState"] = logicalState;
           // //config["VisualState"] = visualState;
           // var options = new JsonSerializerOptions
           // {
           //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
           //     WriteIndented = true
           // };

           // JsonConfiguration configuration = new JsonConfiguration()
           // {
           //     StateVersion = StateVersion,
           //     LogicalState = logicalState,
           //     VisualState = visualState,
           // };

           // var info = JsonSerializer.Serialize<JsonConfiguration>(configuration, options);
           // File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\appsettings.json", info);







            //// var jsonModel = JsonSerializer.Deserialize(jsonString, options);
            //// var modelJson = JsonSerializer.Serialize(config, options);
            //// Console.WriteLine(modelJson);
            ///*
            //Settings.Default.StateVersion = StateVersion;
            //Settings.Default.LogicalState = logicalState;
            //Settings.Default.VisualState = visualState;
            //Settings.Default.Save();
            //*/
        }

        protected virtual void ShowMainWindow()
        {
            App.Current.MainWindow = new MainWindow();
            if (App.Current.MainWindow != null) App.Current.MainWindow.Show();
            if (App.Current.MainWindow != null) App.Current.MainWindow.Closing += OnClosing;
        }
    }

    public class JsonConfiguration
    {
        public string StateVersion { get; set; }
        public string LogicalState { get; set; }
        public string VisualState { get; set; }
    }

}
