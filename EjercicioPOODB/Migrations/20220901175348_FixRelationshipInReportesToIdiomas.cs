using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class FixRelationshipInReportesToIdiomas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Idiomas_Reportes_ReportesID",
                table: "Idiomas");

            migrationBuilder.DropIndex(
                name: "IX_Idiomas_ReportesID",
                table: "Idiomas");

            migrationBuilder.DropColumn(
                name: "ReportesID",
                table: "Idiomas");

            migrationBuilder.AddColumn<int>(
                name: "IdiomasID",
                table: "Reportes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_IdiomasID",
                table: "Reportes",
                column: "IdiomasID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Idiomas_IdiomasID",
                table: "Reportes",
                column: "IdiomasID",
                principalTable: "Idiomas",
                principalColumn: "IdiomasID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Idiomas_IdiomasID",
                table: "Reportes");

            migrationBuilder.DropIndex(
                name: "IX_Reportes_IdiomasID",
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

            migrationBuilder.CreateIndex(
                name: "IX_Idiomas_ReportesID",
                table: "Idiomas",
                column: "ReportesID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Idiomas_Reportes_ReportesID",
                table: "Idiomas",
                column: "ReportesID",
                principalTable: "Reportes",
                principalColumn: "ReportesID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
