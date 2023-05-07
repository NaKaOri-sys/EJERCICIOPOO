using EjercicioPOO.Enum;

namespace EjercicioPOO.Application.Services.Reporte
{
    public interface IReporteService
    {
        public void CreateReporte(int ID, IdiomasEnum idioma);
        public void UpdateReporte(int IdReporte, int ID, IdiomasEnum idioma);
        public string GetReporte(int ID);
        public void DeleteReporte(int ID);
    }
}