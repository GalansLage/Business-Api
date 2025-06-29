using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductTransaction_ProductTransactionId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTransactionId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "InTime",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductState",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTransactionId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "StockStore",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "text", nullable: false),
                    InTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProductState = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ProductTransactionId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductItem_ProductTransaction_ProductTransactionId",
                        column: x => x.ProductTransactionId,
                        principalTable: "ProductTransaction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItem_ProductId",
                table: "ProductItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItem_ProductTransactionId",
                table: "ProductItem",
                column: "ProductTransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "InTime",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductState",
                table: "Product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductTransactionId",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockStore",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTransactionId",
                table: "Product",
                column: "ProductTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductTransaction_ProductTransactionId",
                table: "Product",
                column: "ProductTransactionId",
                principalTable: "ProductTransaction",
                principalColumn: "Id");
        }
    }
}
