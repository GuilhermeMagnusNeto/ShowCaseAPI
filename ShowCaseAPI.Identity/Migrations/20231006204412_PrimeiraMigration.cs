using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VALUE = table.Column<double>(type: "float", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_PICTURE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PASSWORD_HASH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL_CONFIRMED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    STORE_LOGO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.ID);
                    table.ForeignKey(
                        name: "FK_stores_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "showcases",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EXCLUSIVE_URL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STORE_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showcases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_showcases_stores_StoreId1",
                        column: x => x.StoreId1,
                        principalTable: "stores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "store_products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STORE_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PRODUCT_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_store_products_products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_store_products_stores_StoreId1",
                        column: x => x.StoreId1,
                        principalTable: "stores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "showcase_styles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SHOWCASE_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowcaseId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TEMPLATE_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BACKGROUND_COLOR_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SHOW_PRODUCT_VALUE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SHOW_STORE_LOGO = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showcase_styles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_showcase_styles_showcases_ShowcaseId1",
                        column: x => x.ShowcaseId1,
                        principalTable: "showcases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_showcase_styles_templates_TemplateId1",
                        column: x => x.TemplateId1,
                        principalTable: "templates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_showcase_styles_ShowcaseId1",
                table: "showcase_styles",
                column: "ShowcaseId1");

            migrationBuilder.CreateIndex(
                name: "IX_showcase_styles_TemplateId1",
                table: "showcase_styles",
                column: "TemplateId1");

            migrationBuilder.CreateIndex(
                name: "IX_showcases_EXCLUSIVE_URL",
                table: "showcases",
                column: "EXCLUSIVE_URL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_showcases_StoreId1",
                table: "showcases",
                column: "StoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_store_products_ProductId1",
                table: "store_products",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_store_products_StoreId1",
                table: "store_products",
                column: "StoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_stores_NAME",
                table: "stores",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_stores_UserId1",
                table: "stores",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_users_ID",
                table: "users",
                column: "ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "showcase_styles");

            migrationBuilder.DropTable(
                name: "store_products");

            migrationBuilder.DropTable(
                name: "showcases");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
