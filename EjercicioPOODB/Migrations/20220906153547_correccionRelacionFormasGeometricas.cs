using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class correccionRelacionFormasGeometricas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
    }
}
