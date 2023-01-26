using EjercicioPOO.Enum;

namespace EjercicioPOO.Application.Services.Reporte
{
    public interface IReporteService
    {
        public string CreateReporte(int ID, IdiomasEnum idioma);
        public string UpdateReporte(int IdReporte, int ID, IdiomasEnum idioma);
        public string GetReporte(int ID);
        public string DeleteReporte(int ID);
    }
}