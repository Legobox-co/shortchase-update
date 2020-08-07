using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Shortchase.Migrations
{
    public partial class Permissions1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c9dbdcc-c1bb-4cd0-bdf5-7d8aeb60cd4d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbf2840e-72be-4e6b-86ef-157386a45883"));

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.UniqueConstraint("AK_Permissions_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PermissionsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_AspNetUsers_UserId",
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
                value: new DateTime(2019, 10, 11, 15, 52, 13, 623, DateTimeKind.Local).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 11, 15, 52, 13, 619, DateTimeKind.Local).AddTicks(3875));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 11, 15, 52, 13, 622, DateTimeKind.Local).AddTicks(1722));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "GroupName", "Name", "RowDate", "Value" },
                values: new object[,]
                {
                    { 1, "This allows the user to access every feature", "SuperAdmin", "AccessAll", new DateTime(2019, 10, 11, 15, 52, 13, 623, DateTimeKind.Local).AddTicks(7603), 65535 },
                    { 2, "Basic User Role", "Basic User", "User", new DateTime(2019, 10, 11, 15, 52, 13, 623, DateTimeKind.Local).AddTicks(7665), 1 },
                    { 3, "User Has no Role", null, "NotSet", new DateTime(2019, 10, 11, 15, 52, 13, 623, DateTimeKind.Local).AddTicks(7669), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionsId_UserId",
                table: "UserPermissions",
                columns: new[] { "PermissionsId", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("cbf2840e-72be-4e6b-86ef-157386a45883"), "db9ac469-3847-4b4f-b1d2-c57dcb81d80a", "", "User", "USER" },
                    { new Guid("2c9dbdcc-c1bb-4cd0-bdf5-7d8aeb60cd4d"), "31f1db5a-e3f7-4d35-9b45-b27e448c42f2", "", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 9, 13, 11, 22, 748, DateTimeKind.Local).AddTicks(6601));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 10, 9, 13, 11, 22, 745, DateTimeKind.Local).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 10, 9, 13, 11, 22, 748, DateTimeKind.Local).AddTicks(2678));
        }
    }
}