using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace OneClickJS.Domain.Dtos.Responses
{
    public class EmpresaResponse
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string DirectorEmpresa { get; set; }
        public string Direccion {get;set;}
        public string TelefonoEmpresa { get; set; }
        public string MunicipioEmpresa { get; set; }
        public string FotoEmpresa { get; set; }

    }
}