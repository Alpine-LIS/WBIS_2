using WBIS_2.DataModel;
using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DevExpress.Mvvm.ModuleInjection;
using WBIS_2.Common;
using WBIS_2.Modules.Views;
using DevExpress.Xpf.SpellChecker;
using WBIS_2.ViewModels;
using System.Globalization;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules;
using DevExpress.Xpf.Core;
using System.IO;
using DevExpress.Xpf.Bars;
using System.Windows.Media;
using System.Xml;

namespace WBIS_2.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            WBIS_2.DataModel.CurrentUser.CurrentUserChanged += CurrentUserChanged;
            Application.Current.MainWindow.Closing += MainWindow_Closing;
            //Application.Current.MainWindow.Loaded += MainWindow_Loaded;            
            checker = new SpellChecker();
            checker.Culture = new CultureInfo("pl-PL");
            checker.SpellCheckMode = DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType;
            SpellChecker = checker;

            Application.Current.MainWindow.Activated += MainWindow_Activated;
        }
        SpellChecker checker;
        public SpellChecker SpellChecker
        {
            get { return checker; }
            private set { checker = value; }
        }
        private void MainWindow_Activated(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Activated -= MainWindow_Activated;
            //((MainViewModel)DataContext).ShowAnnouncmentsStartup();
        }

        void TestUserControl_ContentRendered(object sender, EventArgs e)
        {
            // Don't forget to unsubscribe from the event
            ((PresentationSource)sender).ContentRendered -= TestUserControl_ContentRendered;
            //((MainViewModel)DataContext).ShowAnnouncmentsStartup();
            // ..
        }
        private void MainView_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GotFocus -= MainView_GotFocus;
            //((MainViewModel)DataContext).ShowAnnouncmentsStartup();
        }



        private void CurrentUserChanged(object sender, EventArgs e)
        {
            if (CurrentUser.AdminPrivileges)
            {
                AdminUserRegion.Height = double.NaN;
                UserRegion.Height = 0d;
            }
            else
            {
                AdminUserRegion.Height = 0d;
                UserRegion.Height = double.NaN;
            }

            //double height = 0;
            //if (AdminUserRegion.Height > 0) height = AdminUserRegion.Height;
            //if (UserRegion.Height > 0) height = UserRegion.Height;

            //if (CurrentUser.AdminPrivileges)
            //{
            //    AdminUserRegion.Height = double.NaN;//.Visibility = Visibility.Visible;
            //    UserRegion.Height = 0d;//.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    AdminUserRegion.Height = 0d;//.Visibility = Visibility.Hidden;
            //    UserRegion.Height = double.NaN;//.Visibility = Visibility.Visible;
            //}
        }

        private void LogOutClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            UserLoginControl userLoginControl = new UserLoginControl(CurrentUser.User);
            CustomControlWindow window = new CustomControlWindow(userLoginControl);
            if (window.DialogResult) UserLoggedIn(userLoginControl.ReturnApplicationUser);// UserMenuControl.Content = "User: " + CurrentUser.UserName;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.ApplicationStarted) return;
            //CheckForUpdates(false);

            WaitWindowHandler w = new WaitWindowHandler();
            w.Start();
            UserLoginControl userLoginControl = new UserLoginControl();
            w.Stop();
            CustomControlWindow userWindow = new CustomControlWindow(userLoginControl, true);

            if (!userWindow.DialogResult)
            {
                Application.Current.MainWindow.Close();
                return;
            }

            w.Start();
            //new StartTimeout();
            UserLoggedIn(userLoginControl.ReturnApplicationUser);
            if (!userLoginControl.SkipMapLoad) Map.Content = new MapView();
            w.Stop();
            CurrentUser.ApplicationStarted = true;
            //AddWatersheds();
        }

        private void UserLoggedIn(ApplicationUser selectedUser)
        {
            if (CurrentUser.CurrentDatabase)
            {
                CurrentUser.User = new WBIS2Model().ApplicationUsers
                           .Include(_ => _.Districts)
                           //.Include(_ => _.ActiveRegens)
                           //.Include(_ => _.ActiveFuelbreaks)
                           .Include(_ => _.ApplicationGroup)
                           .First(_ => _.Guid == selectedUser.Guid);
            }
            else
            {
                CurrentUser.User = new WBIS2Model().ApplicationUsers
                                           .Include(_ => _.Districts)
                                           //.Include(_ => _.ActiveRegens)
                                           //.Include(_ => _.ActiveFuelbreaks)
                                           .Include(_ => _.ApplicationGroup)
                                           .First(_ => _.UserName == selectedUser.UserName);
                var w = Application.Current.MainWindow;
                w.Title = w.Title + " OldDb";
            }

            UserMenuControl.Content = "User: " + CurrentUser.UserName;
            UserMenuControl.Background = null;//Brushes.LightGreen;

            SetViewActiveUnitsAs();
        }


        public Visibility UserVisibility { get; set; } = Visibility.Visible;
        public Visibility AdminUserVisibility { get; set; } = Visibility.Visible;

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Layout.xml");
            DockLayoutManager.SaveLayoutToXml(AppDomain.CurrentDomain.BaseDirectory + "\\Layout.xml");
        }
        private void DockLayoutManager_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Layout.xml"))
            {
                DockLayoutManager.RestoreLayoutFromXml(AppDomain.CurrentDomain.BaseDirectory + "\\Layout.xml");

            }
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //IssueReportingWindow issueReportingWindow = new IssueReportingWindow();
            //issueReportingWindow.ShowDialog();
        }

        private void CheckForUpdatesClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //Alpine.UpdateSupport.Models.AvailableVersions availableVersions;
            //availableVersions = new Alpine.UpdateSupport.Models.AvailableVersions();
            ////var VersionDownloadTracker = new PostGISVersionTrackerHelper();

            ////If the version on the remote file doesn't match the application version, then the user can update
            ////if (availableVersions.UpdateAvailable)
            //// {
            //availableVersions.Dispose();
            //var dataContext = new Alpine.UpdateSupport.ViewModels.AutoUpdaterControlViewModel(null);// Atlas3.Controls.ViewModels.AutoUpdaterViewModel(null,null,null,null);
            //var commands = new List<DevExpress.Mvvm.UICommand>();
            //commands.AddRange(dataContext.GetCommands());
            //var control = new Alpine.UpdateSupport.Views.AutoUpdaterControl { DataContext = dataContext };
            ////var ctrlWithHelpView = new Alpine.UpdateSupport.Views.cont Atlas3.Controls.Views.ControlWithHelpView(control);
            ////ctrlWithHelpView.Height = control.Height + 5;
            ////commands.Add(ctrlWithHelpView.GetToggleHelpCommand());
            ////control = ctrlWithHelpView;

            ////var updateWindow = new DXDialogWindow("Check for Updates", commands);
            ////updateWindow.SizeToContent = SizeToContent.WidthAndHeight;
            ////updateWindow.Content = control;
            ////updateWindow.Owner = System.Windows.Application.Current.MainWindow;
            ////updateWindow.ResizeMode = ResizeMode.NoResize;
            ////updateWindow.WindowStyle = WindowStyle.ToolWindow;
            ////updateWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ////updateWindow.ShowDialog();



            //var window = new DXDialogWindow("Check for Updates", commands);
            //window.SizeToContent = SizeToContent.WidthAndHeight;
            //window.Content = control;
            //window.Owner = System.Windows.Application.Current.MainWindow;
            //window.ResizeMode = ResizeMode.NoResize;
            //window.WindowStyle = WindowStyle.ToolWindow;
            //window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //window.ShowDialog();
            //// }
            //availableVersions.Dispose();
        }
        private void DockLayoutManager_DockItemClosed(object sender, DevExpress.Xpf.Docking.Base.DockItemClosedEventArgs e)
        {
            //((DevExpress.Xpf.Docking.DocumentPanel)e.AffectedItems[0]).
            ModuleManager.DefaultManager.InjectOrNavigate(Regions.MainWindow, Common.Modules.Main);
            AccordionControl.SelectedItem = null;
        }
        private void LightMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (File.Exists($@"{appPath}\WBIS_2\dm.preference")) File.Delete($@"{appPath}\WBIS_2\dm.preference");
            if (!File.Exists($@"{appPath}\WBIS_2\lm.preference")) File.Create($@"{appPath}\WBIS_2\lm.preference").Close();
            ApplicationThemeHelper.ApplicationThemeName = "Office2019Colorful";
            ApplicationThemeHelper.UpdateApplicationThemeName();
        }
        private void DarkMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (File.Exists($@"{appPath}\WBIS_2\lm.preference")) File.Delete($@"{appPath}\WBIS_2\lm.preference");
            if (!File.Exists($@"{appPath}\WBIS_2\dm.preference")) File.Create($@"{appPath}\WBIS_2\dm.preference").Close();
            ApplicationThemeHelper.ApplicationThemeName = "Office2019Black";
            ApplicationThemeHelper.UpdateApplicationThemeName();
        }


        private void SetViewActiveUnitsAs()
        {
            ViewActiveUnitsAsMenuControl.Items.Clear();
            ViewActiveUnitsAsMenuControl.Content = "View Active Units As: " + CurrentUser.UserName;

            BarCheckItem CurrentUserItem = new BarCheckItem() { Content = CurrentUser.UserName, IsChecked = true };
            CurrentUserItem.ItemClick += CurrentUserItem_ItemClick;
            ViewActiveUnitsAsMenuControl.Items.Add(CurrentUserItem);


            var MobileUsers = new WBIS2Model().ApplicationUsers.Include(_ => _.ApplicationGroup)
                .Where(_ => _.ApplicationGroup.GroupName == "Mobile User" && !_._delete && !_.PlaceHolder
                && ((_.Wildlife && CurrentUser.User.Wildlife) || (_.Botany && CurrentUser.User.Botany)))
                .Select(_ => _.UserName).OrderBy(_ => _);
            foreach (var MobileUser in MobileUsers)
            {
                BarCheckItem MobileUserItem = new BarCheckItem() { Content = MobileUser, IsChecked = false };
                MobileUserItem.ItemClick += MobileUserItem_ItemClick;
                ViewActiveUnitsAsMenuControl.Items.Add(MobileUserItem);
            }
            CurrentUser.ViewActiveUnitsAsCurrentUser();
        }

        private void MobileUserItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewActiveUnitsAsMenuControl.Content = "View Active Units As: " + e.Item.Content.ToString();
            UserMenuControl.Content = "User: " + CurrentUser.UserName + ": " + e.Item.Content.ToString();
            UserMenuControl.Background = Brushes.LightGreen;
            foreach (BarCheckItem item in ViewActiveUnitsAsMenuControl.Items)
                item.IsChecked = false;
            ((BarCheckItem)sender).IsChecked = true;
            CurrentUser.ViewActiveUnitsAsMobileUser(e.Item.Content.ToString());
        }

        private void CurrentUserItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewActiveUnitsAsMenuControl.Content = "View Active Units As: " + e.Item.Content.ToString();
            UserMenuControl.Content = "User: " + CurrentUser.UserName;
            UserMenuControl.Background = null;//Brushes.LightGreen;
            foreach (BarCheckItem item in ViewActiveUnitsAsMenuControl.Items)
                item.IsChecked = false;
            ((BarCheckItem)sender).IsChecked = true;
            CurrentUser.ViewActiveUnitsAsCurrentUser();
        }
    }
}
