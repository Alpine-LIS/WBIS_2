using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.IO;
using System.Linq;

namespace WBIS_2.DataModel
{
    /// <summary>
    /// An attribute for properties for if they can be used for imports and if they're required.
    /// </summary>
    public class ImportAttribute : Attribute
    {
        //public ImportAttribute(params object[] t)
        //{
        //    AcceptableChoices = t;
        //}
        public bool Required { get; set; } = false;
        //public object[] AcceptableChoices { get; set; }
        //public bool AcceptableValue(object val)
        //{
        //    if (AcceptableChoices == null) return true;
        //    if (AcceptableChoices.Length == 0) return true;
        //    return AcceptableChoices.Contains(val);
        //}
    }
    public class ListInfo : Attribute
    {
        public bool AutoInclude { get; set; } = false;
        public bool DisplayField { get; set; } = false;
    }
}
