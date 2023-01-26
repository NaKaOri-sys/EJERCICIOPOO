using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class RefactorColeccionesFormas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaGeometricaID",
                table: "ColeccionesDeFormas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormaGeometricaID",
                table: "ColeccionesDeFormas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
