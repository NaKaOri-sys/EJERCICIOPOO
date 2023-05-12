using Microsoft.AspNetCore.Authentication.Cookies;
using shared.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EndpointOptions>(builder.Configuration.GetSection("EndpointOptions"));
builder.Services.Configure<CookiesOptions>(builder.Configuration.GetSection("CookiesOptions"));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "Token"; // Nombre de la cookie
        options.Cookie.HttpOnly = true; // Accesible solo a través de HTTP
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la cookie
        options.SlidingExpiration = true; // Extender la expiración en cada solicitud
        options.LoginPath = "/Login";
    });

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
