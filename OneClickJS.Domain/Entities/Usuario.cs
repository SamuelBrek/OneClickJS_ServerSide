using System;
using System.Collections.Generic;

#nullable disable

namespace OneClickJS.Domain.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            Postulaciones = new HashSet<Postulacione>();
        }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContraseñaUsuario { get; set; }
        public string NivelUsuario { get; set; }

        public virtual ICollection<Postulacione> Postulaciones { get; set; }
    }
}
