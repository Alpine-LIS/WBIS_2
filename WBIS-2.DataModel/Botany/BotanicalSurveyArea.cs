using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BotanicalSurveyArea : UserDataValidator, IUserRecords, INonPointParents, IActiveUnit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }




        [ListInfo(ChildField = true)]
        public ICollection<BotanicalElement> BotanicalElement { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurvey> BotanicalSurvey { get; set; }


        [Column("thp_area_id")]
        public Guid? THP_AreaId { get; set; }
        [ListInfo(AutoInclude = true), ImportAttribute(Required = true)]
        public THP_Area THP_Area { get; set; }

        [Column("botanical_scoping_id")]
        public Guid? BotanicalScopingId { get; set; }
        [ListInfo(ParentField = true)]
        public BotanicalScoping BotanicalScoping { get; set; }


        [Column("area_name"), ImportAttribute(Required = true)]
        public string AreaName { get; set; }
        [Column("survey_type")]
        public string SurveyType { get; set; }
        [Column("general_habitat")]
        public string GeneralHabitat { get; set; }
        [Column("aspect")]
        public string Aspect { get; set; }
        [Column("slope")]
        public string Slope { get; set; }
        [Column("canopy")]
        public string Canopy { get; set; }
        [Column("rock_outcrops")]
        public string RockOutcrops { get; set; }
        [Column("comments")]
        public string Comments { get; set; }
        [Column("boulders")]
        public string Boulders { get; set; }
        [Column("substrate")]
        public string Substrate { get; set; }
        [Column("talus_scree")]
        public bool TalusScree { get; set; } = false;
        [Column("lava_cap")]
        public bool LavaCap { get; set; } = false;
        [Column("spring_seep")]
        public bool SpringSeep { get; set; } = false;
        [Column("pond")]
        public bool Pond { get; set; } = false;
        [Column("other_wetlands")]
        public string OtherWetlands { get; set; }
        [Column("understory_vegetation")]
        public string UnderstoryVegetation { get; set; }
        [Column("surveys")]
        public int Surveys { get; set; }


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


        [Column("is_active")]
        public bool IsActive { get; set; }
        [ListInfo(AutoInclude = true)]
        public ICollection<ApplicationUser> ActiveUsers { get; set; }


        [Column("geometry", TypeName = "geometry(MultiPolygon,26710)")]
        public MultiPolygon Geometry { get; set; }


        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }





        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<BotanicalSurveyArea>();
    }
}
