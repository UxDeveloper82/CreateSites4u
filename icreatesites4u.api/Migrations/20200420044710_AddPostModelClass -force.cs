using Microsoft.EntityFrameworkCore.Migrations;

namespace icreatesites4u.api.Migrations
{
    public partial class AddPostModelClassforce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatorId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatorId",
                table: "Posts",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatorId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatorId",
                table: "Posts",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
