using System.ComponentModel.DataAnnotations;

namespace EjercicioPOO.Application.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User is required.")]
        public string User { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
