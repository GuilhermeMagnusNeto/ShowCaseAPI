using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    /// <inheritdoc />
    public partial class PasswordSaltUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PASSWORD_SALT",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_users_EMAIL",
                table: "users",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_EMAIL",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PASSWORD_SALT",
                table: "users");
        }
    }
}
