using Alpine.FlexForms;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WBIS_2.DataModel;

public class UserFlexRecord : IUserRecords
{
    [Column("guid")]
    [Key]
    public Guid Guid { get; set; }
    [Column("data_form_id")]
    [Required]
    public Guid DataFormID { get; set; }
    public DataForm DataForm { get; set; } = null!;




    [Column("date_added")]
    public DateTime DateAdded { get; set; }
    [Column("date_modified")]
    public DateTime DateModified { get; set; }
    //[Display(Order = -1)]
    public bool _delete { get; set; }
    [Column("repository")]
    public bool Repository { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }
    [ListInfo(AutoInclude = true)]
    public ApplicationUser User { get; set; }
    [Column("user_modified_id")]
    public Guid? UserModifiedId { get; set; }
    [ListInfo(AutoInclude = true)]
    public ApplicationUser UserModified { get; set; }

    [NotMapped, Display(Order = -1)]
    public IInfoTypeManager Manager => new InformationTypeManager<UserFlexRecord>() { };
}
