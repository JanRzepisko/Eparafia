﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CommonWeek__Intention_IntentionId",
                table: "_CommonWeek");

            migrationBuilder.AlterColumn<Guid>(
                name: "IntentionId",
                table: "_CommonWeek",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK__CommonWeek__Intention_IntentionId",
                table: "_CommonWeek",
                column: "IntentionId",
                principalTable: "_Intention",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CommonWeek__Intention_IntentionId",
                table: "_CommonWeek");

            migrationBuilder.AlterColumn<Guid>(
                name: "IntentionId",
                table: "_CommonWeek",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK__CommonWeek__Intention_IntentionId",
                table: "_CommonWeek",
                column: "IntentionId",
                principalTable: "_Intention",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
