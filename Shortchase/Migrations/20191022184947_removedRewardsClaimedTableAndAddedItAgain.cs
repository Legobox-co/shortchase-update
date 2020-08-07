using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class removedRewardsClaimedTableAndAddedItAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RewardsClaimedMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    PointsClaimed = table.Column<int>(nullable: false),
                    EquivalentAmount = table.Column<decimal>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    UsedDate = table.Column<DateTime>(nullable: true),
                    ClaimedIPAddress = table.Column<string>(nullable: true),
                    UsedIPAddress = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardsClaimedMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RewardsClaimedMappings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 12, 49, 46, 167, DateTimeKind.Local).AddTicks(7378));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 12, 49, 46, 159, DateTimeKind.Local).AddTicks(5289));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 12, 49, 46, 165, DateTimeKind.Local).AddTicks(1716));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 12, 49, 46, 168, DateTimeKind.Local).AddTicks(706));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 12, 49, 46, 168, DateTimeKind.Local).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 12, 49, 46, 168, DateTimeKind.Local).AddTicks(862));

            migrationBuilder.CreateIndex(
                name: "IX_RewardsClaimedMappings_UserId",
                table: "RewardsClaimedMappings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RewardsClaimedMappings");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 10, 49, 15, 886, DateTimeKind.Local).AddTicks(4930));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 10, 49, 15, 862, DateTimeKind.Local).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 10, 49, 15, 868, DateTimeKind.Local).AddTicks(8973));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 10, 49, 15, 886, DateTimeKind.Local).AddTicks(9111));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 10, 49, 15, 886, DateTimeKind.Local).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 10, 22, 10, 49, 15, 886, DateTimeKind.Local).AddTicks(9261));
        }
    }
}
