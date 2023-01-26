using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    public partial class ReporteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    IdiomasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idioma = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.IdiomasID);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeFormas",
                columns: table => new
                {
                    TipoDeFormasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeFormas", x => x.TipoDeFormasID);
                });

            migrationBuilder.CreateTable(
                name: "Circulos",
                columns: table => new
                {
                    CirculoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaGeometricaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circulos", x => x.CirculoID);
                });

            migrationBuilder.CreateTable(
                name: "ColeccionesDeFormas",
                columns: table => new
                {
                    ColeccionesFormasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaGeometricaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColeccionesDeFormas", x => x.ColeccionesFormasID);
                });

            migrationBuilder.CreateTable(
                name: "FormasGeometricas",
                columns: table => new
                {
                    FormaGeometricaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoID = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Perimetro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ColeccionesFormasID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasGeometricas", x => x.FormaGeometricaID);
                    table.ForeignKey(
                        name: "FK_FormasGeometricas_ColeccionesDeFormas_ColeccionesFormasID",
                        column: x => x.ColeccionesFormasID,
                        principalTable: "ColeccionesDeFormas",
                        principalColumn: "ColeccionesFormasID");
                    table.ForeignKey(
                        name: "FK_FormasGeometricas_TipoDeFormas_TipoID",
                        column: x => x.TipoID,
                        principalTable: "TipoDeFormas",
                        principalColumn: "TipoDeFormasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    ReportesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColeccionesDeFormasID = table.Column<int>(type: "int", nullable: false),
                    IdiomasID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.ReportesID);
                    table.ForeignKey(
                        name: "FK_Reportes_ColeccionesDeFormas_ColeccionesDeFormasID",
                        column: x => x.ColeccionesDeFormasID,
                        principalTable: "ColeccionesDeFormas",
                        principalColumn: "ColeccionesFormasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reportes_Idiomas_IdiomasID",
                        column: x => x.IdiomasID,
                        principalTable: "Idiomas",
                        principalColumn: "IdiomasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuadrados",
                columns: table => new
                {
                    CuadradoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaGeometricaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuadrados", x => x.CuadradoID);
                    table.ForeignKey(
                        name: "FK_Cuadrados_FormasGeometricas_FormaGeometricaID",
                        column: x => x.FormaGeometricaID,
                        principalTable: "FormasGeometricas",
                        principalColumn: "FormaGeometricaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trapecios",
                columns: table => new
                {
                    TrapecioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaGeometricaID = table.Column<int>(type: "int", nullable: false),
                    LadoIzquierdo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseMenor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trapecios", x => x.TrapecioID);
                    table.ForeignKey(
                        name: "FK_Trapecios_FormasGeometricas_FormaGeometricaID",
                        column: x => x.FormaGeometricaID,
                        principalTable: "FormasGeometricas",
                        principalColumn: "FormaGeometricaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrianguloEquilateros",
                columns: table => new
                {
                    TrianguloEquilateroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaGeometricaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrianguloEquilateros", x => x.TrianguloEquilateroID);
                    table.ForeignKey(
                        name: "FK_TrianguloEquilateros_FormasGeometricas_FormaGeometricaID",
                        column: x => x.FormaGeometricaID,
                        principalTable: "FormasGeometricas",
                        principalColumn: "FormaGeometricaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Circulos_FormaGeometricaID",
                table: "Circulos",
                column: "FormaGeometricaID");

            migrationBuilder.CreateIndex(
                name: "IX_ColeccionesDeFormas_FormaGeometricaID",
                table: "ColeccionesDeFormas",
                column: "FormaGeometricaID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuadrados_FormaGeometricaID",
                table: "Cuadrados",
                column: "FormaGeometricaID");

            migrationBuilder.CreateIndex(
                name: "IX_FormasGeometricas_ColeccionesFormasID",
                table: "FormasGeometricas",
                column: "ColeccionesFormasID");

            migrationBuilder.CreateIndex(
                name: "IX_FormasGeometricas_TipoID",
                table: "FormasGeometricas",
                column: "TipoID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_ColeccionesDeFormasID",
                table: "Reportes",
                column: "ColeccionesDeFormasID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_IdiomasID",
                table: "Reportes",
                column: "IdiomasID");

            migrationBuilder.CreateIndex(
                name: "IX_Trapecios_FormaGeometricaID",
                table: "Trapecios",
                column: "FormaGeometricaID");

            migrationBuilder.CreateIndex(
                name: "IX_TrianguloEquilateros_FormaGeometricaID",
                table: "TrianguloEquilateros",
                column: "FormaGeometricaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Circulos_FormasGeometricas_FormaGeometricaID",
                table: "Circulos",
                column: "FormaGeometricaID",
                principalTable: "FormasGeometricas",
                principalColumn: "FormaGeometricaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColeccionesDeFormas_FormasGeometricas_FormaGeometricaID",
                table: "ColeccionesDeFormas",
                column: "FormaGeometricaID",
                principalTable: "FormasGeometricas",
                principalColumn: "FormaGeometricaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColeccionesDeFormas_FormasGeometricas_FormaGeometricaID",
                table: "ColeccionesDeFormas");

            migrationBuilder.DropTable(
                name: "Circulos");

            migrationBuilder.DropTable(
                name: "Cuadrados");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Trapecios");

            migrationBuilder.DropTable(
                name: "TrianguloEquilateros");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropTable(
                name: "FormasGeometricas");

            migrationBuilder.DropTable(
                name: "ColeccionesDeFormas");

            migrationBuilder.DropTable(
                name: "TipoDeFormas");
        }
    }
}
