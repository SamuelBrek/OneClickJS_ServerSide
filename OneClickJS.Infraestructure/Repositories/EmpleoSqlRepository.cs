using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneClickJS.Domain.Entities;
using OneClickJS.Domain.Interfaces;
using OneClickJS.Infraestructure.Data;

namespace OneClickJS.Infraestructure.Repositories
{
    public class EmpleoSqlRepository : IEmpleoRepository
    {
        private readonly OneClickJSContext _context;

        public EmpleoSqlRepository(OneClickJSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleo>> GetEmpleos()
        {
            var query = _context.Empleos.Select(empleo => empleo);
            return await query.ToListAsync();
        }

        public async Task<Empleo> GetById(int id)
        {
            var empleos = await _context.Empleos.FirstOrDefaultAsync(x => x.IdEmpleo == id);
            return empleos;
        }
        public async Task<int> CreateEmpleo (Empleo newEmpleo)
        {
            var entity = newEmpleo;
            await _context.Empleos.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows <= 0)
                throw new Exception("El registro de la información no puedo ser completado");
            
            return entity.IdEmpleo;
        }
        public async Task<bool> UpdateEmpleo(int id, Empleo updateEmpleo)
        {
            if(id <= 0 || updateEmpleo == null)
            {
                throw new ArgumentException("Llena todos los campos");
            }
            var entity = await GetById(id);
            entity.NombreEmpleo = updateEmpleo.NombreEmpleo;
            entity.VacantesEmpleo = updateEmpleo.VacantesEmpleo;
            entity.PrestacionesEmpleo = updateEmpleo.PrestacionesEmpleo;
            entity.SueldoEmpleo = updateEmpleo.SueldoEmpleo;
            entity.MunicipioEmpleo = updateEmpleo.MunicipioEmpleo;
            entity.DescripcionEmpleo = updateEmpleo.DescripcionEmpleo;
            entity.TipoEmpleo = updateEmpleo.TipoEmpleo;
            entity.FotoEmpleo = updateEmpleo.FotoEmpleo;
            entity.IdCategoria = updateEmpleo.IdCategoria;
            entity.IdEmpresa = updateEmpleo.IdEmpresa;

            _context.Update(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteEmpleo (int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("No existe ningún empleo con el id que ingresó");
            }
            var entity = await GetById(id);
            _context.Remove(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}