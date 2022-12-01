using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parish",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parish", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    HasAvatar = table.Column<bool>(type: "boolean", nullable: false),
                    ParishId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users_Parish_ParishId",
                        column: x => x.ParishId,
                        principalTable: "Parish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Users_ParishId",
                table: "_Users",
                column: "ParishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Users");

            migrationBuilder.DropTable(
                name: "Parish");
        }
    }
}
