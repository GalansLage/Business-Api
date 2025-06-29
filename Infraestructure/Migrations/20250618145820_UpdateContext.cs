using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "precio_divisa",
                table: "Product",
                newName: "price_currency");

            migrationBuilder.RenameColumn(
                name: "precio_amount",
                table: "Product",
                newName: "price_amount");

            migrationBuilder.AddColumn<int>(
                name: "cost_amount",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "cost_currency",
                table: "Product",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost_amount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "cost_currency",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "price_currency",
                table: "Product",
                newName: "precio_divisa");

            migrationBuilder.RenameColumn(
                name: "price_amount",
                table: "Product",
                newName: "precio_amount");
        }
    }
}
