using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Enum;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioPOO.API.Controllers
{
    [Route("api/formaGeometrica")]
    [ApiController]
    public class FormaGeometricaController : ControllerBase
    {
        private readonly IFormaGeometricaService _formaGeometrica;

        public FormaGeometricaController(IFormaGeometricaService formaGeometrica)
        {
            _formaGeometrica = formaGeometrica;
        }

        [HttpPost]
        public ActionResult Post(FormaGeometricaDto request)
        {
            var response = _formaGeometrica.CreateForma(request);
            if (response == null)
            {
                throw new Exception("Error interno");
            }

            return Ok();
        }

        [HttpGet]
        [Route("{ID}")]
        public FormaGeometricaDto Get(int ID)
        {
            var forma = _formaGeometrica.GetForma(ID);
            if (forma == null)
            {
                throw new Exception("Not Found");
            }

            return forma;
        }

        [HttpGet]
        public List<FormaGeometricaDto> GetAll()
        {
            var forma = _formaGeometrica.GetAllFormas();
            if (forma == null)
            {
                throw new Exception("Not Found");
            }

            return forma;
        }

        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            var response = _formaGeometrica.DeleteForma(ID);
            if (response == null)
            {
                return NotFound("No se encontro la forma geometrica indicada.");
            }

            return Ok("Se borró exitosamente.");
        }

        [HttpPut]
        public ActionResult Put([FromForm] FormaGeometricaDto request)
        {
            if (request.FormaGeometricaID <= 0)
                return BadRequest("Ingrese un ID superior a 0");
            var response = _formaGeometrica.UpdateForma(request);
            if (response == null)
                throw new Exception("ERROR INTERNO");

            return Ok("Se actualizó la entidad con éxito.");
        }
    }
}
