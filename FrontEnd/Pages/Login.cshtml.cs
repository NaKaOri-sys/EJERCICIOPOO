using FrontEnd.wwwroot.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using shared.Options;
using System.Text;

namespace FrontEnd.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly EndpointOptions _endpointOptions;
        private readonly CookiesOptions _cookieOptions;
        public LoginModel(IOptions<EndpointOptions> options, IOptions<CookiesOptions> cookieOptions)
        {
            _endpointOptions = options.Value;
            _cookieOptions = cookieOptions.Value;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(LoginDTO login)
        {
            var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(_endpointOptions.Login, content);

                response.EnsureSuccessStatusCode();
                var token = await response.Content.ReadAsStringAsync();
                var cookieOptions = new CookieOptions { Expires = DateTime.UtcNow.AddDays(_cookieOptions.ExpiresIn) };
                Response.Cookies.Append("Token", token, cookieOptions);

                return Redirect("/Reportes");
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("MensajeError", "Hubo un error en la petición:" + ex.Message);
                return Redirect("/Error");
            }
        }
    }
}
