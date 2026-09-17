using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurchaseBill.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "Purchase_Bill_Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Purchase_Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Bill_Items_PurchaseOrderId",
                table: "Purchase_Bill_Items",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Bill_Items_Purchase_Orders_PurchaseOrderId",
                table: "Purchase_Bill_Items",
                column: "PurchaseOrderId",
                principalTable: "Purchase_Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Bill_Items_Purchase_Orders_PurchaseOrderId",
                table: "Purchase_Bill_Items");

            migrationBuilder.DropTable(
                name: "Purchase_Orders");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_Bill_Items_PurchaseOrderId",
                table: "Purchase_Bill_Items");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "Purchase_Bill_Items");
        }
    }
}
