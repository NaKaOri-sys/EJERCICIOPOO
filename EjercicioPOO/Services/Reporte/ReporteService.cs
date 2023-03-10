using EjercicioPOO.Application.Dto;
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

        public ReporteService(IGenericRepository<Reportes> reporteRepository,
                              IGenericRepository<ColeccionesFormas> coleccionFormasRepository,
                              IGenericRepository<Idiomas> idiomasRepository,
                              IGenericRepository<Trapecio> trapecioRepository)
        {
            _reportesRepository = reporteRepository;
            _coleccionFormasRepository = coleccionFormasRepository;
            _idiomasRepository = idiomasRepository;
            _trapecioRepository = trapecioRepository;
        }

        public string CreateReporte(int IdColeccion, IdiomasEnum idioma)
        {
            if (IdColeccion <= 0)
            {
                return null;
            }

            var reporte = new Reportes 
            {
                ColeccionFormas = _coleccionFormasRepository.GetById(IdColeccion),
                Idioma = _idiomasRepository.GetById((int)idioma)
            };
            _reportesRepository.Insert(reporte);
            _reportesRepository.Save();

            return "OK";
        }

        public string UpdateReporte(int IdReporte, int IdColeccion, IdiomasEnum idioma)
        {
            if (IdReporte <= 0 || IdColeccion <= 0)
            {
                return null;
            }

            var reporte = _reportesRepository.GetById(IdReporte);
            reporte.ColeccionFormas = _coleccionFormasRepository.GetById(IdColeccion);
            reporte.Idioma = _idiomasRepository.GetById((int)idioma);

            _reportesRepository.Update(reporte);
            _reportesRepository.Save();

            return "OK";
        }

        public string GetReporte(int IdReporte)
        {
            var reporte = _reportesRepository.GetAll()
                                             .Include(x => x.ColeccionFormas).ThenInclude(o => o.FormasGeometricas)
                                             .Include(m => m.Idioma)
                                             .FirstOrDefault(p => p.ReportesID == IdReporte);
            if (reporte == null)
            {
                return null;
            }
            var dto = new ReporteDto 
            {
                ReporteID = reporte.ReportesID,
                Idioma = new IdiomaDto { IdiomaID = reporte.IdiomasID, Nombre = reporte.Idioma.Idioma},
                ColeccionFormas = new ColeccionFormasDto 
                {
                    ColeccionId = reporte.ColeccionFormas.ColeccionesFormasID,
                    formasGeometricas = MappingFormasGeometricasToDto(reporte.ColeccionFormas.FormasGeometricas)
                }
            };

            var finalString = Imprimir(dto);

            return finalString;
        }

        private List<FormaGeometricaDto> MappingFormasGeometricasToDto(List<FormaGeometrica> formasGeometricas)
        {
            var result = new List<FormaGeometricaDto>();
            foreach (var forma in formasGeometricas) 
            {
                var dto = new FormaGeometricaDto 
                { 
                    FormaGeometricaID = forma.FormaGeometricaID,
                    Lado = forma.Lado,  
                    Tipo = (FormasEnum)forma.TipoID,
                    TipoID = forma.TipoID
                };
                if(forma.TipoID == 4)
                {
                    var trapecio = _trapecioRepository.GetAll().FirstOrDefault(x => x.FormaGeometricaID == forma.FormaGeometricaID);
                    dto.LadoBase = trapecio.BaseMayor;
                    dto.LadoDerecho = trapecio.LadoDerecho;
                    dto.LadoIzquierdo = trapecio.LadoIzquierdo;
                    dto.Altura = trapecio.Altura;
                }
                result.Add(dto);
            }

            return result;
        }

        public string DeleteReporte(int IdReporte)
        {
            if (IdReporte <= 0)
            {
                return null;
            }
            _reportesRepository.Delete(IdReporte);
            _reportesRepository.Save();

            return "OK";
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
