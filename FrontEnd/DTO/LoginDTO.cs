using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.wwwroot.DTO
{
    public class LoginDTO
    {
        [BindProperty]
        public string user { get; set; }
        [BindProperty]
        public string password { get; set; }
    }
}
