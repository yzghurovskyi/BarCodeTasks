using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCodeLab.Migrations
{
    public partial class AddBrandToShoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Manufacturer_ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Shoes");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Shoes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Brand_BrandId",
                table: "Shoes",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Brand_BrandId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Shoes");

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "Shoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ManufacturerId",
                table: "Shoes",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Manufacturer_ManufacturerId",
                table: "Shoes",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
