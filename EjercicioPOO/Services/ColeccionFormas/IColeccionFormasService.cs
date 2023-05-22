using EjercicioPOO.Application.Dto;
using System.Collections.Generic;

namespace EjercicioPOO.Application.Services.ColeccionFormas
{
    public interface IColeccionFormasService
    {
        public void CreateColeccion(int[] IDs);
        public void UpdateColeccion(int ID, int[] IDs);
        public void DeleteColeccion(int ID);
        public ColeccionFormasDto GetColeccion(int ID);
        public List<ColeccionFormasDto> GetAllColeccion();
    }
}