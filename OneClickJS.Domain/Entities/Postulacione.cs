using System;
using System.Collections.Generic;

#nullable disable

namespace OneClickJS.Domain.Entities
{
    public partial class Postulacione
    {
        public int IdPostulacion { get; set; }
        public string ArchivoPostulacion { get; set; }
        public int IdEmpleo { get; set; }
        public int IdUsuario { get; set; }
        public string EstadoPostulacion { get; set; }
        
        

        public virtual Empleo IdEmpleoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
