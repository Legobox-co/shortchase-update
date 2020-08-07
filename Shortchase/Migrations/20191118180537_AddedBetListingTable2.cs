using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedBetListingTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetListing_ListingCategories_CategoryId",
                table: "BetListing");

            migrationBuilder.DropForeignKey(
                name: "FK_BetListing_AspNetUsers_PostedbyId",
                table: "BetListing");

            migrationBuilder.DropForeignKey(
                name: "FK_BetListing_ListingSubCategories_SubCategoryId",
                table: "BetListing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BetListing",
                table: "BetListing");

            migrationBuilder.RenameTable(
                name: "BetListing",
                newName: "BetListings");

            migrationBuilder.RenameIndex(
                name: "IX_BetListing_SubCategoryId",
                table: "BetListings",
                newName: "IX_BetListings_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BetListing_PostedbyId",
                table: "BetListings",
                newName: "IX_BetListings_PostedbyId");

            migrationBuilder.RenameIndex(
                name: "IX_BetListing_CategoryId",
                table: "BetListings",
                newName: "IX_BetListings_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BetListings",
                table: "BetListings",
                column: "Id");

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
                name: "FK_BetListings_ListingCategories_CategoryId",
                table: "BetListings",
                column: "CategoryId",
                principalTable: "ListingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_AspNetUsers_PostedbyId",
                table: "BetListings",
                column: "PostedbyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_ListingSubCategories_SubCategoryId",
                table: "BetListings",
                column: "SubCategoryId",
                principalTable: "ListingSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_ListingCategories_CategoryId",
                table: "BetListings");

            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_AspNetUsers_PostedbyId",
                table: "BetListings");

            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_ListingSubCategories_SubCategoryId",
                table: "BetListings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BetListings",
                table: "BetListings");

            migrationBuilder.RenameTable(
                name: "BetListings",
                newName: "BetListing");

            migrationBuilder.RenameIndex(
                name: "IX_BetListings_SubCategoryId",
                table: "BetListing",
                newName: "IX_BetListing_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BetListings_PostedbyId",
                table: "BetListing",
                newName: "IX_BetListing_PostedbyId");

            migrationBuilder.RenameIndex(
                name: "IX_BetListings_CategoryId",
                table: "BetListing",
                newName: "IX_BetListing_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BetListing",
                table: "BetListing",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 10, 49, 12, 602, DateTimeKind.Local).AddTicks(5454));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 10, 49, 12, 594, DateTimeKind.Local).AddTicks(473));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 10, 49, 12, 599, DateTimeKind.Local).AddTicks(3127));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 10, 49, 12, 603, DateTimeKind.Local).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 10, 49, 12, 603, DateTimeKind.Local).AddTicks(727));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 18, 10, 49, 12, 603, DateTimeKind.Local).AddTicks(737));

            migrationBuilder.AddForeignKey(
                name: "FK_BetListing_ListingCategories_CategoryId",
                table: "BetListing",
                column: "CategoryId",
                principalTable: "ListingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BetListing_AspNetUsers_PostedbyId",
                table: "BetListing",
                column: "PostedbyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BetListing_ListingSubCategories_SubCategoryId",
                table: "BetListing",
                column: "SubCategoryId",
                principalTable: "ListingSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
