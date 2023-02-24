using EjercicioPOO.Application.Dto;
using System.Collections.Generic;

namespace EjercicioPOO.Application.Services.Usuarios
{
    public interface IUsuarioService
    {
        public UsuarioV CreateUser(UsuarioDto usuario);
        public UsuarioV FindUser(string usuario);
        public List<UsuarioV> FindAllUsers();
        public string DeleteUser(UsuarioDto usuario);
        public string UpdateUser(UsuarioDto usuario);
    }
}