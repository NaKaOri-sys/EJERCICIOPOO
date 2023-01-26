using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class ReportesInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DBCC CHECKIDENT ('FormaGeometrica', RESEED, 1000)");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Cuadrados', RESEED, 1000)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
