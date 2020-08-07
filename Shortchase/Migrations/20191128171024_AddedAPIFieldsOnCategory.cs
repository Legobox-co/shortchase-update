using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedAPIFieldsOnCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "APIKey",
                table: "ListingCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "APIType",
                table: "ListingCategories",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 28, 10, 10, 23, 560, DateTimeKind.Local).AddTicks(5682));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 28, 10, 10, 23, 556, DateTimeKind.Local).AddTicks(1930));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 28, 10, 10, 23, 558, DateTimeKind.Local).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 28, 10, 10, 23, 560, DateTimeKind.Local).AddTicks(8832));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 28, 10, 10, 23, 560, DateTimeKind.Local).AddTicks(8918));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 28, 10, 10, 23, 560, DateTimeKind.Local).AddTicks(8923));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "APIKey",
                table: "ListingCategories");

            migrationBuilder.DropColumn(
                name: "APIType",
                table: "ListingCategories");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 16, 18, 24, 284, DateTimeKind.Local).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 16, 18, 24, 279, DateTimeKind.Local).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 16, 18, 24, 282, DateTimeKind.Local).AddTicks(5359));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 16, 18, 24, 284, DateTimeKind.Local).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 16, 18, 24, 284, DateTimeKind.Local).AddTicks(9063));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 16, 18, 24, 284, DateTimeKind.Local).AddTicks(9068));
        }
    }
}
