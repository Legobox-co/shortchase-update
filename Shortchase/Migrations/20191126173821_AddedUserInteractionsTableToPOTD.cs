using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedUserInteractionsTableToPOTD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POTDListingLiveReportingInteractions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    InteractionType = table.Column<string>(nullable: true),
                    InteractedById = table.Column<Guid>(nullable: false),
                    POTDLiveReportId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POTDListingLiveReportingInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POTDListingLiveReportingInteractions_AspNetUsers_InteractedById",
                        column: x => x.InteractedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POTDListingLiveReportingInteractions_POTDListingLiveReports_POTDLiveReportId",
                        column: x => x.POTDLiveReportId,
                        principalTable: "POTDListingLiveReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 10, 38, 20, 429, DateTimeKind.Local).AddTicks(3546));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 10, 38, 20, 422, DateTimeKind.Local).AddTicks(640));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 10, 38, 20, 427, DateTimeKind.Local).AddTicks(4432));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 10, 38, 20, 429, DateTimeKind.Local).AddTicks(6733));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 10, 38, 20, 429, DateTimeKind.Local).AddTicks(6830));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 26, 10, 38, 20, 429, DateTimeKind.Local).AddTicks(6836));

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingLiveReportingInteractions_InteractedById",
                table: "POTDListingLiveReportingInteractions",
                column: "InteractedById");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingLiveReportingInteractions_POTDLiveReportId",
                table: "POTDListingLiveReportingInteractions",
                column: "POTDLiveReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POTDListingLiveReportingInteractions");

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
        }
    }
}
