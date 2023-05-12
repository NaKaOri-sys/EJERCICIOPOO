using FrontEnd.wwwroot.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using shared.Options;
using System.Security.Claims;
using System.Text;

namespace FrontEnd.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly EndpointOptions _endpointOptions;
        public LoginModel(IOptions<EndpointOptions> options)
        {
            _endpointOptions = options.Value;
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

                var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, login.user),
    new Claim(ClaimTypes.Role, "Administrator")// Puedes agregar cualquier información adicional como reclamaciones
};

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Hacer que la cookie persista después de cerrar el navegador
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30) // Tiempo de expiración de la cookie
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                Console.WriteLine($"username: {User.Identity.Name}");
                Response.Headers["X-IsAuthenticated"] = User.Identity.IsAuthenticated.ToString();
                Console.WriteLine($"IsAuthenticated: {User.Identity.IsAuthenticated}");

                return Content(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
