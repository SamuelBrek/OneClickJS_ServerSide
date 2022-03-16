using System.Runtime.InteropServices;
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
    public class EmpresaSqlrepository : IEmpresaRepository
    {
        private readonly OneClickJSContext _context;

        public EmpresaSqlrepository(OneClickJSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetEmpresas()
        {
            var query = _context.Empresas.Select(empresa => empresa);
            return await query.ToListAsync();
        }
        public async Task<Empresa> GetById(int id)
        {
            var empresas = await _context.Empresas.FirstOrDefaultAsync(x => x.IdEmpresa == id);
            return empresas;
        }
        public async Task<int> CreateEmpresa (Empresa newEmpresa)
        {
            var entity = newEmpresa;
            await _context.Empresas.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows <= 0)
                throw new Exception("El registro de la información no pudo ser completado");
            
            return entity.IdEmpresa;
        }
        public async Task<bool> UpdateEmpresa(int id, Empresa updateEmpresa)
        {
            if(id <= 0 || updateEmpresa == null)
            {
                throw new ArgumentException("Llena todos los campos");
            }
            var entity = await GetById(id);
            entity.NombreEmpresa = updateEmpresa.NombreEmpresa;
            entity.DirectorEmpresa = updateEmpresa.DirectorEmpresa;
            entity.CalleEmpresa = updateEmpresa.CalleEmpresa;
            entity.NumeroEmpresa = updateEmpresa.NumeroEmpresa;
            entity.CruzamientoEmpresa = updateEmpresa.CruzamientoEmpresa;
            entity.ColoniaEmpresa = updateEmpresa.CruzamientoEmpresa;
            entity.TelefonoEmpresa = updateEmpresa.TelefonoEmpresa;
            entity.MunicipioEmpresa = updateEmpresa.MunicipioEmpresa;
            entity.FotoEmpresa = updateEmpresa.FotoEmpresa;
            
            _context.Update(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        } 

        public async Task<bool> DeleteEmpresa (int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("No existe ninguna empresa con el id que ingresó");
            }
            var entity = await GetById(id);
            _context.Remove(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}