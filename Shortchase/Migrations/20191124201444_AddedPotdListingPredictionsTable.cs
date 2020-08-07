using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPotdListingPredictionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POTDListingPredictions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    Prediction = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DatePredicted = table.Column<DateTime>(nullable: false),
                    DateVerified = table.Column<DateTime>(nullable: true),
                    VerifiedAsCorrect = table.Column<bool>(nullable: true),
                    PredictedById = table.Column<Guid>(nullable: false),
                    POTDId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POTDListingPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POTDListingPredictions_POTDListings_POTDId",
                        column: x => x.POTDId,
                        principalTable: "POTDListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POTDListingPredictions_AspNetUsers_PredictedById",
                        column: x => x.PredictedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingPredictions_POTDId",
                table: "POTDListingPredictions",
                column: "POTDId");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingPredictions_PredictedById",
                table: "POTDListingPredictions",
                column: "PredictedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POTDListingPredictions");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 22, 12, 35, 46, 818, DateTimeKind.Local).AddTicks(2377));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 22, 12, 35, 46, 810, DateTimeKind.Local).AddTicks(1186));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 22, 12, 35, 46, 815, DateTimeKind.Local).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 22, 12, 35, 46, 818, DateTimeKind.Local).AddTicks(8510));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 22, 12, 35, 46, 818, DateTimeKind.Local).AddTicks(8664));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 22, 12, 35, 46, 818, DateTimeKind.Local).AddTicks(8675));
        }
    }
}
