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
        private readonly ILoginService _login;

        public LoginController(ILoginService loginService)
        {
            _login = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(LoginDto login)
        {
            if (!ModelState.IsValid)
                return BadRequest(login);

            var result = _login.GenerateBearer(login);

            return Ok(result);
        }
    }
}
