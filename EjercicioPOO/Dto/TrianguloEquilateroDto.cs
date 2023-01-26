using EjercicioPOO.Application.Traducciones;
using EjercicioPOO.Enum;
using System;

namespace EjercicioPOO.Application.Dto
{
    public class TrianguloEquilateroDto : FormaGeometricaDto
    {
        public TrianguloEquilateroDto(decimal ancho) : base(ancho, FormasEnum.TrianguloEquilatero)
        {
            Tipo = FormasEnum.TrianguloEquilatero;
        }
        public override decimal Area { get => (decimal)Math.Sqrt(3) / 4 * Lado * Lado; }
        public override decimal Perimetro { get => Lado * 3; }
        public override string TraducirForma(int cantidad) => cantidad == 1 ? Traduccion.Triangulo : Traduccion.Triangulos;

    }
}
