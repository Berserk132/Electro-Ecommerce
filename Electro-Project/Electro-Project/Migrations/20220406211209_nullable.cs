using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electro_Project.Migrations
{
    public partial class nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "WishListId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "WishListId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
