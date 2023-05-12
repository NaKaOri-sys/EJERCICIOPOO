using FrontEnd.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using shared.Options;

namespace FrontEnd.Pages
{
    [Authorize]
    public class ReportesModel : PageModel
    {
        private readonly EndpointOptions _endpointOptions;
        public bool IsAuntenticated { get; set; }
        public ReportesModel(IOptions<EndpointOptions> options)
        {
            _endpointOptions = options.Value;
        }
        public void OnGet() 
        {
            IsAuntenticated = User.Identity.IsAuthenticated;
        }
        public async Task<IActionResult> OnPost(ReporteDTO dto)
        {
            var httpClient = new HttpClient();
            try
            {
                var token = HttpContext.Request.Cookies["Token"];
                if (token == null)
                    return StatusCode(401, "token is empty");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await httpClient.GetAsync(_endpointOptions.Reporte + "?ID=" + dto.ID);

                response.EnsureSuccessStatusCode();

                var reporte = await response.Content.ReadAsStringAsync();

                return Content(reporte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
