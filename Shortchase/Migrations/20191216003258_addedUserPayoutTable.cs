using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class addedUserPayoutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPayouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    PayoutBatchId = table.Column<string>(nullable: true),
                    PayoutSenderBatchId = table.Column<string>(nullable: true),
                    PayoutBatchStatus = table.Column<string>(nullable: true),
                    PayoutBatchCheckLink = table.Column<string>(nullable: true),
                    PayoutBatchCompletedDate = table.Column<DateTime>(nullable: true),
                    PayoutBatchCancelledDate = table.Column<DateTime>(nullable: true),
                    PayoutBatchCreatedDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPayouts_AspNetUsers_UserId",
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
                value: new DateTime(2019, 12, 15, 17, 32, 56, 816, DateTimeKind.Local).AddTicks(9558));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 17, 32, 56, 811, DateTimeKind.Local).AddTicks(1085));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 17, 32, 56, 814, DateTimeKind.Local).AddTicks(7222));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 17, 32, 56, 817, DateTimeKind.Local).AddTicks(3639));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 17, 32, 56, 817, DateTimeKind.Local).AddTicks(3739));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 17, 32, 56, 817, DateTimeKind.Local).AddTicks(3748));

            migrationBuilder.CreateIndex(
                name: "IX_UserPayouts_UserId",
                table: "UserPayouts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPayouts");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 13, 15, 43, 775, DateTimeKind.Local).AddTicks(1455));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 13, 15, 43, 769, DateTimeKind.Local).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 13, 15, 43, 772, DateTimeKind.Local).AddTicks(9666));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 13, 15, 43, 775, DateTimeKind.Local).AddTicks(7158));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 13, 15, 43, 775, DateTimeKind.Local).AddTicks(7274));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 15, 13, 15, 43, 775, DateTimeKind.Local).AddTicks(7282));
        }
    }
}
