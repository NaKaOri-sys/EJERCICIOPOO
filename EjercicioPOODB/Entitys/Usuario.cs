using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioPOO.Domain.Entitys
{
    public class Usuario
    {
        public Usuario(string usuario, string pass, string salt)
        {
            this.User = usuario;
            this.Password = pass;
            this.Sal = salt;
        }
        public Usuario() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Sal { get; set; }
    }
}
