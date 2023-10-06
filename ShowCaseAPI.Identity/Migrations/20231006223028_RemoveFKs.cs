using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_showcase_styles_showcases_ShowcaseId1",
                table: "showcase_styles");

            migrationBuilder.DropForeignKey(
                name: "FK_showcase_styles_templates_TemplateId1",
                table: "showcase_styles");

            migrationBuilder.DropForeignKey(
                name: "FK_showcases_stores_StoreId1",
                table: "showcases");

            migrationBuilder.DropForeignKey(
                name: "FK_store_products_products_ProductId1",
                table: "store_products");

            migrationBuilder.DropForeignKey(
                name: "FK_stores_users_UserId1",
                table: "stores");

            migrationBuilder.DropIndex(
                name: "IX_stores_UserId1",
                table: "stores");

            migrationBuilder.DropIndex(
                name: "IX_store_products_ProductId1",
                table: "store_products");

            migrationBuilder.DropIndex(
                name: "IX_showcases_StoreId1",
                table: "showcases");

            migrationBuilder.DropIndex(
                name: "IX_showcase_styles_ShowcaseId1",
                table: "showcase_styles");

            migrationBuilder.DropIndex(
                name: "IX_showcase_styles_TemplateId1",
                table: "showcase_styles");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "PRODUCT_ID",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "store_products");

            migrationBuilder.DropColumn(
                name: "STORE_ID",
                table: "showcases");

            migrationBuilder.DropColumn(
                name: "StoreId1",
                table: "showcases");

            migrationBuilder.DropColumn(
                name: "SHOWCASE_ID",
                table: "showcase_styles");

            migrationBuilder.DropColumn(
                name: "ShowcaseId1",
                table: "showcase_styles");

            migrationBuilder.DropColumn(
                name: "TEMPLATE_ID",
                table: "showcase_styles");

            migrationBuilder.DropColumn(
                name: "TemplateId1",
                table: "showcase_styles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "USER_ID",
                table: "stores",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "stores",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PRODUCT_ID",
                table: "store_products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "store_products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "STORE_ID",
                table: "showcases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId1",
                table: "showcases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SHOWCASE_ID",
                table: "showcase_styles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ShowcaseId1",
                table: "showcase_styles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TEMPLATE_ID",
                table: "showcase_styles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateId1",
                table: "showcase_styles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_stores_UserId1",
                table: "stores",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_store_products_ProductId1",
                table: "store_products",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_showcases_StoreId1",
                table: "showcases",
                column: "StoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_showcase_styles_ShowcaseId1",
                table: "showcase_styles",
                column: "ShowcaseId1");

            migrationBuilder.CreateIndex(
                name: "IX_showcase_styles_TemplateId1",
                table: "showcase_styles",
                column: "TemplateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_showcase_styles_showcases_ShowcaseId1",
                table: "showcase_styles",
                column: "ShowcaseId1",
                principalTable: "showcases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showcase_styles_templates_TemplateId1",
                table: "showcase_styles",
                column: "TemplateId1",
                principalTable: "templates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showcases_stores_StoreId1",
                table: "showcases",
                column: "StoreId1",
                principalTable: "stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_store_products_products_ProductId1",
                table: "store_products",
                column: "ProductId1",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stores_users_UserId1",
                table: "stores",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
