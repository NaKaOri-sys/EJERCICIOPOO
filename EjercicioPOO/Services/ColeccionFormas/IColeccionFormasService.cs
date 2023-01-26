using EjercicioPOO.Application.Dto;
using System.Collections.Generic;

namespace EjercicioPOO.Application.Services.ColeccionFormas
{
    public interface IColeccionFormasService
    {
        public string CreateColeccion(int[] IDs);
        public string UpdateColeccion(int ID, int[] IDs);
        public string DeleteColeccion(int ID);
        public ColeccionFormasDto GetColeccion(int ID);
        public List<ColeccionFormasDto> GetAllColeccion();
    }
}