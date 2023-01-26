using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class Cuadrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CuadradoID { get; set; }
        public int FormaGeometricaID { get; set; }
        [ForeignKey("FormaGeometricaID")]
        public virtual FormaGeometrica FormaGeometrica { get; set; }
    }
}
