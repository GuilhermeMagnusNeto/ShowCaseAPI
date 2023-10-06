using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class FixFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "USER_ID",
                table: "stores",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PRODUCT_ID",
                table: "store_products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "STORE_ID",
                table: "showcases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SHOWCASE_ID",
                table: "showcase_styles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TEMPLATE_ID",
                table: "showcase_styles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_stores_USER_ID",
                table: "stores",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_store_products_PRODUCT_ID",
                table: "store_products",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_showcases_STORE_ID",
                table: "showcases",
                column: "STORE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_showcase_styles_SHOWCASE_ID",
                table: "showcase_styles",
                column: "SHOWCASE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_showcase_styles_TEMPLATE_ID",
                table: "showcase_styles",
                column: "TEMPLATE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_showcase_styles_showcases_SHOWCASE_ID",
                table: "showcase_styles",
                column: "SHOWCASE_ID",
                principalTable: "showcases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showcase_styles_templates_TEMPLATE_ID",
                table: "showcase_styles",
                column: "TEMPLATE_ID",
                principalTable: "templates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showcases_stores_STORE_ID",
                table: "showcases",
                column: "STORE_ID",
                principalTable: "stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_store_products_products_PRODUCT_ID",
                table: "store_products",
                column: "PRODUCT_ID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stores_users_USER_ID",
                table: "stores",
                column: "USER_ID",
                principalTable: "users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_showcase_styles_showcases_SHOWCASE_ID",
                table: "showcase_styles");

            migrationBuilder.DropForeignKey(
                name: "FK_showcase_styles_templates_TEMPLATE_ID",
                table: "showcase_styles");

            migrationBuilder.DropForeignKey(
                name: "FK_showcases_stores_STORE_ID",
                table: "showcases");

            migrationBuilder.DropForeignKey(
                name: "FK_store_products_products_PRODUCT_ID",
                table: "store_products");

            migrationBuilder.DropForeignKey(
                name: "FK_stores_users_USER_ID",
                table: "stores");

            migrationBuilder.DropIndex(
                name: "IX_stores_USER_ID",
                table: "stores");

            migrationBuilder.DropIndex(
                name: "IX_store_products_PRODUCT_ID",
                table: "store_products");

            migrationBuilder.DropIndex(
                name: "IX_showcases_STORE_ID",
                table: "showcases");

            migrationBuilder.DropIndex(
                name: "IX_showcase_styles_SHOWCASE_ID",
                table: "showcase_styles");

            migrationBuilder.DropIndex(
                name: "IX_showcase_styles_TEMPLATE_ID",
                table: "showcase_styles");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "PRODUCT_ID",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "STORE_ID",
                table: "showcases");

            migrationBuilder.DropColumn(
                name: "SHOWCASE_ID",
                table: "showcase_styles");

            migrationBuilder.DropColumn(
                name: "TEMPLATE_ID",
                table: "showcase_styles");
        }
    }
}
