using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "amphibian_species",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    species_name = table.Column<string>(type: "text", nullable: true),
                    species_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_species", x => x.guid);
                });

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
                    geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owl_diagrams", x => x.guid);
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
                    geometry = table.Column<MultiPolygon>(type: "geometry(MultiPolygon,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_occurrences", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "deleted_geometries",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    object_guid = table.Column<Guid>(type: "uuid", nullable: false),
                    poly_geometry = table.Column<Polygon>(type: "geometry(Polygon,26710)", nullable: true),
                    mpoly_geometry = table.Column<MultiPolygon>(type: "geometry(MultiPolygon,26710)", nullable: true),
                    point_eometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    line_geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deleted_geometries", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_name = table.Column<string>(type: "text", nullable: false),
                    management_area = table.Column<string>(type: "text", nullable: false),
                    district_extended_geometry_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "plant_species",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    sci_name = table.Column<string>(type: "text", nullable: true),
                    com_name = table.Column<string>(type: "text", nullable: true),
                    taxon_group = table.Column<string>(type: "text", nullable: true),
                    elm_code = table.Column<string>(type: "text", nullable: true),
                    fed_list = table.Column<string>(type: "text", nullable: true),
                    cal_list = table.Column<string>(type: "text", nullable: true),
                    g_rank = table.Column<string>(type: "text", nullable: true),
                    s_rank = table.Column<string>(type: "text", nullable: true),
                    r_plant_rank = table.Column<string>(type: "text", nullable: true),
                    other_status = table.Column<string>(type: "text", nullable: true),
                    habitats = table.Column<string>(type: "text", nullable: true),
                    gen_habitat = table.Column<string>(type: "text", nullable: true),
                    micro_habitat = table.Column<string>(type: "text", nullable: true),
                    spi_habitat = table.Column<string>(type: "text", nullable: true),
                    family = table.Column<string>(type: "text", nullable: true),
                    species_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plant_species", x => x.guid);
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
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quad75s", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    region_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.guid);
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
                    geometry = table.Column<MultiPolygon>(type: "geometry(MultiPolygon,26710)", nullable: true)
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
                    email = table.Column<string>(type: "text", nullable: true),
                    hint = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    password_sha = table.Column<string>(type: "text", nullable: true),
                    password_time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    application_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_user_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_application_users_application_users_modified_user_id",
                        column: x => x.modified_user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cdfw_spotted_owl_diagrams_districts",
                schema: "public",
                columns: table => new
                {
                    cdfw_spotted_owl_diagram_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owl_diagrams_districts", x => new { x.cdfw_spotted_owl_diagram_id, x.district_id });
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owl_diagrams_districts_cdfw_spotted_owl_diagra~",
                        column: x => x.cdfw_spotted_owl_diagram_id,
                        principalTable: "cdfw_spotted_owl_diagrams",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owl_diagrams_districts_districts_district_id",
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
                name: "district_extended_geometry",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<MultiPolygon>(type: "geometry(MultiPolygon,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_extended_geometry", x => x.guid);
                    table.ForeignKey(
                        name: "FK_district_extended_geometry_districts_guid",
                        column: x => x.guid,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flowering_timelines",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    active_from = table.Column<string>(type: "text", nullable: true),
                    active_to = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flowering_timelines", x => x.guid);
                    table.ForeignKey(
                        name: "FK_flowering_timelines_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plant_protection_summaries",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    summary = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plant_protection_summaries", x => x.guid);
                    table.ForeignKey(
                        name: "FK_plant_protection_summaries_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_plant_protection_summaries_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spi_plant_polygons",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,26710)", nullable: false),
                    link = table.Column<string>(type: "text", nullable: true),
                    surveyor = table.Column<string>(type: "text", nullable: true),
                    num_ind = table.Column<int>(type: "integer", nullable: false),
                    num_ind_max = table.Column<int>(type: "integer", nullable: false),
                    cnddb_occurrence = table.Column<int>(type: "integer", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    coord_source = table.Column<string>(type: "text", nullable: true),
                    site_quality = table.Column<string>(type: "text", nullable: true),
                    disturbance = table.Column<string>(type: "text", nullable: true),
                    land_use = table.Column<string>(type: "text", nullable: true),
                    threats = table.Column<string>(type: "text", nullable: true),
                    hab_desc = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    land_owner = table.Column<string>(type: "text", nullable: true),
                    obs_contract = table.Column<string>(type: "text", nullable: true),
                    associated = table.Column<string>(type: "text", nullable: true),
                    name1_ = table.Column<string>(type: "text", nullable: true),
                    vegetative = table.Column<int>(type: "integer", nullable: false),
                    flowering = table.Column<int>(type: "integer", nullable: false),
                    fruiting = table.Column<int>(type: "integer", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_plant_polygons", x => x.guid);
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
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
                name: "quad75s_districts",
                schema: "public",
                columns: table => new
                {
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quad75s_districts", x => new { x.district_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_quad75s_districts_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quad75s_districts_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "regional_plant_lists",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    region_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regional_plant_lists", x => x.guid);
                    table.ForeignKey(
                        name: "FK_regional_plant_lists_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_regional_plant_lists_regions_region_id",
                        column: x => x.region_id,
                        principalTable: "regions",
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
                name: "protection_zones",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<MultiPolygon>(type: "geometry(MultiPolygon,26710)", nullable: true),
                    pz_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_protection_zones", x => x.guid);
                    table.ForeignKey(
                        name: "FK_protection_zones_application_users_user_id",
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
                name: "spi_plant_polygons_quad75s",
                schema: "public",
                columns: table => new
                {
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    spi_plant_polygon_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_plant_polygons_quad75s", x => new { x.quad75_id, x.spi_plant_polygon_id });
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_quad75s_spi_plant_polygons_spi_plant_pol~",
                        column: x => x.spi_plant_polygon_id,
                        principalTable: "spi_plant_polygons",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spi_plant_polygons_watersheds",
                schema: "public",
                columns: table => new
                {
                    spi_plant_polygon_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_plant_polygons_watersheds", x => new { x.spi_plant_polygon_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_watersheds_spi_plant_polygons_spi_plant_~",
                        column: x => x.spi_plant_polygon_id,
                        principalTable: "spi_plant_polygons",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
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
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,26710)", nullable: true),
                    current_preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160s", x => x.guid);
                    table.ForeignKey(
                        name: "FK_hex160s_protection_zones_current_preotection_zone_id",
                        column: x => x.current_preotection_zone_id,
                        principalTable: "protection_zones",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
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
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdfw_spotted_owls", x => x.guid);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cdfw_spotted_owls_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cnddb_occurrences_hex160s",
                schema: "public",
                columns: table => new
                {
                    cnddb_occurrence_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnddb_occurrences_hex160s", x => new { x.cnddb_occurrence_id, x.hex160_id });
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_hex160s_cnddb_occurrences_cnddb_occurrenc~",
                        column: x => x.cnddb_occurrence_id,
                        principalTable: "cnddb_occurrences",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnddb_occurrences_hex160s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
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
                    bird_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,26710)", nullable: true)
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
                        name: "FK_hex160s_protection_zones_protection_zones_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "protection_zones",
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
                name: "owl_bandings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    bands = table.Column<string>(type: "text", nullable: false),
                    record_type = table.Column<string>(type: "text", nullable: true),
                    bird_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    banding_leg = table.Column<string>(type: "text", nullable: true),
                    banding_pattern = table.Column<string>(type: "text", nullable: true),
                    usfws_band_num = table.Column<string>(type: "text", nullable: true),
                    usfws_band_color = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bander = table.Column<string>(type: "text", nullable: true),
                    capturer = table.Column<string>(type: "text", nullable: true),
                    trap_code = table.Column<string>(type: "text", nullable: true),
                    capture_method = table.Column<string>(type: "text", nullable: true),
                    preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    temperature = table.Column<double>(type: "double precision", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gps_tag_id = table.Column<string>(type: "text", nullable: true),
                    sex = table.Column<string>(type: "text", nullable: true),
                    age_class = table.Column<string>(type: "text", nullable: true),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    wing_chord = table.Column<double>(type: "double precision", nullable: false),
                    tail_length = table.Column<double>(type: "double precision", nullable: false),
                    footpad = table.Column<double>(type: "double precision", nullable: false),
                    blood = table.Column<bool>(type: "boolean", nullable: false),
                    oral_sample = table.Column<bool>(type: "boolean", nullable: false),
                    ectoparasites = table.Column<bool>(type: "boolean", nullable: false),
                    feathers = table.Column<bool>(type: "boolean", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owl_bandings", x => x.guid);
                    table.ForeignKey(
                        name: "FK_owl_bandings_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_bird_species_bird_species_id",
                        column: x => x.bird_species_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_protection_zones_preotection_zone_id",
                        column: x => x.preotection_zone_id,
                        principalTable: "protection_zones",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_owl_bandings_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "permanent_call_stations",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    pcs_id = table.Column<string>(type: "text", nullable: true),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permanent_call_stations", x => x.guid);
                    table.ForeignKey(
                        name: "FK_permanent_call_stations_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permanent_call_stations_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_calling_repositories",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    starting_lat = table.Column<double>(type: "double precision", nullable: false),
                    starting_lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sunset_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    survey_type1 = table.Column<string>(type: "text", nullable: false),
                    survey_type2 = table.Column<string>(type: "text", nullable: false),
                    bird_species_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_id = table.Column<string>(type: "text", nullable: true),
                    pass_number = table.Column<int>(type: "integer", nullable: false),
                    pz_pass_number = table.Column<int>(type: "integer", nullable: false),
                    preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    yearly_activity_center = table.Column<bool>(type: "boolean", nullable: false),
                    wind = table.Column<string>(type: "text", nullable: true),
                    precipitation = table.Column<string>(type: "text", nullable: true),
                    target_species_present = table.Column<bool>(type: "boolean", nullable: false),
                    site_calling_repository_detection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    occupancy_status = table.Column<string>(type: "text", nullable: false),
                    nesting_status = table.Column<string>(type: "text", nullable: true),
                    reproductive_status = table.Column<string>(type: "text", nullable: true),
                    nest_tree = table.Column<bool>(type: "boolean", nullable: false),
                    nest_type = table.Column<string>(type: "text", nullable: true),
                    tree_species = table.Column<string>(type: "text", nullable: true),
                    dbh = table.Column<double>(type: "double precision", nullable: false),
                    nest_height = table.Column<double>(type: "double precision", nullable: false),
                    tree_tagged = table.Column<bool>(type: "boolean", nullable: false),
                    moused = table.Column<bool>(type: "boolean", nullable: false),
                    area_description = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    site_calling_repository_track_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_repositories", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_bird_species_bird_species_survey_~",
                        column: x => x.bird_species_survey_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_protection_zones_preotection_zone~",
                        column: x => x.preotection_zone_id,
                        principalTable: "protection_zones",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repositories_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_callings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    starting_lat = table.Column<double>(type: "double precision", nullable: false),
                    starting_lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sunset_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    survey_type1 = table.Column<string>(type: "text", nullable: false),
                    survey_type2 = table.Column<string>(type: "text", nullable: false),
                    bird_species_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_id = table.Column<string>(type: "text", nullable: true),
                    pass_number = table.Column<int>(type: "integer", nullable: false),
                    pz_pass_number = table.Column<int>(type: "integer", nullable: false),
                    preotection_zone_id = table.Column<Guid>(type: "uuid", nullable: false),
                    yearly_activity_center = table.Column<bool>(type: "boolean", nullable: false),
                    wind = table.Column<string>(type: "text", nullable: true),
                    precipitation = table.Column<string>(type: "text", nullable: true),
                    target_species_present = table.Column<bool>(type: "boolean", nullable: false),
                    site_calling_detection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    occupancy_status = table.Column<string>(type: "text", nullable: false),
                    nesting_status = table.Column<string>(type: "text", nullable: true),
                    reproductive_status = table.Column<string>(type: "text", nullable: true),
                    nest_tree = table.Column<bool>(type: "boolean", nullable: false),
                    nest_type = table.Column<string>(type: "text", nullable: true),
                    tree_species = table.Column<string>(type: "text", nullable: true),
                    dbh = table.Column<double>(type: "double precision", nullable: false),
                    nest_height = table.Column<double>(type: "double precision", nullable: false),
                    tree_tagged = table.Column<bool>(type: "boolean", nullable: false),
                    moused = table.Column<bool>(type: "boolean", nullable: false),
                    area_description = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    site_calling_track_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                        name: "FK_site_callings_bird_species_bird_species_survey_id",
                        column: x => x.bird_species_survey_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_protection_zones_preotection_zone_id",
                        column: x => x.preotection_zone_id,
                        principalTable: "protection_zones",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_callings_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spi_plant_points",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    thp = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_plant_points", x => x.guid);
                    table.ForeignKey(
                        name: "FK_spi_plant_points_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_points_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_points_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_points_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_points_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spi_plant_polygons_hex160s",
                schema: "public",
                columns: table => new
                {
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    spi_plant_polygon_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spi_plant_polygons_hex160s", x => new { x.hex160_id, x.spi_plant_polygon_id });
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_hex160s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spi_plant_polygons_hex160s_spi_plant_polygons_spi_plant_pol~",
                        column: x => x.spi_plant_polygon_id,
                        principalTable: "spi_plant_polygons",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "watersheds_quad75s",
                schema: "public",
                columns: table => new
                {
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watersheds_quad75s", x => new { x.hex160_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_watersheds_quad75s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_watersheds_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_watersheds_quad75s_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "site_calling_repository_detections",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detection_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bird_species_found_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detection_method = table.Column<string>(type: "text", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    detection_lat = table.Column<double>(type: "double precision", nullable: false),
                    detection_lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    user_location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    bearing = table.Column<double>(type: "double precision", nullable: false),
                    estimated_location = table.Column<bool>(type: "boolean", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<string>(type: "text", nullable: false),
                    number_of_young = table.Column<int>(type: "integer", nullable: false),
                    species_site = table.Column<string>(type: "text", nullable: true),
                    male_banding_leg = table.Column<string>(type: "text", nullable: true),
                    male_banding_pattern = table.Column<string>(type: "text", nullable: true),
                    female_banding_leg = table.Column<string>(type: "text", nullable: true),
                    female_banding_pattern = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_repository_detections", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_repository_detections_bird_species_bird_specie~",
                        column: x => x.bird_species_found_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_repository_detections_site_calling_repositorie~",
                        column: x => x.guid,
                        principalTable: "site_calling_repositories",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_calling_repository_tracks",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_repository_tracks", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_repository_tracks_site_calling_repositories_gu~",
                        column: x => x.guid,
                        principalTable: "site_calling_repositories",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "device_infos",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owl_banding_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: true),
                    device_lat = table.Column<double>(type: "double precision", nullable: false),
                    device_lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_infos", x => x.guid);
                    table.ForeignKey(
                        name: "FK_device_infos_owl_bandings_owl_banding_id",
                        column: x => x.owl_banding_id,
                        principalTable: "owl_bandings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_device_infos_site_calling_repositories_site_calling_reposit~",
                        column: x => x.site_calling_repository_id,
                        principalTable: "site_calling_repositories",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_device_infos_site_callings_site_calling_id",
                        column: x => x.site_calling_id,
                        principalTable: "site_callings",
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
                    site_calling_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_repository_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                        name: "FK_other_wildlife_records_site_calling_repositories_site_calli~",
                        column: x => x.site_calling_repository_id,
                        principalTable: "site_calling_repositories",
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

            migrationBuilder.CreateTable(
                name: "site_calling_detections",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detection_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bird_species_found_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detection_method = table.Column<string>(type: "text", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    detection_lat = table.Column<double>(type: "double precision", nullable: false),
                    detection_lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true),
                    user_location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    bearing = table.Column<double>(type: "double precision", nullable: false),
                    estimated_location = table.Column<bool>(type: "boolean", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<string>(type: "text", nullable: false),
                    number_of_young = table.Column<int>(type: "integer", nullable: false),
                    species_site = table.Column<string>(type: "text", nullable: true),
                    male_banding_leg = table.Column<string>(type: "text", nullable: true),
                    male_banding_pattern = table.Column<string>(type: "text", nullable: true),
                    female_banding_leg = table.Column<string>(type: "text", nullable: true),
                    female_banding_pattern = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_detections", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_detections_bird_species_bird_species_found_id",
                        column: x => x.bird_species_found_id,
                        principalTable: "bird_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_site_calling_detections_site_callings_guid",
                        column: x => x.guid,
                        principalTable: "site_callings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "site_calling_tracks",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_calling_tracks", x => x.guid);
                    table.ForeignKey(
                        name: "FK_site_calling_tracks_site_callings_guid",
                        column: x => x.guid,
                        principalTable: "site_callings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_surveys",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_id = table.Column<string>(type: "text", nullable: false),
                    surveyors = table.Column<string>(type: "text", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lake_stream_name = table.Column<string>(type: "text", nullable: true),
                    water_type = table.Column<string>(type: "text", nullable: true),
                    seasonality_if_flow = table.Column<string>(type: "text", nullable: true),
                    planning_watershed = table.Column<string>(type: "text", nullable: true),
                    county = table.Column<string>(type: "text", nullable: true),
                    elevation = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    weather = table.Column<string>(type: "text", nullable: true),
                    wind = table.Column<string>(type: "text", nullable: true),
                    location_comments = table.Column<string>(type: "text", nullable: true),
                    canopy_closure = table.Column<double>(type: "double precision", nullable: false),
                    stream_gradient = table.Column<string>(type: "text", nullable: true),
                    silt = table.Column<double>(type: "double precision", nullable: false),
                    sand = table.Column<double>(type: "double precision", nullable: false),
                    gravel = table.Column<double>(type: "double precision", nullable: false),
                    cobble = table.Column<double>(type: "double precision", nullable: false),
                    boulders = table.Column<double>(type: "double precision", nullable: false),
                    bedrock = table.Column<double>(type: "double precision", nullable: false),
                    pool = table.Column<double>(type: "double precision", nullable: false),
                    riffle = table.Column<double>(type: "double precision", nullable: false),
                    run = table.Column<double>(type: "double precision", nullable: false),
                    est_avg_stream_width = table.Column<double>(type: "double precision", nullable: false),
                    water_temp = table.Column<double>(type: "double precision", nullable: false),
                    air_temp = table.Column<double>(type: "double precision", nullable: false),
                    flow = table.Column<string>(type: "text", nullable: true),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<LineString>(type: "geometry(LineString,26710)", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_surveys", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_device_infos_device_info_id",
                        column: x => x.device_info_id,
                        principalTable: "device_infos",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_locations",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_detection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    site_calling_repository_detection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    user_lat = table.Column<double>(type: "double precision", nullable: false),
                    user_lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_locations", x => x.guid);
                    table.ForeignKey(
                        name: "FK_user_locations_site_calling_detections_site_calling_detecti~",
                        column: x => x.site_calling_detection_id,
                        principalTable: "site_calling_detections",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_locations_site_calling_repository_detections_site_call~",
                        column: x => x.site_calling_repository_detection_id,
                        principalTable: "site_calling_repository_detections",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_elements",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    record_type = table.Column<string>(type: "text", nullable: true),
                    amphibian_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_location_found_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_point_of_interest_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _delete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_elements", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                        column: x => x.amphibian_survey_id,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_device_infos_device_info_id",
                        column: x => x.device_info_id,
                        principalTable: "device_infos",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_elements_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_surveys_hex160s",
                schema: "public",
                columns: table => new
                {
                    amphibian_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_surveys_hex160s", x => new { x.amphibian_survey_id, x.hex160_id });
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_hex160s_amphibian_surveys_amphibian_surve~",
                        column: x => x.amphibian_survey_id,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_hex160s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_surveys_quad75s",
                schema: "public",
                columns: table => new
                {
                    amphibian_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_surveys_quad75s", x => new { x.amphibian_survey_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_quad75s_amphibian_surveys_amphibian_surve~",
                        column: x => x.amphibian_survey_id,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_surveys_watersheds",
                schema: "public",
                columns: table => new
                {
                    amphibian_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    watershed_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_surveys_watersheds", x => new { x.amphibian_survey_id, x.watershed_id });
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_watersheds_amphibian_surveys_amphibian_su~",
                        column: x => x.amphibian_survey_id,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_watersheds_watersheds_watershed_id",
                        column: x => x.watershed_id,
                        principalTable: "watersheds",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_locations_found",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    point_of_interest = table.Column<string>(type: "text", nullable: false),
                    amphibian_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    number_of_adults = table.Column<double>(type: "double precision", nullable: false),
                    number_of_subadults = table.Column<double>(type: "double precision", nullable: false),
                    number_of_larve = table.Column<double>(type: "double precision", nullable: false),
                    number_of_egg_masses = table.Column<double>(type: "double precision", nullable: false),
                    visual = table.Column<bool>(type: "boolean", nullable: false),
                    aural = table.Column<bool>(type: "boolean", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_locations_found", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_locations_found_amphibian_elements_amphibian_elem~",
                        column: x => x.amphibian_element_id,
                        principalTable: "amphibian_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                        column: x => x.amphibian_species_id,
                        principalTable: "amphibian_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "amphibian_points_of_interest",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    amphibian_element_id = table.Column<Guid>(type: "uuid", nullable: false),
                    point_of_interest = table.Column<string>(type: "text", nullable: false),
                    other_wildlife_id = table.Column<Guid>(type: "uuid", nullable: false),
                    geometry = table.Column<Point>(type: "geometry(Point,26710)", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    datum = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_points_of_interest", x => x.guid);
                    table.ForeignKey(
                        name: "FK_amphibian_points_of_interest_amphibian_elements_amphibian_e~",
                        column: x => x.amphibian_element_id,
                        principalTable: "amphibian_elements",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                        column: x => x.other_wildlife_id,
                        principalTable: "amphibian_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_amphibian_survey_id",
                table: "amphibian_elements",
                column: "amphibian_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_device_info_id",
                table: "amphibian_elements",
                column: "device_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_district_id",
                table: "amphibian_elements",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_hex160_id",
                table: "amphibian_elements",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_quad75_id",
                table: "amphibian_elements",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_user_id",
                table: "amphibian_elements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_elements_watershed_id",
                table: "amphibian_elements",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_locations_found_amphibian_element_id",
                table: "amphibian_locations_found",
                column: "amphibian_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_locations_found_amphibian_species_id",
                table: "amphibian_locations_found",
                column: "amphibian_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_points_of_interest_amphibian_element_id",
                table: "amphibian_points_of_interest",
                column: "amphibian_element_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_points_of_interest_other_wildlife_id",
                table: "amphibian_points_of_interest",
                column: "other_wildlife_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_device_info_id",
                table: "amphibian_surveys",
                column: "device_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_district_id",
                table: "amphibian_surveys",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_user_id",
                table: "amphibian_surveys",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_quad75s_quad75_id",
                schema: "public",
                table: "amphibian_surveys_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_watersheds_watershed_id",
                schema: "public",
                table: "amphibian_surveys_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_users_application_group_id",
                table: "application_users",
                column: "application_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_users_modified_user_id",
                table: "application_users",
                column: "modified_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owl_diagrams_districts_district_id",
                schema: "public",
                table: "cdfw_spotted_owl_diagrams_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owls_district_id",
                table: "cdfw_spotted_owls",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owls_hex160_id",
                table: "cdfw_spotted_owls",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owls_quad75_id",
                table: "cdfw_spotted_owls",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_cdfw_spotted_owls_watershed_id",
                table: "cdfw_spotted_owls",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_occurrences_districts_district_id",
                schema: "public",
                table: "cnddb_occurrences_districts",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_occurrences_hex160s_hex160_id",
                schema: "public",
                table: "cnddb_occurrences_hex160s",
                column: "hex160_id");

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
                name: "IX_device_infos_owl_banding_id",
                table: "device_infos",
                column: "owl_banding_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_site_calling_id",
                table: "device_infos",
                column: "site_calling_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_device_infos_site_calling_repository_id",
                table: "device_infos",
                column: "site_calling_repository_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flowering_timelines_plant_species_id",
                table: "flowering_timelines",
                column: "plant_species_id");

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
                name: "IX_hex160s_watersheds_watershed_id",
                schema: "public",
                table: "hex160s_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_site_calling_id",
                table: "other_wildlife_records",
                column: "site_calling_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_site_calling_repository_id",
                table: "other_wildlife_records",
                column: "site_calling_repository_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_user_id",
                table: "other_wildlife_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_wildlife_records_wildlife_species_id",
                table: "other_wildlife_records",
                column: "wildlife_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_bird_species_id",
                table: "owl_bandings",
                column: "bird_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_district_id",
                table: "owl_bandings",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_hex160_id",
                table: "owl_bandings",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_preotection_zone_id",
                table: "owl_bandings",
                column: "preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_quad75_id",
                table: "owl_bandings",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_user_id",
                table: "owl_bandings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_owl_bandings_watershed_id",
                table: "owl_bandings",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_permanent_call_stations_hex160_id",
                table: "permanent_call_stations",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_permanent_call_stations_user_id",
                table: "permanent_call_stations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_plant_protection_summaries_district_id",
                table: "plant_protection_summaries",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_plant_protection_summaries_plant_species_id",
                table: "plant_protection_summaries",
                column: "plant_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_protection_zones_user_id",
                table: "protection_zones",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_quad75s_districts_quad75_id",
                schema: "public",
                table: "quad75s_districts",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_regional_plant_lists_plant_species_id",
                table: "regional_plant_lists",
                column: "plant_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_regional_plant_lists_region_id",
                table: "regional_plant_lists",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_detections_bird_species_found_id",
                table: "site_calling_detections",
                column: "bird_species_found_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_bird_species_survey_id",
                table: "site_calling_repositories",
                column: "bird_species_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_district_id",
                table: "site_calling_repositories",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_hex160_id",
                table: "site_calling_repositories",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_preotection_zone_id",
                table: "site_calling_repositories",
                column: "preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_quad75_id",
                table: "site_calling_repositories",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_user_id",
                table: "site_calling_repositories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repositories_watershed_id",
                table: "site_calling_repositories",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_calling_repository_detections_bird_species_found_id",
                table: "site_calling_repository_detections",
                column: "bird_species_found_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_bird_species_survey_id",
                table: "site_callings",
                column: "bird_species_survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_district_id",
                table: "site_callings",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_hex160_id",
                table: "site_callings",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_preotection_zone_id",
                table: "site_callings",
                column: "preotection_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_quad75_id",
                table: "site_callings",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_user_id",
                table: "site_callings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_site_callings_watershed_id",
                table: "site_callings",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_points_district_id",
                table: "spi_plant_points",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_points_hex160_id",
                table: "spi_plant_points",
                column: "hex160_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_points_plant_species_id",
                table: "spi_plant_points",
                column: "plant_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_points_quad75_id",
                table: "spi_plant_points",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_points_watershed_id",
                table: "spi_plant_points",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_polygons_district_id",
                table: "spi_plant_polygons",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_polygons_plant_species_id",
                table: "spi_plant_polygons",
                column: "plant_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_polygons_hex160s_spi_plant_polygon_id",
                schema: "public",
                table: "spi_plant_polygons_hex160s",
                column: "spi_plant_polygon_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_polygons_quad75s_spi_plant_polygon_id",
                schema: "public",
                table: "spi_plant_polygons_quad75s",
                column: "spi_plant_polygon_id");

            migrationBuilder.CreateIndex(
                name: "IX_spi_plant_polygons_watersheds_watershed_id",
                schema: "public",
                table: "spi_plant_polygons_watersheds",
                column: "watershed_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_locations_site_calling_detection_id",
                table: "user_locations",
                column: "site_calling_detection_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_locations_site_calling_repository_detection_id",
                table: "user_locations",
                column: "site_calling_repository_detection_id",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_watersheds_quad75s_quad75_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "quad75_id");

            migrationBuilder.CreateIndex(
                name: "IX_watersheds_quad75s_watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "watershed_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amphibian_locations_found");

            migrationBuilder.DropTable(
                name: "amphibian_points_of_interest");

            migrationBuilder.DropTable(
                name: "amphibian_surveys_hex160s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "amphibian_surveys_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "amphibian_surveys_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owl_diagrams_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owls");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences_hex160s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "deleted_geometries");

            migrationBuilder.DropTable(
                name: "district_extended_geometry");

            migrationBuilder.DropTable(
                name: "flowering_timelines");

            migrationBuilder.DropTable(
                name: "hex160_required_passes");

            migrationBuilder.DropTable(
                name: "hex160s_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hex160s_protection_zones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hex160s_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "other_wildlife_records");

            migrationBuilder.DropTable(
                name: "permanent_call_stations");

            migrationBuilder.DropTable(
                name: "plant_protection_summaries");

            migrationBuilder.DropTable(
                name: "quad75s_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "regional_plant_lists");

            migrationBuilder.DropTable(
                name: "site_calling_repository_tracks");

            migrationBuilder.DropTable(
                name: "site_calling_tracks");

            migrationBuilder.DropTable(
                name: "spi_plant_points");

            migrationBuilder.DropTable(
                name: "spi_plant_polygons_hex160s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "spi_plant_polygons_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "spi_plant_polygons_watersheds",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_locations");

            migrationBuilder.DropTable(
                name: "users_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "watersheds_districts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "watersheds_quad75s",
                schema: "public");

            migrationBuilder.DropTable(
                name: "amphibian_elements");

            migrationBuilder.DropTable(
                name: "amphibian_species");

            migrationBuilder.DropTable(
                name: "cdfw_spotted_owl_diagrams");

            migrationBuilder.DropTable(
                name: "cnddb_occurrences");

            migrationBuilder.DropTable(
                name: "wildlife_species");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropTable(
                name: "spi_plant_polygons");

            migrationBuilder.DropTable(
                name: "site_calling_detections");

            migrationBuilder.DropTable(
                name: "site_calling_repository_detections");

            migrationBuilder.DropTable(
                name: "amphibian_surveys");

            migrationBuilder.DropTable(
                name: "plant_species");

            migrationBuilder.DropTable(
                name: "device_infos");

            migrationBuilder.DropTable(
                name: "owl_bandings");

            migrationBuilder.DropTable(
                name: "site_calling_repositories");

            migrationBuilder.DropTable(
                name: "site_callings");

            migrationBuilder.DropTable(
                name: "bird_species");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "hex160s");

            migrationBuilder.DropTable(
                name: "quad75s");

            migrationBuilder.DropTable(
                name: "watersheds");

            migrationBuilder.DropTable(
                name: "protection_zones");

            migrationBuilder.DropTable(
                name: "application_users");

            migrationBuilder.DropTable(
                name: "application_groups");
        }
    }
}
