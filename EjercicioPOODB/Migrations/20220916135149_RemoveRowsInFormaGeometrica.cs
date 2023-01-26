using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class RemoveRowsInFormaGeometrica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "FormasGeometricas");

            migrationBuilder.DropColumn(
                name: "Perimetro",
                table: "FormasGeometricas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "FormasGeometricas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Perimetro",
                table: "FormasGeometricas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
