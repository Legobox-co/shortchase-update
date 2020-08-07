using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class addedMarketplaceImageFieldOnCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarketplaceImage",
                table: "ListingCategories",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 16, 20, 4, 793, DateTimeKind.Local).AddTicks(6861));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 16, 20, 4, 788, DateTimeKind.Local).AddTicks(3376));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 16, 20, 4, 791, DateTimeKind.Local).AddTicks(8842));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 16, 20, 4, 794, DateTimeKind.Local).AddTicks(490));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 16, 20, 4, 794, DateTimeKind.Local).AddTicks(591));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 16, 20, 4, 794, DateTimeKind.Local).AddTicks(599));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketplaceImage",
                table: "ListingCategories");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 14, 5, 32, 390, DateTimeKind.Local).AddTicks(7602));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 14, 5, 32, 383, DateTimeKind.Local).AddTicks(8741));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 14, 5, 32, 388, DateTimeKind.Local).AddTicks(4628));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 14, 5, 32, 391, DateTimeKind.Local).AddTicks(2597));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 14, 5, 32, 391, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 8, 14, 5, 32, 391, DateTimeKind.Local).AddTicks(2740));
        }
    }
}
