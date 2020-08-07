using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class UpdatedUserSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMonths",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "TotalValueCharged",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "ValuePerMonth",
                table: "UserSubscriptions");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidValue",
                table: "UserSubscriptions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SubscriptionPrice",
                table: "UserSubscriptions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 59, 44, 182, DateTimeKind.Local).AddTicks(1482));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 59, 44, 174, DateTimeKind.Local).AddTicks(3234));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 59, 44, 179, DateTimeKind.Local).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 59, 44, 182, DateTimeKind.Local).AddTicks(6009));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 59, 44, 182, DateTimeKind.Local).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 59, 44, 182, DateTimeKind.Local).AddTicks(6150));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidValue",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPrice",
                table: "UserSubscriptions");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMonths",
                table: "UserSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValueCharged",
                table: "UserSubscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValuePerMonth",
                table: "UserSubscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 22, 41, 440, DateTimeKind.Local).AddTicks(1556));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 22, 41, 432, DateTimeKind.Local).AddTicks(719));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 22, 41, 437, DateTimeKind.Local).AddTicks(353));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 22, 41, 440, DateTimeKind.Local).AddTicks(6222));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 22, 41, 440, DateTimeKind.Local).AddTicks(6358));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 15, 22, 41, 440, DateTimeKind.Local).AddTicks(6371));
        }
    }
}
