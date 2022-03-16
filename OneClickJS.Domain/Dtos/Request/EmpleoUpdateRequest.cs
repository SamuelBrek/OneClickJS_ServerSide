using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneClickJS.Domain.Dtos.Request
{
    public class EmpleoUpdateRequest
    {
        public string NombreEmpleo { get; set; }
        public int VacantesEmpleo { get; set; }
        public string PrestacionesEmpleo { get; set; }
        public int SueldoEmpleo { get; set; }
        public string MunicipioEmpleo { get; set; }
        public string DescripcionEmpleo { get; set; }
        public string TipoEmpleo { get; set; }
        public string FotoEmpleo { get; set; }
        public int IdCategoria { get; set; }
        public int IdEmpresa { get; set; }
    }
}