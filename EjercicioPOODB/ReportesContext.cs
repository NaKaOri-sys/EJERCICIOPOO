using EjercicioPOO.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace EjercicioPOO.Domain
{
    public class ReportesContext : DbContext
    {
        public ReportesContext(DbContextOptions<ReportesContext> options) : base(options)
        {

        }

        public ReportesContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer();
        }
        //protected override void OnModelCreating(ModelBuilder builder) 
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<ColeccionesFormas>()
        //        .HasMany(s => s.FormasGeometricas)
        //        .WithOne(g => g.ColeccionForma)
        //        .HasForeignKey(x => x.ColeccionesFormasID)
        //        .IsRequired(false);
        //}
        public DbSet<Reportes> Reportes { get; set; }
        public DbSet<Idiomas> Idiomas { get; set; }
        public DbSet<TipoDeFormas> TipoDeFormas { get; set; }
        public DbSet<FormaGeometrica> FormasGeometricas { get; set; }
        public DbSet<ColeccionesFormas> ColeccionesDeFormas { get; set; }
        public DbSet<Cuadrado> Cuadrados { get; set; }
        public DbSet<Circulo> Circulos { get; set; }
        public DbSet<Trapecio> Trapecios { get; set; }
        public DbSet<TrianguloEquilatero> TrianguloEquilateros { get; set; }

    }
}
