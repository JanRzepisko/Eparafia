using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Parishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CallName = table.Column<string>(type: "longtext", nullable: true)
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
                    ContactPhoneNumber = table.Column<string>(name: "Contact_PhoneNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactEmail = table.Column<string>(name: "Contact_Email", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Parishes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Intention",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AutomaticAllocation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsNovena = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Intention", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Intention__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Payment__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Priests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FunctionParish = table.Column<int>(type: "int", nullable: false),
                    ContactPhoneNumber = table.Column<string>(name: "Contact_PhoneNumber", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactEmail = table.Column<string>(name: "Contact_Email", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhotoPath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhotoPathMin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Priests", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Priests__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhotoPath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhotoPathMin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_CommonWeek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventInWeekId = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<int>(name: "Event_Type", type: "int", nullable: false),
                    EventDescription = table.Column<string>(name: "Event_Description", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventName = table.Column<string>(name: "Event_Name", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDuration = table.Column<int>(name: "Event_Duration", type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    IntentionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CommonWeek", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CommonWeek__Intention_IntentionId",
                        column: x => x.IntentionId,
                        principalTable: "_Intention",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__CommonWeek__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_SpecialEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventType = table.Column<int>(name: "Event_Type", type: "int", nullable: false),
                    EventDescription = table.Column<string>(name: "Event_Description", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventName = table.Column<string>(name: "Event_Name", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDuration = table.Column<int>(name: "Event_Duration", type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IntentionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SpecialEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SpecialEvent__Intention_IntentionId",
                        column: x => x.IntentionId,
                        principalTable: "_Intention",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__SpecialEvent__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Announcement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParishId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Post__Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "_Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Post__Priests_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "_Priests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_AnnouncementsRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnnouncementId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_PostFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePathMin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PostFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PostFile__Post_PostId",
                        column: x => x.PostId,
                        principalTable: "_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX__CommonWeek_IntentionId",
                table: "_CommonWeek",
                column: "IntentionId");

            migrationBuilder.CreateIndex(
                name: "IX__CommonWeek_ParishId",
                table: "_CommonWeek",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__Intention_ParishId",
                table: "_Intention",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__Payment_ParishId",
                table: "_Payment",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__Post_AuthorId",
                table: "_Post",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX__Post_ParishId",
                table: "_Post",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__PostFile_PostId",
                table: "_PostFile",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX__Priests_ParishId",
                table: "_Priests",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX__SpecialEvent_IntentionId",
                table: "_SpecialEvent",
                column: "IntentionId");

            migrationBuilder.CreateIndex(
                name: "IX__SpecialEvent_ParishId",
                table: "_SpecialEvent",
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
                name: "_AnnouncementsRecords");

            migrationBuilder.DropTable(
                name: "_CommonWeek");

            migrationBuilder.DropTable(
                name: "_Payment");

            migrationBuilder.DropTable(
                name: "_PostFile");

            migrationBuilder.DropTable(
                name: "_SpecialEvent");

            migrationBuilder.DropTable(
                name: "_Users");

            migrationBuilder.DropTable(
                name: "_Announcement");

            migrationBuilder.DropTable(
                name: "_Post");

            migrationBuilder.DropTable(
                name: "_Intention");

            migrationBuilder.DropTable(
                name: "_Priests");

            migrationBuilder.DropTable(
                name: "_Parishes");
        }
    }
}
