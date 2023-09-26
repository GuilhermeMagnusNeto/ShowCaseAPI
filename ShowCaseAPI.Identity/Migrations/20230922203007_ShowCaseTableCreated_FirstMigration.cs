using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class ShowCaseTableCreated_FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "show_cases",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EXCLUSIVE_URL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show_cases", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_show_cases_EXCLUSIVE_URL",
                table: "show_cases",
                column: "EXCLUSIVE_URL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "show_cases");
        }
    }
}
