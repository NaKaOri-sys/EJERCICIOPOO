using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.DTO
{
    public class ReporteDTO
    {
        [BindProperty]
        public int ID { get; set; }
    }
}
