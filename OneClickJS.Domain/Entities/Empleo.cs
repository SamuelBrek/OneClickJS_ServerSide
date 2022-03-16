using System;
using System.Collections.Generic;

#nullable disable

namespace OneClickJS.Domain.Entities
{
    public partial class Empleo
    {
        public Empleo()
        {
            Postulaciones = new HashSet<Postulacione>();
        }

        public int IdEmpleo { get; set; }
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

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<Postulacione> Postulaciones { get; set; }
    }
}
