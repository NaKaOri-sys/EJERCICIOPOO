using EjercicioPOO.Application.Traducciones;
using EjercicioPOO.Enum;

namespace EjercicioPOO.Application.Dto
{
    public class TrapecioDto : FormaGeometricaDto
    {
        public TrapecioDto(decimal baseMayor, decimal baseMenor, decimal altura, decimal ladoDerecho, decimal ladoIzquierdo) : base(baseMayor, FormasEnum.Trapecio, baseMenor, altura, ladoIzquierdo, ladoDerecho)
        {

        }

        public int TrapecioID { get; set; }
        public int FormaGeometricaID { get; set; }
        public override decimal Area { get => (Lado + LadoBase) / 2 * Altura; }
        public override decimal Perimetro { get => Lado + LadoBase + LadoDerecho + LadoIzquierdo; }
        public override string TraducirForma(int cantidad) => cantidad == 1 ? Traduccion.Trapecio : Traduccion.Trapecio;
    }
}
