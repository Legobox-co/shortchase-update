using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPAypalFieldsToSubscriptionPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaypalProductId",
                table: "SubscriptionPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalProductName",
                table: "SubscriptionPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalSubscriptionPlanDescription",
                table: "SubscriptionPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalSubscriptionPlanId",
                table: "SubscriptionPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalSubscriptionPlanName",
                table: "SubscriptionPlans",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaypalProductId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PaypalProductName",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PaypalSubscriptionPlanDescription",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PaypalSubscriptionPlanId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PaypalSubscriptionPlanName",
                table: "SubscriptionPlans");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 8, 13, 46, 18, 19, DateTimeKind.Local).AddTicks(2646));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 8, 13, 46, 18, 15, DateTimeKind.Local).AddTicks(1318));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 8, 13, 46, 18, 17, DateTimeKind.Local).AddTicks(7055));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 8, 13, 46, 18, 19, DateTimeKind.Local).AddTicks(5971));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 8, 13, 46, 18, 19, DateTimeKind.Local).AddTicks(6040));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2020, 1, 8, 13, 46, 18, 19, DateTimeKind.Local).AddTicks(6045));
        }
    }
}
