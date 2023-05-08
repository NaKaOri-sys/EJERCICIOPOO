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
                .ForMember(a => a.TipoID, s=> s.MapFrom(d => d.Tipo));
            CreateMap<FormaGeometrica, FormaGeometricaDto>()
                .ForMember(a => a.TipoForma, s => s.MapFrom(d => d.TipoDeFormas.Nombre));
            CreateMap<IdiomaDto, Idiomas>();
            CreateMap<ReporteDto, Reportes>();
            CreateMap<FormaGeometricaDto, Trapecio>()
                .ForMember(a => a.BaseMayor, s => s.MapFrom(d => d.LadoBase))
                .ForMember(a => a.BaseMenor, s => s.MapFrom(d => d.Lado));
            CreateMap<TrapecioDto, Trapecio>()
                .ForMember(a => a.BaseMayor, s => s.MapFrom(d => d.LadoBase))
                .ForMember(a => a.BaseMenor, s => s.MapFrom(d => d.Lado));
            CreateMap<Trapecio, FormaGeometricaDto>()
                .ForMember(a => a.LadoBase, s => s.MapFrom(d => d.BaseMayor))
                .ForMember(a => a.Lado, s => s.MapFrom(d => d.BaseMenor));
            CreateMap<TrianguloEquilateroDto, TrianguloEquilatero>();
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}
