using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class TurnNulleableFKReportesToColecciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.AlterColumn<int>(
                name: "ReportesID",
                table: "ColeccionesDeFormas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas",
                column: "ReportesID",
                principalTable: "Reportes",
                principalColumn: "ReportesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas");

            migrationBuilder.AlterColumn<int>(
                name: "ReportesID",
                table: "ColeccionesDeFormas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ColeccionesDeFormas_Reportes_ReportesID",
                table: "ColeccionesDeFormas",
                column: "ReportesID",
                principalTable: "Reportes",
                principalColumn: "ReportesID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
