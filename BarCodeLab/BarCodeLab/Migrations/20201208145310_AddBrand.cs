using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCodeLab.Migrations
{
    public partial class AddBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "Shoes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shoes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Manufacturer_ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shoes");
        }
    }
}
