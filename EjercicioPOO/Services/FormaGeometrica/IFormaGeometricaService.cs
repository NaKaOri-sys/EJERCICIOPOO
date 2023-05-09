using EjercicioPOO.Application.Dto;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using System.Collections.Generic;

namespace EjercicioPOO.Application.Services.FormaGeometricaService
{
    public interface IFormaGeometricaService
    {
        public FormaGeometrica CreateForma(FormaGeometricaDto request);
        public void DeleteForma(int ID);
        public void UpdateForma(FormaGeometricaDto request);
        public FormaGeometricaDto GetForma(int ID);
        public List<FormaGeometricaDto> GetAllFormas();
        public void MapTrapecioInFormaGeometricaDto(FormaGeometricaDto shape);
    }
}