using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;
using OneClickJS.Domain.Interfaces;

namespace OneClickJS.Application.Services
{
    public class EmpleoService : IEmpleoService
    {
         public bool ValidateCreate(Empleo newEmpleo)
        {
            if(string.IsNullOrEmpty(newEmpleo.NombreEmpleo))
                return false;
            
            if(string.IsNullOrEmpty(newEmpleo.PrestacionesEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.MunicipioEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.DescripcionEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.TipoEmpleo))
                return false;
            
            if(string.IsNullOrEmpty(newEmpleo.FotoEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.DescripcionEmpleo))
                return false;

            return true;
        }

        public bool ValidateUpdate(Empleo newEmpleo)
        {
            if(newEmpleo.IdEmpleo <= 0)
                return false;

            if(string.IsNullOrEmpty(newEmpleo.NombreEmpleo))
                return false;
            
            if(string.IsNullOrEmpty(newEmpleo.PrestacionesEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.MunicipioEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.DescripcionEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.TipoEmpleo))
                return false;
            
            if(string.IsNullOrEmpty(newEmpleo.FotoEmpleo))
                return false;

            if(string.IsNullOrEmpty(newEmpleo.DescripcionEmpleo))
                return false;
                
            return true;
        }
    }
}