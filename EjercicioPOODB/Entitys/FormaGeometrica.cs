using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class FormaGeometrica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FormaGeometricaID { get; set; }
        public decimal Lado { get; set; }
        public int TipoID { get; set; }
        [ForeignKey("TipoID")]
        public virtual TipoDeFormas? TipoDeFormas { get; set; }
        public int? ColeccionesFormasID { get; set; }
        [ForeignKey("ColeccionesFormasID")]
        public virtual ColeccionesFormas? ColeccionForma { get; set; }
    }
}
