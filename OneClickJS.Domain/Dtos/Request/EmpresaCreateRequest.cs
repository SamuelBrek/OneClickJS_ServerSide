using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneClickJS.Domain.Dtos.Request
{
    public class EmpresaCreateRequest
    {
        public string NombreEmpresa { get; set; }
        public string DirectorEmpresa { get; set; }
        public string CorreoEmpresa { get; set; }
        public string ContraseniaEmpresa { get; set; }
        public string CalleEmpresa { get; set; }
        public string NumeroEmpresa { get; set; }
        public string CruzamientoEmpresa { get; set; }
        public string ColoniaEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string MunicipioEmpresa { get; set; }
        public string FotoEmpresa { get; set; }
        public string NivelEmpresa { get; set; }
    }
}