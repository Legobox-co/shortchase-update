using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedFieldToOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "Orders",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 16, 10, 4, 917, DateTimeKind.Local).AddTicks(1741));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 16, 10, 4, 913, DateTimeKind.Local).AddTicks(270));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 16, 10, 4, 915, DateTimeKind.Local).AddTicks(6025));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 16, 10, 4, 917, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 16, 10, 4, 917, DateTimeKind.Local).AddTicks(4774));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 16, 10, 4, 917, DateTimeKind.Local).AddTicks(4779));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(1381));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 464, DateTimeKind.Local).AddTicks(782));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 466, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(4380));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(4445));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(4450));
        }
    }
}
