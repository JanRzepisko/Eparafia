using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Duration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Event_Duration",
                table: "_SpecialEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Event_Duration",
                table: "_CommonWeek",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Event_Duration",
                table: "_SpecialEvent");

            migrationBuilder.DropColumn(
                name: "Event_Duration",
                table: "_CommonWeek");
        }
    }
}
