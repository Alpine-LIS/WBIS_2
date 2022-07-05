using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WBIS_2.DataModel
{
    public class ApplicationGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        [Required,Column("group_name")]
        public string GroupName { get; set; }
        [Required,Column("admin_privileges")]
        public bool AdminPrivileges { get; set; } = false;
        [Required,Column("read_only")]
        public bool ReadOnly { get; set; } = false;
        [Required, Column("desktop_access")]
        public bool DesktopAccess { get; set; } = false;
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
