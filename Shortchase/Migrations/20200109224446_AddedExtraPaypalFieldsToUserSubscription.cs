using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedExtraPaypalFieldsToUserSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaypalFacilitatorAccessToken",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalSubscriptionId",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 15, 44, 44, 982, DateTimeKind.Local).AddTicks(6275));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 15, 44, 44, 978, DateTimeKind.Local).AddTicks(4778));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 15, 44, 44, 981, DateTimeKind.Local).AddTicks(508));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 15, 44, 44, 982, DateTimeKind.Local).AddTicks(9317));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 15, 44, 44, 982, DateTimeKind.Local).AddTicks(9389));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 15, 44, 44, 982, DateTimeKind.Local).AddTicks(9394));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaypalFacilitatorAccessToken",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "PaypalSubscriptionId",
                table: "UserSubscriptions");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 11, 6, 5, 743, DateTimeKind.Local).AddTicks(3391));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 11, 6, 5, 739, DateTimeKind.Local).AddTicks(123));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 11, 6, 5, 741, DateTimeKind.Local).AddTicks(7257));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 11, 6, 5, 743, DateTimeKind.Local).AddTicks(6347));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 11, 6, 5, 743, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2020, 1, 9, 11, 6, 5, 743, DateTimeKind.Local).AddTicks(6424));
        }
    }
}
