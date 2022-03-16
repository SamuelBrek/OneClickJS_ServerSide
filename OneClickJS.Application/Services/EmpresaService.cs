using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;
using OneClickJS.Domain.Interfaces;

namespace OneClickJS.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
         public bool ValidateCreate(Empresa newEmpresa)
        {
            if(string.IsNullOrEmpty(newEmpresa.NombreEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.DirectorEmpresa))
                return false;
            
            if(string.IsNullOrEmpty(newEmpresa.CalleEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.NumeroEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.CruzamientoEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.ColoniaEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.TelefonoEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.MunicipioEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.FotoEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.NivelEmpresa))
                return false;

            return true;
        }

        public bool ValidateUpdate(Empresa newEmpresa)
        {
            if(newEmpresa.IdEmpresa <= 0)
                return false;

            if(string.IsNullOrEmpty(newEmpresa.NombreEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.DirectorEmpresa))
                return false;
            
            if(string.IsNullOrEmpty(newEmpresa.CalleEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.NumeroEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.CruzamientoEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.ColoniaEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.TelefonoEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.MunicipioEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.FotoEmpresa))
                return false;

            if(string.IsNullOrEmpty(newEmpresa.NivelEmpresa))
                return false;
                
            return true;
        }
    }
}