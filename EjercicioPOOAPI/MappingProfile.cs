using AutoMapper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Domain.Entitys;

namespace EjercicioPOO.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CirculoDto, Circulo>();
            CreateMap<ColeccionFormasDto, ColeccionesFormas>();
            CreateMap<CuadradoDto, Cuadrado>();
            CreateMap<FormaGeometricaDto, FormaGeometrica>()
                .ForMember(a => a.TipoID, s => s.MapFrom(d => d.Tipo));
            CreateMap<FormaGeometrica, FormaGeometricaDto>()
                .ForMember(a => a.TipoForma, s => s.MapFrom(d => d.TipoDeFormas.Nombre))
                .ForMember(a => a.Tipo, s => s.MapFrom(d => d.TipoID));
            CreateMap<IdiomaDto, Idiomas>();
            CreateMap<Reportes, ReporteDto>()
                .ForMember(a => a.ReporteID, s => s.MapFrom(d => d.ReportesID));
            CreateMap<FormaGeometricaDto, Trapecio>()
                .ForMember(a => a.BaseMayor, s => s.MapFrom(d => d.LadoBase))
                .ForMember(a => a.BaseMenor, s => s.MapFrom(d => d.Lado)).ReverseMap();
            CreateMap<TrapecioDto, Trapecio>()
                .ForMember(a => a.BaseMayor, s => s.MapFrom(d => d.LadoBase))
                .ForMember(a => a.BaseMenor, s => s.MapFrom(d => d.Lado));
            CreateMap<TrianguloEquilateroDto, TrianguloEquilatero>();
            CreateMap<UsuarioDto, Usuario>();
            CreateMap<ColeccionesFormas, ColeccionFormasDto>()
                .ForMember(a => a.ColeccionId, s => s.MapFrom(d => d.ColeccionesFormasID))
                .ForMember(dest => dest.formasGeometricas,
                           opt => opt.MapFrom(src => src.FormasGeometricas));
            CreateMap<ColeccionFormasDto, FormaGeometricaDto>();
            CreateMap<Idiomas, IdiomaDto>()
                .ForMember(a => a.IdiomaID, s => s.MapFrom(d => d.IdiomasID))
                .ForMember(a => a.Nombre, s => s.MapFrom(d => d.Idioma));
        }
    }
}
