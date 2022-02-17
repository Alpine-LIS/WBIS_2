using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            return $"Server={hostname};Database={dbname};Username={username};Password={password}";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Batch size limits leangth of querries built by entity framework. If batches are too big it cannot desifer them.
            string location = AppDomain.CurrentDomain.BaseDirectory;
            optionsBuilder.EnableSensitiveDataLogging(true)
                .UseNpgsql(GetRDSConnectionString(),
                o => { o.UseNetTopologySuite(); o.MaxBatchSize(25); })
                .EnableDetailedErrors();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");

            modelBuilder.Entity<District>().ToTable("districts");
            modelBuilder.Entity<Quad75>().ToTable("quad75s");
            modelBuilder.Entity<Watershed>().ToTable("watersheds");
            modelBuilder.Entity<CDFW_SpottedOwl>().ToTable("cdfw_spotted_owls");
            modelBuilder.Entity<CDFW_SpottedOwlDiagram>().ToTable("cdfw_spotted_owl_diagrams");
            modelBuilder.Entity<CNDDBOccurrence>().ToTable("cnddb_occurrences");
            modelBuilder.Entity<BirdSpecies>().ToTable("bird_species");
            modelBuilder.Entity<WildlifeSpecies>().ToTable("wildlife_species");
            modelBuilder.Entity<ApplicationGroup>().ToTable("application_groups");
            modelBuilder.Entity<ApplicationUser>().ToTable("application_users");
            modelBuilder.Entity<Hex160>().ToTable("hex160s");
            modelBuilder.Entity<Hex160RequiredPass>().ToTable("hex160_required_passes");
            modelBuilder.Entity<OtherWildlife>().ToTable("other_wildlife_records");
            modelBuilder.Entity<SiteCalling>().ToTable("site_callings");
            modelBuilder.Entity<ProtectionZone>().ToTable("protection_zones");
            modelBuilder.Entity<PermanentCallStation>().ToTable("permanent_call_stations");
            modelBuilder.Entity<DeletedGeometry>().ToTable("deleted_geometries");
            modelBuilder.Entity<SiteCallingDetection>().ToTable("site_calling_detections");
            modelBuilder.Entity<SiteCallingDeviceInfo>().ToTable("site_calling_device_infos");
            modelBuilder.Entity<SiteCallingTrack>().ToTable("site_calling_tracks");




            modelBuilder.Entity<ApplicationUser>()
                .HasMany(_ => _.Districts)
                .WithMany(p => p.ApplicationUsers)
                .UsingEntity<Dictionary<string, object>>("users_districts",
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.HasOne<ApplicationUser>().WithMany().HasForeignKey("application_user_id"),
                        x => x.ToTable("users_districts", "public"));



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
                .HasMany(_ => _.CNDDBOccurrences)
                .WithMany(p => p.Districts)
                .UsingEntity<Dictionary<string, object>>("cnddb_occurrences_districts",
                        x => x.HasOne<CNDDBOccurrence>().WithMany().HasForeignKey("cnddb_occurrence_id"),
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.ToTable("cnddb_occurrences_districts", "public"));
            modelBuilder.Entity<District>()
                .HasMany(_ => _.CDFW_SpottedOwls)
                .WithMany(p => p.Districts)
                .UsingEntity<Dictionary<string, object>>("cdfw_spotted_owls_districts",
                        x => x.HasOne<CDFW_SpottedOwl>().WithMany().HasForeignKey("cdfw_spotted_owl_id"),
                        x => x.HasOne<District>().WithMany().HasForeignKey("district_id"),
                        x => x.ToTable("cdfw_spotted_owls_districts", "public"));



            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.CDFW_SpottedOwls)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("cdfw_spotted_owls_quad75s",
                        x => x.HasOne<CDFW_SpottedOwl>().WithMany().HasForeignKey("cdfw_spotted_owl_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("cdfw_spotted_owls_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.CNDDBOccurrences)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("cnddb_occurrences_quad75s",
                        x => x.HasOne<CNDDBOccurrence>().WithMany().HasForeignKey("cnddb_occurrence_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("cnddb_occurrences_quad75s", "public"));
            modelBuilder.Entity<Quad75>()
                .HasMany(_ => _.Hex160s)
                .WithMany(p => p.Quad75s)
                .UsingEntity<Dictionary<string, object>>("hex160s_quad75s",
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("hex160_id"),
                        x => x.HasOne<Quad75>().WithMany().HasForeignKey("quad75_id"),
                        x => x.ToTable("hex160s_quad75s", "public"));



            modelBuilder.Entity<Watershed>()
                .HasMany(_ => _.CDFW_SpottedOwls)
                .WithMany(p => p.Watersheds)
                .UsingEntity<Dictionary<string, object>>("cdfw_spotted_owls_watersheds",
                        x => x.HasOne<CDFW_SpottedOwl>().WithMany().HasForeignKey("cdfw_spotted_owl_id"),
                        x => x.HasOne<Watershed>().WithMany().HasForeignKey("watershed_id"),
                        x => x.ToTable("cdfw_spotted_owls_watersheds", "public"));
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

            modelBuilder.Entity<Hex160>()
                .HasMany(_=>_.ProtectionZones)
                .WithMany(p=>p.Hex160s)
                .UsingEntity<Dictionary<string, object>>("hex160s_protection_zones",
                        x => x.HasOne<ProtectionZone>().WithMany().HasForeignKey("hex160_id"),
                        x => x.HasOne<Hex160>().WithMany().HasForeignKey("protection_zone_id"),
                        x => x.ToTable("hex160s_protection_zones", "public"));

        }
        public DbSet<District> Districts { get; set; }
        public DbSet<Quad75> Quad75s { get; set; }
        public DbSet<Watershed> Watersheds { get; set; }
        public DbSet<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }
        public DbSet<CDFW_SpottedOwlDiagram> CDFW_SpottedOwlDiagrams { get; set; }
        public DbSet<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        public DbSet<BirdSpecies> BirdSpecies { get; set; }
        public DbSet<WildlifeSpecies> WildlifeSpecies { get; set; }
        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Hex160> Hex160s { get; set; }
        public DbSet<Hex160RequiredPass> Hex160RequiredPasses { get; set; }
        public DbSet<OtherWildlife> OtherWildlifeRecords { get; set; }
        public DbSet<SiteCalling> SiteCallings { get; set; }
        public DbSet<PermanentCallStation> PermanentCallStations { get; set; }
        public DbSet<ProtectionZone> ProtectionZones { get; set; }
        public DbSet<DeletedGeometry> DeletedGeometries { get; set; }
        public DbSet<SiteCallingDetection> siteCallingDetections { get; set; }
        public DbSet<SiteCallingTrack> siteCallingTracks { get; set; }
        public DbSet<SiteCallingDeviceInfo> siteCallingDeviceInfos { get; set; }

        //public DbSet<Quad75> Quad75s { get; set; }
        //public DbSet<Watershed> Watersheds { get; set; }
        //public DbSet<CDFW_SpottedOwl> CDFW_SpottedOwls { get; set; }
        //public DbSet<CDFW_SpottedOwlDiagram> CDFW_SpottedOwlDiagrams { get; set; }
        //public DbSet<CNDDBOccurrence> CNDDBOccurrences { get; set; }
        //public DbSet<BirdSpecies> BirdSpecies { get; set; }
        //public DbSet<WildlifeSpecies> WildlifeSpecies { get; set; }
        //public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<Hex160> Hex160s { get; set; }
        //public DbSet<Hex160RequredPass> Hex160RequredPasses { get; set; }
        //public DbSet<SiteCalling> SiteCallings { get; set; }
    }
}
