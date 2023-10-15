using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWrongIdStoreProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_store_products_stores_StoreId1",
                table: "store_products");

            migrationBuilder.DropIndex(
                name: "IX_store_products_StoreId1",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "STORE_ID",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "StoreId1",
                table: "store_products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STORE_ID",
                table: "store_products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId1",
                table: "store_products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_store_products_StoreId1",
                table: "store_products",
                column: "StoreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_store_products_stores_StoreId1",
                table: "store_products",
                column: "StoreId1",
                principalTable: "stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
