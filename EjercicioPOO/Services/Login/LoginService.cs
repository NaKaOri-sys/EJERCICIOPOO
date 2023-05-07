using EjercicioPOO.API.Helper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EjercicioPOO.Application.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IGenericRepository<Usuario> _genericRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IGenericRepository<Usuario> genericRepository, IConfiguration configuration)
        {
            _genericRepository = genericRepository;
            _configuration = configuration;
        }

        public string GenerateBearer(LoginDto login) 
        {
            var usuario = _genericRepository.GetAll().FirstOrDefault(x => x.User.Equals(login.User));
            if (usuario == null)
                return "404";
            if(HashHelper.VerifyPassword(login.Password, usuario.Password, usuario.Sal))
            {
                var secretKey = _configuration.GetSection("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey.Value);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.User));

                var tokenDescriptor = new SecurityTokenDescriptor 
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                var token_bearer = tokenHandler.WriteToken(createdToken);

                return token_bearer;
            }
            return null;
        } 
    }
}
