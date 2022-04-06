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
    public class OwlBanding : UserDataValidator, IUserRecords, IQueryStuff, IPointParents, IPointLayer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
               

        [Required, Column("bands")]
        public string Bands { get; set; }
        [Column("record_type")]
        public string RecordType { get; set; }

       
        
        [Required, Column("bird_species_id")]
        public Guid BirdSpeciesId { get; set; }
        public BirdSpecies BirdSpecies { get; set; }
       
        
        [Column("banding_leg")]
        public string BandingLeg { get; set; }
        [Column("banding_pattern")]
        public string BandingPattern { get; set; }
        [Column("usfws_band_num")]
        public string USFWS_BandNum { get; set; }
        [Column("usfws_band_color")]
        public string USFWS_BandColor { get; set; }
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



        [Column("bander")]
        public string Bander { get; set; }
        [Column("capturer")]
        public string Capturer { get; set; }
        [Column("trap_code")]
        public string TrapCode { get; set; }
        [Column("capture_method")]
        public string CaptureMethod { get; set; }



        [Column("preotection_zone_id")]
        public Guid ProtectionZoneID { get; set; }
        public ProtectionZone ProtectionZone { get; set; }
        [Required, Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }
        [Column("lat")]
        public double Lat { get; set; }
        [Column("lon")]
        public double Lon { get; set; }
        [Column("datum")]
        public string Datum { get; set; }


       // [Column("device_info_id")]
       // public Guid DeviceInfoID { get; set; }
        public DeviceInfo DeviceInfo { get; set; }


        [Column("temperature")]
        public double Temperature { get; set; }

        [Required, Column("start_time")]
        public DateTime StartTime { get; set; }
        [Required, Column("end_time")]
        public DateTime EndTime { get; set; }

        [Column("gps_tag_id")]
        public string GPS_TagId { get; set; }
        [Column("sex")]
        public string Sex { get; set; }
        [Column("age_class")]
        public string AgeClass { get; set; }
        [Column("weight")]
        public double Weight { get; set; }
        [Column("wing_chord")]
        public double WingChord { get; set; }
        [Column("tail_length")]
        public double TailLength { get; set; }
        [Column("footpad")]
        public double Footpad { get; set; }
        [Column("blood")]
        public bool Blood { get; set; }
        [Column("oral_sample")]
        public bool OralSample { get; set; }
        [Column("ectoparasites")]
        public bool Ectoparasites { get; set; }
        [Column("feathers")]
        public bool Feathers { get; set; }
        [Column("comments")]
        public string Comments { get; set; }







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


        public ICollection<Picture> Pictures { get; set; }




        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Owl Banding"; } }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<OwlBanding>();
            var a = (Expression<Func<OwlBanding, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(District))
                return returnVal.Include(_ => _.District).Where(a);
            else if (QueryType == typeof(Watershed))
                return returnVal.Include(_ => _.Watershed).Where(a);
            else if (QueryType == typeof(Quad75))
                return returnVal.Include(_ => _.Quad75).Where(a);
            else if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160).Where(a);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<OwlBanding, bool>> a;
            if (QueryType == typeof(District))
                a = _ => Query.Cast<District>().Contains(_.District);
            else if (QueryType == typeof(Watershed))
                a = _ => Query.Cast<Watershed>().Contains(_.Watershed);
            else if (QueryType == typeof(Quad75))
                a = _ => Query.Cast<Quad75>().Contains(_.Quad75);
            else if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
            a = _ => Query.Contains(_);
            return a;
        }


    }
}
