﻿using EjercicioPOO.API.Helper;
using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.Repository;
using EjercicioPOO.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EjercicioPOO.Application.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuariosRepository;

        public UsuarioService(IGenericRepository<Usuario> usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public UsuarioV CreateUser(UsuarioDto usuario)
        {
            var usuarioExistente = _usuariosRepository.GetAll().Where(x => x.User.Equals(usuario.User)).FirstOrDefault();
            if (usuarioExistente != null)
                throw new InternalErrorException("El usuario ya existe.");

            var hashPass = HashHelper.HashPasword(usuario.Password);
            var usuarioNuevo = new Usuario(usuario.User, hashPass.Password, hashPass.Salt);
            try
            {
                _usuariosRepository.Insert(usuarioNuevo);
                _usuariosRepository.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
            var userV = new UsuarioV(usuarioNuevo.IdUser, usuarioNuevo.User);

            return userV;
        }

        public UsuarioV FindUser(string usuario)
        {
            var usuarioExistente = _usuariosRepository.GetAll().Where(x => x.User.Equals(usuario)).FirstOrDefault();
            var usuarioDto = new UsuarioV(usuarioExistente.IdUser, usuarioExistente.User);

            return usuarioDto;
        }

        public List<UsuarioV> FindAllUsers()
        {
            var usuarios = _usuariosRepository.GetAll()
                .Select(x => new UsuarioV(x.IdUser, x.User))
                .ToList();

            return usuarios;
        }

        public void DeleteUser(string usuario)
        {
            try
            {
                var usuarioExistente = _usuariosRepository.GetAll().Where(x => x.User == usuario).FirstOrDefault();
                if (usuarioExistente == null)
                    throw new NotFoundException("No se pudo encontrar al usuario indicado.");
                _usuariosRepository.Delete(usuarioExistente.IdUser);
                _usuariosRepository.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        public void UpdateUser(UsuarioDto usuario)
        {
            try
            {
                var usuarioExistente = _usuariosRepository.GetAll().Where(x => x.User == usuario.User).FirstOrDefault();
                var hashPass = HashHelper.HashPasword(usuario.Password);
                var usuarioNuevo = new Usuario(usuario.User, hashPass.Password, hashPass.Salt);
                _usuariosRepository.Update(usuarioExistente);
                _usuariosRepository.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }
    }
}
