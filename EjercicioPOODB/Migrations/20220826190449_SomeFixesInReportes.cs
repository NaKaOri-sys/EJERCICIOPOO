using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class SomeFixesInReportes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_ColeccionesDeFormas_ColeccionesDeFormasID",
                table: "Reportes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Idiomas_IdiomasID",
                table: "Reportes");

            migrationBuilder.DropIndex(
                name: "IX_Reportes_ColeccionesDeFormasID",
                table: "Reportes");

            migrationBuilder.DropIndex(
                name: "IX_Reportes_IdiomasID",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "ColeccionesDeFormasID",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "IdiomasID",
                table: "Reportes");

            migrationBuilder.AddColumn<int>(
                name: "ReportesID",
                table: "Idiomas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportesID",
                table: "ColeccionesDeFormas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Idiomas_ReportesID",
                table: "Idiomas",
                column: "ReportesID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColeccionesDeFormas_ReportesID",
                table: "ColeccionesDeFormas",
                column: "ReportesID");

            migrationBuilder.AddForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas",
                column: "ReportesID",
                principalTable: "Reportes",
                principalColumn: "ReportesID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Idiomas_Reportes_ReportesID",
                table: "Idiomas",
                column: "ReportesID",
                principalTable: "Reportes",
                principalColumn: "ReportesID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.DropForeignKey(
                name: "FK_Idiomas_Reportes_ReportesID",
                table: "Idiomas");

            migrationBuilder.DropIndex(
                name: "IX_Idiomas_ReportesID",
                table: "Idiomas");

            migrationBuilder.DropIndex(
                name: "IX_ColeccionesDeFormas_ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.DropColumn(
                name: "ReportesID",
                table: "Idiomas");

            migrationBuilder.DropColumn(
                name: "ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.AddColumn<int>(
                name: "ColeccionesDeFormasID",
                table: "Reportes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdiomasID",
                table: "Reportes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_ColeccionesDeFormasID",
                table: "Reportes",
                column: "ColeccionesDeFormasID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_IdiomasID",
                table: "Reportes",
                column: "IdiomasID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_ColeccionesDeFormas_ColeccionesDeFormasID",
                table: "Reportes",
                column: "ColeccionesDeFormasID",
                principalTable: "ColeccionesDeFormas",
                principalColumn: "ColeccionesFormasID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Idiomas_IdiomasID",
                table: "Reportes",
                column: "IdiomasID",
                principalTable: "Idiomas",
                principalColumn: "IdiomasID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
