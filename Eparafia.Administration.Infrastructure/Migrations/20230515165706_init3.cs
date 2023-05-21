using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Administration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
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
                name: "_homeRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    table.PrimaryKey("PK__homeRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK__homeRecord__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_priest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__priest", x => x.Id);
                    table.ForeignKey(
                        name: "FK__priest__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_deadRegister",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfSacrament = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SacramentalMakerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__deadRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK__deadRegister__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__deadRegister__sacramentalMaker_SacramentalMakerId",
                        column: x => x.SacramentalMakerId,
                        principalTable: "_sacramentalMaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingRegister",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MenId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WomenId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfSacrament = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SacramentalMakerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingRegister__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__weddingRegister__sacramentalMaker_SacramentalMakerId",
                        column: x => x.SacramentalMakerId,
                        principalTable: "_sacramentalMaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismRegister",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ActIdId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParentsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GodmotherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GodfatherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfSacrament = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SacramentalMakerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__baptismRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismRegister_ActId_ActIdId",
                        column: x => x.ActIdId,
                        principalTable: "ActId",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__baptismRegister__parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__baptismRegister__sacramentalMaker_SacramentalMakerId",
                        column: x => x.SacramentalMakerId,
                        principalTable: "_sacramentalMaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeRecordId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__person", x => x.Id);
                    table.ForeignKey(
                        name: "FK__person__homeRecord_HomeRecordId",
                        column: x => x.HomeRecordId,
                        principalTable: "_homeRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_deadClient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DeathDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DeadRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__deadClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK__deadClient__deadRegister_DeadRegisterId",
                        column: x => x.DeadRegisterId,
                        principalTable: "_deadRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingMen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeddingRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingMen", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingMen__weddingRegister_WeddingRegisterId",
                        column: x => x.WeddingRegisterId,
                        principalTable: "_weddingRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingWitness",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeddingRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingWitness", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingWitness__weddingRegister_WeddingRegisterId",
                        column: x => x.WeddingRegisterId,
                        principalTable: "_weddingRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_weddingWomen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Job = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeddingRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weddingWomen", x => x.Id);
                    table.ForeignKey(
                        name: "FK__weddingWomen__weddingRegister_WeddingRegisterId",
                        column: x => x.WeddingRegisterId,
                        principalTable: "_weddingRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismClient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BaptismRegisterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BaptismDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__baptismClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismClient__baptismRegister_BaptismRegisterId",
                        column: x => x.BaptismRegisterId,
                        principalTable: "_baptismRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismGodfather",
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
                    table.PrimaryKey("PK__baptismGodfather", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismGodfather__baptismRegister_BaptismRegisterId",
                        column: x => x.BaptismRegisterId,
                        principalTable: "_baptismRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismGodmother",
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
                    table.PrimaryKey("PK__baptismGodmother", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismGodmother__baptismRegister_BaptismRegisterId",
                        column: x => x.BaptismRegisterId,
                        principalTable: "_baptismRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismParentsRelation",
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
                    table.PrimaryKey("PK__baptismParentsRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismParentsRelation__baptismRegister_BaptismRegisterId",
                        column: x => x.BaptismRegisterId,
                        principalTable: "_baptismRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismFather",
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
                    table.PrimaryKey("PK__baptismFather", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismFather__baptismParentsRelation_BaptismParentsRelatio~",
                        column: x => x.BaptismParentsRelationId,
                        principalTable: "_baptismParentsRelation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_baptismMother",
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
                    table.PrimaryKey("PK__baptismMother", x => x.Id);
                    table.ForeignKey(
                        name: "FK__baptismMother__baptismParentsRelation_BaptismParentsRelatio~",
                        column: x => x.BaptismParentsRelationId,
                        principalTable: "_baptismParentsRelation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX__baptismClient_BaptismRegisterId",
                table: "_baptismClient",
                column: "BaptismRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismFather_BaptismParentsRelationId",
                table: "_baptismFather",
                column: "BaptismParentsRelationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismGodfather_BaptismRegisterId",
                table: "_baptismGodfather",
                column: "BaptismRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismGodmother_BaptismRegisterId",
                table: "_baptismGodmother",
                column: "BaptismRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismMother_BaptismParentsRelationId",
                table: "_baptismMother",
                column: "BaptismParentsRelationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismParentsRelation_BaptismRegisterId",
                table: "_baptismParentsRelation",
                column: "BaptismRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegister_ActIdId",
                table: "_baptismRegister",
                column: "ActIdId");

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegister_ParishId",
                table: "_baptismRegister",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegister_SacramentalMakerId",
                table: "_baptismRegister",
                column: "SacramentalMakerId");

            migrationBuilder.CreateIndex(
                name: "IX__deadClient_DeadRegisterId",
                table: "_deadClient",
                column: "DeadRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__deadRegister_ParishId",
                table: "_deadRegister",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__deadRegister_SacramentalMakerId",
                table: "_deadRegister",
                column: "SacramentalMakerId");

            migrationBuilder.CreateIndex(
                name: "IX__homeRecord_ParishId",
                table: "_homeRecord",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__person_HomeRecordId",
                table: "_person",
                column: "HomeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX__priest_ParishId",
                table: "_priest",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__weddingMen_WeddingRegisterId",
                table: "_weddingMen",
                column: "WeddingRegisterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__weddingRegister_ParishId",
                table: "_weddingRegister",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__weddingRegister_SacramentalMakerId",
                table: "_weddingRegister",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_baptismClient");

            migrationBuilder.DropTable(
                name: "_baptismFather");

            migrationBuilder.DropTable(
                name: "_baptismGodfather");

            migrationBuilder.DropTable(
                name: "_baptismGodmother");

            migrationBuilder.DropTable(
                name: "_baptismMother");

            migrationBuilder.DropTable(
                name: "_deadClient");

            migrationBuilder.DropTable(
                name: "_person");

            migrationBuilder.DropTable(
                name: "_priest");

            migrationBuilder.DropTable(
                name: "_weddingMen");

            migrationBuilder.DropTable(
                name: "_weddingWitness");

            migrationBuilder.DropTable(
                name: "_weddingWomen");

            migrationBuilder.DropTable(
                name: "_baptismParentsRelation");

            migrationBuilder.DropTable(
                name: "_deadRegister");

            migrationBuilder.DropTable(
                name: "_homeRecord");

            migrationBuilder.DropTable(
                name: "_weddingRegister");

            migrationBuilder.DropTable(
                name: "_baptismRegister");

            migrationBuilder.DropTable(
                name: "ActId");

            migrationBuilder.DropTable(
                name: "_parish");

            migrationBuilder.DropTable(
                name: "_sacramentalMaker");
        }
    }
}
