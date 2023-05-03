using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Administration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismParentsRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BaptismRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParentRelation = table.Column<int>(type: "int", nullable: false),
                    FatherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MotherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__baptismParentsRelations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_parish",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CallName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__parish", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_sacramentalMaker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sacramentalMaker", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActId",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActId", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_fathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BaptismParentsRelationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CityOfBirth = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confession = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressCity = table.Column<string>(name: "Address_City", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__fathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK__fathers__baptismParentsRelations_BaptismParentsRelationId",
                        column: x => x.BaptismParentsRelationId,
                        principalTable: "_baptismParentsRelations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_mothers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BaptismParentsRelationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CityOfBirth = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confession = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressCity = table.Column<string>(name: "Address_City", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mothers", x => x.Id);
                    table.ForeignKey(
                        name: "FK__mothers__baptismParentsRelations_BaptismParentsRelationId",
                        column: x => x.BaptismParentsRelationId,
                        principalTable: "_baptismParentsRelations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Priest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Priest__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_deadRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeadClientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SacramentalMakerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfSacrament = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__deadRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK__deadRegisters__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__deadRegisters__sacramentalMaker_SacramentalMakerId",
                        column: x => x.SacramentalMakerId,
                        principalTable: "_sacramentalMaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MenId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WomenId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SacramentalMakerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfSacrament = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingRegisters__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__weddingRegisters__sacramentalMaker_SacramentalMakerId",
                        column: x => x.SacramentalMakerId,
                        principalTable: "_sacramentalMaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ActIdId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParentsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GodmotherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GodfatherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SacramentalMakerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfSacrament = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__baptismRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismRegisters_ActId_ActIdId",
                        column: x => x.ActIdId,
                        principalTable: "ActId",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__baptismRegisters__baptismParentsRelations_ParentsId",
                        column: x => x.ParentsId,
                        principalTable: "_baptismParentsRelations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__baptismRegisters__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__baptismRegisters__sacramentalMaker_SacramentalMakerId",
                        column: x => x.SacramentalMakerId,
                        principalTable: "_sacramentalMaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_deadClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeadRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DeathDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__deadClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK__deadClients__deadRegisters_DeadRegisterId",
                        column: x => x.DeadRegisterId,
                        principalTable: "_deadRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingMen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeddingRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Confession = table.Column<int>(type: "int", nullable: false),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressCity = table.Column<string>(name: "Address_City", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingMen", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingMen__weddingRegisters_WeddingRegisterId",
                        column: x => x.WeddingRegisterId,
                        principalTable: "_weddingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingWitness",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeddingRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Confession = table.Column<int>(type: "int", nullable: false),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressCity = table.Column<string>(name: "Address_City", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingWitness", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingWitness__weddingRegisters_WeddingRegisterId",
                        column: x => x.WeddingRegisterId,
                        principalTable: "_weddingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingWomen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeddingRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Confession = table.Column<int>(type: "int", nullable: false),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressCity = table.Column<string>(name: "Address_City", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingWomen", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingWomen__weddingRegisters_WeddingRegisterId",
                        column: x => x.WeddingRegisterId,
                        principalTable: "_weddingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BaptismRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__baptismClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismClients__baptismRegisters_BaptismRegisterId",
                        column: x => x.BaptismRegisterId,
                        principalTable: "_baptismRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_godfathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BaptismRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CityOfBirth = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confession = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressCity = table.Column<string>(name: "Address_City", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__godfathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK__godfathers__baptismRegisters_BaptismRegisterId",
                        column: x => x.BaptismRegisterId,
                        principalTable: "_baptismRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_godmothers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BaptismRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CityOfBirth = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confession = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressCity = table.Column<string>(name: "Address_City", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__godmothers", x => x.Id);
                    table.ForeignKey(
                        name: "FK__godmothers__baptismRegisters_BaptismRegisterId",
                        column: x => x.BaptismRegisterId,
                        principalTable: "_baptismRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX__baptismClients_BaptismRegisterId",
                table: "_baptismClients",
                column: "BaptismRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegisters_ActIdId",
                table: "_baptismRegisters",
                column: "ActIdId");

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegisters_ParentsId",
                table: "_baptismRegisters",
                column: "ParentsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegisters_ParishId",
                table: "_baptismRegisters",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegisters_SacramentalMakerId",
                table: "_baptismRegisters",
                column: "SacramentalMakerId");

            migrationBuilder.CreateIndex(
                name: "IX__deadClients_DeadRegisterId",
                table: "_deadClients",
                column: "DeadRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__deadRegisters_ParishId",
                table: "_deadRegisters",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__deadRegisters_SacramentalMakerId",
                table: "_deadRegisters",
                column: "SacramentalMakerId");

            migrationBuilder.CreateIndex(
                name: "IX__fathers_BaptismParentsRelationId",
                table: "_fathers",
                column: "BaptismParentsRelationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__godfathers_BaptismRegisterId",
                table: "_godfathers",
                column: "BaptismRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__godmothers_BaptismRegisterId",
                table: "_godmothers",
                column: "BaptismRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__mothers_BaptismParentsRelationId",
                table: "_mothers",
                column: "BaptismParentsRelationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__weddingMen_WeddingRegisterId",
                table: "_weddingMen",
                column: "WeddingRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__weddingRegisters_ParishId",
                table: "_weddingRegisters",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__weddingRegisters_SacramentalMakerId",
                table: "_weddingRegisters",
                column: "SacramentalMakerId");

            migrationBuilder.CreateIndex(
                name: "IX__weddingWitness_WeddingRegisterId",
                table: "_weddingWitness",
                column: "WeddingRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX__weddingWomen_WeddingRegisterId",
                table: "_weddingWomen",
                column: "WeddingRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Priest_ParishId",
                table: "Priest",
                column: "ParishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_baptismClients");

            migrationBuilder.DropTable(
                name: "_deadClients");

            migrationBuilder.DropTable(
                name: "_fathers");

            migrationBuilder.DropTable(
                name: "_godfathers");

            migrationBuilder.DropTable(
                name: "_godmothers");

            migrationBuilder.DropTable(
                name: "_mothers");

            migrationBuilder.DropTable(
                name: "_weddingMen");

            migrationBuilder.DropTable(
                name: "_weddingWitness");

            migrationBuilder.DropTable(
                name: "_weddingWomen");

            migrationBuilder.DropTable(
                name: "Priest");

            migrationBuilder.DropTable(
                name: "_deadRegisters");

            migrationBuilder.DropTable(
                name: "_baptismRegisters");

            migrationBuilder.DropTable(
                name: "_weddingRegisters");

            migrationBuilder.DropTable(
                name: "ActId");

            migrationBuilder.DropTable(
                name: "_baptismParentsRelations");

            migrationBuilder.DropTable(
                name: "_parish");

            migrationBuilder.DropTable(
                name: "_sacramentalMaker");
        }
    }
}
