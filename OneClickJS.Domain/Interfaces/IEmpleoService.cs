using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;

namespace OneClickJS.Domain.Interfaces
{
    public interface IEmpleoService
    {
        bool ValidateCreate(Empleo newEmpleo);
        bool ValidateUpdate(Empleo newEmpleo);
    }
}