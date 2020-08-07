using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPaymentStatusToSubscriptionPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "SubscriptionPlans",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 3, 43, 832, DateTimeKind.Local).AddTicks(5017));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 3, 43, 827, DateTimeKind.Local).AddTicks(2274));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 3, 43, 830, DateTimeKind.Local).AddTicks(4528));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 3, 43, 832, DateTimeKind.Local).AddTicks(8902));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 3, 43, 832, DateTimeKind.Local).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 3, 43, 832, DateTimeKind.Local).AddTicks(8996));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "SubscriptionPlans");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 16, 27, 19, 913, DateTimeKind.Local).AddTicks(7760));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 16, 27, 19, 903, DateTimeKind.Local).AddTicks(1505));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 16, 27, 19, 910, DateTimeKind.Local).AddTicks(128));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 16, 27, 19, 914, DateTimeKind.Local).AddTicks(3475));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 16, 27, 19, 914, DateTimeKind.Local).AddTicks(3646));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 14, 16, 27, 19, 914, DateTimeKind.Local).AddTicks(3663));
        }
    }
}
