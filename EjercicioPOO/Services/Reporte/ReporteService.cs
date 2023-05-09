using AutoMapper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.FormaGeometricaService;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Application.Traducciones;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace EjercicioPOO.Application.Services.Reporte
{
    public class ReporteService : IReporteService
    {
        private readonly IGenericRepository<Reportes> _reportesRepository;
        private readonly IGenericRepository<ColeccionesFormas> _coleccionFormasRepository;
        private readonly IGenericRepository<Idiomas> _idiomasRepository;
        private readonly IGenericRepository<Trapecio> _trapecioRepository;
        private readonly IFormaGeometricaService _formaGeometricaService;
        private readonly IMapper _mapper;

        public ReporteService(IGenericRepository<Reportes> reporteRepository,
                              IGenericRepository<ColeccionesFormas> coleccionFormasRepository,
                              IGenericRepository<Idiomas> idiomasRepository,
                              IGenericRepository<Trapecio> trapecioRepository,
                              IFormaGeometricaService formaGeometricaService,
                              IMapper mapper)
        {
            _reportesRepository = reporteRepository;
            _coleccionFormasRepository = coleccionFormasRepository;
            _idiomasRepository = idiomasRepository;
            _trapecioRepository = trapecioRepository;
            _formaGeometricaService = formaGeometricaService;
            _mapper = mapper;
        }

        public void CreateReporte(int IdColeccion, IdiomasEnum idioma)
        {
            if (IdColeccion <= 0 || idioma.Equals(0))
            {
                throw new BadRequestException("El Id debe ser mayor a 0, se debe seleccionar un idioma para continuar.");
            }

            var reporte = new Reportes
            {
                ColeccionFormas = _coleccionFormasRepository.GetById(IdColeccion),
                Idioma = _idiomasRepository.GetById((int)idioma)
            };
            _reportesRepository.Insert(reporte);
            _reportesRepository.Save();
        }

        public void UpdateReporte(int IdReporte, int IdColeccion, IdiomasEnum idioma)
        {
            if (IdReporte <= 0 || IdColeccion <= 0 || idioma.Equals(0))
            {
                throw new BadRequestException("El IdReporte debe ser mayor a 0, el IdColección debe ser mayor a 0, debe ingresar un idioma para continuar.");
            }

            var reporte = _reportesRepository.GetById(IdReporte);
            reporte.ColeccionFormas = _coleccionFormasRepository.GetById(IdColeccion);
            reporte.Idioma = _idiomasRepository.GetById((int)idioma);

            _reportesRepository.Update(reporte);
            _reportesRepository.Save();
        }

        public string GetReporte(int IdReporte)
        {
            var reporte = _reportesRepository.GetAll()
                                             .Include(x => x.ColeccionFormas).ThenInclude(o => o.FormasGeometricas)
                                             .Include(m => m.Idioma)
                                             .FirstOrDefault(p => p.ReportesID == IdReporte);
            if (reporte == null)
            {
                throw new NotFoundException("No se pudo encontrar el reporte solicitado.");
            }
            var dto = _mapper.Map<ReporteDto>(reporte);
            foreach (var shapes in dto.ColeccionFormas.formasGeometricas)
            {
                if (shapes.TipoID == 4)
                {
                    _formaGeometricaService.MapTrapecioInFormaGeometricaDto(shapes);
                }
            }

            try
            {
                var finalString = Imprimir(dto);

                return finalString;
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        public void DeleteReporte(int IdReporte)
        {
            if (IdReporte <= 0)
            {
                throw new BadRequestException("El IdReporte debe ser mayor a 0.");
            }
            try
            {
                _reportesRepository.Delete(IdReporte);
                _reportesRepository.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        public static string Imprimir(ReporteDto reporte)
        {
            var sb = new StringBuilder();
            var formas = reporte.ColeccionFormas.formasGeometricas;
            var formasIndexadas = new Dictionary<int, DataFormaDto>();
            var formasSinRepeticion = new List<FormaGeometricaDto>();
            SetLenguage((IdiomasEnum)reporte.Idioma.IdiomaID);
            if (!formas.Any())
            {
                sb.Append(Traduccion.NoFormas);
                return sb.ToString();
            }
            // Hay por lo menos una forma
            // HEADER
            sb.Append(Traduccion.Header);
            ProcesarInformacionFormasGeometricas(formas, formasIndexadas, formasSinRepeticion);
            sb.Append(ContenidoReporte(formasSinRepeticion, formasIndexadas));

            return sb.ToString();
        }

        private static string ContenidoReporte(List<FormaGeometricaDto> formasSinRepeticion, Dictionary<int, DataFormaDto> formasIndexadas)
        {
            var totalFormas = 0;
            var totalArea = 0m;
            var totalPerimetro = 0m;
            var sb = new StringBuilder();
            foreach (var forma in formasSinRepeticion)
            {
                var formaDto = MappingFormaGeometricaDto(forma);
                var dataForma = formasIndexadas[forma.TipoID];
                var traduccion = formaDto.TraducirForma(dataForma.SumaCantidad);
                sb.Append(ObtenerLinea(dataForma.SumaCantidad, dataForma.SumaArea, dataForma.SumaPerimetro, traduccion));
                totalFormas += dataForma.SumaCantidad;
                totalArea += dataForma.SumaArea;
                totalPerimetro += dataForma.SumaPerimetro;
            }

            // FOOTER
            sb.Append(Traduccion.Total);
            sb.Append(totalFormas + " " + Traduccion.Formas + " ");
            sb.Append($"{Traduccion.Perimetro} { totalPerimetro:#.##} ");
            sb.Append($"{Traduccion.Area} {totalArea:#.##}");

            return sb.ToString();
        }

        private static void ProcesarInformacionFormasGeometricas(List<FormaGeometricaDto> formas, Dictionary<int, DataFormaDto> formasIndexadas, List<FormaGeometricaDto> formasSinRepeticion)
        {
            foreach (var forma in formas)
            {
                var formaDto = MappingFormaGeometricaDto(forma);
                var sumaArea = formaDto.Area;
                var sumaPerimetro = formaDto.Perimetro;
                var cantidad = 1;

                if (!formasSinRepeticion.Any(x => x.TipoID.Equals(forma.TipoID)))
                {
                    formasSinRepeticion.Add(forma);
                }
                if (formasIndexadas.ContainsKey(forma.TipoID))
                {
                    var keyFormas = formasIndexadas[forma.TipoID];
                    cantidad += keyFormas.SumaCantidad;
                    sumaArea += keyFormas.SumaArea;
                    sumaPerimetro += keyFormas.SumaPerimetro;
                }
                formasIndexadas[forma.TipoID] = new DataFormaDto { SumaArea = sumaArea, SumaCantidad = cantidad, SumaPerimetro = sumaPerimetro };
            }
        }

        private static FormaGeometricaDto MappingFormaGeometricaDto(FormaGeometricaDto forma)
        {
            switch (forma.Tipo)
            {
                case FormasEnum.Cuadrado:
                    forma = new CuadradoDto(forma.Lado);
                    return forma;
                case FormasEnum.TrianguloEquilatero:
                    forma = new TrianguloEquilateroDto(forma.Lado);
                    return forma;
                case FormasEnum.Circulo:
                    forma = new CirculoDto(forma.Lado);
                    return forma;
                case FormasEnum.Trapecio:
                    forma = new TrapecioDto(forma.Lado, forma.LadoBase, forma.Altura, forma.LadoDerecho, forma.LadoIzquierdo);
                    return forma;
                default:
                    throw new Exception("ERROR!");
            }
        }

        private static void SetLenguage(IdiomasEnum idioma)
        {
            switch (idioma)
            {
                case IdiomasEnum.Castellano:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("es");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
                    break;
                case IdiomasEnum.Ingles:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    break;
                case IdiomasEnum.Portugues:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("pt");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt");
                    break;
                default:
                    throw new NotImplementedException("Lenguaje no mapeado");
            }
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, string traduccion)
        {
            if (cantidad > 0)
            {
                var traduccionForma = traduccion;
                return $"{cantidad} {traduccionForma} | {Traduccion.Area} {area:#.##} | {Traduccion.Perimetro} {perimetro:#.##} <br/>";
            }

            return string.Empty;
        }
    }
}
