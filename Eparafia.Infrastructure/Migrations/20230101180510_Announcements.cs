using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Announcements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Announcement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParishId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Announcement", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Announcement__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Announcement__Priests_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "_Priests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_AnnouncementsRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    AnnouncementId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AnnouncementsRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AnnouncementsRecords__Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "_Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Announcement_AuthorId",
                table: "_Announcement",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX__Announcement_ParishId",
                table: "_Announcement",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__AnnouncementsRecords_AnnouncementId",
                table: "_AnnouncementsRecords",
                column: "AnnouncementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_AnnouncementsRecords");

            migrationBuilder.DropTable(
                name: "_Announcement");
        }
    }
}
