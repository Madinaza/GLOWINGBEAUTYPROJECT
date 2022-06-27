using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlowingFinal.Migrations
{
    public partial class invoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankCarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartNo = table.Column<long>(nullable: false),
                    CardExpiry = table.Column<DateTime>(nullable: false),
                    Cvc = table.Column<int>(nullable: false),
                    HolderName = table.Column<string>(maxLength: 60, nullable: false),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "invoiceNos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoiceNos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unregisteredCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    CountyName = table.Column<string>(maxLength: 30, nullable: false),
                    TownCity = table.Column<string>(maxLength: 30, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    PostcodeZip = table.Column<string>(maxLength: 10, nullable: false),
                    Apartment = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 25, nullable: false),
                    OrderNotes = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unregisteredCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNo = table.Column<string>(nullable: true),
                    SaleDate = table.Column<DateTime>(nullable: false),
                    EndUserId = table.Column<string>(nullable: true),
                    UnregisteredCustomerId = table.Column<int>(nullable: true),
                    hideToClien = table.Column<bool>(nullable: false),
                    isReaded = table.Column<bool>(nullable: false),
                    isHide = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_AspNetUsers_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_unregisteredCustomers_UnregisteredCustomerId",
                        column: x => x.UnregisteredCustomerId,
                        principalTable: "unregisteredCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<byte>(nullable: false),
                    ProductSizeToProductId = table.Column<int>(nullable: false),
                    SaleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_ProductSizeToProducts_ProductSizeToProductId",
                        column: x => x.ProductSizeToProductId,
                        principalTable: "ProductSizeToProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_ProductSizeToProductId",
                table: "SaleItems",
                column: "ProductSizeToProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EndUserId",
                table: "Sales",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UnregisteredCustomerId",
                table: "Sales",
                column: "UnregisteredCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankCarts");

            migrationBuilder.DropTable(
                name: "invoiceNos");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "unregisteredCustomers");
        }
    }
}
