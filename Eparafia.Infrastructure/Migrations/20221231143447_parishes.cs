using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class parishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Parishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AddressRegion = table.Column<string>(name: "Address_Region", type: "text", nullable: false),
                    AddressCity = table.Column<string>(name: "Address_City", type: "text", nullable: false),
                    AddressStreet = table.Column<string>(name: "Address_Street", type: "text", nullable: false),
                    AddressBuildingNumber = table.Column<string>(name: "Address_BuildingNumber", type: "text", nullable: false),
                    AddressFlatNumber = table.Column<string>(name: "Address_FlatNumber", type: "text", nullable: false),
                    AddressPostCode = table.Column<string>(name: "Address_PostCode", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Parishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Priests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    HasAvatar = table.Column<bool>(type: "boolean", nullable: false),
                    ParishId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Priests", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Priests__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    HasAvatar = table.Column<bool>(type: "boolean", nullable: false),
                    ParishId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX__Priests_ParishId",
                table: "_Priests",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__Users_ParishId",
                table: "_Users",
                column: "ParishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Priests");

            migrationBuilder.DropTable(
                name: "_Users");

            migrationBuilder.DropTable(
                name: "_Parishes");
        }
    }
}
