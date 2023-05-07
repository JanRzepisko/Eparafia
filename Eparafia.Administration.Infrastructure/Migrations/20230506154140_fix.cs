using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Administration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__priest__parish_ParishId",
                table: "_priest");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "_priest");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "_priest",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "_baptismGodmother",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "_baptismGodfather",
                newName: "Address_Street");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "_priest",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Address_BuildingNumber",
                table: "_baptismGodmother",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "_baptismGodmother",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostCode",
                table: "_baptismGodmother",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_Region",
                table: "_baptismGodmother",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_BuildingNumber",
                table: "_baptismGodfather",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "_baptismGodfather",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostCode",
                table: "_baptismGodfather",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_Region",
                table: "_baptismGodfather",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK__priest__parish_ParishId",
                table: "_priest",
                column: "ParishId",
                principalTable: "_parish",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__priest__parish_ParishId",
                table: "_priest");

            migrationBuilder.DropColumn(
                name: "Address_BuildingNumber",
                table: "_baptismGodmother");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "_baptismGodmother");

            migrationBuilder.DropColumn(
                name: "Address_PostCode",
                table: "_baptismGodmother");

            migrationBuilder.DropColumn(
                name: "Address_Region",
                table: "_baptismGodmother");

            migrationBuilder.DropColumn(
                name: "Address_BuildingNumber",
                table: "_baptismGodfather");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "_baptismGodfather");

            migrationBuilder.DropColumn(
                name: "Address_PostCode",
                table: "_baptismGodfather");

            migrationBuilder.DropColumn(
                name: "Address_Region",
                table: "_baptismGodfather");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "_priest",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "_baptismGodmother",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "_baptismGodfather",
                newName: "Address");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "_priest",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "_priest",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK__priest__parish_ParishId",
                table: "_priest",
                column: "ParishId",
                principalTable: "_parish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
