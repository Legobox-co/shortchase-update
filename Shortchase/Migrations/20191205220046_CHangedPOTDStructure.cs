using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class CHangedPOTDStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "POTDListings");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "POTDListings");

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "POTDListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickId",
                table: "POTDListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipId",
                table: "POTDListings",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_POTDListings_MarketId",
                table: "POTDListings",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListings_PickId",
                table: "POTDListings",
                column: "PickId");

            migrationBuilder.CreateIndex(
                name: "IX_POTDListings_TipId",
                table: "POTDListings",
                column: "TipId");

            migrationBuilder.AddForeignKey(
                name: "FK_POTDListings_Markets_MarketId",
                table: "POTDListings",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_POTDListings_Picks_PickId",
                table: "POTDListings",
                column: "PickId",
                principalTable: "Picks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_POTDListings_Tips_TipId",
                table: "POTDListings",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POTDListings_Markets_MarketId",
                table: "POTDListings");

            migrationBuilder.DropForeignKey(
                name: "FK_POTDListings_Picks_PickId",
                table: "POTDListings");

            migrationBuilder.DropForeignKey(
                name: "FK_POTDListings_Tips_TipId",
                table: "POTDListings");

            migrationBuilder.DropIndex(
                name: "IX_POTDListings_MarketId",
                table: "POTDListings");

            migrationBuilder.DropIndex(
                name: "IX_POTDListings_PickId",
                table: "POTDListings");

            migrationBuilder.DropIndex(
                name: "IX_POTDListings_TipId",
                table: "POTDListings");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "POTDListings");

            migrationBuilder.DropColumn(
                name: "PickId",
                table: "POTDListings");

            migrationBuilder.DropColumn(
                name: "TipId",
                table: "POTDListings");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "POTDListings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "POTDListings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }
    }
}
