using EjercicioPOO.Application.Traducciones;
using EjercicioPOO.Enum;

namespace EjercicioPOO.Application.Dto
{
    public class CuadradoDto : FormaGeometricaDto
    {
        public CuadradoDto(decimal ancho) : base(ancho, FormasEnum.Cuadrado)
        {
            Tipo = FormasEnum.Cuadrado;
        }
        public int CuadradoID { get; set; }
        public int FormaGeometricaID { get; set; }
        public override decimal Area { get => Lado * Lado; }
        public override decimal Perimetro { get => Lado * 4; }

        public override string TraducirForma(int cantidad) => cantidad == 1 ? Traduccion.Cuadrado : Traduccion.Cuadrados;
    }
}
