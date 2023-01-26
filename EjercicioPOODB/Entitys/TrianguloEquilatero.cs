using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class TrianguloEquilatero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrianguloEquilateroID { get; set; }
        public int FormaGeometricaID { get; set; }
        [ForeignKey("FormaGeometricaID")]
        public virtual FormaGeometrica FormaGeometrica { get; set; }
    }
}
