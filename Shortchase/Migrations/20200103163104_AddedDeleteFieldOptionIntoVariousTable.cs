using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedDeleteFieldOptionIntoVariousTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Tips",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Picks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Markets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ListingSubCategories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ListingCategories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Bookmakers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 3, 9, 31, 2, 630, DateTimeKind.Local).AddTicks(4109));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 3, 9, 31, 2, 626, DateTimeKind.Local).AddTicks(1463));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 3, 9, 31, 2, 628, DateTimeKind.Local).AddTicks(8111));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 3, 9, 31, 2, 630, DateTimeKind.Local).AddTicks(7019));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 3, 9, 31, 2, 630, DateTimeKind.Local).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2020, 1, 3, 9, 31, 2, 630, DateTimeKind.Local).AddTicks(7095));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Picks");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ListingSubCategories");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ListingCategories");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Bookmakers");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 31, 15, 6, 34, 554, DateTimeKind.Local).AddTicks(1872));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 31, 15, 6, 34, 550, DateTimeKind.Local).AddTicks(385));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 31, 15, 6, 34, 552, DateTimeKind.Local).AddTicks(6175));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 31, 15, 6, 34, 554, DateTimeKind.Local).AddTicks(4745));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 31, 15, 6, 34, 554, DateTimeKind.Local).AddTicks(4818));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 31, 15, 6, 34, 554, DateTimeKind.Local).AddTicks(4822));
        }
    }
}
