using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace WBIS_2.DataModel
{
    public class District : IInformationType, IQueryStuff<District>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("district_name")]
        public string DistrictName { get; set; }
        [Required, Column("management_area")]
        public string ManagementArea { get; set; }



        [Column("geometry"), DataType("geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }


        public ICollection<Hex160> Hex160s { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }

      


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "District"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new Watershed(), new Hex160(), new SiteCalling(), new CNDDBOccurrence(), new CDFW_SpottedOwl() }; }
        }
        public Expression<Func<District, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<District, bool>> a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("DistrictName", "District"),
                new KeyValuePair<string, string>("ManagementArea", "District")};
            }
        }
    }
}
