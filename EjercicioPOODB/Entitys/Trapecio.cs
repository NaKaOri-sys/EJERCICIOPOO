using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class Trapecio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrapecioID { get; set; }
        public int FormaGeometricaID { get; set; }
        public decimal LadoIzquierdo { get; set; }
        public decimal LadoDerecho { get; set; }
        public decimal BaseMenor { get; set; }
        public decimal BaseMayor { get; set; }
        public decimal Altura { get; set; }
        [ForeignKey("FormaGeometricaID")]
        public virtual FormaGeometrica FormaGeometrica { get; set; }
    }
}
