using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class ChangedMarket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Markets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bookmakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmakers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 11, 57, 42, 329, DateTimeKind.Local).AddTicks(512));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 11, 57, 42, 325, DateTimeKind.Local).AddTicks(1341));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 11, 57, 42, 327, DateTimeKind.Local).AddTicks(5992));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 11, 57, 42, 329, DateTimeKind.Local).AddTicks(3180));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 11, 57, 42, 329, DateTimeKind.Local).AddTicks(3243));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 11, 57, 42, 329, DateTimeKind.Local).AddTicks(3247));

            migrationBuilder.CreateIndex(
                name: "IX_Markets_CategoryId",
                table: "Markets",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_ListingCategories_CategoryId",
                table: "Markets",
                column: "CategoryId",
                principalTable: "ListingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markets_ListingCategories_CategoryId",
                table: "Markets");

            migrationBuilder.DropTable(
                name: "Bookmakers");

            migrationBuilder.DropIndex(
                name: "IX_Markets_CategoryId",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Markets");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 10, 11, 40, 992, DateTimeKind.Local).AddTicks(5406));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 10, 11, 40, 988, DateTimeKind.Local).AddTicks(3906));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 10, 11, 40, 990, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 10, 11, 40, 992, DateTimeKind.Local).AddTicks(8357));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 10, 11, 40, 992, DateTimeKind.Local).AddTicks(8426));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 4, 10, 11, 40, 992, DateTimeKind.Local).AddTicks(8431));
        }
    }
}
