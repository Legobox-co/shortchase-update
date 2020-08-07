using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedBetListingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetListing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    PickType = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Pick = table.Column<string>(nullable: true),
                    Odds = table.Column<string>(nullable: true),
                    OddsFormat = table.Column<string>(nullable: true),
                    WhereToPlay = table.Column<string>(nullable: true),
                    Analysis = table.Column<string>(nullable: true),
                    Prediction = table.Column<string>(nullable: true),
                    Stake = table.Column<decimal>(nullable: false),
                    Profit = table.Column<decimal>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    IsCorrect = table.Column<bool>(nullable: true),
                    PostedbyId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetListing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetListing_ListingCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ListingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetListing_AspNetUsers_PostedbyId",
                        column: x => x.PostedbyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetListing_ListingSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "ListingSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BetListing_CategoryId",
                table: "BetListing",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BetListing_PostedbyId",
                table: "BetListing",
                column: "PostedbyId");

            migrationBuilder.CreateIndex(
                name: "IX_BetListing_SubCategoryId",
                table: "BetListing",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetListing");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 360, DateTimeKind.Local).AddTicks(817));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 363, DateTimeKind.Local).AddTicks(2867));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(7976));
        }
    }
}
