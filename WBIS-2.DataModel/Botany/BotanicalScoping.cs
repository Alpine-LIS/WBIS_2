using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BotanicalScoping : UserDataValidator, IUserRecords, IQueryStuff<AmphibianSurvey>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }


      

        public ICollection<BotanicalElement> BotanicalElements { get; set; }
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }


        [Column("thp_area_id")]
        public Guid THP_AreaId { get; set; }
        public THP_Area THP_Area { get; set; }

        
        public string Forester { get; set; }

        [Column("region_id")]
        public Guid RegionId { get; set; }
        public Region Region { get; set; }

        [Column("ecological_unit")]
        public string EcologicalUnit { get; set; }
        [Column("elevation_max")]
        public int ElevationMax { get; set; }
        [Column("elevation_min")]
        public int ElevationMin { get; set; }
        [Column("wshd_elevation_max")]
        public int WshdElevationMax { get; set; }
        [Column("wshd_elevation_min")]
        public int WshdElevationMin { get; set; }




        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Display(Order = -1)]
        public bool _delete { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = CurrentUser.User;

        public ICollection<District> Districts { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }





        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Amphibian Survay"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }
        public Expression<Func<AmphibianSurvey, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<AmphibianSurvey, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            else
                a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
