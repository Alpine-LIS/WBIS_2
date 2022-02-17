using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "application_groups",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    group_name = table.Column<string>(type: "text", nullable: false),
                    admin_privileges = table.Column<bool>(type: "boolean", nullable: false),
                    read_only = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_groups", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "bird_species",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    species = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bird_species", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "cdfw_spotted_owl_diagrams",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owl_diagrams", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "cdfw_spotted_owls",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    sname = table.Column<string>(type: "text", nullable: true),
                    cname = table.Column<string>(type: "text", nullable: true),
                    obsid = table.Column<int>(type: "integer", nullable: false),
                    masterowl = table.Column<string>(type: "text", nullable: true),
                    typeobs = table.Column<string>(type: "text", nullable: true),
                    observer = table.Column<string>(type: "text", nullable: true),
                    dateobs = table.Column<string>(type: "text", nullable: true),
                    timeobs = table.Column<string>(type: "text", nullable: true),
                    numadobs = table.Column<int>(type: "integer", nullable: false),
                    agesex = table.Column<string>(type: "text", nullable: true),
                    pair = table.Column<string>(type: "text", nullable: true),
                    nest = table.Column<string>(type: "text", nullable: true),
                    numyoung = table.Column<string>(type: "text", nullable: true),
                    subspecies = table.Column<string>(type: "text", nullable: true),
                    londd_n83 = table.Column<double>(type: "double precision", nullable: false),
                    latdd_n83 = table.Column<double>(type: "double precision", nullable: false),
                    coordsrc = table.Column<string>(type: "text", nullable: true),
                    docid = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    mtrs = table.Column<string>(type: "text", nullable: true),
                    highestuse = table.Column<string>(type: "text", nullable: true),
                    symbology = table.Column<string>(type: "text", nullable: true),
                    geometry = table.Column<Point>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owls", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "cnddb_occurrences",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    sname = table.Column<string>(type: "text", nullable: true),
                    cname = table.Column<string>(type: "text", nullable: true),
                    elmcode = table.Column<string>(type: "text", nullable: true),
                    occnumber = table.Column<int>(type: "integer", nullable: false),
                    mapndx = table.Column<string>(type: "text", nullable: true),
                    eondx = table.Column<int>(type: "integer", nullable: false),
                    keyquad = table.Column<string>(type: "text", nullable: true),
                    kquadname = table.Column<string>(type: "text", nullable: true),
                    keycounty = table.Column<string>(type: "text", nullable: true),
                    plss = table.Column<string>(type: "text", nullable: true),
                    elevation = table.Column<int>(type: "integer", nullable: false),
                    parts = table.Column<int>(type: "integer", nullable: false),
                    elmtype = table.Column<int>(type: "integer", nullable: false),
                    taxongroup = table.Column<string>(type: "text", nullable: true),
                    eocount = table.Column<int>(type: "integer", nullable: false),
                    accuracy = table.Column<string>(type: "text", nullable: true),
                    presence = table.Column<string>(type: "text", nullable: true),
                    occtype = table.Column<string>(type: "text", nullable: true),
                    occrank = table.Column<string>(type: "text", nullable: true),
                    sensitive = table.Column<string>(type: "text", nullable: true),
                    sitedate = table.Column<string>(type: "text", nullable: true),
                    elmdate = table.Column<string>(type: "text", nullable: true),
                    ownermgt = table.Column<string>(type: "text", nullable: true),
                    fedlist = table.Column<string>(type: "text", nullable: true),
                    callist = table.Column<string>(type: "text", nullable: true),
                    grank = table.Column<string>(type: "text", nullable: true),
                    srank = table.Column<string>(type: "text", nullable: true),
                    rplantrank = table.Column<string>(type: "text", nullable: true),
                    cdfwstatus = table.Column<string>(type: "text", nullable: true),
                    othrstatus = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    locdetails = table.Column<string>(type: "text", nullable: true),
                    ecological = table.Column<string>(type: "text", nullable: true),
                    general = table.Column<string>(type: "text", nullable: true),
                    threat = table.Column<string>(type: "text", nullable: true),
                    threatlist = table.Column<string>(type: "text", nullable: true),
                    lastupdate = table.Column<string>(type: "text", nullable: true),
                    area = table.Column<double>(type: "double precision", nullable: false),
                    perimeter = table.Column<double>(type: "double precision", nullable: false),
                    avlcode = table.Column<int>(type: "integer", nullable: false),
                    symbology = table.Column<int>(type: "integer", nullable: false),
                    symbology_text = table.Column<string>(type: "text", nullable: true),
                    geometry = table.Column<MultiPolygon>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_occurrences", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_name = table.Column<string>(type: "text", nullable: false),
                    management_area = table.Column<string>(type: "text", nullable: false),
                    geometry = table.Column<MultiPolygon>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "quad75s",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    quad_code = table.Column<string>(type: "text", nullable: false),
                    isgs_code = table.Column<string>(type: "text", nullable: false),
                    cnps_code = table.Column<string>(type: "text", nullable: true),
                    quad_name = table.Column<string>(type: "text", nullable: true),
                    quad_size = table.Column<string>(type: "text", nullable: true),
                    q24_year = table.Column<int>(type: "integer", nullable: false),
                    q100_name = table.Column<string>(type: "text", nullable: true),
                    border = table.Column<string>(type: "text", nullable: true),
                    utm_zone = table.Column<string>(type: "text", nullable: true),
                    b_m = table.Column<string>(type: "text", nullable: true),
                    sensitive = table.Column<string>(type: "text", nullable: true),
                    perimeter = table.Column<double>(type: "double precision", nullable: false),
                    area = table.Column<double>(type: "double precision", nullable: false),
                    geometry = table.Column<Polygon>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quad75s", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "watersheds",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_name = table.Column<string>(type: "text", nullable: false),
                    watershed_id = table.Column<string>(type: "text", nullable: false),
                    mouth_trs = table.Column<string>(type: "text", nullable: true),
                    mouth_lat = table.Column<double>(type: "double precision", nullable: false),
                    mouth_lon = table.Column<double>(type: "double precision", nullable: false),
                    elevation_min = table.Column<double>(type: "double precision", nullable: false),
                    elevation_max = table.Column<double>(type: "double precision", nullable: false),
                    basin_length = table.Column<double>(type: "double precision", nullable: false),
                    perim001 = table.Column<double>(type: "double precision", nullable: false),
                    vally_length = table.Column<double>(type: "double precision", nullable: false),
                    chanel_length = table.Column<double>(type: "double precision", nullable: false),
                    chanel_orientation = table.Column<string>(type: "text", nullable: true),
                    ws_order = table.Column<double>(type: "double precision", nullable: false),
                    humint_pop = table.Column<string>(type: "text", nullable: true),
                    humint_rec = table.Column<string>(type: "text", nullable: true),
                    humint_vis = table.Column<string>(type: "text", nullable: true),
                    geology = table.Column<string>(type: "text", nullable: true),
                    basin_m_type = table.Column<int>(type: "integer", nullable: false),
                    river_name = table.Column<string>(type: "text", nullable: true),
                    down_str_wshd = table.Column<string>(type: "text", nullable: true),
                    hydro_reg = table.Column<string>(type: "text", nullable: true),
                    rwqcb = table.Column<string>(type: "text", nullable: true),
                    hydrologic = table.Column<string>(type: "text", nullable: true),
                    hydro_area = table.Column<string>(type: "text", nullable: true),
                    hydro_suba = table.Column<string>(type: "text", nullable: true),
                    super_plan = table.Column<string>(type: "text", nullable: true),
                    threatend = table.Column<string>(type: "text", nullable: true),
                    asp_up = table.Column<string>(type: "text", nullable: true),
                    toc = table.Column<bool>(type: "boolean", nullable: true),
                    esu = table.Column<bool>(type: "boolean", nullable: true),
                    d303 = table.Column<string>(type: "text", nullable: true),
                    wshd_acres = table.Column<double>(type: "double precision", nullable: false),
                    spi_acres = table.Column<double>(type: "double precision", nullable: false),
                    perimeter = table.Column<double>(type: "double precision", nullable: false),
                    geometry = table.Column<MultiPolygon>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watersheds", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "wildlife_species",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    alpha_code = table.Column<string>(type: "text", nullable: true),
                    wildlife_species_description = table.Column<string>(type: "text", nullable: true),
                    @class = table.Column<string>(name: "class", type: "text", nullable: true),
                    order = table.Column<string>(type: "text", nullable: true),
                    family = table.Column<string>(type: "text", nullable: true),
                    genus = table.Column<string>(type: "text", nullable: true),
                    pecies = table.Column<string>(type: "text", nullable: true),
                    wl_sorting = table.Column<int>(type: "integer", nullable: false),
                    sub_species = table.Column<string>(type: "text", nullable: true),
                    whr_num = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wildlife_species", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "application_users",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    email_default = table.Column<string>(type: "text", nullable: true),
                    hint = table.Column<string>(type: "text", nullable: true),
                    deleted_ = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_ = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_ = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    password_sha = table.Column<string>(type: "text", nullable: true),
                    password_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    application_group_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_users", x => x.guid);
                    table.ForeignKey(
                        name: "FK_application_users_application_groups_application_group_id",
                        column: x => x.application_group_id,
                        principalTable: "application_groups",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cdfw_spotted_owls_districts",
                schema: "public",
                columns: table => new
                {
                    cdfw_spotted_owl_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owls_districts", x => new { x.cdfw_spotted_owl_id, x.district_id });
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_districts_cdfw_spotted_owls_cdfw_spotted_~",
                        column: x => x.cdfw_spotted_owl_id,
                        principalTable: "cdfw_spotted_owls",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cnddb_occurrences_districts",
                schema: "public",
                columns: table => new
                {
                    cnddb_occurrence_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_occurrences_districts", x => new { x.cnddb_occurrence_id, x.district_id });
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_districts_cnddb_occurrences_cnddb_occurre~",
                        column: x => x.cnddb_occurrence_id,
                        principalTable: "cnddb_occurrences",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cdfw_spotted_owls_quad75s",
                schema: "public",
                columns: table => new
                {
                    cdfw_spotted_owl_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owls_quad75s", x => new { x.cdfw_spotted_owl_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_quad75s_cdfw_spotted_owls_cdfw_spotted_ow~",
                        column: x => x.cdfw_spotted_owl_id,
                        principalTable: "cdfw_spotted_owls",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cnddb_occurrences_quad75s",
                schema: "public",
                columns: table => new
                {
                    cnddb_occurrence_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_occurrences_quad75s", x => new { x.cnddb_occurrence_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_quad75s_cnddb_occurrences_cnddb_occurrenc~",
                        column: x => x.cnddb_occurrence_id,
                        principalTable: "cnddb_occurrences",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cdfw_spotted_owls_watersheds",
                schema: "public",
                columns: table => new
                {
                    cdfw_spotted_owl_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owls_watersheds", x => new { x.cdfw_spotted_owl_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_watersheds_cdfw_spotted_owls_cdfw_spotted~",
                        column: x => x.cdfw_spotted_owl_id,
                        principalTable: "cdfw_spotted_owls",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cnddb_occurrences_watersheds",
                schema: "public",
                columns: table => new
                {
                    cnddb_occurrence_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_occurrences_watersheds", x => new { x.cnddb_occurrence_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_watersheds_cnddb_occurrences_cnddb_occurr~",
                        column: x => x.cnddb_occurrence_id,
                        principalTable: "cnddb_occurrences",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "watersheds_districts",
                schema: "public",
                columns: table => new
                {
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watersheds_districts", x => new { x.district_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_watersheds_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_watersheds_districts_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "protection_zone",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<MultiPolygon>(type: "geometry", nullable: true),
                    pz_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_protection_zone", x => x.guid);
                    table.ForeignKey(
                        name: "FK_protection_zone_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_districts",
                schema: "public",
                columns: table => new
                {
                    application_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_districts", x => new { x.application_user_id, x.district_id });
                    table.ForeignKey(
                        name: "FK_users_districts_application_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hex160s",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<string>(type: "text", nullable: false),
                    calling_responses = table.Column<int>(type: "integer", nullable: false),
                    follow_ups = table.Column<int>(type: "integer", nullable: false),
                    skips = table.Column<int>(type: "integer", nullable: false),
                    drops = table.Column<int>(type: "integer", nullable: false),
                    recent_activity = table.Column<string>(type: "text", nullable: true),
                    latest_activity = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    geometry = table.Column<Polygon>(type: "geometry", nullable: true),
                    current_preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160s", x => x.guid);
                    table.ForeignKey(
                        name: "FK_hex160s_protection_zone_current_preotection_zone_id",
                        column: x => x.current_preotection_zone_id,
                        principalTable: "protection_zone",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hex160_required_passes",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    required_passes = table.Column<int>(type: "integer", nullable: false),
                    current_passes = table.Column<int>(type: "integer", nullable: false),
                    dropped = table.Column<bool>(type: "boolean", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bird_species_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160_required_passes", x => x.guid);
                    table.ForeignKey(
                        name: "FK_hex160_required_passes_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160_required_passes_bird_species_bird_species_id",
                        column: x => x.bird_species_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160_required_passes_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hex160s_districts",
                schema: "public",
                columns: table => new
                {
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160s_districts", x => new { x.district_id, x.hex160_id });
                    table.ForeignKey(
                        name: "FK_hex160s_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160s_districts_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hex160s_protection_zones",
                schema: "public",
                columns: table => new
                {
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    protection_zone_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160s_protection_zones", x => new { x.hex160_id, x.protection_zone_id });
                    table.ForeignKey(
                        name: "FK_hex160s_protection_zones_hex160s_protection_zone_id",
                        column: x => x.protection_zone_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160s_protection_zones_protection_zone_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "protection_zone",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hex160s_quad75s",
                schema: "public",
                columns: table => new
                {
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160s_quad75s", x => new { x.hex160_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_hex160s_quad75s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160s_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hex160s_watersheds",
                schema: "public",
                columns: table => new
                {
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160s_watersheds", x => new { x.hex160_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_hex160s_watersheds_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160s_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "permanent_call_station",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry", nullable: true),
                    pcs_id = table.Column<string>(type: "text", nullable: true),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permanent_call_station", x => x.guid);
                    table.ForeignKey(
                        name: "FK_permanent_call_station_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permanent_call_station_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_callings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    starting_location = table.Column<Point>(type: "geometry", nullable: true),
                    starting_lat = table.Column<double>(type: "double precision", nullable: false),
                    starting_lon = table.Column<double>(type: "double precision", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sunset_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    detection_type = table.Column<string>(type: "text", nullable: true),
                    survey_type1 = table.Column<string>(type: "text", nullable: true),
                    survey_type2 = table.Column<string>(type: "text", nullable: true),
                    bird_species_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_type = table.Column<string>(type: "text", nullable: true),
                    site_id = table.Column<string>(type: "text", nullable: true),
                    pass_number = table.Column<int>(type: "integer", nullable: false),
                    preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    yearly_activity_center = table.Column<bool>(type: "boolean", nullable: false),
                    wind = table.Column<string>(type: "text", nullable: true),
                    precipitation = table.Column<string>(type: "text", nullable: true),
                    detection_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    target_species_present = table.Column<bool>(type: "boolean", nullable: false),
                    bird_species_found_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detection_method = table.Column<string>(type: "text", nullable: true),
                    detection_location = table.Column<Point>(type: "geometry", nullable: true),
                    detection_lat = table.Column<double>(type: "double precision", nullable: false),
                    detection_lon = table.Column<double>(type: "double precision", nullable: false),
                    user_location = table.Column<Point>(type: "geometry", nullable: true),
                    user_lat = table.Column<double>(type: "double precision", nullable: false),
                    user_lon = table.Column<double>(type: "double precision", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    bearing = table.Column<double>(type: "double precision", nullable: false),
                    estimated_location = table.Column<bool>(type: "boolean", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: true),
                    age = table.Column<string>(type: "text", nullable: true),
                    number_of_young = table.Column<int>(type: "integer", nullable: false),
                    species_site = table.Column<string>(type: "text", nullable: true),
                    male_banding_leg = table.Column<string>(type: "text", nullable: true),
                    male_banding_pattern = table.Column<string>(type: "text", nullable: true),
                    female_banding_leg = table.Column<string>(type: "text", nullable: true),
                    female_banding_pattern = table.Column<string>(type: "text", nullable: true),
                    occupancy_status = table.Column<string>(type: "text", nullable: true),
                    nesting_status = table.Column<string>(type: "text", nullable: true),
                    nest_tree = table.Column<bool>(type: "boolean", nullable: false),
                    nest_type = table.Column<string>(type: "text", nullable: true),
                    tree_species = table.Column<string>(type: "text", nullable: true),
                    dbh = table.Column<double>(type: "double precision", nullable: false),
                    nest_height = table.Column<double>(type: "double precision", nullable: false),
                    tree_tagged = table.Column<bool>(type: "boolean", nullable: false),
                    moused = table.Column<bool>(type: "boolean", nullable: false),
                    area_description = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    user_track = table.Column<LineString>(type: "geometry", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    device_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    device_location = table.Column<Point>(type: "geometry", nullable: true),
                    device_lat = table.Column<double>(type: "double precision", nullable: false),
                    device_lon = table.Column<double>(type: "double precision", nullable: false),
                    PermanentCallStationGuid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_callings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_callings_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_bird_species_bird_species_found_id",
                        column: x => x.bird_species_found_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_bird_species_bird_species_survey_id",
                        column: x => x.bird_species_survey_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_permanent_call_station_PermanentCallStationGu~",
                        column: x => x.PermanentCallStationGuid,
                        principalTable: "permanent_call_station",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_site_callings_protection_zone_preotection_zone_id",
                        column: x => x.preotection_zone_id,
                        principalTable: "protection_zone",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "other_wildlife_records",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    wildlife_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_other_wildlife_records", x => x.guid);
                    table.ForeignKey(
                        name: "FK_other_wildlife_records_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_other_wildlife_records_site_callings_site_calling_id",
                        column: x => x.site_calling_id,
                        principalTable: "site_callings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_other_wildlife_records_wildlife_species_wildlife_species_id",
                        column: x => x.wildlife_species_id,
                        principalTable: "wildlife_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_application_users_application_group_id",
                table: "application_users",
                column: "application_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owls_districts_district_id",
                schema: "public",
                table: "cdfw_spotted_owls_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owls_quad75s_quad75_id",
                schema: "public",
                table: "cdfw_spotted_owls_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owls_watersheds_watershed_id",
                schema: "public",
                table: "cdfw_spotted_owls_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_occurrences_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_occurrences_quad75s_quad75_id",
                schema: "public",
                table: "cnddb_occurrences_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_occurrences_watersheds_watershed_id",
                schema: "public",
                table: "cnddb_occurrences_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160_required_passes_bird_species_id",
                table: "hex160_required_passes",
                column: "bird_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160_required_passes_hex160_id",
                table: "hex160_required_passes",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160_required_passes_user_id",
                table: "hex160_required_passes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160s_current_preotection_zone_id",
                table: "hex160s",
                column: "current_preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160s_districts_hex160_id",
                schema: "public",
                table: "hex160s_districts",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160s_protection_zones_protection_zone_id",
                schema: "public",
                table: "hex160s_protection_zones",
                column: "protection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_hex160s_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_site_calling_id",
                table: "other_wildlife_records",
                column: "site_calling_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_user_id",
                table: "other_wildlife_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_wildlife_species_id",
                table: "other_wildlife_records",
                column: "wildlife_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_permanent_call_station_hex160_id",
                table: "permanent_call_station",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_permanent_call_station_user_id",
                table: "permanent_call_station",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_protection_zone_user_id",
                table: "protection_zone",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_bird_species_found_id",
                table: "site_callings",
                column: "bird_species_found_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_bird_species_survey_id",
                table: "site_callings",
                column: "bird_species_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_hex160_id",
                table: "site_callings",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_PermanentCallStationGuid",
                table: "site_callings",
                column: "PermanentCallStationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_preotection_zone_id",
                table: "site_callings",
                column: "preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_user_id",
                table: "site_callings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_districts_district_id",
                schema: "public",
                table: "users_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_watersheds_districts_watershed_id",
                schema: "public",
                table: "watersheds_districts",
                column: "watershed_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owls_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owls_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owls_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hex160_required_passes");

            migrationBuilder.DropTable(
                name: "hex160s_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hex160s_protection_zones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hex160s_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hex160s_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "other_wildlife_records");

            migrationBuilder.DropTable(
                name: "users_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "watersheds_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owls");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences");

            migrationBuilder.DropTable(
                name: "quad75s");

            migrationBuilder.DropTable(
                name: "site_callings");

            migrationBuilder.DropTable(
                name: "wildlife_species");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "watersheds");

            migrationBuilder.DropTable(
                name: "bird_species");

            migrationBuilder.DropTable(
                name: "permanent_call_station");

            migrationBuilder.DropTable(
                name: "hex160s");

            migrationBuilder.DropTable(
                name: "protection_zone");

            migrationBuilder.DropTable(
                name: "application_users");

            migrationBuilder.DropTable(
                name: "application_groups");
        }
    }
}
