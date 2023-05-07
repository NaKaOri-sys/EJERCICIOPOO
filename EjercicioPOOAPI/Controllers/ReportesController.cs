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
            _reporteService.CreateReporte(ID, idioma);

            return Ok();
        }

        [HttpGet]
        public string Get(int ID)
        {
            var reporte = _reporteService.GetReporte(ID);

            return reporte;
        }

        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            _reporteService.DeleteReporte(ID);

            return Ok();
        }

        [HttpPut]
        public ActionResult Put(int IdReporte, int ID, IdiomasEnum idiomas)
        {
            _reporteService.UpdateReporte(IdReporte, ID, idiomas);

            return Ok();
        }
    }
}
