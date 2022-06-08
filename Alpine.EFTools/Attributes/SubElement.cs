using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.IO;
using System.Linq;

namespace Alpine.EFTools.Attributes
{
    /// <summary>
    /// Inicates what information type this is a sub element of.
    /// </summary>
    public class SubElement : Attribute
    {
        public SubElement(Type parentType) => ParentType = parentType;
        public Type ParentType { get; set; }
    }
}
