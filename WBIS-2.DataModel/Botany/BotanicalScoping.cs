using Alpine.EFTools.Attributes;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [SubstituteLayer(typeof(Watershed)),DisplayOrder(Index = 8), TypeGrouper(GroupName = "Botany")]
    public class BotanicalScoping : UserDataValidator, IUserRecords
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }




        [ListInfo(ChildField = true)]
        public ICollection<BotanicalElement> BotanicalElements { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        //[ListInfo(ChildField = true)]
        public ICollection<BotanicalScopingSpecies> BotanicalScopingSpecies { get; set; }


        [Column("thp_area_id")]
        public Guid THP_AreaId { get; set; }
        [ListInfo(AutoInclude = true)]
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
        //[Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }



        [Column("user_id")]
        public Guid? UserId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser UserModified { get; set; }





        [ListInfo(AutoInclude = true)]
        public ICollection<District> Districts { get; set; }
        [ListInfo(AutoInclude = true)]
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }





        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<BotanicalScoping>() { };
    }
}
