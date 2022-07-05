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
    /// Interaction logic for SetUserPasswordControl.xaml
    /// </summary>
    public partial class SetUserPasswordControl : UserControl
    {
        WBIS2Model WBIS2Model = new WBIS2Model();
        public ApplicationUser ReturnApplicationUser { get; set; }
        public string ReturnPassword { get; set; }
        public SetUserPasswordControl(ApplicationUser user)
        {
            InitializeComponent();
            ReturnApplicationUser = user;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (TbxPassword.Text.Length == 0)
            {
                MessageBox.Show("The password length must be greater than 0.");
                return;
            }
            if (TbxPassword.Text != TbxConfirmPassword.Text)
            {
                MessageBox.Show("The passwords must match.");
                return;
            }

            string Accurate = PasswordTools.HashPassword(TbxPassword.Text + "#com.alpinelis.rmsdataapp#");

            var user = WBIS2Model.ApplicationUsers.First(_=>_.Id == ReturnApplicationUser.Id);
            //user.PasswordSHA = Accurate;// PasswordTools.HashPassword(NewPassword + "#com.alpinelis.rmsdataapp#");
            //user.PasswordTimestamp = DateTime.Now;
            WBIS2Model.ApplicationUsers.Update(user);
            WBIS2Model.SaveChanges();
            ReturnPassword = Accurate;

            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }
    }
}
