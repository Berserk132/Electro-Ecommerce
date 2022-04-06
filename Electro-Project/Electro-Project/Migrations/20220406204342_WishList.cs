using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electro_Project.Migrations
{
    public partial class WishList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishList_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_WishListId",
                table: "Product",
                column: "WishListId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WishListId",
                table: "AspNetUsers",
                column: "WishListId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_UserId",
                table: "WishList",
                column: "UserId");
            
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_WishList_WishListId",
                table: "Product",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_WishList_WishListId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropIndex(
                name: "IX_Product_WishListId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WishListId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "AspNetUsers");
        }
    }
}
