using AutoMapper;
using EjercicioPOO.API;
using EjercicioPOO.API.Extensions;
using EjercicioPOO.Application.CustomExceptionMiddleware;
using EjercicioPOO.Application.Services.ColeccionFormas;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Login;
using EjercicioPOO.Application.Services.Reporte;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Application.Services.Usuarios;
using EjercicioPOO.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shared.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<LoginOptions>(builder.Configuration.GetSection("LoginOptions"));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IFormaGeometricaService, FormaGeometricaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IColeccionFormasService, ColeccionFormasService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretKey"));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddDbContext<ReportesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReporteConnection"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ReportesContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseHttpsRedirection();
var logger = app.Services.GetRequiredService<ILogger<ExceptionMiddleware>>();
//app.ConfigureExceptionHandler(logger);
app.ConfigureCustomExceptionMiddleware();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
