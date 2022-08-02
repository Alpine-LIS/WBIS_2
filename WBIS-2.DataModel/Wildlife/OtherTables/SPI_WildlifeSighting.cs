using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("spi_wildlife_sightings")]
    [TypeGrouper(GroupName = "Wildlife")]
    public class SPI_WildlifeSighting :  IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }


        [Column("wildlife_species"), Import]
        public string WildlifeSpecies { get; set; }
        [Column("genus"), Import]
        public string Genus { get; set; }
        [Column("species"), Import]
        public string Species { get; set; }
        [Column("year"), Import]
        public int Year { get; set; }
        [Column("num_observed"), Import]
        public int NumObserved { get; set; }
        [Column("activity_observed"), Import]
        public string ActivityObserved { get; set; }
        [Column("longitude"), Import]
        public double Longitude { get; set; }
        [Column("latitude"), Import]
        public double Latitude { get; set; }
        [Column("iucn_rating"), Import]
        public string IUCN_Rating { get; set; }



        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        [ListInfo(AutoInclude = true), Import]
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        [ListInfo(AutoInclude = true), Import]
        public Watershed Watershed { get; set; }

        public bool _delete { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPI_WildlifeSighting>();
    }
}
