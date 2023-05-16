using AutoFixture;
using AutoMapper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Reporte;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EjercicioPOO.Test
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
        private FormaGeometricaDto CreateTrapecioInFormaGeometricaDto()
        {
            var shape = new Fixture().Create<FormaGeometricaDto>();
            shape.Lado = 10;
            shape.LadoBase = 10;
            shape.LadoIzquierdo = 10;
            shape.LadoDerecho = 10;
            shape.Area = 10;
            shape.Altura = 10;
            shape.Tipo = FormasEnum.Trapecio;

            return shape;
        }

        private ReporteDto CreateReporteDto(int ID)
        {
            var reporte = new Fixture().Create<ReporteDto>();
            reporte.ReporteID = ID;
            reporte.Idioma.IdiomaID = ID;
            reporte.ColeccionFormas.formasGeometricas = new List<FormaGeometricaDto> { CreateTrapecioInFormaGeometricaDto() };

            return reporte;
        }
        private Reportes CreateEntityReporte(int ID)
        {
            var reporte = new Reportes
            {
                ReportesID = ID,
                ColeccionesFormasID = 1,
                ColeccionFormas = new ColeccionesFormas(),
                IdiomasID = 1,
                Idioma = new Idiomas()
            };

            return reporte;
        }
        #endregion

        [Theory]
        [InlineData(1, IdiomasEnum.Castellano)]
        [InlineData(1, IdiomasEnum.Ingles)]
        [InlineData(1, IdiomasEnum.Portugues)]
        public void CreateReporte_TryingCreateReporteButTheCollectionWasntFoundIt_ThrowsInternalErrorException(int IdColeccion, IdiomasEnum idioma) 
        {
            var reporte = new Reportes
            {
                ColeccionesFormasID = IdColeccion,
                ColeccionFormas = new ColeccionesFormas(),
                IdiomasID = (int)idioma,
                Idioma = new Idiomas()
            };
            var colecciones = new List<ColeccionesFormas>();
            var queryColecciones = colecciones.AsQueryable();
            _coleccionFormasRepository.GetById(IdColeccion).Throws(new InternalErrorException("Collection cannot be found."));
            
            Assert.Throws<InternalErrorException>(() => _reporteService.CreateReporte(IdColeccion, idioma));
        }
        
        [Theory]
        [InlineData(0, default(IdiomasEnum))]
        [InlineData(1, default(IdiomasEnum))]
        [InlineData(0, IdiomasEnum.Portugues)]
        public void CreateReporte_TryingCreateReporteButParametersAreIncorrect_ThrowsBadRequestException(int IdColeccion, IdiomasEnum idioma) 
        {
            var reporte = new Reportes
            {
                ColeccionesFormasID = IdColeccion,
                ColeccionFormas = new ColeccionesFormas(),
                IdiomasID = (int)idioma,
                Idioma = new Idiomas()
            };
            
            Assert.Throws<BadRequestException>(() => _reporteService.CreateReporte(IdColeccion, idioma));
        }

        [Fact]
        public void GetReporte_IDNotMatchInDB_ThrowsNotFoundException()
        {
            Assert.Throws<NotFoundException>(() => _reporteService.GetReporte(1));
        }
        [Fact]
        public void UpdateReporte_IDNotMatchInDB_ThrowsNotFoundException()
        {
            _reportesRepository.GetById(1).Throws(new NotFoundException("The report dont exist."));


            Assert.Throws<InternalErrorException>(() => _reporteService.UpdateReporte(1, 1, IdiomasEnum.Castellano));
        }
        [Fact]
        public void DeleteReporte_IDNotMatchInDB_ThrowsInternalErrorException()
        {
            Assert.Throws<InternalErrorException>(() => _reporteService.DeleteReporte(1));
        }
        
        [Theory]
        [InlineData(1, 0, IdiomasEnum.Castellano)]
        [InlineData(1, 1, default(IdiomasEnum))]
        [InlineData(0, 1, IdiomasEnum.Ingles)]
        public void UpdateReporte_TheParametersAreIncorrect_ThrowsNotFoundException(int IdReporte, int IdColeccion, IdiomasEnum idioma)
        {
            Assert.Throws<BadRequestException>(() => _reporteService.UpdateReporte(IdReporte, IdColeccion, idioma));
        }
        [Fact]
        public void DeleteReporte_TheParametersAreIncorrect_ThrowsInternalErrorException()
        {
            Assert.Throws<BadRequestException>(() => _reporteService.DeleteReporte(0));
        }

        [Fact]
        public void GetReporte_ReporteDTOContainsAnCollectionWhatNotContainsShapes_ThrowsInternalErrorException()
        {
            //Arrange
            var reporte = new Reportes
            {
                ReportesID = 1,
                ColeccionesFormasID = 1,
                ColeccionFormas = new ColeccionesFormas(),
                IdiomasID = 1,
                Idioma = new Idiomas()
            };
            var listaReportes = new List<Reportes> { reporte };
            var queryableReporte = listaReportes.AsQueryable();
            _reportesRepository.GetAll().Returns(queryableReporte);
            var reporteDto = CreateReporteDto(1);
            reporteDto.ColeccionFormas.formasGeometricas = null;
            _mapper.Map<ReporteDto>(reporte).Returns(reporteDto);

            //Assert
            Assert.Throws<InternalErrorException>(() => _reporteService.GetReporte(1));
        }

        [Fact]
        public void GetReporte_IdiomaIsSpanishAndReportIsReadyToShow_ReturnsReporteInSpanish()
        {
            //Arrange
            var reporte = CreateEntityReporte(1);
            var listaReportes = new List<Reportes> { reporte };
            var queryableReporte = listaReportes.AsQueryable();
            _reportesRepository.GetAll().Returns(queryableReporte);
            var reporteDto = CreateReporteDto(1);
            _mapper.Map<ReporteDto>(reporte).Returns(reporteDto);

            //Act
            var response = _reporteService.GetReporte(1);
            //Assert
            response.Should().BeEquivalentTo("<h1>Reporte de Formas</h1>1 Trapecio | Area 100 | Perimetro 40 <br/>TOTAL:<br/>1 formas Perimetro 40 Area 100");
        } 
        
        [Fact]
        public void GetReporte_IdiomaIsEnglishAndReportIsReadyToShow_ReturnsReporteInEnglish()
        {
            //Arrange
            var reporte = CreateEntityReporte(1);
            var listaReportes = new List<Reportes> { reporte };
            var queryableReporte = listaReportes.AsQueryable();
            _reportesRepository.GetAll().Returns(queryableReporte);
            var reporteDto = CreateReporteDto(1);
            reporteDto.Idioma.IdiomaID = 2;
            _mapper.Map<ReporteDto>(reporte).Returns(reporteDto);

            //Act
            var response = _reporteService.GetReporte(1);
            //Assert
            response.Should().BeEquivalentTo("<h1>Shapes report</h1>1 Trapeze | Area 100 | Perimeter 40 <br/>TOTAL:<br/>1 shapes Perimeter 40 Area 100");
        }
        
        [Fact]
        public void GetReporte_IdiomaIsPortuguesAndReportIsReadyToShow_ReturnsReporteInPortugues()
        {
            //Arrange
            var reporte = CreateEntityReporte(1);
            var listaReportes = new List<Reportes> { reporte };
            var queryableReporte = listaReportes.AsQueryable();
            _reportesRepository.GetAll().Returns(queryableReporte);
            var reporteDto = CreateReporteDto(1);
            reporteDto.Idioma.IdiomaID = 3;
            _mapper.Map<ReporteDto>(reporte).Returns(reporteDto);

            //Act
            var response = _reporteService.GetReporte(1);
            //Assert
            response.Should().BeEquivalentTo("<h1>Relatório de formulários</h1>1 Trapézio | Área 100 | Perímetro 40 <br/>TOTAL:<br/>1 formas Perímetro 40 Área 100");
        }

        [Fact]
        public void GetReporte_IdiomaIsSpanishAndReportIsReadyToShow_ReturnsReporteInPortugues()
        {
            //Arrange
            var reporte = CreateEntityReporte(1);
            var listaReportes = new List<Reportes> { reporte };
            var queryableReporte = listaReportes.AsQueryable();
            _reportesRepository.GetAll().Returns(queryableReporte);
            var reporteDto = CreateReporteDto(1);
            reporteDto.Idioma.IdiomaID = 3;
            _mapper.Map<ReporteDto>(reporte).Returns(reporteDto);

            //Act
            var response = _reporteService.GetReporte(1);
            //Assert
            response.Should().BeEquivalentTo("<h1>Relatório de formulários</h1>1 Trapézio | Área 100 | Perímetro 40 <br/>TOTAL:<br/>1 formas Perímetro 40 Área 100");
        }
        
        [Fact]
        public void GetReporte_ShapesAreEmptyInReporte_ReturnsMessageEmptyShapes()
        {
            //Arrange
            var reporte = CreateEntityReporte(1);
            var listaReportes = new List<Reportes> { reporte };
            var queryableReporte = listaReportes.AsQueryable();
            _reportesRepository.GetAll().Returns(queryableReporte);
            var reporteDto = CreateReporteDto(1);
            reporteDto.ColeccionFormas.formasGeometricas = new List<FormaGeometricaDto>();
            reporteDto.Idioma.IdiomaID = 1;
            _mapper.Map<ReporteDto>(reporte).Returns(reporteDto);

            //Act
            var response = _reporteService.GetReporte(1);
            //Assert
            response.Should().BeEquivalentTo("<h1>Lista vacía de formas!</h1>");
        }
    }
}