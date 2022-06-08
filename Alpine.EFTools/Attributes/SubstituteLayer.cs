using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.IO;
using System.Linq;

namespace Alpine.EFTools.Attributes
{
    /// <summary>
    /// Soe information types have no geometry and defer to another layer.
    /// </summary>
    public class SubstituteLayer : Attribute
    {
        public SubstituteLayer(Type _SubLayer) => SubLayer = _SubLayer;
        public Type SubLayer { get; set; }
    }
}
