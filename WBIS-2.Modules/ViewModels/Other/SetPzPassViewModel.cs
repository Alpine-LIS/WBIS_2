using DevExpress.Mvvm;
using System.Linq;
using System.Windows;
using System;
using System.Windows.Media.Imaging;
using System.IO;
using DevExpress.Xpf.LayoutControl;
using WBIS_2.DataModel;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WBIS_2.Modules.ViewModels
{
    public class SetPzPassViewModel : BindableBase
    {
        WBIS2Model Database = new WBIS2Model();
        SiteCalling[] SiteCallings { get; set; }
        public SetPzPassViewModel(SiteCalling[] siteCallings)
        {
            SiteCallings = siteCallings;
        }

        [Required, Range(0,999)]
        public int PassNumber { get; set; } = 0;
      
        public bool SetPasses()
        {           
            foreach(var s in SiteCallings)
            {
                var site = Database.SiteCallings
                    .First(_=>_.Guid == s.Guid);
                site.PZPassNumber = PassNumber;
            }
            Database.SaveChanges();

            return true;
        }      
    }
}