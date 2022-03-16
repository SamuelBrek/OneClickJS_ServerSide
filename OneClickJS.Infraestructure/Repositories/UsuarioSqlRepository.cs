using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;
using OneClickJS.Infraestructure.Data;
using OneClickJS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OneClickJS.Infraestructure.Repositories
{
    public class UsuarioSqlRepository : IUsuarioRepository
    {
        private readonly OneClickJSContext _context;

        public UsuarioSqlRepository(OneClickJSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var query = _context.Usuarios.Select(usuario => usuario);
            return await query.ToListAsync();
        }
        public async Task<Usuario> GetById(int id)
        {
            var query = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
            return query;
        }

        public async Task<int> CreateUsuario(Usuario newUsuario)
        {
            var entity = newUsuario;
           await _context.Usuarios.AddAsync(entity);
           var rows = await _context.SaveChangesAsync();

           if(rows <= 0)
           {
               throw new Exception("No se pudo completar el registro del usuario");
           }
            return entity.IdUsuario;
        }
        public async Task<bool> UpdateUsuario(int id, Usuario updateUsuario)
        {
            if(id <= 0 || updateUsuario == null)
            {
                throw new ArgumentException("Llena todos los campos");
            }
            var entity = await GetById(id);
            entity.NombreUsuario = updateUsuario.NombreUsuario;
            entity.ApellidoUsuario = updateUsuario.ApellidoUsuario;
            entity.NivelUsuario = updateUsuario.NivelUsuario;

            _context.Update(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        } 
         public bool Exist(Expression<Func<Usuario, bool>> expression)
         {
             return _context.Usuarios.Any(expression);
         }
        public async Task<bool> DeleteUsuario (int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("No existe ningún usuario con el id que ingresó");
            }
            var entity = await GetById(id);
            _context.Remove(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}