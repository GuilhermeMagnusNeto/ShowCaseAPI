using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class FixIdStoreProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "STORE_ID",
                table: "store_products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_store_products_STORE_ID",
                table: "store_products",
                column: "STORE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_store_products_stores_STORE_ID",
                table: "store_products",
                column: "STORE_ID",
                principalTable: "stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_store_products_stores_STORE_ID",
                table: "store_products");

            migrationBuilder.DropIndex(
                name: "IX_store_products_STORE_ID",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "STORE_ID",
                table: "store_products");
        }
    }
}
