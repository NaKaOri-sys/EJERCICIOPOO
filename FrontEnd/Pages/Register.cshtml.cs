using FrontEnd.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using shared.Options;
using System.Text;

namespace FrontEnd.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly EndpointOptions _endpointOptions;

        public RegisterModel(IOptions<EndpointOptions> endpointOptions)
        {
            _endpointOptions = endpointOptions.Value;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(RegisterDTO register)
        {
            var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(_endpointOptions.Register, content);

                response.EnsureSuccessStatusCode();

                return Redirect("/Login");
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("MensajeError", "Hubo un error en la petición:" + ex.Message);
                return Redirect("/Error");
            }
        }
    }
}
