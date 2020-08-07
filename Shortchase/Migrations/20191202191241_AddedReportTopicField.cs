using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedReportTopicField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportTopic",
                table: "BetListingReports",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 2, 12, 12, 40, 848, DateTimeKind.Local).AddTicks(3570));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 2, 12, 12, 40, 844, DateTimeKind.Local).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 2, 12, 12, 40, 846, DateTimeKind.Local).AddTicks(8732));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 2, 12, 12, 40, 848, DateTimeKind.Local).AddTicks(6330));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 2, 12, 12, 40, 848, DateTimeKind.Local).AddTicks(6395));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 2, 12, 12, 40, 848, DateTimeKind.Local).AddTicks(6399));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportTopic",
                table: "BetListingReports");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 13, 56, 55, 298, DateTimeKind.Local).AddTicks(2538));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 13, 56, 55, 294, DateTimeKind.Local).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 13, 56, 55, 296, DateTimeKind.Local).AddTicks(8256));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 13, 56, 55, 298, DateTimeKind.Local).AddTicks(5300));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 13, 56, 55, 298, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 29, 13, 56, 55, 298, DateTimeKind.Local).AddTicks(5374));
        }
    }
}
