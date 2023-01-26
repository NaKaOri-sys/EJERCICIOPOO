using EjercicioPOO.Application.Traducciones;
using EjercicioPOO.Enum;
using System;

namespace EjercicioPOO.Application.Dto
{
    public class CirculoDto : FormaGeometricaDto
    {
        public CirculoDto(decimal ancho) : base(ancho, FormasEnum.Circulo)
        {
            Tipo = FormasEnum.Circulo;
        }
        public int CirculoID { get; set; }
        public int FormaGeometricaID { get; set; }
        public override decimal Area { get => (decimal)Math.PI * (Lado / 2) * (Lado / 2); }
        public override decimal Perimetro { get => (decimal)Math.PI * Lado; }
        public override string TraducirForma(int cantidad) => cantidad == 1 ? Traduccion.Circulo : Traduccion.Circulos;
    }
}
