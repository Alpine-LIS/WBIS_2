using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("thp_areas")]
    [DisplayOrder(Index = 4), TypeGrouper(IgnoreGroups = true)]
    public class THP_Area : IInformationType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        [Required, Column("thp_name"), ListInfo(DisplayField = true), ImportAttribute(Required = true)]
        public string THPName { get; set; }


        [ListInfo(ChildField = true)]
        public ICollection<BotanicalScoping> BotanicalScopings { get; set; }
        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }

        [ListInfo(ChildField = true)]
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }

        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<THP_Area>();
    }
}
