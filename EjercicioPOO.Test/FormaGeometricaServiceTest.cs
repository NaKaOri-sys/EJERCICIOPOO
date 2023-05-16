using AutoFixture;
using AutoMapper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EjercicioPOO.Tests
{
    public class FormaGeometricaServiceTest
    {
        private readonly FormaGeometricaService _formaGeometrica;
        private readonly IGenericRepository<FormaGeometrica> _formaGeometricaRepository = Substitute.For<IGenericRepository<FormaGeometrica>>();
        private readonly IGenericRepository<Trapecio> _trapecioRepository = Substitute.For<IGenericRepository<Trapecio>>();
        private readonly IGenericRepository<Cuadrado> _cuadradoRepository = Substitute.For<IGenericRepository<Cuadrado>>();
        private readonly IGenericRepository<TrianguloEquilatero> _trianguloRepository = Substitute.For<IGenericRepository<TrianguloEquilatero>>();
        private readonly IGenericRepository<Circulo> _circuloRepository = Substitute.For<IGenericRepository<Circulo>>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        public FormaGeometricaServiceTest()
        {
            _formaGeometrica = new FormaGeometricaService(_formaGeometricaRepository,
                                                   _trapecioRepository,
                                                   _cuadradoRepository,
                                                   _trianguloRepository,
                                                   _circuloRepository,
                                                   _mapper);
        }

        #region UtilityMethods
        private Trapecio CreateTrapecioEntity()
        {
            var trapecio = new Trapecio
            {
                FormaGeometricaID = 1,
                TrapecioID = 2,
                BaseMayor = 10,
                BaseMenor = 10,
                LadoIzquierdo = 10,
                LadoDerecho = 10,
                Altura = 10,
            };
            return trapecio;
        }
        private FormaGeometricaDto CreateFormaGeometricaDto(FormasEnum forma)
        {
            var shape = new Fixture().Create<FormaGeometricaDto>();
            shape.FormaGeometricaID = 1;
            shape.Lado = 10;
            shape.LadoBase = 10;
            shape.LadoIzquierdo = 10;
            shape.LadoDerecho = 10;
            shape.Area = 10;
            shape.Altura = 10;
            shape.Tipo = forma;

            return shape;
        }

        private FormaGeometrica CreateFormaGeometricaEntity(int tipoId)
        {
            var shape = new FormaGeometrica
            {
                ColeccionesFormasID = 1,
                ColeccionForma = new ColeccionesFormas(),
                FormaGeometricaID = 1,
                Lado = 1,
                TipoDeFormas = new TipoDeFormas(),
                TipoID = tipoId
            };

            return shape;
        }
        #endregion

        [Fact]
        private void CreateForma_LadoAreLessOrEqualThanZero_ThrowsBadRequestException()
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Cuadrado);
            shapeDto.Lado = 0;

            Assert.Throws<BadRequestException>(() => _formaGeometrica.CreateForma(shapeDto));
        }

        [Fact]
        private void CreateForma_FormasEnumHaveTheDefaultValue_ThrowsInternalErrorException()
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(default);

            Assert.Throws<InternalErrorException>(() => _formaGeometrica.CreateForma(shapeDto));
        }

        [Fact]
        private void CreateForma_CreateSuccessfulSquareShape_ReturnsFormaGeometricaEntity()
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Cuadrado);
            var shape = CreateFormaGeometricaEntity(1);
            _mapper.Map<FormaGeometrica>(shapeDto).Returns(shape);
            //Act
            var response = _formaGeometrica.CreateForma(shapeDto);

            //Assert
            Assert.True(response != null);
        }

        [Fact]
        private void CreateForma_CreateSuccessfulTrapeziumShape_ReturnsFormaGeometricaEntity()
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Trapecio);
            var shape = CreateFormaGeometricaEntity(4);
            _mapper.Map<FormaGeometrica>(shapeDto).Returns(shape);
            //Act
            var response = _formaGeometrica.CreateForma(shapeDto);

            //Assert
            Assert.True(response != null);
        }

        [Fact]
        private void GetForma_CorrectIDAndObtainSpecificShape_ReturnsFormaGeometricaDto()
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Trapecio);
            var shape = CreateFormaGeometricaEntity(4);
            var trapecio = CreateTrapecioEntity();
            var listaFormas = new List<FormaGeometrica> { shape };
            var listaTrapecios = new List<Trapecio> { trapecio };
            var queryFormas = listaFormas.AsQueryable();
            var queryTrapecios = listaTrapecios.AsQueryable();
            _formaGeometricaRepository.GetAll().Returns(queryFormas);
            _trapecioRepository.GetAll().Returns(queryTrapecios);
            _mapper.Map<FormaGeometricaDto>(shape).Returns(shapeDto);

            //Act
            var response = _formaGeometrica.GetForma(1);

            //Assert
            Assert.True(response != null);
        }

        [Fact]
        private void GetForma_IncorrectIDAndCannotObtainShape_ThrowsNotFoundException()
        {
            Assert.Throws<NotFoundException>(() => _formaGeometrica.GetForma(1));
        }

        [Fact]
        private void GetAllFormas_ObtainAllShapesSavedInDB_ReturnsListFormaGeometricaDto()
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Trapecio);
            var shape = CreateFormaGeometricaEntity(4);
            var trapecio = CreateTrapecioEntity();
            var listaFormas = new List<FormaGeometrica> { shape };
            var listaFormasDto = new List<FormaGeometricaDto> { shapeDto };
            var listaTrapecios = new List<Trapecio> { trapecio };
            var queryFormas = listaFormas.AsQueryable();
            var queryTrapecios = listaTrapecios.AsQueryable();
            _formaGeometricaRepository.GetAll().Returns(queryFormas);
            _trapecioRepository.GetAll().Returns(queryTrapecios);
            _mapper.Map<List<FormaGeometricaDto>>(Arg.Any<List<FormaGeometrica>>()).Returns(listaFormasDto);

            //Act
            var response = _formaGeometrica.GetAllFormas();

            //Assert
            response.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        private void GetAllFormas_NoShapesSaved_ThrowsInternalErrorException()
        {
            //Assert
            Assert.Throws<InternalErrorException>(() => _formaGeometrica.GetAllFormas());
        }

        [Fact]
        private void Update_TheShapeCannotBeFoundThenIdSpecified_ThrowsNotFoundException() 
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Trapecio);

            //Assert
            Assert.Throws<NotFoundException>(() => _formaGeometrica.UpdateForma(shapeDto));
        }

        [Fact]
        private void Update_AnErrorOcurredWhenShapeTryingToUpdating_ThrowsInternalErrorException() 
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Trapecio);
            var shape = CreateFormaGeometricaEntity(4);
            _formaGeometricaRepository.GetById(shapeDto.FormaGeometricaID).Returns(shape);
            _trapecioRepository.GetAll().Throws(new InternalErrorException("Error in DB."));

            //Assert
            Assert.Throws<InternalErrorException>(()=> _formaGeometrica.UpdateForma(shapeDto));
        }

        [Fact]
        private void DeleteForma_ShapeCannotBeFoundWithSpecifiedId_ThrowsNotFoundException() 
        {
            Assert.Throws<NotFoundException>(() => _formaGeometrica.DeleteForma(1));
        }
        
        [Fact]
        private void DeleteForma_AnErrorOCurredWhenTryingToDeleteTheShape_ThrowsInternalErrorException() 
        {
            //Arrange
            var shapeDto = CreateFormaGeometricaDto(FormasEnum.Trapecio);
            var shape = CreateFormaGeometricaEntity(4);
            _formaGeometricaRepository.GetById(shapeDto.FormaGeometricaID).Returns(shape);
            _cuadradoRepository.GetAll().Throws(new InternalErrorException("Some error in DB"));

            //Assert
            Assert.Throws<InternalErrorException>(() => _formaGeometrica.DeleteForma(1));
        }
    }
}
