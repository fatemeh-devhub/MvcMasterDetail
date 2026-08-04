using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetailSample01.Migrations
{
    /// <inheritdoc />
    public partial class _16July : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GuidKey",
                table: "OrderHeader",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentGuid",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuidKey",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "ParentGuid",
                table: "OrderDetails");
        }
    }
}
