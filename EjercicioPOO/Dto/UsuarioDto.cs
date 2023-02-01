using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EjercicioPOO.Application.Dto
{
    public class UsuarioDto
    {
        [JsonIgnore]
        public int IdUser { get; set; }
        [Required(ErrorMessage = "User is required.")]
        public string User { get; set; }
        [Required(ErrorMessage = "Pass is required.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The entry password does not match with the old password.")]
        public string ConfirmarPassword { get; set; }
        [JsonIgnore]
        public string Sal { get; set; }
    }
}
