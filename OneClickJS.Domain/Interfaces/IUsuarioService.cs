using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;

namespace OneClickJS.Domain.Interfaces
{
    public interface IUsuarioService
    {
        bool ValidateCreate(Usuario newUsuario);
        bool ValidateUpdate(Usuario newUsuario);
    }
}