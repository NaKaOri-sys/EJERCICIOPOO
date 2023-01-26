using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class Reportes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportesID { get; set; }
        public int IdiomasID { get; set; }
        [ForeignKey("IdiomasID")]
        public virtual Idiomas? Idioma { get; set; }
        public int? ColeccionesFormasID { get; set; }
        [ForeignKey("ColeccionesFormasID")]
        public virtual ColeccionesFormas? ColeccionFormas { get; set; }
    }
}
