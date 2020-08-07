using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedRelevantDatesToUserSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionCancelDate",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionDeleteDate",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 360, DateTimeKind.Local).AddTicks(817));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 363, DateTimeKind.Local).AddTicks(2867));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 18, 27, 10, 365, DateTimeKind.Local).AddTicks(7976));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionCancelDate",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionDeleteDate",
                table: "UserSubscriptions");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 16, 45, 29, 556, DateTimeKind.Local).AddTicks(1418));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 16, 45, 29, 544, DateTimeKind.Local).AddTicks(942));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 16, 45, 29, 549, DateTimeKind.Local).AddTicks(2556));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 16, 45, 29, 556, DateTimeKind.Local).AddTicks(8079));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 16, 45, 29, 556, DateTimeKind.Local).AddTicks(8215));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 16, 45, 29, 556, DateTimeKind.Local).AddTicks(8224));
        }
    }
}
