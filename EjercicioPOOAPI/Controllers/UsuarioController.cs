using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Services.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioPOO.API.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(usuario);
            var result = _usuarioService.CreateUser(usuario);

            return Ok(result);
        }

        [HttpGet]
        [Route("{usuario}")]
        public IActionResult Get(string usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(usuario);
            var result = _usuarioService.FindUser(usuario);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _usuarioService.FindAllUsers();

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put(UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(usuario);
            _usuarioService.UpdateUser(usuario);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(usuario);
            }
            _usuarioService.DeleteUser(usuario);

            return Ok();
        }
    }
}
