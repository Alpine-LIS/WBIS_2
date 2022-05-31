using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [DisplayOrder(Index = 23)]
    public class BDOWSighting : UserDataValidator, IUserRecords, IPointParents, IPointLayer, IWildlifeRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }


        [Column("date")]
        public DateTime? Date { get; set; }
        [Required, Column("moon_phase")]
        public string MoonPhase { get; set; }

        [Required, Column("species_id")]
        public Guid SpeciesId { get; set; }
        [ImportAttribute(Required = true), ListInfo(AutoInclude = true)]
        public BirdSpecies Species { get; set; }

        [Column("barred_owl_territory_id")]
        public string BarredOwlTerritoryID { get; set; }
        [Column("weight")]
        public double Weight { get; set; }
        [Column("sex")]
        public string Sex { get; set; }
        [Column("wing_chord")]
        public double WingChord { get; set; }
        [Column("tail_length")]
        public double TailLength { get; set; }
        [Column("foot_pad")]
        public double FootPad { get; set; }
        [Column("age")]
        public string Age { get; set; }
        [Column("shotgun")]
        public bool Shotgun { get; set; }
        [Column("shotgun_text")]
        public string ShotgunText { get; set; }
        [Column("ectoparasites_noticed")]
        public bool EctoparasitesNoticed {get;set;}
        [Column("shots_taken")]
        public int ShotsTaken { get; set; }
        [Column("feathers_collected")]
        public int FeathersCollected { get; set; }





        [Column("user_id")]
        public Guid? UserId { get; set; }
        [ImportAttribute(Required = true), ListInfo(AutoInclude = true)]
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid? UserModifiedId { get; set; }
        [ListInfo(AutoInclude = true)]
        public ApplicationUser UserModified { get; set; }


        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("lat")]
        public double Lat { get; set; }
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }


        [Column("comments"), ImportAttribute]
        public string Comments { get; set; }

        public DeviceInfo DeviceInfo { get; set; }






        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }




        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true)]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }
        [Column("quad75_id")]
        public Guid? Quad75Id { get; set; }
        public Quad75 Quad75 { get; set; }
        [Column("hex160_id")]
        public Guid? Hex160Id { get; set; }
        [ListInfo(AutoInclude = true)]
        public Hex160 Hex160 { get; set; }


        public ICollection<Picture> Pictures { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new InformationTypeManager<BDOWSighting>(); } }
    }
}
