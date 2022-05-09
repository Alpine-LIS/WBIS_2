using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WBIS_2.Modules.Views
{
    /// <summary>
    /// Interaction logic for UserLoginControl.xaml
    /// </summary>
    public partial class UserLoginControl : UserControl
    {
        WBIS2Model WBIS2Model= new WBIS2Model();
        public ApplicationUser ReturnApplicationUser { get; set; }
        bool ApplicationStart = true;
        private string RememberedPassword = "";
        //List<string> Activities;
        public UserLoginControl(ApplicationUser user = null)
        {
            InitializeComponent();
           
            try
            {
                CbxUsers.ItemsSource = WBIS2Model.ApplicationUsers
                .IgnoreAutoIncludes()//(_=>_.Districts)
                //.Include(_=>_.ActiveRegens)
                //.Include(_ => _.ActiveFuelbreaks)
                .Include(_ => _.ApplicationGroup)
                .Where(_ => _.ApplicationGroup.DesktopAccess && !_._delete &&  !_.PlaceHolder).OrderBy(_ => _.UserName);
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was an issue connecting to the RMS3 database.");
            }
            

            if (user != null)
            {
                ApplicationStart = false;
                ChbxOfflineGeo.IsChecked = CurrentUser.UserGeoFolder == "MapDataOffline";
                CbxUsers.SelectedItem = user;
                CbxUsers.Text = user.UserName;
            }
            else
            {
                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\user.txt"))
                {
                    string userStr = ""; 
                    using(StreamReader sr = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\user.txt"))
                    {
                        string txt = sr.ReadToEnd().TrimEnd();
                        if (txt.Contains("{"))
                        {
                            var vals = txt.Split(new string[] { "}{"}, StringSplitOptions.None);
                            userStr = vals[0].Substring(1);
                            RememberedPassword = vals[1].Substring(0, vals[1].Length - 1);
                            TbxPassword.Text = "RemeberedPassword";
                        }
                        else
                        {
                            userStr = txt;
                        }
                    }

                    CbxUsers.SelectedItem = WBIS2Model.ApplicationUsers
                        .IgnoreAutoIncludes()//(_=>_.Districts)
                                             //.Include(_=>_.ActiveRegens)
                                             //.Include(_ => _.ActiveFuelbreaks)
                        .Include(_ => _.ApplicationGroup)
                        .First(_=>_.UserName == userStr);
                    CbxUsers.Text = userStr;
                }

                if (Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\MapDataOffline"))
                {
                    ChbxOfflineGeo.IsEnabled = true;
                    if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\MapDataOffline\\Default.Settings"))
                        ChbxOfflineGeo.IsChecked = true;
                }
            }

            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\user.txt"))
            {
                ChbxRemember.IsChecked = true;
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (((ApplicationUser)CbxUsers.SelectedItem).PasswordSHA == null)
            {
                SetPassword();
                return;
            }

            string Accurate = PasswordTools.HashPassword(TbxPassword.Text + "#com.alpinelis.rmsdataapp#");
            if (RememberedPassword != "" && TbxPassword.Text == "RemeberedPassword")
            {
                if (RememberedPassword != ((ApplicationUser)CbxUsers.SelectedItem).PasswordSHA)
                {
                    MessageBox.Show("The password is wrong.");
                    return;
                }
                Accurate = RememberedPassword;
            }
            else
            {
                if (Accurate != ((ApplicationUser)CbxUsers.SelectedItem).PasswordSHA)
                {
                    MessageBox.Show("The password is wrong.");
                    return;
                }
            }


            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
            if (CbxUsers.SelectedItem == null) return;
            ReturnApplicationUser = (ApplicationUser)CbxUsers.SelectedItem;
            
            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\user.txt")) File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}\\user.txt");
            if (ChbxRemember.IsChecked.Value)
            {
                using (StreamWriter sw = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}\\user.txt"))
                {
                    sw.WriteLine("{" + ReturnApplicationUser.UserName + "}{" + Accurate + "}");
                }
            }

            if (ApplicationStart)
            {
                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\MapDataOffline\\Default.Settings")) File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}\\MapDataOffline\\Default.Settings");
                if (ChbxOfflineGeo.IsChecked.Value)
                {
                    using (StreamWriter sw = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}\\MapDataOffline\\Default.Settings"))
                        sw.WriteLine("");
                    CurrentUser.UserGeoFolder = "MapDataOffline";
                }
                else CurrentUser.UserGeoFolder = "MapData";
            }

            if (!CurrentUser.CurrentDatabase && !SkipMapLoad)
            {
                if (ChbxOfflineGeo.IsChecked.Value)
                {
                    SkipMapLoad = !File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\MapDataOffline\\RMS3_Old.geo3");
                }
                else
                {
                    SkipMapLoad = !File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\MapData\\RMS3_Old.geo3");
                }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }
      
        private void SetPassword()
        {
            SetUserPasswordControl setUserPasswordControl = new SetUserPasswordControl((ApplicationUser)CbxUsers.SelectedItem);
            CustomControlWindow userWindow = new CustomControlWindow(setUserPasswordControl,true);

            if (userWindow.DialogResult)
            {
                ((ApplicationUser)CbxUsers.SelectedItem).PasswordSHA = setUserPasswordControl.ReturnPassword;
            }
        }

        public bool SkipMapLoad { get; set; } = false;

        //F1: Start current db w/out map
        //F2: Old db w/ map if geo 'RMS3_Old' exists
        //F3: Old db w/out map
        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LogIn_Click(sender, e);
            else if (e.Key == Key.Escape)
                Close_Click(sender, e);
            else if (ApplicationStart)
            {
                if (e.Key == Key.F1)
                {
                    LogIn_Click(sender, e);
                    SkipMapLoad = true;
                }
                else if (e.Key == Key.F2)
                {
                    if (CurrentUser.CurrentDatabase)
                    {
                        CurrentUser.CurrentDatabase = false;
                        LblOld.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CurrentUser.CurrentDatabase = true;
                        LblOld.Visibility = Visibility.Hidden;
                    }
                    LogIn_Click(sender, e);
                }
                else if (e.Key == Key.F3)
                {
                    if (CurrentUser.CurrentDatabase)
                    {
                        CurrentUser.CurrentDatabase = false;
                        LblOld.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CurrentUser.CurrentDatabase = true;
                        LblOld.Visibility = Visibility.Hidden;
                    }
                    SkipMapLoad = true;
                    LogIn_Click(sender, e);
                }
            }
        }
      

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Activate();
            this.Focus();
            TbxPassword.Focus(); 
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            if (TbxPasswordShow.Visibility == Visibility.Hidden)
            {
                TbxPasswordShow.Visibility = Visibility.Visible;
                TbxPassword.Visibility = Visibility.Hidden;
            }
            else
            {
                TbxPasswordShow.Visibility = Visibility.Hidden;
                TbxPassword.Visibility = Visibility.Visible;
            }
            //if (TbxPassword.PasswordChar == '*')
            //    TbxPassword.show.PasswordChar = '';
            //else TbxPassword.PasswordChar = '*';
        }

        private void TbxPasswordShow_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            TbxPassword.Text = TbxPasswordShow.Text;
        }

        private void TbxPassword_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            TbxPasswordShow.Text = TbxPassword.Text;
        }
    }
}
