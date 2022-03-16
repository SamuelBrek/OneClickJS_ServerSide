using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneClickJS.Domain.Dtos.Request
{
    public class UsuarioCreateRequest
    {
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContraseñaUsuario { get; set; }
        public string NivelUsuario { get; set; }
    }
}