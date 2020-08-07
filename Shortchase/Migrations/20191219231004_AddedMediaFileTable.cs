using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedMediaFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    PhysicalPath = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Size = table.Column<decimal>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    FolderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFiles_MediaFolders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "MediaFolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 16, 10, 2, 523, DateTimeKind.Local).AddTicks(8961));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 16, 10, 2, 518, DateTimeKind.Local).AddTicks(3493));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 16, 10, 2, 521, DateTimeKind.Local).AddTicks(6471));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 16, 10, 2, 524, DateTimeKind.Local).AddTicks(3628));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 16, 10, 2, 524, DateTimeKind.Local).AddTicks(3723));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 16, 10, 2, 524, DateTimeKind.Local).AddTicks(3730));

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_FolderId",
                table: "MediaFiles",
                column: "FolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 14, 9, 25, 456, DateTimeKind.Local).AddTicks(8523));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 14, 9, 25, 451, DateTimeKind.Local).AddTicks(4273));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 14, 9, 25, 454, DateTimeKind.Local).AddTicks(6903));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 14, 9, 25, 457, DateTimeKind.Local).AddTicks(2799));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 14, 9, 25, 457, DateTimeKind.Local).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 19, 14, 9, 25, 457, DateTimeKind.Local).AddTicks(2901));
        }
    }
}
