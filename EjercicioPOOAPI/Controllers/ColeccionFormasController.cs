using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Services.ColeccionFormas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioPOO.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ColeccionFormasController : ControllerBase
    {
        private readonly IColeccionFormasService _coleccionFormasService;

        public ColeccionFormasController(IColeccionFormasService coleccionFormas)
        {
            _coleccionFormasService = coleccionFormas;
        }

        [HttpPost]
        [Route("coleccionFormas")]
        public ActionResult Post(int[] IDs)
        {
            if (IDs.Length == 0)
            {
                return BadRequest("Se deben ingresar IDs para crear una coleccion.");
            }
            _coleccionFormasService.CreateColeccion(IDs);

            return Ok();
        }

        [HttpGet]
        [Route("coleccionFormas")]
        public List<ColeccionFormasDto> GetAll()
        {
            var response = _coleccionFormasService.GetAllColeccion();

            return response;
        }

        [HttpGet]
        [Route("coleccionFormas/{ID}")]
        public ColeccionFormasDto Get(int ID)
        {
            var coleccion = _coleccionFormasService.GetColeccion(ID);

            return coleccion;
        }

        [HttpDelete]
        [Route("coleccionFormas")]
        public ActionResult Delete(int ID)
        {
            if (ID <= 0)
                return BadRequest("No se encontro la coleccion.");

            _coleccionFormasService.DeleteColeccion(ID);

            return Ok();
        }

        [HttpPut]
        [Route("coleccionFormas")]
        public ActionResult Put(int ID, int[] IDs)
        {
            if (ID <= 0 || IDs == null)
            {
                return BadRequest();
            }
            _coleccionFormasService.UpdateColeccion(ID, IDs);

            return Ok();
        }
    }
}
