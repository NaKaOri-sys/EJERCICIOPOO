using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.DTO
{
    public class ReporteDTO
    {
        [BindProperty]
        public string ID { get; set; }
    }
}
