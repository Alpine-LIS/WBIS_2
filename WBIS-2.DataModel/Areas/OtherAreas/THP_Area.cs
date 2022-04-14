using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public class THP_Area : IInformationType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("thp_name")]
        public string THPName { get; set; }


        public ICollection<BotanicalScoping> BotanicalScopins { get; set; }
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager { get { return new THP_AreaManager(); } }
    }
}
