﻿using Microsoft.EntityFrameworkCore;
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
    public class BotanicalSurvey : UserDataValidator, IUserRecords, INonPointParents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }


        [Required, Column("botanical_survey_area_id")]
        public Guid BotanicalSurveyAreaId { get; set; }
        public BotanicalSurveyArea BotanicalSurveyArea { get; set; }

        [Column("botanical_scoping_id")]
        public Guid BotanicalScopingId { get; set; }
        public BotanicalScoping BotanicalScoping { get; set; }

        public ICollection<BotanicalElement> BotanicalElement { get; set; }


        [Column("survey_name")]
        public string SurveyName { get; set; }
        [Column("other_surveyors")]
        public string OtherSurveyors { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        [Column("time_spent")]
        public TimeSpan TimeSpent { get; set; }

        [Column("manual_track")]
        public bool ManualTrack { get; set; } = false;





        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        [Display(Order = -1)]
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }





        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }





        [Required, Column("geometry", TypeName = "geometry(LineString,26710)")]
        public LineString Geometry { get; set; }



        //[Column("device_info_id")]
        //public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }






        [Column("district_id")]
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Watershed> Watersheds { get; set; }
        public ICollection<Quad75> Quad75s { get; set; }
        public ICollection<Hex160> Hex160s { get; set; }


        [Column("thp_area_id")]
        public Guid THP_AreaId { get; set; }
        public THP_Area THP_Area { get; set; }


        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager<IInformationType> Manager => (IInfoTypeManager<IInformationType>)new BotanicalSurveyManager();
    }

    public class BotanicalSurveyManager : IInfoTypeManager<BotanicalSurvey>
    {
        public string DisplayName { get { return "Botanical Survey"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[] { new BotanicalElement() }; }
        }

        public IQueryable<BotanicalSurvey> GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<BotanicalSurvey>();
            var a = (Expression<Func<BotanicalSurvey, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                return returnVal.Include(_ => _.District).Where(a);
            else if (QueryType == typeof(Watershed))
                return returnVal.Include(_ => _.Watersheds).Where(a);
            else if (QueryType == typeof(Quad75))
                return returnVal.Include(_ => _.Quad75s).Where(a);
            else if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160s).Where(a);
            else if (QueryType == typeof(BotanicalScoping))
                return returnVal.Include(_ => _.BotanicalScoping).Where(a);
            else if (QueryType == typeof(BotanicalSurveyArea))
                return returnVal.Include(_ => _.BotanicalSurveyArea).Where(a);

            return returnVal.Where(a);
        }
        public Expression<Func<BotanicalSurvey, bool>> GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<BotanicalSurvey, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => _.Watersheds.Any(d => Query.Cast<Watershed>().Contains(d));
            else if (QueryType == typeof(Quad75))
                a = _ => _.Quad75s.Any(d => Query.Cast<Quad75>().Contains(d));
            else if (QueryType == typeof(Hex160))
                a = _ => _.Hex160s.Any(d => Query.Cast<Hex160>().Contains(d));
            else if (QueryType == typeof(BotanicalScoping))
                a = _ => Query.Cast<BotanicalScoping>().Contains(_.BotanicalScoping);
            else if (QueryType == typeof(BotanicalSurveyArea))
                a = _ => Query.Cast<BotanicalSurveyArea>().Contains(_.BotanicalSurveyArea);
            else
                a = _ => Query.Contains(_);
            return a;
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}
