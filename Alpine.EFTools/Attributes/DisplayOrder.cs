using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.IO;
using System.Linq;

namespace Alpine.EFTools.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All ,Inherited = true)]
    public class DisplayOrder : Attribute
    {        
        public int Index { get; set; } = int.MaxValue;
    }
}
