using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class PickPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Team1Photo",
                table: "Picks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team2Photo",
                table: "Picks",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 8, 4, 12, 15, 5, 312, DateTimeKind.Local).AddTicks(2141));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 8, 4, 12, 15, 5, 309, DateTimeKind.Local).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 8, 4, 12, 15, 5, 310, DateTimeKind.Local).AddTicks(9445));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 8, 4, 12, 15, 5, 312, DateTimeKind.Local).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 8, 4, 12, 15, 5, 312, DateTimeKind.Local).AddTicks(4655));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2020, 8, 4, 12, 15, 5, 312, DateTimeKind.Local).AddTicks(4658));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team1Photo",
                table: "Picks");

            migrationBuilder.DropColumn(
                name: "Team2Photo",
                table: "Picks");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 10, 10, 0, 52, 354, DateTimeKind.Local).AddTicks(7205));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 10, 10, 0, 52, 350, DateTimeKind.Local).AddTicks(1448));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 10, 10, 0, 52, 353, DateTimeKind.Local).AddTicks(793));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2020, 1, 10, 10, 0, 52, 355, DateTimeKind.Local).AddTicks(140));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2020, 1, 10, 10, 0, 52, 355, DateTimeKind.Local).AddTicks(209));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2020, 1, 10, 10, 0, 52, 355, DateTimeKind.Local).AddTicks(214));
        }
    }
}
