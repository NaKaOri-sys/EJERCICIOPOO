using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class ReportesPOO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColeccionesDeFormas_FormasGeometricas_FormaGeometricaID",
                table: "ColeccionesDeFormas");

            migrationBuilder.DropForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas");

            migrationBuilder.DropIndex(
                name: "IX_ColeccionesDeFormas_FormaGeometricaID",
                table: "ColeccionesDeFormas");

            migrationBuilder.AlterColumn<int>(
                name: "ColeccionesFormasID",
                table: "FormasGeometricas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas",
                column: "ColeccionesFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas");

            migrationBuilder.AlterColumn<int>(
                name: "ColeccionesFormasID",
                table: "FormasGeometricas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ColeccionesDeFormas_FormaGeometricaID",
                table: "ColeccionesDeFormas",
                column: "FormaGeometricaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ColeccionesDeFormas_FormasGeometricas_FormaGeometricaID",
                table: "ColeccionesDeFormas",
                column: "FormaGeometricaID",
                principalTable: "FormasGeometricas",
                principalColumn: "FormaGeometricaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas",
                column: "ColeccionesFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID");
        }
    }
}
