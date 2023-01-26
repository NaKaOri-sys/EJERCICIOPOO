using AutoMapper;
using EjercicioPOO.Application.Services.ColeccionFormas;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Reporte;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IFormaGeometricaService, FormaGeometricaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IColeccionFormasService, ColeccionFormasService>();
builder.Services.AddScoped(typeof(IGenericRepository< >), typeof(GenericRepository< >));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
