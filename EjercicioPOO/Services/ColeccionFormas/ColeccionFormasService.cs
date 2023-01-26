using EjercicioPOO.Application.Dto;
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

        public ColeccionFormasService(IGenericRepository<ColeccionesFormas> coleccionFormasRepository,
                                      IGenericRepository<FormaGeometrica> formaGeometricaRepository)
        {
            _coleccionFormasRepository = coleccionFormasRepository;
            _formaGeometricaRepository = formaGeometricaRepository;
        }

        public string CreateColeccion(int[] IDsFormasGeometricas)
        {
            var coleccion = new ColeccionesFormas();
            foreach (var ID in IDsFormasGeometricas)
            {
                var entity = _formaGeometricaRepository.GetById(ID);
                coleccion.FormasGeometricas.Add(entity);
                _coleccionFormasRepository.Insert(coleccion);
            }

            _coleccionFormasRepository.Save();

            return "Se creo con éxito la colección.";
        }

        public ColeccionFormasDto GetColeccion(int IdColeccion)
        {
            var coleccion = _coleccionFormasRepository.GetAll()
                                                      .Include(x => x.FormasGeometricas)
                                                      .ThenInclude(o => o.TipoDeFormas)
                                                      .FirstOrDefault(p => p.ColeccionesFormasID == IdColeccion);
            if (coleccion == null)
            {
                throw new Exception("Error Get Coleccion");
            }

            var dto = new ColeccionFormasDto
            {
                ColeccionId = coleccion.ColeccionesFormasID,
                formasGeometricas = coleccion.FormasGeometricas
                .Select(x => new FormaGeometricaDto
                {
                    FormaGeometricaID = x.FormaGeometricaID,
                    TipoID = x.TipoID,
                    TipoForma = x.TipoDeFormas.Nombre,
                    Lado = x.Lado
                }).ToList()
            };

            return dto;
        }

        public List<ColeccionFormasDto> GetAllColeccion()
        {
            var colecciones = _coleccionFormasRepository.GetAll()
                .Include(x => x.FormasGeometricas).ThenInclude(o => o.TipoDeFormas)
                .Select(z => new ColeccionFormasDto
                {
                    ColeccionId = z.ColeccionesFormasID,
                    formasGeometricas = z.FormasGeometricas
                    .Select(m => new FormaGeometricaDto
                    {
                        FormaGeometricaID = m.FormaGeometricaID,
                        TipoID = m.TipoID,
                        TipoForma = m.TipoDeFormas.Nombre,
                        Lado = m.Lado
                    }).ToList()
                }).ToList();


            return colecciones;
        }

        public string DeleteColeccion(int IdColeccion)
        {
            var coleccion = _coleccionFormasRepository.GetById(IdColeccion);
            if (coleccion == null)
                return null;
            DeleteReferenceInFormaGeometricas(coleccion);

            _coleccionFormasRepository.Delete(coleccion);
            _coleccionFormasRepository.Save();

            return "OK";
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

        public string UpdateColeccion(int IdColeccion, int[] IDsFormasGeometricas)
        {
            var coleccion = _coleccionFormasRepository.GetById(IdColeccion);
            UpdateRelationships(coleccion, IDsFormasGeometricas);

            if (coleccion == null)
            {
                return null;
            }
            _coleccionFormasRepository.Save();

            return "OK";
        }

        private void UpdateRelationships(ColeccionesFormas coleccion, int[] IDsFormasGeometricas)
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
    }
}
