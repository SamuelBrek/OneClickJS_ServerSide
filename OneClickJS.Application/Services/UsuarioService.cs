using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;
using OneClickJS.Domain.Interfaces;

namespace OneClickJS.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        public bool ValidateCreate(Usuario newUsuario)
        {
            if(string.IsNullOrEmpty(newUsuario.NombreUsuario))
                return false;

            if(string.IsNullOrEmpty(newUsuario.ApellidoUsuario))
                return false;
            
            if(string.IsNullOrEmpty(newUsuario.CorreoUsuario))
                return false;

            if(string.IsNullOrEmpty(newUsuario.ContraseñaUsuario))
                return false;

            if(string.IsNullOrEmpty(newUsuario.NivelUsuario))
                return false;

            return true;
        }

        public bool ValidateUpdate(Usuario newUsuario)
        {
            if(newUsuario.IdUsuario <= 0)
                return false;

            if(string.IsNullOrEmpty(newUsuario.NombreUsuario))
                return false;

            if(string.IsNullOrEmpty(newUsuario.ApellidoUsuario))
                return false;
            
            if(string.IsNullOrEmpty(newUsuario.CorreoUsuario))
                return false;

            if(string.IsNullOrEmpty(newUsuario.ContraseñaUsuario))
                return false;

            if(string.IsNullOrEmpty(newUsuario.NivelUsuario))
                return false;

            return true;
        }
    }
}