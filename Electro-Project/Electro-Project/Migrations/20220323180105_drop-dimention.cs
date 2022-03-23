using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electro_Project.Migrations
{
    public partial class dropdimention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptop_Dimension_DimensionID",
                table: "Laptop");

            migrationBuilder.DropForeignKey(
                name: "FK_Mobile_Dimension_DimensionID",
                table: "Mobile");

            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.DropIndex(
                name: "IX_Mobile_DimensionID",
                table: "Mobile");

            migrationBuilder.DropIndex(
                name: "IX_Laptop_DimensionID",
                table: "Laptop");

            migrationBuilder.DropColumn(
                name: "DimensionID",
                table: "Mobile");

            migrationBuilder.DropColumn(
                name: "DimensionID",
                table: "Laptop");

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Mobile",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Thickness",
                table: "Mobile",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Mobile",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Laptop",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Thickness",
                table: "Laptop",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Laptop",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Mobile");

            migrationBuilder.DropColumn(
                name: "Thickness",
                table: "Mobile");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Mobile");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Laptop");

            migrationBuilder.DropColumn(
                name: "Thickness",
                table: "Laptop");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Laptop");

            migrationBuilder.AddColumn<int>(
                name: "DimensionID",
                table: "Mobile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DimensionID",
                table: "Laptop",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Thickness = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimension", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mobile_DimensionID",
                table: "Mobile",
                column: "DimensionID");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_DimensionID",
                table: "Laptop",
                column: "DimensionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptop_Dimension_DimensionID",
                table: "Laptop",
                column: "DimensionID",
                principalTable: "Dimension",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mobile_Dimension_DimensionID",
                table: "Mobile",
                column: "DimensionID",
                principalTable: "Dimension",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
