using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NetTopologySuite.Geometries;


namespace WBIS_2.DataModel
{
    public class WBIS2Model : DbContext
    {
        public static string GetSqlQuery<T>(List<string> exlude)
        {
            var query = "Select ";
            var entityType = typeof(T);
            using var context = new WBIS2Model();

            if (context.Model.FindEntityType(typeof(T)) is IEntityType et)
            {
                var properties = et.GetProperties();
                //TODO:
                /* 
                var navs = et.GetIndexes();
                var other = et.GetNavigations();
                var fc = et.GetReferencingForeignKeys();
                */
                foreach (var prop in properties)
                {
                    if (exlude.Contains(prop.Name))
                    {
                        query += $"null as \"{prop.Name}\" ,";
                        continue;
                    }
                    query += $"\"{prop.Name}\" ,";
                }
                query = query.Remove(query.Length - 1, 1);

                query += $" from \"{et.GetSchemaQualifiedTableName()}\"";
            }
            return query;
        }
        public static string GetSqlQuery<T>(Type ExcludeType)
        {
            var query = "Select ";
            var entityType = typeof(T);
            using var context = new WBIS2Model();

            if (context.Model.FindEntityType(typeof(T)) is IEntityType et)
            {
                var properties = et.GetProperties();
                //TODO:
                /* 
                var navs = et.GetIndexes();
                var other = et.GetNavigations();
                var fc = et.GetReferencingForeignKeys();
                */
                foreach (var prop in properties)
                {
                    if (prop.ClrType.BaseType == ExcludeType || prop.ClrType == ExcludeType)
                    {
                        query += $"null as \"{prop.Name}\" ,";
                        continue;
                    }
                    query += $"\"{prop.Name}\" ,";
                }
                query = query.Remove(query.Length - 1, 1);

                query += $" from \"{et.GetSchemaQualifiedTableName()}\"";
            }
            return query;
        }

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
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            
            TableNames(modelBuilder);
            ManyToManyRelations(modelBuilder);
        }

        private void TableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>().ToTable("districts");
            modelBuilder.Entity<DistrictExtendedGeometry>().ToTable("district_extended_geometries");
            modelBuilder.Entity<Quad75>().ToTable("quad75s");
            modelBuilder.Entity<Watershed>().ToTable("watersheds");
            modelBuilder.Entity<CDFW_SpottedOwl>().ToTable("cdfw_spotted_owls");
            modelBuilder.Entity<CDFW_SpottedOwlDiagram>().ToTable("cdfw_spotted_owl_diagrams");
            modelBuilder.Entity<CNDDBOccurrence>().ToTable("cnddb_occurrences");
            modelBuilder.Entity<CNDDBQuadElement>().ToTable("cnddb_quad_elements");
            modelBuilder.Entity<BirdSpecies>().ToTable("bird_species");
            modelBuilder.Entity<WildlifeSpecies>().ToTable("wildlife_species");
            modelBuilder.Entity<ApplicationGroup>().ToTable("application_groups");
            modelBuilder.Entity<ApplicationUser>().ToTable("application_users");
            modelBuilder.Entity<Hex160>().ToTable("hex160s");
            modelBuilder.Entity<Hex160RequiredPass>().ToTable("hex160_required_passes");
            modelBuilder.Entity<OtherWildlife>().ToTable("other_wildlife_records");
            modelBuilder.Entity<OwlBanding>().ToTable("owl_bandings");
            modelBuilder.Entity<SiteCalling>().ToTable("site_callings");
            modelBuilder.Entity<ProtectionZone>().ToTable("protection_zones");
            modelBuilder.Entity<PermanentCallStation>().ToTable("permanent_call_stations");
            modelBuilder.Entity<DeletedGeometry>().ToTable("deleted_geometries");
            modelBuilder.Entity<SiteCallingDetection>().ToTable("site_calling_detections");
            modelBuilder.Entity<DeviceInfo>().ToTable("device_infos");
            modelBuilder.Entity<SiteCallingTrack>().ToTable("site_calling_tracks");
            modelBuilder.Entity<UserLocation>().ToTable("user_locations");

