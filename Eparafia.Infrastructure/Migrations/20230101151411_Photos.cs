using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Photos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAvatar",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "HasAvatar",
                table: "_Priests");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "_Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPathMin",
                table: "_Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "_Priests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPathMin",
                table: "_Priests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "PhotoPathMin",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "PhotoPathMin",
                table: "_Priests");

            migrationBuilder.AddColumn<bool>(
                name: "HasAvatar",
                table: "_Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasAvatar",
                table: "_Priests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
