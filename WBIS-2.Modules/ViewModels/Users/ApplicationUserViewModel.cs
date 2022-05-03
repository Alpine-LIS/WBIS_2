using DevExpress.Data.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Microsoft.EntityFrameworkCore;
using WBIS_2.DataModel;
using WBIS_2.Modules.Tools;
using WBIS_2.Modules.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WBIS_2.Modules.ViewModels
{
    public class ApplicationUserViewModel : WBISViewModelBase, IDocumentContent
    {
        public ApplicationUser User { get; set; }
        public object Title { get {
                if (User.UserName != null) return $"User: {User.UserName} {ChangedSign}";
                else return $"User: (New User)";
            }
        }
        public static ApplicationUserViewModel Create(ApplicationUser user)
        {
            return ViewModelSource.Create(() => new ApplicationUserViewModel(user)
            {
                Caption = user.UserName,
                Content = user.UserName
            });
        }

        //public string NewPassword { get; set; }
        //public string ConfirmPassword { get; set; }


        public override void CloseForm()
        {
            var w = Application.Current.Windows.Cast<Window>().FirstOrDefault(_ => _.Uid == "ApplicationUserViewModel");// "AddNewUser");
            if (w == null)
            {
                IModuleManager Manager = ModuleManager.DefaultManager;
                Manager.Remove(Common.Regions.Documents, typeof(ApplicationUser).Name);
            }
            else
            {
                w.DialogResult = false;
                w.Close();
            }
        }

        public void OnClose(CancelEventArgs e)
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

        public void OnDestroy()
        {
           
        }

        protected ApplicationUserViewModel(ApplicationUser user)
        {
            User = Database.ApplicationUsers
                .Include(_ => _.ApplicationGroup).ThenInclude(r => r.ApplicationUsers)
                .Include(_ => _.Districts)
                .First(_ => _.Guid == user.Guid);
            if (User == null) User = user;

            ApplicationGroups = Database.ApplicationGroups.Where(_=>_.GroupName != "Admin").ToArray();
            CreateDistrictList();
            RaisePropertyChanged(nameof(DistrictList));
            CreateLayerList();
            RaisePropertyChanged(nameof(LayerList));
        }
        public ApplicationGroup[] ApplicationGroups { get; set; }
        public override void Tracker_ChangesSaved(object sender, IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> e)
        {
        }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public override void Save()
        {
            if (HasErrors() )
            {
                MessageBox.Show("Please ensure that all field requirements are met.");
                return;
            }

            if (NewPassword != null || ConfirmPassword != null)
            {
                if (NewPassword != ConfirmPassword)
                {
                    MessageBox.Show("The passwords must match");
                }
                User.PasswordSHA = PasswordTools.HashPassword(NewPassword + "#com.alpinelis.rmsdataapp#");
                User.PasswordTimestamp = DateTime.Now;
            }

            if (DistrictList.Count(_ => _.IsSelected) == 0)
            {
                MessageBox.Show("There must be at least on district selected.");
                return;
            }


            User.Districts = new List<District>();
            User.Districts = DistrictList.Where(_ => _.IsSelected).Select(_ => _.District).ToList();
            ApplyGroup();

            if (!Database.ApplicationUsers.Contains(User)) Database.ApplicationUsers.Add(User);
            else Database.ApplicationUsers.Update(User);

            Database.SaveChanges();
            RaisePropertyChanged("Title");
            this.Changed = false;

            if (CurrentUser.User != null) CurrentUser.User = User;
            
        }

    
        private void ApplyGroup()
        {
            if (User.ApplicationGroup == null) return;

            var groups = Database.ApplicationGroups.Where(_ => _.ApplicationUsers.Contains(User));
            foreach (var g in groups)
            {
                g.ApplicationUsers.Remove(User);
                //Database.Counties.Update(d);
            }

            if (User.ApplicationGroup.ApplicationUsers == null) User.ApplicationGroup.ApplicationUsers = new List<ApplicationUser>();
            User.ApplicationGroup.ApplicationUsers.Add(User);
            //Database.Counties.Update(Regen.County);
        }

        public ObservableCollection<DistrictBoolStringClass> DistrictList { get; set; }
        private void CreateDistrictList()
        {
            DistrictList = new ObservableCollection<DistrictBoolStringClass>();
            foreach (District district in Database.Districts)
            {
                DistrictList.Add(new DistrictBoolStringClass() { District = district, IsSelected = User.Districts.Contains(district) });
            }
        }





        public ObservableCollection<StringBoolStringClass> LayerList { get; set; }
        private void CreateLayerList()
        {
            //servi




            LayerList = new ObservableCollection<StringBoolStringClass>();
            foreach (string layer in CurrentUser.AllLayers)
            {
                LayerList.Add(new StringBoolStringClass() { Layer = layer, IsSelected = User.Districts.Contains(district) });
            }
        }


        public class DistrictBoolStringClass
        {
            public District District { get; set; }
            public bool IsSelected { get; set; }
        }
        public class StringBoolStringClass
        {
            public string Layer { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
