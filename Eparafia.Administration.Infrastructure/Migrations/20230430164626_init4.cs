using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Administration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__baptismRegisters_ActId_ActIdId",
                table: "_baptismRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_Priest__parish_ParishId",
                table: "Priest");

            migrationBuilder.DropTable(
                name: "ActId");

            migrationBuilder.DropIndex(
                name: "IX__baptismRegisters_ActIdId",
                table: "_baptismRegisters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Priest",
                table: "Priest");

            migrationBuilder.DropColumn(
                name: "ActIdId",
                table: "_baptismRegisters");

            migrationBuilder.RenameTable(
                name: "Priest",
                newName: "_priests");

            migrationBuilder.RenameIndex(
                name: "IX_Priest_ParishId",
                table: "_priests",
                newName: "IX__priests_ParishId");

            migrationBuilder.AddColumn<string>(
                name: "ActId_Id",
                table: "_baptismRegisters",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK__priests",
                table: "_priests",
                column: "Id");

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
                name: "_person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HomeRecordId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HomeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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

            migrationBuilder.CreateIndex(
                name: "IX__homeRecord_ParishId",
                table: "_homeRecord",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__person_HomeRecordId",
                table: "_person",
                column: "HomeRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK__priests__parish_ParishId",
                table: "_priests",
                column: "ParishId",
                principalTable: "_parish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__priests__parish_ParishId",
                table: "_priests");

            migrationBuilder.DropTable(
                name: "_person");

            migrationBuilder.DropTable(
                name: "_homeRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK__priests",
                table: "_priests");

            migrationBuilder.DropColumn(
                name: "ActId_Id",
                table: "_baptismRegisters");

            migrationBuilder.RenameTable(
                name: "_priests",
                newName: "Priest");

            migrationBuilder.RenameIndex(
                name: "IX__priests_ParishId",
                table: "Priest",
                newName: "IX_Priest_ParishId");

            migrationBuilder.AddColumn<string>(
                name: "ActIdId",
                table: "_baptismRegisters",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Priest",
                table: "Priest",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX__baptismRegisters_ActIdId",
                table: "_baptismRegisters",
                column: "ActIdId");

            migrationBuilder.AddForeignKey(
                name: "FK__baptismRegisters_ActId_ActIdId",
                table: "_baptismRegisters",
                column: "ActIdId",
                principalTable: "ActId",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Priest__parish_ParishId",
                table: "Priest",
                column: "ParishId",
                principalTable: "_parish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
