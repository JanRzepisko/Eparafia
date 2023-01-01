using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewInfrastructureFixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Priests__Parishes_ParishId",
                table: "_Priests");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "_Parishes",
                newName: "CallName");

            migrationBuilder.RenameColumn(
                name: "Address_FlatNumber",
                table: "_Parishes",
                newName: "Contact_PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "_Priests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "_Priests",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Contact_Email",
                table: "_Priests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contact_PhoneNumber",
                table: "_Priests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FunctionAtParish",
                table: "_Priests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Contact_Email",
                table: "_Parishes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK__Priests__Parishes_ParishId",
                table: "_Priests",
                column: "ParishId",
                principalTable: "_Parishes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Priests__Parishes_ParishId",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "Contact_Email",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "Contact_PhoneNumber",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "FunctionAtParish",
                table: "_Priests");

            migrationBuilder.DropColumn(
                name: "Contact_Email",
                table: "_Parishes");

            migrationBuilder.RenameColumn(
                name: "Contact_PhoneNumber",
                table: "_Parishes",
                newName: "Address_FlatNumber");

            migrationBuilder.RenameColumn(
                name: "CallName",
                table: "_Parishes",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "_Priests",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "_Priests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Priests__Parishes_ParishId",
                table: "_Priests",
                column: "ParishId",
                principalTable: "_Parishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
