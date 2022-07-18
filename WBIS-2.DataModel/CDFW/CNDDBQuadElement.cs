using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("cnddb_quad_elements")]
    [DisplayOrder(Index = 6), TypeGrouper(IgnoreGroups = true)]
    public class CNDDBQuadElement : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Column("elm_type")]
        public string ElmType { get; set; }
        [Column("sci_name")]
        public string SciName { get; set; }
        [Column("common_name")]
        public string CommonName { get; set; }
        [Column("elm_code")]
        public string ElmCode { get; set; }
        [Column("fed_status")]
        public string FedStatus { get; set; }
        [Column("cal_status")]
        public string CalStatus { get; set; }
        [Column("cdfw_status")]
        public string CDFWStatus { get; set; }
        [Column("rare_plant_rank")]
        public string RPlantRank { get; set; }
        [Column("data_status")]
        public string DataStatus { get; set; }
        [Column("taxon_sort")]
        public string TaxonSort { get; set; }


        public ICollection<District> Districts { get; set; }

        [Column("quad75_id")]
        public Guid? Quad75Id { get; set; }
        [ListInfo(AutoInclude = true)]
        public Quad75 Quad75 { get; set; }


        [Column("plant_species_id")]
        public Guid? PlantSpeciesId { get; set; }
        //[ListInfo(AutoInclude = true)]
        public PlantSpecies PlantSpecies { get; set; }



        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<CNDDBQuadElement>();
    }
}
