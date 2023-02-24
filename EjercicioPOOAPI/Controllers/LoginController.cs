using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioPOO.API.Controllers
{
    [Route("api/Login")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _login;

        public LoginController(IConfiguration configuration, ILoginService loginService)
        {
            _configuration = configuration;
            _login = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(LoginDto login)
        {
            if (!ModelState.IsValid)
                return BadRequest(login);

            var secretKey = _configuration.GetValue<string>("SecretKey");
            var result = _login.GenerateBearer(login, secretKey);

            if (result == null)
                return Forbid();
            if (result.Equals("404"))
                return NotFound();

            return Ok(result);
        }
    }
}