            modelBuilder.Entity<AmphibianSpecies>().ToTable("amphibian_species");
            modelBuilder.Entity<AmphibianSurvey>().ToTable("amphibian_surveys");
            modelBuilder.Entity<AmphibianElement>().ToTable("amphibian_elements");
            modelBuilder.Entity<AmphibianLocationFound>().ToTable("amphibian_locations_found");
            modelBuilder.Entity<AmphibianPointOfInterest>().ToTable("amphibian_points_of_interest");

            modelBuilder.Entity<SPIPlantPolygon>().ToTable("spi_plant_polygons");
            modelBuilder.Entity<SPIPlantPoint>().ToTable("spi_plant_points");
            modelBuilder.Entity<FloweringTimeline>().ToTable("flowering_timelines");
            modelBuilder.Entity<PlantProtectionSummary>().ToTable("plant_protection_summaries");
            modelBuilder.Entity<PlantSpecies>().ToTable("plant_species");
            modelBuilder.Entity<Region>().ToTable("regions");
            modelBuilder.Entity<RegionalPlantList>().ToTable("regional_plant_lists");

            modelBuilder.Entity<THP_Area>().ToTable("thp_areas");
            modelBuilder.Entity<BotanicalElement>().ToTable("botanical_elements");
            modelBuilder.Entity<BotanicalPlantOfInterest>().ToTable("botanical_plants_of_interest");
            modelBuilder.Entity<BotanicalPointOfInterest>().ToTable("botanical_points_of_interest");
            modelBuilder.Entity<BotanicalPlantList>().ToTable("botanical_plants_list");
            modelBuilder.Entity<BotanicalSurvey>().ToTable("botanical_surveys");
            modelBuilder.Entity<BotanicalSurveyArea>().ToTable("botanical_survey_areas");
            modelBuilder.Entity<BotanicalScoping>().ToTable("botanical_scopings");
            modelBuilder.Entity<BotanicalScopingSpecies>().ToTable("botanical_scoping_species");
            modelBuilder.Entity<Picture>().ToTable("pictures");

            modelBuilder.Entity<ForestCarnivoreCameraStation>().ToTable("forest_carnivore_camera_stations");
            modelBuilder.Entity<CarnivoreOccurrence>().ToTable("carnivore_occurrences");
            modelBuilder.Entity<RanchPhotoPoint>().ToTable("ranch_photo_points");
            modelBuilder.Entity<DOMonitoring>().ToTable("do_monitorings");
            modelBuilder.Entity<BDOWSighting>().ToTable("bdow_sightings");


            modelBuilder.Entity<UserMapLayer>().ToTable("user_map_layers");
            modelBuilder.Entity<DropdownOption>().ToTable("dropdown_options");
            modelBuilder.Entity<TableModified>().ToTable("tables_modified");
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


        public DbSet<ForestCarnivoreCameraStation> ForestCarnivoreCameraStations { get; set; }
        public DbSet<CarnivoreOccurrence> CarnivoreOccurrences { get; set; }
        public DbSet<RanchPhotoPoint> RanchPhotoPoints { get; set; }
        public DbSet<DOMonitoring> DOMonitorings { get; set; }
        public DbSet<BDOWSighting> BDOWSightings { get; set; }

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
                ApplicationUser user = ApplicationUsers.FirstOrDefault(_ => _.Guid == CurrentUser.User.Guid);

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
                var guidProp = e.CurrentValues.Properties.FirstOrDefault(_ => _.Name == "Guid");
                //var userProp = e.CurrentValues.Properties.FirstOrDefault(_ => _.Name == "User");
                //var modifiedProp = e.CurrentValues.Properties.FirstOrDefault(_ => _.Name == "UserModified");

                if (guidProp == null) continue;

                Guid entityId = (Guid)e.CurrentValues["Guid"];//.GetValue<Guid>("Guid");

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

    }
}
