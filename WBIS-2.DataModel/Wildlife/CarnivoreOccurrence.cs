using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [DisplayOrder(Index = 27)]
    public class CarnivoreOccurrence : UserDataValidator, IInformationType, IWildlifeRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        public bool _delete { get; set; }




        [Required, Column("wildlife_species_id")]
        public Guid WildlifeSpeciesId { get; set; }
        [ListInfo(AutoInclude = true)]
        public WildlifeSpecies WildlifeSpecies { get; set; }

        [Column("date_time"),]
        public DateTime DateTime { get; set; }

        [Column("forest_carnivore_camera_station_id"),]
        public Guid ForestCarnivoreCameraStationId { get; set; }
        public ForestCarnivoreCameraStation ForestCarnivoreCameraStation { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new InformationTypeManager<CarnivoreOccurrence>(); } }
    }
}
