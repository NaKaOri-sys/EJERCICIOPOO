using AutoMapper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EjercicioPOO.Application.Services.ColeccionFormas
{
    public class ColeccionFormasService : IColeccionFormasService
    {
        private readonly IGenericRepository<ColeccionesFormas> _coleccionFormasRepository;
        private readonly IGenericRepository<FormaGeometrica> _formaGeometricaRepository;
        private readonly IMapper _mapper;
        private readonly IFormaGeometricaService _formaGeometricaService;

        public ColeccionFormasService(IGenericRepository<ColeccionesFormas> coleccionFormasRepository,
                                      IGenericRepository<FormaGeometrica> formaGeometricaRepository,
                                      IMapper mapper,
                                      IFormaGeometricaService formaGeometricaService)
        {
            _coleccionFormasRepository = coleccionFormasRepository;
            _formaGeometricaRepository = formaGeometricaRepository;
            _mapper = mapper;
            _formaGeometricaService = formaGeometricaService;
        }

        public void CreateColeccion(int[] IDsFormasGeometricas)
        {
            try
            {
                var coleccion = new ColeccionesFormas();
                foreach (var ID in IDsFormasGeometricas)
                {
                    var entity = _formaGeometricaRepository.GetById(ID);
                    coleccion.FormasGeometricas.Add(entity);
                    _coleccionFormasRepository.Insert(coleccion);
                }

                _coleccionFormasRepository.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        public ColeccionFormasDto GetColeccion(int IdColeccion)
        {
            var coleccion = _coleccionFormasRepository.GetAll()
                                                      .Include(x => x.FormasGeometricas)
                                                      .ThenInclude(o => o.TipoDeFormas)
                                                      .FirstOrDefault(p => p.ColeccionesFormasID == IdColeccion);
            if (coleccion == null)
            {
                throw new NotFoundException("No se pudo obtener la colección indicada.");
            }

            var dto = _mapper.Map<ColeccionFormasDto>(coleccion);
            try
            {
                foreach (var shape in dto.formasGeometricas)
                {
                    if (shape.TipoID == 4)
                    {
                        _formaGeometricaService.MapTrapecioInFormaGeometricaDto(shape);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new InternalErrorException(ex.Message);
            }

            return dto;
        }

        public List<ColeccionFormasDto> GetAllColeccion()
        {
            var colecciones = _coleccionFormasRepository.GetAll()
                .Include(x => x.FormasGeometricas).ThenInclude(o => o.TipoDeFormas)
                .ToList();
            if (colecciones == null || colecciones.Count == 0)
            {
                throw new NotFoundException("The collection cannot be found.");
            }

            var coleccionesDto = _mapper.Map<List<ColeccionFormasDto>>(colecciones);
            try
            {
                foreach (var collection in coleccionesDto)
                {
                    foreach (var shape in collection.formasGeometricas)
                    {
                        if (shape.TipoID == 4)
                        {
                            _formaGeometricaService.MapTrapecioInFormaGeometricaDto(shape);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }

            return coleccionesDto;
        }

        public void DeleteColeccion(int IdColeccion)
        {
            var coleccion = _coleccionFormasRepository.GetById(IdColeccion);
            if (coleccion == null)
                throw new NotFoundException("No se pudo encontrar la colección indicada.");
            try
            {
                DeleteReferenceInFormaGeometricas(coleccion);

                _coleccionFormasRepository.Delete(coleccion);
                _coleccionFormasRepository.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        private void DeleteReferenceInFormaGeometricas(ColeccionesFormas coleccion)
        {
            var formaGeometrica = _formaGeometricaRepository.GetAll().Where(x => x.ColeccionesFormasID == coleccion.ColeccionesFormasID).ToList();
            foreach (var row in formaGeometrica)
            {
                row.ColeccionForma.FormasGeometricas.Remove(row);
            }
            _formaGeometricaRepository.Save();
        }

        public void UpdateColeccion(int IdColeccion, int[] IDsFormasGeometricas)
        {
            var coleccion = _coleccionFormasRepository.GetById(IdColeccion);
            if (coleccion == null)
            {
                throw new NotFoundException("No se pudo actualizar la colección indicada.");
            }

            UpdateRelationships(coleccion, IDsFormasGeometricas);

            _coleccionFormasRepository.Save();
        }

        private void UpdateRelationships(ColeccionesFormas coleccion, int[] IDsFormasGeometricas)
        {
            try
            {
                DeleteReferenceInFormaGeometricas(coleccion);

                foreach (var row in IDsFormasGeometricas)
                {
                    var entity = _formaGeometricaRepository.GetById(row);
                    if (entity != null)
                    {
                        coleccion.FormasGeometricas.Add(entity);
                    }
                }
                _coleccionFormasRepository.Update(coleccion);
            }
            catch (Exception ex)
            {

                throw new InternalErrorException(ex.Message);
            }
        }
    }
}
