using AutoFixture;
using AutoMapper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.ColeccionFormas;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EjercicioPOO.Tests
{
    public class ColeccionFormasServiceTest
    {
        private readonly ColeccionFormasService _coleccionFormasService;
        private readonly IGenericRepository<ColeccionesFormas> _coleccionFormasRepository = Substitute.For<IGenericRepository<ColeccionesFormas>>();
        private readonly IGenericRepository<FormaGeometrica> _formaGeometricaRepository = Substitute.For<IGenericRepository<FormaGeometrica>>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly IFormaGeometricaService _formaGeometricaService = Substitute.For<IFormaGeometricaService>();
        private readonly IGenericRepository<Trapecio> _trapecioRepository = Substitute.For<IGenericRepository<Trapecio>>();


        public ColeccionFormasServiceTest()
        {
            _coleccionFormasService = new ColeccionFormasService(_coleccionFormasRepository,
                                                                 _formaGeometricaRepository,
                                                                 _mapper,
                                                                 _formaGeometricaService);
        }

        #region UtilityMethods
        private ColeccionesFormas CreateColeccionEntity()
        {
            var collection = new ColeccionesFormas
            {
                ColeccionesFormasID = 1,
                Reportes = new List<Reportes>(),
                FormasGeometricas = new List<FormaGeometrica>()
            };

            return collection;
        }

        private ColeccionFormasDto CreateColeccionDto()
        {
            var collection = new Fixture().Create<ColeccionFormasDto>();
            collection.formasGeometricas.Capacity = 3;

            return collection;
        }
        #endregion
        [Fact]
        private void CreateCollection_CannotCreateCollectionBecauseSomeIdDoesntExistInDB_ThrowsInternalErrorException()
        {
            //Arrange
            var IdsFormasGeometricas = new int[] { 1, 2, 3 };
            _formaGeometricaRepository.GetById(Arg.Any<int>()).Throws(new NotFoundException("The shape cannot be found"));

            //Assert
            Assert.Throws<InternalErrorException>(() => _coleccionFormasService.CreateColeccion(IdsFormasGeometricas));
        }

        [Fact]
        private void GetCollection_CannotObtainSpecificCollectionBecauseTheCollectionDontExistInDB_ThrowsNotFoundException()
        {
            //Assert
            Assert.Throws<NotFoundException>(() => _coleccionFormasService.GetColeccion(1));
        }

        [Fact]
        private void GetCollection_TheCollectionDontHaveShapes_ThrowsInternalErrorException()
        {
            //Arrange
            var coleccionEntity = CreateColeccionEntity();
            var listaColecciones = new List<ColeccionesFormas> { coleccionEntity };
            var coleccionDto = CreateColeccionDto();
            coleccionDto.formasGeometricas = null;

            _coleccionFormasRepository.GetAll().Returns(listaColecciones.AsQueryable());
            _mapper.Map<ColeccionFormasDto>(coleccionEntity).Returns(coleccionDto);

            //Assert
            Assert.Throws<InternalErrorException>(() => _coleccionFormasService.GetColeccion(1));
        }

        [Fact]
        private void GetAllCollection_CannotObtainCollectionsBecauseAnyCollectionExistInDB_ThrowsNotFoundException()
        {
            //Assert
            Assert.Throws<NotFoundException>(() => _coleccionFormasService.GetAllColeccion());
        }

        [Fact]
        private void GetAllCollection_SomeCollectionDontHaveShapes_ThrowsInternalErrorException()
        {
            //Arrange
            var coleccionEntity = CreateColeccionEntity();
            var listaColecciones = new List<ColeccionesFormas> { coleccionEntity };
            var coleccionDto = CreateColeccionDto();
            coleccionDto.formasGeometricas = null;

            _coleccionFormasRepository.GetAll().Returns(listaColecciones.AsQueryable());
            _mapper.Map<ColeccionFormasDto>(coleccionEntity).Returns(coleccionDto);

            //Assert
            Assert.Throws<InternalErrorException>(() => _coleccionFormasService.GetAllColeccion());
        }

        [Fact]
        private void DeleteColeccion_CannotDeleteSpecificCollectionBecauseTheCollectionDontExistInDB_ThrowsNotFoundException()
        {
            //Assert
            Assert.Throws<NotFoundException>(() => _coleccionFormasService.DeleteColeccion(1));
        }

        [Fact]
        private void DeleteColeccion_AnErrorOcurredWhenTryingToDeleteRefenceInShapes_ThrowsInternalErrorException()
        {
            //Arrange
            var coleccionEntity = CreateColeccionEntity();


            _coleccionFormasRepository.GetById(1).Returns(coleccionEntity);
            _formaGeometricaRepository.GetAll().Throws(new Exception("Some error in DB."));

            //Assert
            Assert.Throws<InternalErrorException>(() => _coleccionFormasService.DeleteColeccion(1));
        }

        [Fact]
        private void UpdateColeccion_CannotUpdateCollectionBecauseDontExistInDB_ThrowsNotFoundException()
        {
            Assert.Throws<NotFoundException>(() => _coleccionFormasService.UpdateColeccion(1, new int[] { 1 }));
        }
        
        [Fact]
        private void UpdateColeccion_AnErrorOcurredWhenTryingToDeleteOldRefenceInShapes_ThrowsInternalErrorException()
        {
            //Arrange
            var coleccionEntity = CreateColeccionEntity();

            _coleccionFormasRepository.GetById(1).Returns(coleccionEntity);
            _formaGeometricaRepository.GetAll().Throws(new Exception("Some error in DB."));

            //Assert
            Assert.Throws<InternalErrorException>(() => _coleccionFormasService.UpdateColeccion(1, new int[] { 1 }));
        }
        
        [Fact]
        private void UpdateColeccion_AnErrorOcurredWhenTryingToUpdateRefenceInShapes_ThrowsInternalErrorException()
        {
            //Arrange
            var coleccionEntity = CreateColeccionEntity();

            _coleccionFormasRepository.GetById(1).Returns(coleccionEntity);
            _formaGeometricaRepository.GetById(1).Throws(new Exception("Some error in DB."));

            //Assert
            Assert.Throws<InternalErrorException>(() => _coleccionFormasService.UpdateColeccion(1, new int[] { 1 }));
        }
    }
}
