using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("plant_species")]
    public class PlantSpecies : UserDataValidator, IInformationType, IPlaceHolder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }

        [Column("sci_name"), ImportAttribute(Required = true), ListInfo(DisplayField = true)]
        public string SciName { get; set; }
        [Column("com_name"), ImportAttribute(Required = false), ListInfo(DisplayField = true)]
        public string ComName { get; set; }
        [Column("taxon_group")]
        public string TaxonGroup { get; set; }
        [Column("elm_code")]
        public string ElmCode { get; set; }
        [Column("fed_list")]
        public string FedList { get; set; }
        [Column("cal_list")]
        public string CalList { get; set; }
        [Column("g_rank")]
        public string GRank { get; set; }
        [Column("s_rank")]
        public string SRank { get; set; }
        [Column("r_plant_rank")]
        public string RPlantRank { get; set; }
        [Column("other_status")]
        public string OtherStatus { get; set; }
        [Column("habitats")]
        public string Habitats { get; set; }
        [Column("gen_habitat")]
        public string GenHabitat { get; set; }
        [Column("micro_habitat")]
        public string MicroHabitat { get; set; }
        [Column("spi_habitat")]
        public string SpiHabitat { get; set; }
        [Column("family")]
        public string Family { get; set; }
        [Column("species_code")]
        public string SpeciesCode { get; set; }
        [Column("place_holder")]
        public bool PlaceHolder { get; set; }

        //public ICollection<BotanicalScopingSpecies> BotanicalScopingSpecies { get; set; }
        public ICollection<BotanicalPlantOfInterest> BotanicalPlantsOfInterest { get; set; }
        public ICollection<BotanicalPlantList> BotanicalPlantsList { get; set; }
        public ICollection<RegionalPlantList> RegionalPlantLists { get; set; }
        public ICollection<FloweringTimeline> FloweringTimelines { get; set; }
        public ICollection<PlantProtectionSummary> PlantProtectionSummaries { get; set; }
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }
        public ICollection<SPIPlantPolygon> SPIPlantPolys { get; set; }
        public ICollection<BotanicalScopingSpecies> BotanicalScopingSpecies { get; set; }
        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public ICollection<CNDDBQuadElement> CNDDBQuadElements { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<PlantSpecies>();
    }
}
