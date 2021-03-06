using Alpine.EntityFrameworkCore.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [Table("application_users")]
    public class ApplicationUser: IInformationType, IApplicationUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public Guid Id { get; set; }
        [Required,Column("user_name"), ImportAttribute(Required = true), ListInfo(DisplayField = true)]
        public string UserName { get; set; }

        [Required, Column("application_group_id")]
       public Guid ApplicationGroupId { get; set; }
        public ApplicationGroup ApplicationGroup { get; set; }

        [Column("wildlife")]
        public bool Wildlife { get; set; } = false;
        [Column("botany")]
        public bool Botany { get; set; } = false;

        [Column("auto_filter_active_units")]
        public bool AutoFilterActiveUnits { get; set; } = false;


        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsApplicationAdministrator { get; set; } = false;


        //public ICollection<ApplicationUser> Contractors { get; set; }
        public ICollection<District> Districts { get; set; }


        [InverseProperty("User")]
        public ICollection<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        [InverseProperty("User")]
        public ICollection<SiteCalling> SiteCallings { get; set; }
        [InverseProperty("User")]
        public ICollection<SiteCallingDetection> SiteCallingDetections { get; set; }
        [InverseProperty("User")]
        public ICollection<OwlBanding> OwlBandings { get; set; }
        [InverseProperty("User")]
        public ICollection<AmphibianSurvey> AmphibianSurveys { get; set; }
        [InverseProperty("User")]
        public ICollection<AmphibianElement> AmphibianElements { get; set; }
        [InverseProperty("User")]
        public ICollection<ProtectionZone> ProtectionZones { get; set; }
        [InverseProperty("User")]
        public ICollection<PermanentCallStation> PermanentCallStations { get; set; }
        [InverseProperty("User")]
        public ICollection<BotanicalElement> BotanicalElements { get; set; }
        [InverseProperty("User")]
        public ICollection<BotanicalScoping> BotanicalScopings { get; set; }
        [InverseProperty("User")]
        public ICollection<BotanicalSurvey> BotanicalSurveys { get; set; }
        [InverseProperty("User")]
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }

        [InverseProperty("User")]
        public ICollection<UserFlexRecord> UserFlexRecords { get; set; }




        [InverseProperty("UserModified")]
        public ICollection<Hex160RequiredPass> Hex160RequiredPassesModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<SiteCalling> SiteCallingsModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<SiteCallingDetection> SiteCallingDetectionsModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<OwlBanding> OwlBandingsModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<AmphibianSurvey> AmphibianSurveysModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<AmphibianElement> AmphibianElementsModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<ProtectionZone> ProtectionZonesModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<PermanentCallStation> PermanentCallStationsModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<BotanicalElement> BotanicalElementsModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<BotanicalScoping> BotanicalScopingsModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<BotanicalSurvey> BotanicalSurveysModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<BotanicalSurveyArea> BotanicalSurveyAreasModified { get; set; }
        [InverseProperty("UserModified")]
        public ICollection<UserFlexRecord> UserFlexRecordUserModifieds { get; set; }

        [InverseProperty("ApplicationUser")]
        public ICollection<UserMapLayer> UserMapLayers { get; set; }



        public ICollection<Hex160> ActiveHex160s { get; set; }
        public ICollection<BotanicalSurveyArea> ActiveBotanicalSurveyAreas { get; set; }




        [NotMapped, Display(Order = -1)]
        public IInfoTypeManager Manager => new InformationTypeManager<ApplicationUser>();      
    }
}
