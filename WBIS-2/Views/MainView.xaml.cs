using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using WBIS_2.DataModel;
using WBIS_2.Modules.Views;

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

            WBIS2Model wBIS2Model = new WBIS2Model();
            //wBIS2Model.ApplicationUsers.Add(new ApplicationUser() { ApplicationGroup = wBIS2Model.ApplicationGroups.First(), UserName = "Tyler D. Suran" });
            //wBIS2Model.SaveChanges();
            CurrentUser.User = wBIS2Model.ApplicationUsers.Include(_=>_.ApplicationGroup).First();
            //RMS_3.DataModel.CurrentUser.CurrentUserChanged += CurrentUserChanged;
            //Application.Current.MainWindow.Closing += MainWindow_Closing;
            //Application.Current.MainWindow.Loaded += MainWindow_Loaded;            
        }



        private void CurrentUserChanged(object sender, EventArgs e)
        {
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
        //    UserLoginControl userLoginControl = new UserLoginControl(CurrentUser.User);
        //    CustomControlWindow window = new CustomControlWindow(userLoginControl);
        //    if (window.DialogResult) UserLoggedIn(userLoginControl.ReturnApplicationUser);// UserMenuControl.Content = "User: " + CurrentUser.UserName;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ////DbChooser dbChooser = new DbChooser();
            ////CustomControlWindow dbWindow = new CustomControlWindow(dbChooser);

            //Alpine.UpdateSupport.Models.AvailableVersions availableVersions;
            //availableVersions = new Alpine.UpdateSupport.Models.AvailableVersions();
            ////var VersionDownloadTracker = new PostGISVersionTrackerHelper();

            ////If the version on the remote file doesn't match the application version, then the user can update
            //if (availableVersions.UpdateAvailable)
            //{
            //    availableVersions.Dispose();
            //    var dataContext = new Alpine.UpdateSupport.ViewModels.AutoUpdaterControlViewModel(null);// Atlas3.Controls.ViewModels.AutoUpdaterViewModel(null,null,null,null);
            //    var commands = new List<DevExpress.Mvvm.UICommand>();
            //    commands.AddRange(dataContext.GetCommands());
            //    var control = new Alpine.UpdateSupport.Views.AutoUpdaterControl { DataContext = dataContext };
            //    //var ctrlWithHelpView = new Alpine.UpdateSupport.Views.cont Atlas3.Controls.Views.ControlWithHelpView(control);
            //    //ctrlWithHelpView.Height = control.Height + 5;
            //    //commands.Add(ctrlWithHelpView.GetToggleHelpCommand());
            //    //control = ctrlWithHelpView;

            //    var updateWindow = new DXDialogWindow("Check for Updates", commands);
            //    updateWindow.SizeToContent = SizeToContent.WidthAndHeight;
            //    updateWindow.Content = control;
            //    updateWindow.Owner = System.Windows.Application.Current.MainWindow;
            //    updateWindow.ResizeMode = ResizeMode.NoResize;
            //    updateWindow.WindowStyle = WindowStyle.ToolWindow;
            //    updateWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //    updateWindow.ShowDialog();
            //}
            //availableVersions.Dispose();


            //var serverControl = new WhichDbControl();
            //var serverWindow = new DXDialogWindow("Pick a server");
            //serverWindow.SizeToContent = SizeToContent.WidthAndHeight;
            //serverWindow.Content = serverControl;
            //serverWindow.Owner = System.Windows.Application.Current.MainWindow;
            //serverWindow.ResizeMode = ResizeMode.NoResize;
            //serverWindow.WindowStyle = WindowStyle.ToolWindow;
            //serverWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //serverWindow.ShowDialog();


            //UserLoginControl userLoginControl = new UserLoginControl();
            //CustomControlWindow userWindow = new CustomControlWindow(userLoginControl);

            //if (!userWindow.DialogResult)
            //{
            //    Application.Current.MainWindow.Close();
            //    return;
            //}

            //WaitWindowHandler w = new WaitWindowHandler();
            //w.Start();
            ////new StartTimeout();
            //UserLoggedIn(userLoginControl.ReturnApplicationUser);
            //Map.Content = new MapView();
            //w.Stop();
        }


        private void UserLoggedIn(ApplicationUser selectedUser)
        {
            //CurrentUser.User = new RMS3Model().ApplicationUsers
            //           .Include(_ => _.Districts)
            //           //.Include(_ => _.ActiveRegens)
            //           //.Include(_ => _.ActiveFuelbreaks)
            //           .Include(_ => _.ApplicationGroup)
            //           .First(_ => _.Guid == selectedUser.Guid);


            //UserMenuControl.Content = "User: " + CurrentUser.UserName;
            //if (CurrentUser.RegenUser)
            //{
            //    DefaultViewMenuControl.Content = "Default View: Regen";
            //    RegenDefaultMenuControl.IsChecked = true;
            //    FuelbreakDefaultMenuControl.IsChecked = false;
            //}
            //else
            //{
            //    DefaultViewMenuControl.Content = "Default View: Fuelbreak";
            //    RegenDefaultMenuControl.IsChecked = false;
            //    FuelbreakDefaultMenuControl.IsChecked = true;
            //}
        }
        private void RegenDefultClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //DefaultViewMenuControl.Content = "Default View: Regen";
            //RegenDefaultMenuControl.IsChecked = true;
            //FuelbreakDefaultMenuControl.IsChecked = false;
            //CurrentUser.RegenUser = true;
            //CurrentUser.RegenOrFuelbreakChanged?.Invoke(new object(), new EventArgs());
        }

        private void FuelbreakDefaultClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //DefaultViewMenuControl.Content = "Default View: Fuelbreak";
            //RegenDefaultMenuControl.IsChecked = false;
            //FuelbreakDefaultMenuControl.IsChecked = true;
            //CurrentUser.RegenUser = false;
            //CurrentUser.RegenOrFuelbreakChanged?.Invoke(new object(), new EventArgs());
        }



        public Visibility UserVisibility { get; set; } = Visibility.Visible;
        public Visibility AdminUserVisibility { get; set; } = Visibility.Visible;

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //XmlDocument doc = new XmlDocument();
            //System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\RMS3Layout.xml");
            //DockLayoutManager.SaveLayoutToXml(AppDomain.CurrentDomain.BaseDirectory + "\\RMS3Layout.xml");
        }
        private void DockLayoutManager_Loaded(object sender, RoutedEventArgs e)
        {
            //if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\RMS3Layout.xml"))
            //{
            //    DockLayoutManager.RestoreLayoutFromXml(AppDomain.CurrentDomain.BaseDirectory + "\\RMS3Layout.xml");

            //}
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
    }

     


    class WaitWindowHandler
    {
        //private Thread StatusThread = null;

        //WaitWindow Popup = null;

        public void Start()
        {
        //    //create the thread with its ThreadStart method
        //    this.StatusThread = new Thread(() =>
        //    {
        //        try
        //        {
        //            Popup = new WaitWindow();
        //            this.Popup.Show();
        //            this.Popup.Closed += (lsender, le) =>
        //            {
        //                //when the window closes, close the thread invoking the shutdown of the dispatcher
        //                this.Popup.Dispatcher.InvokeShutdown();
        //                this.Popup = null;
        //                this.StatusThread = null;
        //            };

        //            //this call is needed so the thread remains open until the dispatcher is closed
        //            System.Windows.Threading.Dispatcher.Run();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
        //        }
        //    });

        //    //run the thread in STA mode to make it work correctly
        //    this.StatusThread.SetApartmentState(ApartmentState.STA);
        //    this.StatusThread.Priority = ThreadPriority.Normal;
        //    this.StatusThread.Start();
        }

        public void Stop()
        {
            //while (this.Popup == null)
            //{ }
            ////need to use the dispatcher to call the Close method, because the window is created in another thread, and this method is called by the main thread
            //this.Popup.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    this.Popup.Close();
            //}));
        }
    }
}
