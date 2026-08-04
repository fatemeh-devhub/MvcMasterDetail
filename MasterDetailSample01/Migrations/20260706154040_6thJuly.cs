using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetailSample01.Migrations
{
    /// <inheritdoc />
    public partial class _6thJuly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Sellers",
                newName: "SellerLastName");

            migrationBuilder.RenameColumn(
                name: "CustomerFullName",
                table: "Customers",
                newName: "CustomerLastName");

            migrationBuilder.AddColumn<string>(
                name: "SellerFirstName",
                table: "Sellers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "OrderHeader",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CustomerFirstName",
                table: "Customers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerFirstName",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "CustomerFirstName",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "SellerLastName",
                table: "Sellers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "CustomerLastName",
                table: "Customers",
                newName: "CustomerFullName");
        }
    }
}
