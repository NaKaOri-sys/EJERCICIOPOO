using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using shared.Options;

namespace FrontEnd.Pages
{
    public class ReportesModel : PageModel
    {
        private readonly EndpointOptions _endpointOptions;

        public ReportesModel(IOptions<EndpointOptions> options)
        {
            _endpointOptions = options.Value;
        }
        public void OnGet() 
        {
            
        }
        public void OnPost() 
        {

        }
        public async Task<IActionResult> OnGetReporte(string ID)
        {
            var httpClient = new HttpClient();
            try
            {
                var token = HttpContext.Request.Cookies["Token"];
                if (token == null)
                    throw new Exception("token is empty");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await httpClient.GetAsync(_endpointOptions.Reporte + "?ID=" + ID);

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
