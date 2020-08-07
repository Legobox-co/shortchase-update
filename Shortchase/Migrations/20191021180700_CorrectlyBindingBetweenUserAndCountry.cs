using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class CorrectlyBindingBetweenUserAndCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneCountry",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "PhoneCountryId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 12, 6, 59, 147, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 12, 6, 59, 139, DateTimeKind.Local).AddTicks(468));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 12, 6, 59, 144, DateTimeKind.Local).AddTicks(4683));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 12, 6, 59, 147, DateTimeKind.Local).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 12, 6, 59, 147, DateTimeKind.Local).AddTicks(6764));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 12, 6, 59, 147, DateTimeKind.Local).AddTicks(6775));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneCountryId",
                table: "AspNetUsers",
                column: "PhoneCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_PhoneCountryId",
                table: "AspNetUsers",
                column: "PhoneCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_PhoneCountryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhoneCountryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneCountryId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PhoneCountry",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 11, 13, 9, 438, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 11, 13, 9, 431, DateTimeKind.Local).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 11, 13, 9, 436, DateTimeKind.Local).AddTicks(5351));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 11, 13, 9, 438, DateTimeKind.Local).AddTicks(8924));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 11, 13, 9, 438, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 10, 21, 11, 13, 9, 438, DateTimeKind.Local).AddTicks(9043));
        }
    }
}
