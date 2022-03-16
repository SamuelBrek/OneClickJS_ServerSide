using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;

namespace OneClickJS.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuarios();
        Task<Usuario> GetById(int id);
        Task<int> CreateUsuario(Usuario newUsuario);
        Task<bool> UpdateUsuario(int id, Usuario updateUsuario);
        Task<bool> DeleteUsuario(int id);
        bool Exist(Expression<Func<Usuario, bool>> expression);
    }
}