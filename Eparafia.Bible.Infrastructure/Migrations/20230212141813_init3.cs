using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Bible.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "_Days");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "_Days",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
