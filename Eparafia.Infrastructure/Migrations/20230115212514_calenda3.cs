using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class calenda3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Event_EventDescription",
                table: "_SpecialEvent",
                newName: "Event_Description");

            migrationBuilder.RenameColumn(
                name: "Event_EventDescription",
                table: "_CommonWeek",
                newName: "Event_Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Event_Description",
                table: "_SpecialEvent",
                newName: "Event_EventDescription");

            migrationBuilder.RenameColumn(
                name: "Event_Description",
                table: "_CommonWeek",
                newName: "Event_EventDescription");
        }
    }
}
