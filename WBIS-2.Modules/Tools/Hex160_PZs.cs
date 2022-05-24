using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.Tools
{
    public class Hex160_PZs
    {
        WBIS2Model Database = new WBIS2Model();
        
        /// <summary>
        /// sets if repository or _delete is being modified.
        /// </summary>
        PropertyInfo property { get; set; }
        public Hex160_PZs(string[] alteredHex160s)
        {
            foreach(string hexId in alteredHex160s)
            {
                var hex = Database.Hex160s.Include(_=>_.ProtectionZones).First(_=>_.Hex160ID == hexId);
                if (hex.ProtectionZones.Count ==0)
                    hex.CurrentProtectionZone = null;
                else if (hex.ProtectionZones.Count == 1)
                    hex.CurrentProtectionZone = hex.ProtectionZones.First();
                else
                {
                    double dist = hex.ProtectionZones.Min(_ => _.Geometry.Distance(hex.Geometry));
                    hex.CurrentProtectionZone = hex.ProtectionZones.First(_ => _.Geometry.Distance(hex.Geometry) == dist);
                }
            }
            Database.SaveChanges();
        }
    }
}
