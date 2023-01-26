using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.DropForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas");

            migrationBuilder.DropIndex(
                name: "IX_ColeccionesDeFormas_ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.DropColumn(
                name: "ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.AddColumn<int>(
                name: "ColeccionesFormasID",
                table: "Reportes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColeccionesFormasID",
                table: "FormasGeometricas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_ColeccionesFormasID",
                table: "Reportes",
                column: "ColeccionesFormasID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas",
                column: "ColeccionesFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_ColeccionesDeFormas_ColeccionesFormasID",
                table: "Reportes",
                column: "ColeccionesFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_ColeccionesDeFormas_ColeccionesFormasID",
                table: "Reportes");

            migrationBuilder.DropIndex(
                name: "IX_Reportes_ColeccionesFormasID",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "ColeccionesFormasID",
                table: "Reportes");

            migrationBuilder.AlterColumn<int>(
                name: "ColeccionesFormasID",
                table: "FormasGeometricas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReportesID",
                table: "ColeccionesDeFormas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColeccionesDeFormas_ReportesID",
                table: "ColeccionesDeFormas",
                column: "ReportesID");

            migrationBuilder.AddForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas",
                column: "ReportesID",
                principalTable: "Reportes",
                principalColumn: "ReportesID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                table: "FormasGeometricas",
                column: "ColeccionesFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID");
        }
    }
}
