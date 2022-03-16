using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;

namespace OneClickJS.Domain.Interfaces
{
    public interface IEmpleoRepository
    {
        Task<IEnumerable<Empleo>> GetEmpleos();
        Task<Empleo> GetById(int id);
        Task<int> CreateEmpleo(Empleo empleo);
        Task<bool> UpdateEmpleo(int id, Empleo updateEmpleo);
        Task<bool> DeleteEmpleo(int id);
        
    }
}