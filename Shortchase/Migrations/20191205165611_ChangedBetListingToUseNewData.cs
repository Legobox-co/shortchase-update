using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class ChangedBetListingToUseNewData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "Pick",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "Prediction",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "WhereToPlay",
                table: "BetListings");

            migrationBuilder.AddColumn<int>(
                name: "BookmakerId",
                table: "BetListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "BetListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickId",
                table: "BetListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipId",
                table: "BetListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 9, 56, 10, 561, DateTimeKind.Local).AddTicks(8799));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 9, 56, 10, 557, DateTimeKind.Local).AddTicks(7308));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 9, 56, 10, 560, DateTimeKind.Local).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 9, 56, 10, 562, DateTimeKind.Local).AddTicks(1689));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 9, 56, 10, 562, DateTimeKind.Local).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 5, 9, 56, 10, 562, DateTimeKind.Local).AddTicks(1762));

            migrationBuilder.CreateIndex(
                name: "IX_BetListings_BookmakerId",
                table: "BetListings",
                column: "BookmakerId");

            migrationBuilder.CreateIndex(
                name: "IX_BetListings_MarketId",
                table: "BetListings",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_BetListings_PickId",
                table: "BetListings",
                column: "PickId");

            migrationBuilder.CreateIndex(
                name: "IX_BetListings_TipId",
                table: "BetListings",
                column: "TipId");

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_Bookmakers_BookmakerId",
                table: "BetListings",
                column: "BookmakerId",
                principalTable: "Bookmakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_Markets_MarketId",
                table: "BetListings",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_Picks_PickId",
                table: "BetListings",
                column: "PickId",
                principalTable: "Picks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BetListings_Tips_TipId",
                table: "BetListings",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_Bookmakers_BookmakerId",
                table: "BetListings");

            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_Markets_MarketId",
                table: "BetListings");

            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_Picks_PickId",
                table: "BetListings");

            migrationBuilder.DropForeignKey(
                name: "FK_BetListings_Tips_TipId",
                table: "BetListings");

            migrationBuilder.DropIndex(
                name: "IX_BetListings_BookmakerId",
                table: "BetListings");

            migrationBuilder.DropIndex(
                name: "IX_BetListings_MarketId",
                table: "BetListings");

            migrationBuilder.DropIndex(
                name: "IX_BetListings_PickId",
                table: "BetListings");

            migrationBuilder.DropIndex(
                name: "IX_BetListings_TipId",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "BookmakerId",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "PickId",
                table: "BetListings");

            migrationBuilder.DropColumn(
                name: "TipId",
                table: "BetListings");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "BetListings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Pick",
                table: "BetListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prediction",
                table: "BetListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "BetListings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "WhereToPlay",
                table: "BetListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 16, 15, 57, 585, DateTimeKind.Local).AddTicks(2421));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 16, 15, 57, 580, DateTimeKind.Local).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 16, 15, 57, 583, DateTimeKind.Local).AddTicks(6509));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 16, 15, 57, 585, DateTimeKind.Local).AddTicks(5312));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 16, 15, 57, 585, DateTimeKind.Local).AddTicks(5384));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 16, 15, 57, 585, DateTimeKind.Local).AddTicks(5389));
        }
    }
}
