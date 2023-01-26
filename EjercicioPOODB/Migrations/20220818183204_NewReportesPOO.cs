using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class NewReportesPOO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas");

            migrationBuilder.AddForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas",
                column: "ColeccionesFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas");

            migrationBuilder.AddForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas",
                column: "ColeccionesFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
