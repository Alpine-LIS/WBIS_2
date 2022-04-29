﻿using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WBIS_2.DataModel
{
    public class BotanicalPlantList : UserDataValidator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid"), ForeignKey("BotanicalElement")]
        public Guid Guid { get; set; }

        //[Required, Column("botanical_element_id"),ForeignKey("BotanicalElement")]
        //public Guid BotanicalElementId { get; set; }
        public BotanicalElement BotanicalElement { get; set; }


        [Column("botanical_plant_of_interest_id")]
        public Guid? BotanicalPlantOfInterestId { get; set; }
        public BotanicalPlantOfInterest BotanicalPlantOfInterest { get;set;}


        [Column("plant_species_id")]
        public Guid PlantSpeciesId { get; set; }
        public PlantSpecies PlantSpecies { get; set; }

        [Required, Column("date_time")]
        public DateTime DateTime { get; set; }
        [Column("comments")]
        public string Comments { get; set; }
    }
}
