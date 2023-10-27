using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_store_products_products_PRODUCT_ID",
                table: "store_products");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropIndex(
                name: "IX_stores_NAME",
                table: "stores");

            migrationBuilder.DropIndex(
                name: "IX_store_products_PRODUCT_ID",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "PRODUCT_ID",
                table: "store_products");

            migrationBuilder.RenameColumn(
                name: "EXCLUSIVE_URL",
                table: "showcases",
                newName: "EXCLUSIVE_CODE");

            migrationBuilder.RenameIndex(
                name: "IX_showcases_EXCLUSIVE_URL",
                table: "showcases",
                newName: "IX_showcases_EXCLUSIVE_CODE");

            migrationBuilder.AddColumn<string>(
                name: "NAME",
                table: "store_products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PRODUCT_PICTURE",
                table: "store_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "store_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VALUE",
                table: "store_products",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NAME",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "PRODUCT_PICTURE",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "VALUE",
                table: "store_products");

            migrationBuilder.RenameColumn(
                name: "EXCLUSIVE_CODE",
                table: "showcases",
                newName: "EXCLUSIVE_URL");

            migrationBuilder.RenameIndex(
                name: "IX_showcases_EXCLUSIVE_CODE",
                table: "showcases",
                newName: "IX_showcases_EXCLUSIVE_URL");

            migrationBuilder.AddColumn<Guid>(
                name: "PRODUCT_ID",
                table: "store_products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DELETED = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    NAME = table.Column<string>(type: "text", nullable: false),
                    PRODUCT_PICTURE = table.Column<string>(type: "text", nullable: true),
                    SKU = table.Column<string>(type: "text", nullable: true),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VALUE = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stores_NAME",
                table: "stores",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_store_products_PRODUCT_ID",
                table: "store_products",
                column: "PRODUCT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_store_products_products_PRODUCT_ID",
                table: "store_products",
                column: "PRODUCT_ID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
