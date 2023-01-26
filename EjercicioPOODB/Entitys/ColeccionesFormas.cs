using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class ColeccionesFormas
    {
        public ColeccionesFormas()
        {
            Reportes = new List<Reportes>();
            FormasGeometricas = new List<FormaGeometrica>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColeccionesFormasID { get; set; }
        public virtual List<FormaGeometrica> FormasGeometricas { get; set; }
        public virtual List<Reportes> Reportes { get; set; }
    }
}
