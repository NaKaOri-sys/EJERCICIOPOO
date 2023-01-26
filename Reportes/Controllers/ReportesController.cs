using Microsoft.AspNetCore.Mvc;

namespace EjercicioPOOApi.Controllers
{
    [ApiController]
    [Route("reportes")]
    public class ReportesController : ControllerBase
    {
        [HttpGet(Name = "GetReporte")]
        public Reportes GetReporte() 
        {

        }
    }
}
