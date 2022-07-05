using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [TypeGrouper(GroupName = "Wildlife")]
    public class SPI_WildlifeSighting :  IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }


        [Column("wildlife_species")]
        public string WildlifeSpecies { get; set; }
        [Column("genus")]
        public string Genus { get; set; }
        [Column("species")]
        public string Species { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("num_observed")]
        public int NumObserved { get; set; }
        [Column("activity_observed")]
        public string ActivityObserved { get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }
        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("iucn_rating")]
        public string IUCN_Rating { get; set; }



        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true)]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }
        


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_WildlifeSighting>();
    }
}
