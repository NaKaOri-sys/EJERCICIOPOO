using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioPOO.API.Controllers
{
    [Route("api/formaGeometrica")]
    [ApiController]
    [Authorize]
    public class FormaGeometricaController : ControllerBase
    {
        private readonly IFormaGeometricaService _formaGeometrica;

        public FormaGeometricaController(IFormaGeometricaService formaGeometrica)
        {
            _formaGeometrica = formaGeometrica;
        }

        [HttpPost]
        public ActionResult Post([FromForm] FormaGeometricaDto request)
        {
            var response = _formaGeometrica.CreateForma(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("{ID}")]
        public FormaGeometricaDto Get(int ID)
        {
            var forma = _formaGeometrica.GetForma(ID);

            return forma;
        }

        [HttpGet]
        public List<FormaGeometricaDto> GetAll()
        {
            var forma = _formaGeometrica.GetAllFormas();

            return forma;
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int ID)
        {
            _formaGeometrica.DeleteForma(ID);

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromForm] FormaGeometricaDto request)
        {
            if (request.FormaGeometricaID <= 0)
                return BadRequest("Ingrese un ID superior a 0");
            _formaGeometrica.UpdateForma(request);

            return Ok();
        }
    }
}
