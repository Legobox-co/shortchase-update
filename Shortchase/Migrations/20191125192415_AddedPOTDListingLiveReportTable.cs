using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPOTDListingLiveReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POTDListingLiveReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    Report = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DateTimeReported = table.Column<DateTime>(nullable: false),
                    ReportedById = table.Column<Guid>(nullable: false),
                    POTDId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POTDListingLiveReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POTDListingLiveReports_POTDListings_POTDId",
                        column: x => x.POTDId,
                        principalTable: "POTDListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POTDListingLiveReports_AspNetUsers_ReportedById",
                        column: x => x.ReportedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 25, 12, 24, 14, 687, DateTimeKind.Local).AddTicks(24));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 25, 12, 24, 14, 679, DateTimeKind.Local).AddTicks(2356));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 25, 12, 24, 14, 684, DateTimeKind.Local).AddTicks(2092));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 25, 12, 24, 14, 687, DateTimeKind.Local).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 25, 12, 24, 14, 687, DateTimeKind.Local).AddTicks(4970));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 25, 12, 24, 14, 687, DateTimeKind.Local).AddTicks(4982));

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingLiveReports_POTDId",
                table: "POTDListingLiveReports",
                column: "POTDId");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingLiveReports_ReportedById",
                table: "POTDListingLiveReports",
                column: "ReportedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POTDListingLiveReports");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 24, 13, 14, 43, 127, DateTimeKind.Local).AddTicks(5911));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 24, 13, 14, 43, 120, DateTimeKind.Local).AddTicks(733));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 24, 13, 14, 43, 124, DateTimeKind.Local).AddTicks(1219));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 24, 13, 14, 43, 128, DateTimeKind.Local).AddTicks(323));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 24, 13, 14, 43, 128, DateTimeKind.Local).AddTicks(429));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 24, 13, 14, 43, 128, DateTimeKind.Local).AddTicks(437));
        }
    }
}
