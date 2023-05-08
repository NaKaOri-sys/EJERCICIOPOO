using AutoMapper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public FormaGeometricaService(IGenericRepository<FormaGeometrica> formaGeometricaRepository,
                                      IGenericRepository<Trapecio> trapecioRepository,
                                      IGenericRepository<Cuadrado> cuadradoRepository,
                                      IGenericRepository<TrianguloEquilatero> trianguloRepository,
                                      IGenericRepository<Circulo> circuloRepository,
                                      IMapper mapper)
        {
            _formaGeometricaRepository = formaGeometricaRepository;
            _trapecioRepository = trapecioRepository;
            _cuadradoRepository = cuadradoRepository;
            _trianguloRepository = trianguloRepository;
            _circuloRepository = circuloRepository;
            _mapper = mapper;
        }

        public FormaGeometrica CreateForma(FormaGeometricaDto request)
        {
            if (request.Lado <= 0)
            {
                throw new BadRequestException("El lado de la forma debe ser mayor a 0.");
            }

            var shapeToCreate = Create(request);

            return shapeToCreate;
        }

        public void DeleteForma(int IdFormaGeometrica)
        {
            var formaGeometrica = _formaGeometricaRepository.GetById(IdFormaGeometrica);
            if (formaGeometrica == null)
                throw new NotFoundException("No se pudo encontrar la forma indicada.");
            FindAndRemoveShape(formaGeometrica);
            _formaGeometricaRepository.Delete(formaGeometrica);
            _formaGeometricaRepository.Save();
        }

        public void UpdateForma(FormaGeometricaDto request)
        {
            var formaGeometrica = _formaGeometricaRepository.GetById(request.FormaGeometricaID);
            if (formaGeometrica == null)
                throw new NotFoundException("No se pudo encontrar la forma indicada.");
            Update(formaGeometrica, request);
        }

        public FormaGeometricaDto GetForma(int IdFormaGeometrica)
        {
            var forma = _formaGeometricaRepository.GetAll()?.Include(x => x.TipoDeFormas).FirstOrDefault(m => m.FormaGeometricaID == IdFormaGeometrica);
            if (forma == null)
            {
                throw new NotFoundException("No se pudo obtener la forma solicitada.");
            }

            var dto = _mapper.Map<FormaGeometricaDto>(forma);
            if (forma.TipoID == 4)
            {
                var trapecio = _trapecioRepository.GetAll().FirstOrDefault(x => x.FormaGeometricaID == forma.FormaGeometricaID);
                dto.LadoBase = trapecio.BaseMayor;
                dto.LadoDerecho = trapecio.LadoDerecho;
                dto.LadoIzquierdo = trapecio.LadoIzquierdo;
                dto.Altura = trapecio.Altura;
            }

            return dto;
        }

        public List<FormaGeometricaDto> GetAllFormas()
        {
            var formas = _formaGeometricaRepository.GetAll()
                .Include(z => z.TipoDeFormas).ToList();
            var result = _mapper.Map<List<FormaGeometricaDto>>(formas);
            foreach (var shape in result)
            {
                if (shape.TipoID == 4)
                {
                    var trapecio = _trapecioRepository.GetAll().FirstOrDefault(x => x.FormaGeometricaID == shape.FormaGeometricaID);
                    shape.LadoBase = trapecio.BaseMayor;
                    shape.LadoDerecho = trapecio.LadoDerecho;
                    shape.LadoIzquierdo = trapecio.LadoIzquierdo;
                    shape.Altura = trapecio.Altura;
                }
            }

            return result;
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
            shape = _mapper.Map<FormaGeometrica>(formaGeometricaDto);
            _formaGeometricaRepository.Update(shape);
            _formaGeometricaRepository.Save();

            if (formaGeometricaDto.Tipo.Equals(FormasEnum.Trapecio))
                UpdateTrapecioEntity(formaGeometricaDto);
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
                    formaGeometrica.FormaGeometricaID = shape.FormaGeometricaID;
                    CreateTrapecioEntity(formaGeometrica);
                    break;
                default:
                    throw new InternalErrorException("No se pudo crear la forma solicitada.");
            }

            return shape;
        }

        private FormaGeometrica MappingFormaGeometrica(FormaGeometricaDto formaGeometrica)
        {
            var shape = _mapper.Map<FormaGeometrica>(formaGeometrica);

            return shape;
        }

        private void CreateTrapecioEntity(FormaGeometricaDto shape)
        {
            var entity = _mapper.Map<Trapecio>(shape);
            _trapecioRepository.Insert(entity);
            _trapecioRepository.Save();
        }
        private void UpdateTrapecioEntity(FormaGeometricaDto dto)
        {
            var entity = _trapecioRepository.GetAll().FirstOrDefault(x => x.FormaGeometricaID == dto.FormaGeometricaID);
            if (entity == null)
                throw new NotFoundException("No se pudo encontrar la forma solicitada.");

            entity = _mapper.Map<Trapecio>(dto);

            _trapecioRepository.Update(entity);
            _trapecioRepository.Save();
        }

        private void CreateCirculoEntity(FormaGeometrica shape)
        {
            var entity = _mapper.Map<Circulo>(shape);
            _circuloRepository.Insert(entity);
            _circuloRepository.Save();
        }

        private void CreateTrianguloEquilateroEntity(FormaGeometrica shape)
        {
            var entity = _mapper.Map<TrianguloEquilatero>(shape);
            _trianguloRepository.Insert(entity);
            _trianguloRepository.Save();
        }

        private void CreateCuadradoEntity(FormaGeometrica shape)
        {
            var entity = _mapper.Map<Cuadrado>(shape);
            _cuadradoRepository.Insert(entity);
            _cuadradoRepository.Save();
        }
    }
}
