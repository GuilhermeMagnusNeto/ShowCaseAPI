using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNameUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NAME",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NAME",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
