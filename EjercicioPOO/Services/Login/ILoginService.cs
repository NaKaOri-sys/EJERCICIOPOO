using EjercicioPOO.Application.Dto;

namespace EjercicioPOO.Application.Services.Login
{
    public interface ILoginService
    {
        public string GenerateBearer(LoginDto login, string secretKey);
    }
}