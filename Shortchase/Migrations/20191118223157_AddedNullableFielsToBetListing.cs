using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedNullableFielsToBetListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_ListingSubCategories_SubCategoryId",
                table: "BetListings");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "BetListings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_ListingSubCategories_SubCategoryId",
                table: "BetListings",
                column: "SubCategoryId",
                principalTable: "ListingSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_ListingSubCategories_SubCategoryId",
                table: "BetListings");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "BetListings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 11, 5, 36, 167, DateTimeKind.Local).AddTicks(6164));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 11, 5, 36, 154, DateTimeKind.Local).AddTicks(5747));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 11, 5, 36, 164, DateTimeKind.Local).AddTicks(2426));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 11, 5, 36, 168, DateTimeKind.Local).AddTicks(5411));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 11, 5, 36, 168, DateTimeKind.Local).AddTicks(6692));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 11, 5, 36, 168, DateTimeKind.Local).AddTicks(8700));

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_ListingSubCategories_SubCategoryId",
                table: "BetListings",
                column: "SubCategoryId",
                principalTable: "ListingSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
