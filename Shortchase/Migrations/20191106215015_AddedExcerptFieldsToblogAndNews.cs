using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedExcerptFieldsToblogAndNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_NewsPosts_Slug",
                table: "NewsPosts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BlogPosts_Slug",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "NewsPosts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Excerpt",
                table: "NewsPosts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "BlogPosts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Excerpt",
                table: "BlogPosts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 14, 50, 14, 728, DateTimeKind.Local).AddTicks(5275));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 14, 50, 14, 719, DateTimeKind.Local).AddTicks(5318));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 14, 50, 14, 724, DateTimeKind.Local).AddTicks(711));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 14, 50, 14, 729, DateTimeKind.Local).AddTicks(6765));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 14, 50, 14, 729, DateTimeKind.Local).AddTicks(6984));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 14, 50, 14, 729, DateTimeKind.Local).AddTicks(6999));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excerpt",
                table: "NewsPosts");

            migrationBuilder.DropColumn(
                name: "Excerpt",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "NewsPosts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "BlogPosts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_NewsPosts_Slug",
                table: "NewsPosts",
                column: "Slug");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BlogPosts_Slug",
                table: "BlogPosts",
                column: "Slug");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 11, 49, 30, 123, DateTimeKind.Local).AddTicks(2569));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 11, 49, 30, 111, DateTimeKind.Local).AddTicks(644));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 11, 49, 30, 121, DateTimeKind.Local).AddTicks(1098));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 11, 49, 30, 123, DateTimeKind.Local).AddTicks(7382));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 11, 49, 30, 123, DateTimeKind.Local).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 11, 6, 11, 49, 30, 123, DateTimeKind.Local).AddTicks(7521));
        }
    }
}
