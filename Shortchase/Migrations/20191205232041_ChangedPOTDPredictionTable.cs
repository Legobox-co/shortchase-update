using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class ChangedPOTDPredictionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "POTDListingPredictions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipId",
                table: "POTDListingPredictions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 16, 20, 40, 998, DateTimeKind.Local).AddTicks(3674));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 16, 20, 40, 994, DateTimeKind.Local).AddTicks(5348));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 16, 20, 40, 996, DateTimeKind.Local).AddTicks(9270));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 16, 20, 40, 998, DateTimeKind.Local).AddTicks(6351));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 16, 20, 40, 998, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 16, 20, 40, 998, DateTimeKind.Local).AddTicks(6423));

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingPredictions_MarketId",
                table: "POTDListingPredictions",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListingPredictions_TipId",
                table: "POTDListingPredictions",
                column: "TipId");

            migrationBuilder.AddForeignKey(
                name: "FK_POTDListingPredictions_Markets_MarketId",
                table: "POTDListingPredictions",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_POTDListingPredictions_Tips_TipId",
                table: "POTDListingPredictions",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POTDListingPredictions_Markets_MarketId",
                table: "POTDListingPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_POTDListingPredictions_Tips_TipId",
                table: "POTDListingPredictions");

            migrationBuilder.DropIndex(
                name: "IX_POTDListingPredictions_MarketId",
                table: "POTDListingPredictions");

            migrationBuilder.DropIndex(
                name: "IX_POTDListingPredictions_TipId",
                table: "POTDListingPredictions");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "POTDListingPredictions");

            migrationBuilder.DropColumn(
                name: "TipId",
                table: "POTDListingPredictions");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 15, 0, 46, 86, DateTimeKind.Local).AddTicks(9670));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 15, 0, 46, 82, DateTimeKind.Local).AddTicks(8564));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 15, 0, 46, 85, DateTimeKind.Local).AddTicks(4439));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 15, 0, 46, 87, DateTimeKind.Local).AddTicks(2571));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 15, 0, 46, 87, DateTimeKind.Local).AddTicks(2642));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 15, 0, 46, 87, DateTimeKind.Local).AddTicks(2647));
        }
    }
}
