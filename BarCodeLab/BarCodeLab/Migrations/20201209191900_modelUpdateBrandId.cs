using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCodeLab.Migrations
{
    public partial class modelUpdateBrandId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Brand_BrandId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Country_ManufacturerId",
                table: "Shoes");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Shoes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Shoes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Brand_BrandId",
                table: "Shoes",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Country_ManufacturerId",
                table: "Shoes",
                column: "ManufacturerId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Brand_BrandId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Country_ManufacturerId",
                table: "Shoes");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Shoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Shoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Brand_BrandId",
                table: "Shoes",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Country_ManufacturerId",
                table: "Shoes",
                column: "ManufacturerId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
