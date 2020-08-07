using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPOTDTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POTDListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    PostedbyId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POTDListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POTDListings_ListingCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ListingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POTDListings_AspNetUsers_PostedbyId",
                        column: x => x.PostedbyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POTDListings_ListingSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "ListingSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 21, 9, 57, 56, 62, DateTimeKind.Local).AddTicks(1099));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 21, 9, 57, 56, 49, DateTimeKind.Local).AddTicks(4404));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 21, 9, 57, 56, 58, DateTimeKind.Local).AddTicks(3325));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 21, 9, 57, 56, 63, DateTimeKind.Local).AddTicks(4589));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 21, 9, 57, 56, 63, DateTimeKind.Local).AddTicks(4786));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 21, 9, 57, 56, 63, DateTimeKind.Local).AddTicks(4800));

            migrationBuilder.CreateIndex(
                name: "IX_POTDListings_CategoryId",
                table: "POTDListings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListings_PostedbyId",
                table: "POTDListings",
                column: "PostedbyId");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListings_SubCategoryId",
                table: "POTDListings",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POTDListings");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 20, 12, 20, 12, 739, DateTimeKind.Local).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 20, 12, 20, 12, 732, DateTimeKind.Local).AddTicks(3024));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 20, 12, 20, 12, 737, DateTimeKind.Local).AddTicks(314));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 20, 12, 20, 12, 739, DateTimeKind.Local).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 20, 12, 20, 12, 739, DateTimeKind.Local).AddTicks(5222));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 20, 12, 20, 12, 739, DateTimeKind.Local).AddTicks(5228));
        }
    }
}
