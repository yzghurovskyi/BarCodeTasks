using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCodeLab.Migrations
{
    public partial class modelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Shoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "Shoes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "Shoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ManufacturerId",
                table: "Shoes",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Country_ManufacturerId",
                table: "Shoes",
                column: "ManufacturerId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Country_ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Shoes");
        }
    }
}
