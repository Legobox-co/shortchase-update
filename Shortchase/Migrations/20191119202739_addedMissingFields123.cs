using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class addedMissingFields123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateVerifiedByApi",
                table: "BetListings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResultVerificationByApi",
                table: "BetListings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 19, 13, 27, 38, 418, DateTimeKind.Local).AddTicks(3962));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 19, 13, 27, 38, 412, DateTimeKind.Local).AddTicks(9279));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 19, 13, 27, 38, 416, DateTimeKind.Local).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 19, 13, 27, 38, 418, DateTimeKind.Local).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 19, 13, 27, 38, 418, DateTimeKind.Local).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 19, 13, 27, 38, 418, DateTimeKind.Local).AddTicks(8113));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateVerifiedByApi",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "ResultVerificationByApi",
                table: "BetListings");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 15, 31, 56, 609, DateTimeKind.Local).AddTicks(1005));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 15, 31, 56, 597, DateTimeKind.Local).AddTicks(4875));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 15, 31, 56, 604, DateTimeKind.Local).AddTicks(9064));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 15, 31, 56, 611, DateTimeKind.Local).AddTicks(2976));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 15, 31, 56, 611, DateTimeKind.Local).AddTicks(3173));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 15, 31, 56, 611, DateTimeKind.Local).AddTicks(3184));
        }
    }
}
