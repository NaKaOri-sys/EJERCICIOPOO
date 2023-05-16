using System;
using System.Collections.Generic;
using AutoMapper;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Reporte;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace EjercicioPOO.Tests
{
    public class ReporteServiceTest
    {
        private readonly ReporteService _reporteService;
        private readonly IGenericRepository<Reportes> _reportesRepository = Substitute.For<IGenericRepository<Reportes>>();
        private readonly IGenericRepository<ColeccionesFormas> _coleccionFormasRepository = Substitute.For<IGenericRepository<ColeccionesFormas>>();
        private readonly IGenericRepository<Idiomas> _idiomasRepository = Substitute.For<IGenericRepository<Idiomas>>();
        private readonly IGenericRepository<Trapecio> _trapecioRepository = Substitute.For<IGenericRepository<Trapecio>>();
        private readonly IFormaGeometricaService _formaGeometricaService = Substitute.For<IFormaGeometricaService>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        public ReporteServiceTest()
        {
            _reporteService = new ReporteService(_reportesRepository,
                                                 _coleccionFormasRepository,
                                                 _idiomasRepository,
                                                 _trapecioRepository,
                                                 _formaGeometricaService,
                                                 _mapper);
        }

        #region UtilityMethods
        private Reportes CreateReporte() 
        {
            var reporte = new Reportes 
            {
                ReportesID = 10
            };

            return reporte;
        }
        #endregion
        [Fact]
        public void TestReporteNoEncontrado()
        {
            Assert.Throws<NotFoundException>(() => _reporteService.GetReporte(1));
        }

        //[Fact]
        //public void TestResumenListaVaciaFormasEnIngles()
        //{
        //    Assert.AreEqual("<h1>Empty list of shapes!</h1>",
        //        FormaGeometricaDto.Imprimir(new List<FormaGeometricaDto>(), IdiomasEnum.Ingles));
        //}

        //[Fact]
        //public void TestResumenListaVaciaFormasEnPortugues()
        //{
        //    Assert.AreEqual("<h1>Lista vazia de formas!</h1>",
        //        FormaGeometricaDto.Imprimir(new List<FormaGeometricaDto>(), IdiomasEnum.Portugues));
        //}

        //[Fact]
        //public void TestResumenListaConUnCuadrado()
        //{
        //    var cuadrado = new CuadradoDto(5);
        //    var cuadrados = new List<FormaGeometricaDto> { cuadrado };

        //    var resumen = FormaGeometricaDto.Imprimir(cuadrados, IdiomasEnum.Castellano);

        //    Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perimetro 20 <br/>TOTAL:<br/>1 formas Perimetro 20 Area 25", resumen);
        //}

        //[Fact]
        //public void TestResumenListaConMasCuadrados()
        //{
        //    var cuadrados = new List<FormaGeometricaDto>
        //    {
        //        new CuadradoDto(5),
        //        new CuadradoDto(1),
        //        new CuadradoDto(3)
        //    };

        //    var resumen = FormaGeometricaDto.Imprimir(cuadrados, IdiomasEnum.Ingles);

        //    Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br/>TOTAL:<br/>3 shapes Perimeter 36 Area 35", resumen);
        //}

        //[Fact]
        //public void TestResumenListaConUnCirculoPortugues()
        //{
        //    var circulo = new CirculoDto(3);

        //    var resumen = FormaGeometricaDto.Imprimir(new List<FormaGeometricaDto> { circulo }, IdiomasEnum.Portugues);

        //    Assert.AreEqual("<h1>Relat�rio de formul�rios</h1>1 C�rculo | �rea 7,07 | Per�metro 9,42 <br/>TOTAL:<br/>1 formas Per�metro 9,42 �rea 7,07", resumen);
        //}

        //[Fact]
        //public void TestResumenListaConMasCirculoPortugues()
        //{
        //    var circulo = new List<FormaGeometricaDto>
        //    {
        //        new CirculoDto(3),
        //        new CirculoDto(1),
        //        new CirculoDto(4)
        //    };

        //    var resumen = FormaGeometricaDto.Imprimir(circulo, IdiomasEnum.Portugues);

        //    Assert.AreEqual("<h1>Relat�rio de formul�rios</h1>3 C�rculos | �rea 20,42 | Per�metro 25,13 <br/>TOTAL:<br/>3 formas Per�metro 25,13 �rea 20,42", resumen);
        //}

        //[Fact]
        //public void TestResumenListaConMasTiposEnPortugues()
        //{
        //    var formas = new List<FormaGeometricaDto>
        //    {
        //        new CuadradoDto(5),
        //        new CirculoDto(3),
        //        new TrianguloEquilateroDto(4),
        //        new CuadradoDto(2),
        //        new TrianguloEquilateroDto(9),
        //        new CirculoDto(2.75m),
        //        new TrianguloEquilateroDto(4.2m)
        //    };

        //    var resumen = FormaGeometricaDto.Imprimir(formas, IdiomasEnum.Portugues);

        //    Assert.AreEqual(
        //        "<h1>Relat�rio de formul�rios</h1>2 Quadrados | �rea 29 | Per�metro 28 <br/>2 C�rculos | �rea 13,01 | Per�metro 18,06 <br/>3 Tri�ngulos | �rea 49,64 | Per�metro 51,6 <br/>TOTAL:<br/>7 formas Per�metro 97,66 �rea 91,65",
        //        resumen);
        //}

        //[Fact]
        //public void TestResumenListaConMasTipos()
        //{
        //    var formas = new List<FormaGeometricaDto>
        //    {
        //        new CuadradoDto(5),
        //        new CirculoDto(3),
        //        new TrianguloEquilateroDto(4),
        //        new CuadradoDto(2),
        //        new TrianguloEquilateroDto(9),
        //        new CirculoDto(2.75m),
        //        new TrianguloEquilateroDto(4.2m)
        //    };

        //    var resumen = FormaGeometricaDto.Imprimir(formas, IdiomasEnum.Ingles);

        //    Assert.AreEqual(
        //        "<h1>Shapes report</h1>2 Squares | Area 29 | Perimeter 28 <br/>2 Circles | Area 13.01 | Perimeter 18.06 <br/>3 Triangles | Area 49.64 | Perimeter 51.6 <br/>TOTAL:<br/>7 shapes Perimeter 97.66 Area 91.65",
        //        resumen);
        //}

        //[Fact]
        //public void TestResumenListaConMasTiposEnCastellano()
        //{
        //    var formas = new List<FormaGeometricaDto>
        //    {
        //        new CuadradoDto(5),
        //        new CirculoDto(3),
        //        new TrianguloEquilateroDto(4),
        //        new CuadradoDto(2),
        //        new TrianguloEquilateroDto(9),
        //        new CirculoDto(2.75m),
        //        new TrianguloEquilateroDto(4.2m)
        //    };

        //    var resumen = FormaGeometricaDto.Imprimir(formas, IdiomasEnum.Castellano);

        //    Assert.AreEqual(
        //        "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perimetro 28 <br/>2 C�rculos | Area 13,01 | Perimetro 18,06 <br/>3 Tri�ngulos | Area 49,64 | Perimetro 51,6 <br/>TOTAL:<br/>7 formas Perimetro 97,66 Area 91,65",
        //        resumen);
        //}
    }
}