using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EjercicioPOO.Application.Services.FormaGeometricaService
{
    public class FormaGeometricaService : IFormaGeometricaService
    {
        private readonly IGenericRepository<FormaGeometrica> _formaGeometricaRepository;
        private readonly IGenericRepository<Trapecio> _trapecioRepository;
        private readonly IGenericRepository<Cuadrado> _cuadradoRepository;
        private readonly IGenericRepository<TrianguloEquilatero> _trianguloRepository;
        private readonly IGenericRepository<Circulo> _circuloRepository;

        public FormaGeometricaService(IGenericRepository<FormaGeometrica> formaGeometricaRepository,
                                      IGenericRepository<Trapecio> trapecioRepository,
                                      IGenericRepository<Cuadrado> cuadradoRepository,
                                      IGenericRepository<TrianguloEquilatero> trianguloRepository,
                                      IGenericRepository<Circulo> circuloRepository)
        {
            _formaGeometricaRepository = formaGeometricaRepository;
            _trapecioRepository = trapecioRepository;
            _cuadradoRepository = cuadradoRepository;
            _trianguloRepository = trianguloRepository;
            _circuloRepository = circuloRepository;
        }

        public FormaGeometrica CreateForma(FormaGeometricaDto request)
        {
            if (request.Lado <= 0)
            {
                throw new Exception("BAD REQUEST");
            }

            var shapeToCreate = Create(request);

            return shapeToCreate;
        }

        public string DeleteForma(int IdFormaGeometrica)
        {
            var formaGeometrica = _formaGeometricaRepository.GetById(IdFormaGeometrica);
            if (formaGeometrica == null)
                return null;
            FindAndRemoveShape(formaGeometrica);
            _formaGeometricaRepository.Delete(formaGeometrica);
            _formaGeometricaRepository.Save();

            return "OK";
        }

        public string UpdateForma(FormaGeometricaDto request)
        {
            var formaGeometrica = _formaGeometricaRepository.GetById(request.FormaGeometricaID);
            if (formaGeometrica == null)
                return null;
            Update(formaGeometrica, request);

            return "OK";
        }

        public FormaGeometricaDto GetForma(int IdFormaGeometrica)
        {
            var forma = _formaGeometricaRepository.GetAll().Include(x => x.TipoDeFormas).FirstOrDefault(m => m.FormaGeometricaID == IdFormaGeometrica);
            var dto = new FormaGeometricaDto
            {
                FormaGeometricaID = forma.FormaGeometricaID,
                TipoID = forma.TipoID,
                TipoForma = forma.TipoDeFormas.Nombre
            };

            return dto;
        }

        public List<FormaGeometricaDto> GetAllFormas()
        {
            var formas = _formaGeometricaRepository.GetAll()
                .Include(z => z.TipoDeFormas)
                .Select(x => new FormaGeometricaDto
                {
                    FormaGeometricaID = x.FormaGeometricaID,
                    TipoID = x.TipoID,
                    TipoForma = x.TipoDeFormas.Nombre
                }).ToList();

            return formas;
        }

        private void FindAndRemoveShape(FormaGeometrica shapeToDelete)
        {
            var cuadrados = _cuadradoRepository.GetAll()?.FirstOrDefault(x => x.FormaGeometricaID == shapeToDelete.FormaGeometricaID);
            var circulos = _circuloRepository.GetAll()?.FirstOrDefault(x => x.FormaGeometricaID == shapeToDelete.FormaGeometricaID);
            var triangulos = _trianguloRepository.GetAll()?.FirstOrDefault(x => x.FormaGeometricaID == shapeToDelete.FormaGeometricaID);
            var trapecios = _trapecioRepository.GetAll()?.FirstOrDefault(x => x.FormaGeometricaID == shapeToDelete.FormaGeometricaID);

            if (cuadrados != null)
            {
                _cuadradoRepository.Delete(cuadrados);
                return;
            }
            if (circulos != null)
            {
                _circuloRepository.Delete(circulos);
                return;
            }
            if (triangulos != null)
            {
                _trianguloRepository.Delete(triangulos);
                return;
            }
            if (trapecios != null)
            {
                _trapecioRepository.Delete(trapecios);
                return;
            }
        }

        private void Update(FormaGeometrica shape, FormaGeometricaDto formaGeometricaDto)
        {
            shape.Lado = formaGeometricaDto.Lado;
            shape.TipoID = (int)formaGeometricaDto.Tipo;
            _formaGeometricaRepository.Update(shape);
            _formaGeometricaRepository.Save();

            if (formaGeometricaDto.Tipo.Equals(FormasEnum.Trapecio))
                UpdateTrapecioEntity(formaGeometricaDto, shape.FormaGeometricaID);
        }

        private FormaGeometrica Create(FormaGeometricaDto formaGeometrica)
        {
            var shape = MappingFormaGeometrica(formaGeometrica);

            _formaGeometricaRepository.Insert(shape);
            _formaGeometricaRepository.Save();
            switch (formaGeometrica.Tipo)
            {
                case FormasEnum.Cuadrado:
                    CreateCuadradoEntity(shape);
                    break;
                case FormasEnum.TrianguloEquilatero:
                    CreateTrianguloEquilateroEntity(shape);
                    break;
                case FormasEnum.Circulo:
                    CreateCirculoEntity(shape);
                    break;
                case FormasEnum.Trapecio:
                    CreateTrapecioEntity(formaGeometrica, shape.FormaGeometricaID);
                    break;
                default:
                    throw new Exception("ERROR 500");
            }

            return shape;
        }

        private FormaGeometrica MappingFormaGeometrica(FormaGeometricaDto formaGeometrica)
        {
            var shape = new FormaGeometrica
            {
                Lado = formaGeometrica.Lado,
                TipoID = (int)formaGeometrica.Tipo
            };

            return shape;
        }

        private void CreateTrapecioEntity(FormaGeometricaDto shape, int formaGeometricaID)
        {
            var entity = new Trapecio
            {
                Altura = shape.Altura,
                BaseMenor = shape.Lado,
                BaseMayor = shape.LadoBase,
                LadoIzquierdo = shape.LadoIzquierdo,
                LadoDerecho = shape.LadoDerecho,
                FormaGeometricaID = formaGeometricaID,
            };
            _trapecioRepository.Insert(entity);
            _trapecioRepository.Save();
        }
        private void UpdateTrapecioEntity(FormaGeometricaDto dto, int formaGeometricaID)
        {
            var entity = _trapecioRepository.GetAll().FirstOrDefault(x => x.FormaGeometricaID == formaGeometricaID);
            entity.Altura = dto.Altura;
            entity.BaseMenor = dto.Lado;
            entity.BaseMayor = dto.LadoBase;
            entity.LadoIzquierdo = dto.LadoIzquierdo;
            entity.LadoDerecho = dto.LadoDerecho;

            _trapecioRepository.Update(entity);
            _trapecioRepository.Save();
        }

        private void CreateCirculoEntity(FormaGeometrica shape)
        {
            var entity = new Circulo
            {
                FormaGeometricaID = shape.FormaGeometricaID
            };
            _circuloRepository.Insert(entity);
            _circuloRepository.Save();
        }

        private void CreateTrianguloEquilateroEntity(FormaGeometrica shape)
        {
            var entity = new TrianguloEquilatero
            {
                FormaGeometricaID = shape.FormaGeometricaID
            };
            _trianguloRepository.Insert(entity);
            _trianguloRepository.Save();
        }

        private void CreateCuadradoEntity(FormaGeometrica shape)
        {
            var entity = new Cuadrado
            {
                FormaGeometricaID = shape.FormaGeometricaID
            };
            _cuadradoRepository.Insert(entity);
            _cuadradoRepository.Save();
        }
    }
}
