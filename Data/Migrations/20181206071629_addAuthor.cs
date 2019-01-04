using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class addAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "authorId",
                table: "posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_authorId",
                table: "posts",
                column: "authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_authorId",
                table: "posts",
                column: "authorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_authorId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_authorId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "authorId",
                table: "posts");
        }
    }
}
