﻿// <auto-generated />
using EjercicioPOO.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EjercicioPOO.Domain.Migrations
{
    [DbContext(typeof(ReportesContext))]
    [Migration("20220818181533_ReportesPOO")]
    partial class ReportesPOO
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.ColeccionesFormas", b =>
                {
                    b.Property<int>("ColeccionesFormasID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColeccionesFormasID"), 1L, 1);

                    b.Property<int>("FormaGeometricaID")
                        .HasColumnType("int");

                    b.HasKey("ColeccionesFormasID");

                    b.ToTable("ColeccionesDeFormas");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.FormaGeometrica", b =>
                {
                    b.Property<int>("FormaGeometricaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormaGeometricaID"), 1L, 1);

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ColeccionesFormasID")
                        .HasColumnType("int");

                    b.Property<decimal>("Lado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Perimetro")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoID")
                        .HasColumnType("int");

                    b.HasKey("FormaGeometricaID");

                    b.HasIndex("ColeccionesFormasID");

                    b.HasIndex("TipoID");

                    b.ToTable("FormasGeometricas");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Idiomas", b =>
                {
                    b.Property<int>("IdiomasID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdiomasID"), 1L, 1);

                    b.Property<string>("Idioma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdiomasID");

                    b.ToTable("Idiomas");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Reportes", b =>
                {
                    b.Property<int>("ReportesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportesID"), 1L, 1);

                    b.Property<int>("ColeccionesDeFormasID")
                        .HasColumnType("int");

                    b.Property<int>("IdiomasID")
                        .HasColumnType("int");

                    b.HasKey("ReportesID");

                    b.HasIndex("ColeccionesDeFormasID");

                    b.HasIndex("IdiomasID");

                    b.ToTable("Reportes");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.Circulo", b =>
                {
                    b.Property<int>("CirculoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CirculoID"), 1L, 1);

                    b.Property<int>("FormaGeometricaID")
                        .HasColumnType("int");

                    b.HasKey("CirculoID");

                    b.HasIndex("FormaGeometricaID");

                    b.ToTable("Circulos");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.Cuadrado", b =>
                {
                    b.Property<int>("CuadradoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CuadradoID"), 1L, 1);

                    b.Property<int>("FormaGeometricaID")
                        .HasColumnType("int");

                    b.HasKey("CuadradoID");

                    b.HasIndex("FormaGeometricaID");

                    b.ToTable("Cuadrados");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.Trapecio", b =>
                {
                    b.Property<int>("TrapecioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrapecioID"), 1L, 1);

                    b.Property<decimal>("Altura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BaseMayor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BaseMenor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FormaGeometricaID")
                        .HasColumnType("int");

                    b.Property<decimal>("LadoDerecho")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LadoIzquierdo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TrapecioID");

                    b.HasIndex("FormaGeometricaID");

                    b.ToTable("Trapecios");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.TrianguloEquilatero", b =>
                {
                    b.Property<int>("TrianguloEquilateroID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrianguloEquilateroID"), 1L, 1);

                    b.Property<int>("FormaGeometricaID")
                        .HasColumnType("int");

                    b.HasKey("TrianguloEquilateroID");

                    b.HasIndex("FormaGeometricaID");

                    b.ToTable("TrianguloEquilateros");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.TipoDeFormas", b =>
                {
                    b.Property<int>("TipoDeFormasID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoDeFormasID"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoDeFormasID");

                    b.ToTable("TipoDeFormas");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.FormaGeometrica", b =>
                {
                    b.HasOne("EjercicioPOO.Domain.Entitys.ColeccionesFormas", "ColeccionesFormas")
                        .WithMany("FormasGeometricas")
                        .HasForeignKey("ColeccionesFormasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EjercicioPOO.Domain.Entitys.TipoDeFormas", "TipoDeFormas")
                        .WithMany()
                        .HasForeignKey("TipoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColeccionesFormas");

                    b.Navigation("TipoDeFormas");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Reportes", b =>
                {
                    b.HasOne("EjercicioPOO.Domain.Entitys.ColeccionesFormas", "ColeccionesFormas")
                        .WithMany()
                        .HasForeignKey("ColeccionesDeFormasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EjercicioPOO.Domain.Entitys.Idiomas", "Idiomas")
                        .WithMany()
                        .HasForeignKey("IdiomasID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColeccionesFormas");

                    b.Navigation("Idiomas");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.Circulo", b =>
                {
                    b.HasOne("EjercicioPOO.Domain.Entitys.FormaGeometrica", "FormaGeometrica")
                        .WithMany()
                        .HasForeignKey("FormaGeometricaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormaGeometrica");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.Cuadrado", b =>
                {
                    b.HasOne("EjercicioPOO.Domain.Entitys.FormaGeometrica", "FormaGeometrica")
                        .WithMany()
                        .HasForeignKey("FormaGeometricaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormaGeometrica");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.Trapecio", b =>
                {
                    b.HasOne("EjercicioPOO.Domain.Entitys.FormaGeometrica", "FormaGeometrica")
                        .WithMany()
                        .HasForeignKey("FormaGeometricaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormaGeometrica");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.Shapes.TrianguloEquilatero", b =>
                {
                    b.HasOne("EjercicioPOO.Domain.Entitys.FormaGeometrica", "FormaGeometrica")
                        .WithMany()
                        .HasForeignKey("FormaGeometricaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormaGeometrica");
                });

            modelBuilder.Entity("EjercicioPOO.Domain.Entitys.ColeccionesFormas", b =>
                {
                    b.Navigation("FormasGeometricas");
                });
#pragma warning restore 612, 618
        }
    }
}
