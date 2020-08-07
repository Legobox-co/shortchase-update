using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPayoutToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PayoutId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 30, 9, 39, 39, 638, DateTimeKind.Local).AddTicks(3097));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 30, 9, 39, 39, 633, DateTimeKind.Local).AddTicks(9738));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 30, 9, 39, 39, 636, DateTimeKind.Local).AddTicks(6708));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 30, 9, 39, 39, 638, DateTimeKind.Local).AddTicks(6073));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 30, 9, 39, 39, 638, DateTimeKind.Local).AddTicks(6146));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 30, 9, 39, 39, 638, DateTimeKind.Local).AddTicks(6150));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PayoutId",
                table: "OrderItems",
                column: "PayoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_UserPayouts_PayoutId",
                table: "OrderItems",
                column: "PayoutId",
                principalTable: "UserPayouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_UserPayouts_PayoutId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_PayoutId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PayoutId",
                table: "OrderItems");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 27, 16, 15, 8, 524, DateTimeKind.Local).AddTicks(3804));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 27, 16, 15, 8, 520, DateTimeKind.Local).AddTicks(1948));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 27, 16, 15, 8, 522, DateTimeKind.Local).AddTicks(7827));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 27, 16, 15, 8, 524, DateTimeKind.Local).AddTicks(6877));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 27, 16, 15, 8, 524, DateTimeKind.Local).AddTicks(6947));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 27, 16, 15, 8, 524, DateTimeKind.Local).AddTicks(6952));
        }
    }
}
