using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "Contact_Email",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "Contact_PhoneNumber",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "_Priests");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "_Users",
                newName: "PhotoPath_PathMin");

            migrationBuilder.RenameColumn(
                name: "PhotoPathMin",
                table: "_Users",
                newName: "PhotoPath_Path");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "_Priests",
                newName: "PhotoPath_PathMin");

            migrationBuilder.RenameColumn(
                name: "PhotoPathMin",
                table: "_Priests",
                newName: "PhotoPath_Path");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath_PathMin",
                table: "_Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "PhotoPath_Path",
                table: "_Users",
                newName: "PhotoPathMin");

            migrationBuilder.RenameColumn(
                name: "PhotoPath_PathMin",
                table: "_Priests",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "PhotoPath_Path",
                table: "_Priests",
                newName: "PhotoPathMin");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "_Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "_Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "_Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "_Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "_Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Contact_Email",
                table: "_Priests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Contact_PhoneNumber",
                table: "_Priests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "_Priests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "_Priests",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "_Priests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "_Priests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "_Priests",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
