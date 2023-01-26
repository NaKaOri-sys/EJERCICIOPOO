/*
 * Refactorear la clase para respetar principios de programación orientada a objetos. Qué pasa si debemos soportar un nuevo idioma para los reportes, o
 * agregar más formas geométricas?
 *
 * Se puede hacer cualquier cambio que se crea necesario tanto en el código como en los tests. La única condición es que los tests pasen OK.
 *
 * TODO: Implementar Trapecio/Rectangulo, agregar otro idioma a reporting.
 * */

using EjercicioPOO.Enum;
using System.Text.Json.Serialization;

namespace EjercicioPOO.Application.Dto
{
    public class FormaGeometricaDto
    {
        public FormaGeometricaDto(decimal ancho, FormasEnum TipoForma, decimal ladoBase = 0, decimal altura = 0, decimal ladoIzquierdo = 0, decimal ladoDerecho = 0)
        {
            Lado = ancho;
            LadoBase = ladoBase;
            Altura = altura;
            LadoIzquierdo = ladoIzquierdo;
            LadoDerecho = ladoDerecho;
            Tipo = TipoForma;
        }

        public FormaGeometricaDto()
        {

        }

        public decimal LadoBase { get; set; }
        public decimal Altura { get; set; }
        public decimal LadoIzquierdo { get; set; }
        public decimal LadoDerecho { get; set; }
        public decimal Lado { get; set; }
        public int FormaGeometricaID { get; set; }
        public int TipoID { get; set; } = 0;
        public string TipoForma { get; set; }
        [JsonIgnore]
        public FormasEnum Tipo { get; set; }
        [JsonIgnore]
        public virtual decimal Area { get; set; } = 0;
        [JsonIgnore]
        public virtual decimal Perimetro { get; set; } = 0;


        public virtual string TraducirForma(int cantidad)
        {
            return string.Empty;
        }
    }
}
