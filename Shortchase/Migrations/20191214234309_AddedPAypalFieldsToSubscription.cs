using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPAypalFieldsToSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCancelled",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePaid",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalOrderId",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalOrderStatus",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaidOnPaypal",
                table: "UserSubscriptions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 14, 16, 43, 7, 26, DateTimeKind.Local).AddTicks(9915));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 14, 16, 43, 7, 20, DateTimeKind.Local).AddTicks(5002));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 14, 16, 43, 7, 24, DateTimeKind.Local).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 14, 16, 43, 7, 27, DateTimeKind.Local).AddTicks(4921));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 14, 16, 43, 7, 27, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 14, 16, 43, 7, 27, DateTimeKind.Local).AddTicks(5040));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCancelled",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "DatePaid",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "DateRejected",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "PaypalOrderId",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "PaypalOrderStatus",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "TotalPaidOnPaypal",
                table: "UserSubscriptions");

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
    }
}
