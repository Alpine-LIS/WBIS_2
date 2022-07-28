using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("spi_plant_points")]
    [DisplayOrder(Index = 13), TypeGrouper(GroupName = "Botany"), GeometryEdits(Locked = false), ReportableTable]
    public class SPIPlantPoint: IInformationType, IPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Required, Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }   
        [ListInfo(AutoInclude = true)]
        public PlantSpecies PlantSpecies { get; set; }
        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("lat")]
        public double Lat { get; set; }
        [Column("lon")]
        public double Lon { get;set; }
        [Column("notes")]
        public string Notes { get; set; }
        [Column("thp")]
        public string THP { get; set; }


        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid? WatershedId { get; set; }
        public Watershed Watershed { get; set; }
        [Column("quad75_id")]
        public Guid? Quad75Id { get; set; }
        public Quad75 Quad75 { get; set; }
        [Column("hex160_id")]
        public Guid? Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }

        public bool _delete { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<SPIPlantPoint>();
    }
}
