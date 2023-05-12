using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.DTO
{
    public class RegisterDTO
    {
        [BindProperty]
        public string user { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string confirmPassword { get; set; }
    }
}
