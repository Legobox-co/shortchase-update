using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedPaymentStatusToUserSubscriptionPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "SubscriptionPlans");

            migrationBuilder.AddColumn<Guid>(
                name: "GiftById",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "UserSubscriptions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 16, 11, 267, DateTimeKind.Local).AddTicks(8614));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 16, 11, 262, DateTimeKind.Local).AddTicks(7124));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 16, 11, 265, DateTimeKind.Local).AddTicks(8225));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 16, 11, 268, DateTimeKind.Local).AddTicks(2626));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 16, 11, 268, DateTimeKind.Local).AddTicks(2716));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 17, 14, 16, 11, 268, DateTimeKind.Local).AddTicks(2725));

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_GiftById",
                table: "UserSubscriptions",
                column: "GiftById");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_AspNetUsers_GiftById",
                table: "UserSubscriptions",
                column: "GiftById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_AspNetUsers_GiftById",
                table: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptions_GiftById",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "GiftById",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "UserSubscriptions");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
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
    }
}
