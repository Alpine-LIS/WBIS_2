using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.IO;
using System.Linq;

namespace WBIS_2.DataModel
{
    public class ListInfo : Attribute
    {
        /// <summary>
        /// Fields to be included for a list view query regardless of the parent.
        /// </summary>
        public bool AutoInclude { get; set; } = false;
        /// <summary>
        /// Fields to be displayed by auto included properties.
        /// </summary>
        public bool DisplayField { get; set; } = false;
        /// <summary>
        /// Child information types
        /// </summary>
        public bool ChildField { get; set; } = false;
    }
}
