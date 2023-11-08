using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class descriptionProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "store_products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "showcase_products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    SHOWCASE_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    STORE_PRODUCT_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DELETED = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showcase_products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_showcase_products_showcases_SHOWCASE_ID",
                        column: x => x.SHOWCASE_ID,
                        principalTable: "showcases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_showcase_products_store_products_STORE_PRODUCT_ID",
                        column: x => x.STORE_PRODUCT_ID,
                        principalTable: "store_products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_showcase_products_SHOWCASE_ID",
                table: "showcase_products",
                column: "SHOWCASE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_showcase_products_STORE_PRODUCT_ID",
                table: "showcase_products",
                column: "STORE_PRODUCT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "showcase_products");

            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "store_products");
        }
    }
}
