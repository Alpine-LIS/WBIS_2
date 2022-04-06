using Microsoft.EntityFrameworkCore;
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
    public class PermanentCallStation : UserDataValidator, IUserRecords, IQueryStuff
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("guid")]
        public Guid Guid { get; set; }
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        [Column("date_modified")]
        public DateTime DateModified { get; set; }
        public bool _delete { get; set; }
        [Column("repository")]
        public bool Repository { get; set; }



        [Column("user_id")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Column("user_modified_id")]
        public Guid UserModifiedId { get; set; }
        public ApplicationUser UserModified { get; set; }




        [Column("geometry", TypeName = "geometry(Point,26710)")]
        public Point Geometry { get; set; }

        [Column("pcs_id")]
        public string PCS_ID { get; set; }

        [Required, Column("hex160_id")]
        public Guid Hex160Id { get; set; }
        public Hex160 Hex160 { get; set; }


        [NotMapped, Display(Order = -1)]
        public string DisplayName { get { return "Permanent Call Stations"; } }
        [NotMapped]
        public ICollection<IChild> Children
        {
            get
            {
                return ParentChildQuerries.GetChildren(this.GetType());
            }
        }
        [NotMapped]
        public ICollection<IParent> Parents
        {
            get
            {
                return ParentChildQuerries.GetParents(this.GetType());
            }
        }

        [NotMapped]
        public IInformationType[] AvailibleChildren
        {
            get
            { return new IInformationType[0]; }
        }

        [NotMapped]
        public List<KeyValuePair<string, string>> DisplayFields
        {
            get
            {
                return new List<KeyValuePair<string, string>>();
            }
        }

        public IQueryable GetQueryable(object[] Query, Type QueryType, WBIS2Model model)
        {
            var returnVal = model.Set<PermanentCallStation>();
            var a = (Expression<Func<PermanentCallStation, bool>>)GetParentWhere(Query, QueryType);

            if (QueryType == typeof(Hex160))
                return returnVal.Include(_ => _.Hex160).Where(a);

            return returnVal.Where(a);
        }
        public Expression GetParentWhere(object[] Query, Type QueryType)
        {
            Expression<Func<PermanentCallStation, bool>> a;
            if (QueryType == typeof(Hex160))
                a = _ => Query.Cast<Hex160>().Contains(_.Hex160);
            a = _ => Query.Contains(_);
            return a;
        }
    }
}
