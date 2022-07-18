using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Alpine.FlexForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NetTopologySuite.Geometries;


namespace WBIS_2.DataModel
{
    public class WBIS2Model : DbContext
    {
        public static string GetRDSConnectionString()
        {
            var hostname = "alpine-database-1.cz1ugaicrz33.us-west-1.rds.amazonaws.com";
            var dbname = "WBIS2";
            var username = "postgres";
            var password = "i$mppWMB$I7Y4XoD";
            return $"Server={hostname};Database={dbname};Username={username};Password={password};Command Timeout=0;Include Error Detail=true";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Batch size limits leangth of querries built by entity framework. If batches are too big it cannot desifer them.
            string location = AppDomain.CurrentDomain.BaseDirectory;
            optionsBuilder.EnableSensitiveDataLogging(true)
                .UseNpgsql(GetRDSConnectionString(),
                o => { o.UseNetTopologySuite(); o.MaxBatchSize(25); })
                //.LogTo(message => Debug.WriteLine(message))
                .EnableDetailedErrors();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //optionsBuilder.UseSnakeCaseNamingConvention();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            
            ManyToManyRelations(modelBuilder);
        }
    private void ManyToManyRelations(ModelBuilder modelBuilder)
        {
            ManyToManyUsers(modelBuilder);
            ManyToManyDistricts(modelBuilder);
            ManyToManyWatersheds(modelBuilder);
            ManyToManyQuad75s(modelBuilder);
            ManyToManyHex160s(modelBuilder);
        }
        private void ManyToManyUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                            .HasMany(_ => _.Districts)
                            .WithMany(p => p.ApplicationUsers)
                            .UsingEntity<Dictionary<string, object>>("users_districts",
                                    x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                                    x => x.HasOne<ApplicationUser>().WithMany().HasForeignKey("application_user_id"),
                                    x => x.ToTable("users_districts", "public"));

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(_ => _.ActiveBotanicalSurveyAreas)
                .WithMany(p => p.ActiveUsers)
                .UsingEntity<Dictionary<string, object>>("active_botanical_survey_areas",
                        x => x.HasOne<BotanicalSurveyArea>().WithMany().HasForeignKey("unit_id"),
                        x => x.HasOne<ApplicationUser>().WithMany().HasForeignKey("application_user_id"),
                        x => x.ToTable("active_botanical_survey_areas", "public"));
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(_ => _.ActiveHex160s)
                .WithMany(p => p.ActiveUsers)
                .UsingEntity<Dictionary<string, object>>("active_hex160s",
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("unit_id"),
                        x => x.HasOne<ApplicationUser>().WithMany().HasForeignKey("application_user_id"),
                        x => x.ToTable("active_hex160s", "public"));
        }
        private void ManyToManyDistricts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
               .HasMany(_ => _.Hex160s)
               .WithMany(p => p.Districts)
               .UsingEntity<Dictionary<string, object>>("hex160s_districts",
                       x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                       x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                       x => x.ToTable("hex160s_districts", "public"));
            modelBuilder.Entity<District>()
                .HasMany(_ => _.Watersheds)
                .WithMany(p => p.Districts)
                .UsingEntity<Dictionary<string, object>>("watersheds_districts",
                        x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.ToTable("watersheds_districts", "public"));
            modelBuilder.Entity<District>()
                .HasMany(_ => _.Quad75s)
                .WithMany(p => p.Districts)
                .UsingEntity<Dictionary<string, object>>("quad75s_districts",
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.ToTable("quad75s_districts", "public"));
            modelBuilder.Entity<District>()
                .HasMany(_ => _.CNDDBOccurrences)
                .WithMany(p => p.Districts)
                .UsingEntity<Dictionary<string, object>>("cnddb_occurrences_districts",
                        x => x.HasOne<CNDDBOccurrence>().WithMany().HasForeignKey("cnddb_occurrence_id"),
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.ToTable("cnddb_occurrences_districts", "public"));
            modelBuilder.Entity<District>()
                .HasMany(_ => _.CNDDBQuadElements)
                .WithMany(p => p.Districts)
                .UsingEntity<Dictionary<string, object>>("cnddb_quad_elements_districts",
                        x => x.HasOne<CNDDBQuadElement>().WithMany().HasForeignKey("cnddb_quad_element_id"),
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.ToTable("cnddb_quad_elements_districts", "public"));
            modelBuilder.Entity<District>()
                .HasMany(_ => _.BotanicalScopings)
                .WithMany(p => p.Districts)
                .UsingEntity<Dictionary<string, object>>("botanical_scopings_districts",
                        x => x.HasOne<BotanicalScoping>().WithMany().HasForeignKey("botanical_scoping_id"),
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.ToTable("botanical_scopings_districts", "public"));
        }
        private void ManyToManyWatersheds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Watershed>()
                           .HasMany(_ => _.CNDDBOccurrences)
                           .WithMany(p => p.Watersheds)
                           .UsingEntity<Dictionary<string, object>>("cnddb_occurrences_watersheds",
                                   x => x.HasOne<CNDDBOccurrence>().WithMany().HasForeignKey("cnddb_occurrence_id"),
                                   x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                                   x => x.ToTable("cnddb_occurrences_watersheds", "public"));
            modelBuilder.Entity<Watershed>()
                .HasMany(_ => _.Hex160s)
                .WithMany(p => p.Watersheds)
                .UsingEntity<Dictionary<string, object>>("hex160s_watersheds",
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                        x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                        x => x.ToTable("hex160s_watersheds", "public"));
            modelBuilder.Entity<Watershed>()
               .HasMany(_ => _.SPIPlantPolygons)
               .WithMany(p => p.Watersheds)
               .UsingEntity<Dictionary<string, object>>("spi_plant_polygons_watersheds",
                       x => x.HasOne<SPIPlantPolygon>().WithMany().HasForeignKey("spi_plant_polygon_id"),
                       x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                       x => x.ToTable("spi_plant_polygons_watersheds", "public"));
            modelBuilder.Entity<Watershed>()
                .HasMany(_ => _.AmphibianSurveys)
                .WithMany(p => p.Watersheds)
                .UsingEntity<Dictionary<string, object>>("amphibian_surveys_watersheds",
                        x => x.HasOne<AmphibianSurvey>().WithMany().HasForeignKey("amphibian_survey_id"),
                        x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                        x => x.ToTable("amphibian_surveys_watersheds", "public"));
            modelBuilder.Entity<Watershed>()
               .HasMany(_ => _.BotanicalScopings)
               .WithMany(p => p.Watersheds)
               .UsingEntity<Dictionary<string, object>>("botanical_scopings_watersheds",
                       x => x.HasOne<BotanicalScoping>().WithMany().HasForeignKey("botanical_scoping_id"),
                       x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                       x => x.ToTable("botanical_scopings_watersheds", "public"));
            modelBuilder.Entity<Watershed>()
                .HasMany(_ => _.BotanicalSurveyAreas)
                .WithMany(p => p.Watersheds)
                .UsingEntity<Dictionary<string, object>>("botanical_survey_areas_watersheds",
                        x => x.HasOne<BotanicalSurveyArea>().WithMany().HasForeignKey("botanical_survey_area_id"),
                        x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                        x => x.ToTable("botanical_survey_areas_watersheds", "public"));
            modelBuilder.Entity<Watershed>()
                .HasMany(_ => _.BotanicalSurveys)
                .WithMany(p => p.Watersheds)
                .UsingEntity<Dictionary<string, object>>("botanical_surveys_watersheds",
                        x => x.HasOne<BotanicalSurvey>().WithMany().HasForeignKey("botanical_survey_id"),
                        x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                        x => x.ToTable("botanical_surveys_watersheds", "public"));
        }
        private void ManyToManyQuad75s(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quad75>()
                           .HasMany(_ => _.Hex160s)
                           .WithMany(p => p.Quad75s)
                           .UsingEntity<Dictionary<string, object>>("hex160s_quad75s",
                                   x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                                   x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                                   x => x.ToTable("hex160s_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.Watersheds)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("watersheds_quad75s",
                        x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("watersheds_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.CNDDBOccurrences)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("cnddb_occurrences_quad75s",
                        x => x.HasOne<CNDDBOccurrence>().WithMany().HasForeignKey("cnddb_occurrence_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("cnddb_occurrences_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.SPIPlantPolygons)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("spi_plant_polygons_quad75s",
                        x => x.HasOne<SPIPlantPolygon>().WithMany().HasForeignKey("spi_plant_polygon_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("spi_plant_polygons_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.AmphibianSurveys)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("amphibian_surveys_quad75s",
                        x => x.HasOne<AmphibianSurvey>().WithMany().HasForeignKey("amphibian_survey_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("amphibian_surveys_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
              .HasMany(_ => _.BotanicalScopings)
              .WithMany(p => p.Quad75s)
              .UsingEntity<Dictionary<string, object>>("botanical_scopings_quad75s",
                      x => x.HasOne<BotanicalScoping>().WithMany().HasForeignKey("botanical_scoping_id"),
                      x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                      x => x.ToTable("botanical_scopings_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.BotanicalSurveyAreas)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("botanical_survey_areas_quad75s",
                        x => x.HasOne<BotanicalSurveyArea>().WithMany().HasForeignKey("botanical_survey_area_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("botanical_survey_areas_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.BotanicalSurveys)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("botanical_surveys_quad75s",
                        x => x.HasOne<BotanicalSurvey>().WithMany().HasForeignKey("botanical_survey_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("botanical_surveys_quad75s", "public"));
        }
        private void ManyToManyHex160s(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hex160>()
                .HasMany(_ => _.ProtectionZones)
                .WithMany(p => p.Hex160s)
                .UsingEntity<Dictionary<string, object>>("hex160s_protection_zones",
                        x => x.HasOne<ProtectionZone>().WithMany().HasForeignKey("protection_zone_id"),
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                        x => x.ToTable("hex160s_protection_zones", "public"));
            modelBuilder.Entity<Hex160>()
                .HasMany(_ => _.CNDDBOccurrences)
                .WithMany(p => p.Hex160s)
                .UsingEntity<Dictionary<string, object>>("cnddb_occurrences_hex160s",
                        x => x.HasOne<CNDDBOccurrence>().WithMany().HasForeignKey("cnddb_occurrence_id"),
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                        x => x.ToTable("cnddb_occurrences_hex160s", "public"));
            modelBuilder.Entity<Hex160>()
                .HasMany(_ => _.AmphibianSurveys)
                .WithMany(p => p.Hex160s)
                .UsingEntity<Dictionary<string, object>>("amphibian_surveys_hex160s",
                        x => x.HasOne<AmphibianSurvey>().WithMany().HasForeignKey("amphibian_survey_id"),
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                        x => x.ToTable("amphibian_surveys_hex160s", "public"));
            modelBuilder.Entity<Hex160>()
                .HasMany(_ => _.SPIPlantPolygons)
                .WithMany(p => p.Hex160s)
                .UsingEntity<Dictionary<string, object>>("spi_plant_polygons_hex160s",
                        x => x.HasOne<SPIPlantPolygon>().WithMany().HasForeignKey("spi_plant_polygon_id"),
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                        x => x.ToTable("spi_plant_polygons_hex160s", "public"));
            modelBuilder.Entity<Hex160>()
               .HasMany(_ => _.BotanicalSurveyAreas)
               .WithMany(p => p.Hex160s)
               .UsingEntity<Dictionary<string, object>>("botanical_survey_areas_hex160s",
                       x => x.HasOne<BotanicalSurveyArea>().WithMany().HasForeignKey("botanical_survey_area_id"),
                       x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                       x => x.ToTable("botanical_survey_areas_hex160s", "public"));
            modelBuilder.Entity<Hex160>()
                .HasMany(_ => _.BotanicalSurveys)
                .WithMany(p => p.Hex160s)
                .UsingEntity<Dictionary<string, object>>("botanical_surveys_hex160s",
                        x => x.HasOne<BotanicalSurvey>().WithMany().HasForeignKey("botanical_survey_id"),
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                        x => x.ToTable("botanical_surveys_hex160s", "public"));
        }

        #region "DbSets"

        public DbSet<UserFlexRecord> UserFlexRecords => Set<UserFlexRecord>();
        public DbSet<DataForm> DataForms => Set<DataForm>();
        public DbSet<Template> Templates => Set<Template>();
        public DbSet<TemplateField> TemplateFields => Set<TemplateField>();

        public DbSet<District> Districts { get; set; }
        public DbSet<DistrictExtendedGeometry> DistrictExtendedGeometries { get; set; }
        public DbSet<Quad75> Quad75s { get; set; }
        public DbSet<Watershed> Watersheds { get; set; }
        public DbSet<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }
        public DbSet<CDFW_SpottedOwlDiagram> CDFW_SpottedOwlDiagrams { get; set; }
        public DbSet<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public DbSet<CNDDBQuadElement> CNDDBQuadElements { get; set; }
        public DbSet<BirdSpecies> BirdSpecies { get; set; }
        public DbSet<WildlifeSpecies> WildlifeSpecies { get; set; }
        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Hex160> Hex160s { get; set; }
        public DbSet<Hex500> Hex500s { get; set; }
        public DbSet<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        public DbSet<OtherWildlife> OtherWildlifeRecords { get; set; }
        public DbSet<OwlBanding> OwlBandings { get; set; }
        public DbSet<SiteCalling> SiteCallings { get; set; }
        public DbSet<PermanentCallStation> PermanentCallStations { get; set; }
        public DbSet<ProtectionZone> ProtectionZones { get; set; }
        public DbSet<DeletedGeometry> DeletedGeometries { get; set; }
        public DbSet<SiteCallingDetection> SiteCallingDetections { get; set; }
        public DbSet<SiteCallingTrack> SiteCallingTracks { get; set; }
        public DbSet<DeviceInfo> DeviceInfos { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<AmphibianSpecies> AmphibianSpecies { get; set; }
        public DbSet<AmphibianSurvey> AmphibianSurveys { get; set; }
        public DbSet<AmphibianElement> AmphibianElements { get; set; }
        public DbSet<AmphibianLocationFound> AmphibianLocationsFound { get; set; }
        public DbSet<AmphibianPointOfInterest> AmphibianPointsOfInterest { get; set; }
        public DbSet<SPIPlantPolygon> SPIPlantPolygons { get; set; }
        public DbSet<SPIPlantPoint> SPIPlantPoints { get; set; }
        public DbSet<FloweringTimeline> FloweringTimelines { get; set; }
        public DbSet<PlantProtectionSummary> PlantProtectionSummaries { get; set; }
        public DbSet<PlantSpecies> PlantSpecies { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionalPlantList> RegionalPlantLists { get; set; }

        public DbSet<THP_Area> THP_Areas { get; set; }
        public DbSet<BotanicalElement> BotanicalElements { get; set; }
        public DbSet<BotanicalPlantOfInterest> BotanicalPlantsOfInterest { get; set; }
        public DbSet<BotanicalPointOfInterest> BotanicalPointsOfInterest { get; set; }
        public DbSet<BotanicalPlantList> BotanicalPlantsList { get; set; }
        public DbSet<BotanicalSurvey> BotanicalSurveys { get; set; }
        public DbSet<BotanicalSurveyArea> BotanicalSurveyAreas { get; set; }
        public DbSet<BotanicalScoping> BotanicalScopings { get; set; }
        public DbSet<BotanicalScopingSpecies> BotanicalScopingSpecies { get; set; }
        public DbSet<Picture> Pictures { get; set; }


        public DbSet<UserMapLayer> UserMapLayers { get; set; }
        public DbSet<DropdownOption> DropdownOptions { get; set; }
        public DbSet<TableModified> TablesModified { get; set; }


        public DbSet<SPI_GGOW> SPI_GGOWs { get; set; }
        public DbSet<SPI_SPOW> SPI_SPOWs { get; set; }
        public DbSet<SPI_NOGO> SPI_NOGOs { get; set; }
        public DbSet<SPI_WildlifeSighting> SPI_WildlifeSightings { get; set; }
        public DbSet<CdfwVintage> CdfwVintages { get; set; }
        public DbSet<ScopingText> ScopingTexts { get; set; }

        //public DbSet<ForestCarnivoreCameraStation> ForestCarnivoreCameraStations { get; set; }
        //public DbSet<CarnivoreOccurrence> CarnivoreOccurrences { get; set; }
        //public DbSet<RanchPhotoPoint> RanchPhotoPoints { get; set; }
        //public DbSet<DOMonitoring> DOMonitorings { get; set; }
        //public DbSet<BDOWSighting> BDOWSightings { get; set; }

        #endregion

        public override int SaveChanges()
        {
            if (CurrentUser.User == null)
            {
                int returnVal = base.SaveChanges();
                return returnVal;
            }
            else
            {
                Tracker.ChangesSaving = true;
                ApplicationUser user = ApplicationUsers.FirstOrDefault(_ => _.Id == CurrentUser.User.Id);
                GenerateFlexUserRecord(user);

                var entries = ChangeTracker.Entries();
                var GeoModified = SaveToChangesTable(entries, user);

                GeoModified = GeoModified.Distinct().ToList();

                int returnVal = base.SaveChanges();

                if (GeoModified.Count > 0) MapDataPasser.RefreshLayers(GeoModified);
                Tracker.ChangesSaving = false;
                Tracker.SendEvent(entries);
                return returnVal;
            }
        }
        //Only record new/deleted regens and activities
        private List<string> SaveToChangesTable(IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> entries, ApplicationUser user)
        {
            List<string> GeoModified = new List<string>();

            //List<RecordChange> changes = new List<RecordChange>();
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry e in entries.Where(_ => _.State != EntityState.Unchanged))
            {
                var guidProp = e.CurrentValues.Properties.FirstOrDefault(_ => _.Name == "Id");
                //var userProp = e.CurrentValues.Properties.FirstOrDefault(_ => _.Name == "User");
                //var modifiedProp = e.CurrentValues.Properties.FirstOrDefault(_ => _.Name == "UserModified");

                if (guidProp == null) continue;

                Guid entityId = (Guid)e.CurrentValues["Id"];//.GetValue<Guid>("Guid");

                if (e.State == EntityState.Added)
                {
                    if (e.Entity.GetType().GetInterfaces().Contains(typeof(IUserRecords)))
                    {
                        if (user == null)
                            ((IUserRecords)e.Entity).User = user;
                        ((IUserRecords)e.Entity).UserModified = user;
                    }
                    if (e.CurrentValues.Properties.Any(_ => _.Name == "Geometry"))
                    {
                        GeoModified.Add(e.Metadata.GetTableName());// recordChange.Table);
                    }
                }
                else if (e.State == EntityState.Modified)
                {
                    if (e.Entity.GetType().GetInterfaces().Contains(typeof(IUserRecords)))
                    {
                       // if (user != null)
                            ((IUserRecords)e.Entity).UserModified = user;
                    }

                    foreach (Microsoft.EntityFrameworkCore.Metadata.IProperty p in e.CurrentValues.Properties)
                    {
                        if (p.Name != "_delete" && p.Name != "Geometry" && p.Name != "Repository") continue;

                        var oldVal = e.OriginalValues[p];//.GetValue<object>(p.Name);
                        var newVal = e.CurrentValues[p];//.GetValue<object>(p.Name);
                        if (oldVal == null || newVal == null)
                        {
                            if (!object.Equals(newVal, oldVal))
                            {
                                GeoModified.Add(e.Metadata.GetTableName());
                            }
                        }
                        else
                        {
                            if (!object.Equals(newVal, oldVal))
                            {
                                GeoModified.Add(e.Metadata.GetTableName());
                            }
                        }
                    }
                }
            }

            return GeoModified.Distinct().ToList();
        }


        private void GenerateFlexUserRecord(ApplicationUser user)
        {
            var dataForms = ChangeTracker.Entries().Where(_ => _.State != EntityState.Unchanged && _.Entity is DataForm).ToList();
            foreach(var entry in dataForms)
            {
                if (entry.State == EntityState.Added)
                {
                    UserFlexRecord userFlexRecord = new UserFlexRecord()
                    {
                        _delete = false,
                        Repository = false,
                        DataFormID = (Guid)entry.CurrentValues["Id"],
                        User = user
                    };
                    UserFlexRecords.Add(userFlexRecord);
                }
                else if (entry.State == EntityState.Modified)
                {
                   var userFlexRecord = UserFlexRecords
                        .Include(_=>_.DataForm)
                        .FirstOrDefault(_ => _.DataFormID == (Guid)entry.CurrentValues["Id"]);
                    if (userFlexRecord != null)
                        UserFlexRecords.Update(userFlexRecord);
                }
            }
        }

    }
}
