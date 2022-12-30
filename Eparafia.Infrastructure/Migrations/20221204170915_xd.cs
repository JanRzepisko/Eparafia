using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class xd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Users__Parishes_ParishId",
                table: "_Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "_Users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__Users__Parishes_ParishId",
                table: "_Users",
                column: "ParishId",
                principalTable: "_Parishes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Users__Parishes_ParishId",
                table: "_Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "_Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Users__Parishes_ParishId",
                table: "_Users",
                column: "ParishId",
                principalTable: "_Parishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
