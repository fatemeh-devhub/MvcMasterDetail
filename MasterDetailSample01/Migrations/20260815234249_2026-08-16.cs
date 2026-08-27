using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetailSample01.Migrations
{
    /// <inheritdoc />
    public partial class _20260816 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeader_OrderId",
                table: "OrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_GuidKey",
                table: "OrderHeader",
                column: "GuidKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeader_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "OrderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeader_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_GuidKey",
                table: "OrderHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeader_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "OrderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
