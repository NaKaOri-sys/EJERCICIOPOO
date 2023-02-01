using EjercicioPOO.Application.Services.Reporte;
using EjercicioPOO.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioPOO.API.Controllers
{
    [Route("api/reportes")]
    [ApiController]
    [Authorize]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpPost]
        public ActionResult Post(int ID, IdiomasEnum idioma)
        {
            if (ID <= 0)
                return BadRequest();
            var response = _reporteService.CreateReporte(ID, idioma);

            return Ok(response);
        }

        [HttpGet]
        public string Get(int ID)
        {
            var reporte = _reporteService.GetReporte(ID);
            if (reporte == null)
                throw new Exception("ERROR!");

            return reporte;
        }

        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            if (ID <= 0)
                return BadRequest("Debe ingresar un ID mayor a 0.");

            var reporte = _reporteService.DeleteReporte(ID);
            if (reporte == null)
                return NotFound("No se encontró un reporte para el ID solicitado.");

            return Ok("Se borro exitosamente el reporte.");
        }

        [HttpPut]
        public ActionResult Put(int IdReporte, int ID, IdiomasEnum idiomas)
        {
            if (IdReporte <= 0 || ID <= 0)
                return BadRequest("El IdReporte o IdColeccion deben ser mayor a 0");
            var response = _reporteService.UpdateReporte(IdReporte, ID, idiomas);

            if (response == null)
                throw new Exception("ERROR!!!");

            return Ok(response);
        }
    }
}
