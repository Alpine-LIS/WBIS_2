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
    public class SPIPlantPoint: IInformationType, IPointParents, IQueryStuff<SPIPlantPoint>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
       
        [Required, Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }    
        public PlantSpecies PlantSpecies { get; set; }
        [Required, Column("geometry", TypeName = "geometry(Point,26710)")]
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
        public Guid DistrictId { get; set; }
        public District District { get; set; }
        [Column("watershed_id")]
        public Guid WatershedId { get; set; }
        public Watershed Watershed { get; set; }
        [Column("quad75_id")]
        public Guid Quad75Id { get; set; }
        public Quad75 Quad75 { get; set; }
        [Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "SPI Plant Point"; } }

        public IInformationType[] AvailibleChildren => throw new NotImplementedException();

        public List<KeyValuePair<string, string>> DisplayFields => throw new NotImplementedException();

        public Expression<Func<SPIPlantPoint, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            throw new NotImplementedException();
        }
    }
}
