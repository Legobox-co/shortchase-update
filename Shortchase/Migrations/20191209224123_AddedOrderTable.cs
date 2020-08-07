using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortchase.Migrations
{
    public partial class AddedOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    PaymentType = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<string>(nullable: true),
                    DatePaid = table.Column<DateTime>(nullable: true),
                    DateCancelled = table.Column<DateTime>(nullable: true),
                    DateRejected = table.Column<DateTime>(nullable: true),
                    CancelledReason = table.Column<string>(nullable: true),
                    RejectedReason = table.Column<string>(nullable: true),
                    TotalBeforeTaxAndFees = table.Column<decimal>(nullable: false),
                    TotalAfterTax = table.Column<decimal>(nullable: false),
                    ServiceFee = table.Column<decimal>(nullable: false),
                    EstimatedTax = table.Column<decimal>(nullable: false),
                    ServiceFeePercent = table.Column<decimal>(nullable: false),
                    EstimatedTaxPercent = table.Column<decimal>(nullable: false),
                    WalletBalanceBeforePurchase = table.Column<decimal>(nullable: false),
                    WalletBalanceAfterPurchase = table.Column<decimal>(nullable: false),
                    CapperComission = table.Column<decimal>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ListingTitle = table.Column<string>(nullable: true),
                    SoldBy = table.Column<string>(nullable: true),
                    BetListingId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_BetListings_BetListingId",
                        column: x => x.BetListingId,
                        principalTable: "BetListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(1381));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 464, DateTimeKind.Local).AddTicks(782));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 466, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(4380));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(4445));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 9, 15, 41, 22, 468, DateTimeKind.Local).AddTicks(4450));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BetListingId",
                table: "OrderItems",
                column: "BetListingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.UpdateData(
                table: "EmailConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 7, 22, 18, 56, 916, DateTimeKind.Local).AddTicks(9887));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 7, 22, 18, 56, 911, DateTimeKind.Local).AddTicks(4765));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 7, 22, 18, 56, 914, DateTimeKind.Local).AddTicks(8201));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RowDate",
                value: new DateTime(2019, 12, 7, 22, 18, 56, 917, DateTimeKind.Local).AddTicks(4024));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "RowDate",
                value: new DateTime(2019, 12, 7, 22, 18, 56, 917, DateTimeKind.Local).AddTicks(4138));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "RowDate",
                value: new DateTime(2019, 12, 7, 22, 18, 56, 917, DateTimeKind.Local).AddTicks(4147));
        }
    }
}
