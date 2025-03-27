using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningCSharp.RSC.Migrations
{
    /// <inheritdoc />
    public partial class AddUSerID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_AppUserId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Books",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AppUserId",
                table: "Books",
                newName: "IX_Books_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Books",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserId",
                table: "Books",
                newName: "IX_Books_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_AppUserId",
                table: "Books",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
