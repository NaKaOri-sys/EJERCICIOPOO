using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class Idiomas
    {
        public Idiomas()
        {
            Reportes = new List<Reportes>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdiomasID { get; set; }
        public string Idioma { get; set; }
        public virtual List<Reportes> Reportes { get; set; }
    }
}
