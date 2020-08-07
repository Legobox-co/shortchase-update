using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedReportListingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "BetListings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BetListingReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    ReportContent = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false),
                    DateReported = table.Column<DateTime>(nullable: false),
                    ReportedListingId = table.Column<Guid>(nullable: false),
                    ReportedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetListingReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetListingReports_AspNetUsers_ReportedById",
                        column: x => x.ReportedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BetListingReports_BetListings_ReportedListingId",
                        column: x => x.ReportedListingId,
                        principalTable: "BetListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BetListingReports_ReportedById",
                table: "BetListingReports",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_BetListingReports_ReportedListingId",
                table: "BetListingReports",
                column: "ReportedListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetListingReports");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "BetListings");

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
    }
}
