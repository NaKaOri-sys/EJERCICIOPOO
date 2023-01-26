using System.Collections.Generic;

namespace EjercicioPOO.Application.Dto
{
    public class ColeccionFormasDto
    {
        public ColeccionFormasDto()
        {
            formasGeometricas = new List<FormaGeometricaDto>();
        }
        public int ColeccionId { get; set; }
        public List<FormaGeometricaDto> formasGeometricas { get; set; }
    }
}
