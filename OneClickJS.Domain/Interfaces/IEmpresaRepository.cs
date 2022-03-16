using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;

namespace OneClickJS.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetEmpresas();
        Task<Empresa> GetById(int id);
        Task<int> CreateEmpresa(Empresa empresa);
        Task<bool> UpdateEmpresa(int id, Empresa updateEmpresa);
        Task<bool> DeleteEmpresa(int id);
    }
}