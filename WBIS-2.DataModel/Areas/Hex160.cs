﻿using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WBIS_2.DataModel
{
    public interface IChild
    {
        public ICollection<Type> Parents { get; }
    }

    public interface IParentModel
    {
        ICollection<IChild> Childrens { get; }
    }
    public class Hex160 : IInformationType, IQueryStuff, IParentModel//<Hex160>
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Required, Column("hex160_id")]
        public string Hex160ID { get; set; }
        [Column("calling_responses")]
        public int CallingResponses { get; set; }
        [Column("follow_ups")]
        public int FollowUps { get; set; }
        [Column("skips")]
        public int Skips { get; set; }
        [Column("drops")]
        public int Drops { get; set; }
        [Column("recent_activity")]
        public string RecentActivity { get; set; }
        [Column("latest_activity")]
        public DateTime LatestActivity { get; set; }



        [Column("geometry", TypeName = "geometry(Polygon,26710)")]
        public Polygon Geometry { get; set; }

        [Column("current_preotection_zone_id"), ForeignKey("ProtectionZone")]
        public Guid? CurrentProtectionZoneID { get; set; }
        public ProtectionZone CurrentProtectionZone { get; set; }
        public ICollection<ProtectionZone> ProtectionZones { get; set; }
        public ICollection<PermanentCallStation> PermanentCallStations { get; set; }

        public ICollection<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        public ICollection<SiteCalling> SiteCallings { get; set; }
        public ICollection<OwlBanding> OwlBandings { get; set; }
        public ICollection<AmphibianSurvey> AmphibianSurveys { get; set; }
        public ICollection<AmphibianElement> AmphibianElements { get; set; }
        public ICollection<SPIPlantPolygon> SPIPlantPolygons { get; set; }
        public ICollection<SPIPlantPoint> SPIPlantPoints { get; set; }


        public ICollection<District> Districts { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }

        public ICollection<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public ICollection<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }

        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        public ICollection<BotanicalElement> BotanicalElements { get; set; }


        [NotMapped, Display(Order = -1)]
        public static string DisplayName { get { return "Hex160"; } }

        [NotMapped] 
        public ICollection<IChild> Childrens
        {
            get
            {
                //return new List<IChild>();
                WBIS2Model context = new WBIS2Model();
                var s = context.Model.FindEntityTypes(typeof(IChild));
                s = s.Where(_ => (_ as IChild).Parents.Contains(this.GetType()));
                s = s.Where(_ => _.GetType().GetProperties().FirstOrDefault(_ => _.GetType() == typeof(Hex160) || _.GetType() == typeof(ICollection<Hex160>)) != null);
                return (ICollection<IChild>)s;
            }
        }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            {
                return new IInformationType[] { new ProtectionZone(), new PermanentCallStation(), new Hex160RequiredPass(), new SiteCalling(), new OwlBanding(),
                new AmphibianSurvey(), new AmphibianElement(), new SPIPlantPolygon(), new SPIPlantPoint(),
            new CNDDBOccurrence(),new CDFW_SpottedOwl(),new BotanicalSurveyArea(),new BotanicalSurvey(),new BotanicalElement()};
            }
        }
        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<Hex160>();
            var a = (Expression<Func<Hex160, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                return returnVal.Include(_ => _.Districts).Where(a);
            else if (QueryType == typeof(Watershed))
                return returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Quad75))
                return returnVal.Include(_ => _.Quad75s).Where(a);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<Hex160, bool>> a;
            if (QueryType == typeof(District))
                a = _ => _.Districts.Any(d => Query.Cast<District>().Contains(d));
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else
                a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>()
                { new KeyValuePair<string, string>("Hex160ID", "Hex160")};
            }
        }
    }
}
