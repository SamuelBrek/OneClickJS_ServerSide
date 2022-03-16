using System;
using System.Collections.Generic;

#nullable disable

namespace OneClickJS.Domain.Entities
{
    public partial class Empresa
    {
        public Empresa()
        {
            Empleos = new HashSet<Empleo>();
        }

        public int IdEmpresa { get; set; }
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

        public virtual ICollection<Empleo> Empleos { get; set; }
    }
}
