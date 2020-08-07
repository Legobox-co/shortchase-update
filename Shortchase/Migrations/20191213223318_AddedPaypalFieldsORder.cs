using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPaypalFieldsORder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaypalOrderId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalOrderStatus",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaidOnPaypal",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 15, 33, 17, 481, DateTimeKind.Local).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 15, 33, 17, 477, DateTimeKind.Local).AddTicks(1734));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 15, 33, 17, 479, DateTimeKind.Local).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 15, 33, 17, 481, DateTimeKind.Local).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 15, 33, 17, 481, DateTimeKind.Local).AddTicks(4412));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 15, 33, 17, 481, DateTimeKind.Local).AddTicks(4417));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaypalOrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaypalOrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPaidOnPaypal",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 12, 5, 25, 68, DateTimeKind.Local).AddTicks(4445));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 12, 5, 25, 64, DateTimeKind.Local).AddTicks(5477));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 12, 5, 25, 66, DateTimeKind.Local).AddTicks(9763));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 12, 5, 25, 68, DateTimeKind.Local).AddTicks(7133));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 12, 5, 25, 68, DateTimeKind.Local).AddTicks(7197));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 13, 12, 5, 25, 68, DateTimeKind.Local).AddTicks(7202));
        }
    }
}
