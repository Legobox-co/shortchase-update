using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedBoisterousAndShortchaseProFieldsOnBetListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBoisterousListing",
                table: "BetListings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProCapperListing",
                table: "BetListings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "BetListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 12, 42, 17, 850, DateTimeKind.Local).AddTicks(9714));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 12, 42, 17, 847, DateTimeKind.Local).AddTicks(117));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 12, 42, 17, 849, DateTimeKind.Local).AddTicks(4736));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 12, 42, 17, 851, DateTimeKind.Local).AddTicks(2594));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 12, 42, 17, 851, DateTimeKind.Local).AddTicks(2662));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 12, 42, 17, 851, DateTimeKind.Local).AddTicks(2667));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBoisterousListing",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "IsProCapperListing",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "BetListings");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 11, 1, 38, 585, DateTimeKind.Local).AddTicks(601));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 11, 1, 38, 579, DateTimeKind.Local).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 11, 1, 38, 583, DateTimeKind.Local).AddTicks(832));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 11, 1, 38, 585, DateTimeKind.Local).AddTicks(5399));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 11, 1, 38, 585, DateTimeKind.Local).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 11, 1, 38, 585, DateTimeKind.Local).AddTicks(5603));
        }
    }
}
