using EjercicioPOO.Application.Dto;
using EjercicioPOO.Domain.Entitys;
using EjercicioPOO.Enum;
using System.Collections.Generic;

namespace EjercicioPOO.Application.Services.FormaGeometricaService
{
    public interface IFormaGeometricaService
    {
        public FormaGeometrica CreateForma(FormaGeometricaDto request);
        public string DeleteForma(int ID);
        public string UpdateForma(FormaGeometricaDto request);
        public FormaGeometricaDto GetForma(int ID);
        public List<FormaGeometricaDto> GetAllFormas();
    }
}