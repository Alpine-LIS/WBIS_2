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

namespace WBIS_2
{
    public class Bootstrapper
    {
        const string StateVersion = "3.1";
        protected static IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        public virtual void Run()
        {
            WBIS_2.Modules.GdalConfiguration.ConfigureGdal();
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
            //Manager.Register(Regions.NavigationUser, new Module(AppModules.ModuleUser, () => new NavigationItem("User")));
            //Manager.Register(Regions.NavigationAdminUser, new Module(AppModules.ModuleAdminUser, () => new NavigationItem("Users")));

            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleDistricts, () => new NavigationItem("Districts")));
            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleWatersheds, () => new NavigationItem("Watersheds")));
            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleQuad75s, () => new NavigationItem("Quad75s")));
            Manager.Register(Regions.NavigationAreas, new Module(AppModules.ModuleHex160s, () => new NavigationItem("Hex160s")));

            Manager.Register(Regions.NavigationCalifornia, new Module(AppModules.ModuleCnddbOccurrence, () => new NavigationItem("CnddbOccurrence")));
            Manager.Register(Regions.NavigationCalifornia, new Module(AppModules.ModuleCdfwSpottedOwl, () => new NavigationItem("CdfwSpottedOwl")));
            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleUser,
            //   () => ApplicationUserViewModel.Create(CurrentUser.User), typeof(ApplicationUserView)));
            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleAdminUser,
            //  () => ApplicationUserAdminViewModel.Create(), typeof(ApplicationUserAdminView)));

            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleDistricts,
            //    () => DistrictsListViewModel.Create("Districts", "Districts"), typeof(DistrictsListView)));
            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleWatersheds,
            //    () => WatershedsListViewModel.Create("Watersheds", "Watersheds"), typeof(WatershedsListView)));
            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleHex160s,
            //    () => Hex160sListViewModel.Create("Hex160s", "Hex160s"), typeof(Hex160sListView)));
            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleQuad75s,
            //    () => Quad75sListViewModel.Create("Quad75s", "Quad75s"), typeof(Quad75sListView)));

            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleCnddbOccurrence,
            //   () => CnddbsListViewModel.Create("CnddbOccurrence", "CnddbOccurrence"), typeof(CnddbsListView)));
            //Manager.Register(Regions.Documents, new Module(AppModules.ModuleCdfwSpottedOwl,
            //   () => CdfwOwlsListViewModel.Create("CdfwSpottedOwl", "CdfwSpottedOwl"), typeof(CdfwOwlsListView)));
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

            //Manager.Inject(Regions.NavigationUser, AppModules.ModuleUser);
            //Manager.Inject(Regions.NavigationUser, AppModules.ModuleMap);

            //Manager.Inject(Regions.NavigationAdminUser, AppModules.ModuleAdminUser);
            //Manager.Inject(Regions.NavigationAdminUser, AppModules.ModuleMap);

            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleDistricts);
            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleWatersheds);
            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleQuad75s);
            Manager.Inject(Regions.NavigationAreas, AppModules.ModuleHex160s);

            Manager.Inject(Regions.NavigationCalifornia, AppModules.ModuleCnddbOccurrence);
            Manager.Inject(Regions.NavigationCalifornia, AppModules.ModuleCdfwSpottedOwl);
        }

        protected virtual void ConfigureNavigation()
        {
            //Manager.GetEvents(Regions.NavigationUser).Navigation += OnNavigation;
            //Manager.GetEvents(Regions.NavigationAdminUser).Navigation += OnNavigation;
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
