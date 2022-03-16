using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OneClickJS.Domain.Dtos.Request;

namespace OneClickJS.Infraestructure.Validators
{
    public class EmpleoCreateRequestValidator : AbstractValidator<EmpleoCreateRequest>
    {
        public EmpleoCreateRequestValidator()
        {
            RuleFor(x => x.NombreEmpleo).NotNull().NotEmpty().WithMessage("El Nombre, debe ser diferente de vacio")
                .Must(x => x.Length > 3).WithMessage("El Nombre, debe tener más de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Nombre, debe tener menos de 51 caracteres");        

            RuleFor(x => x.VacantesEmpleo).NotNull().WithMessage("Las vacantes no pueden ser nulo");

            RuleFor(x => x.PrestacionesEmpleo).NotNull().NotEmpty().WithMessage("Las prestaciones deben ser diferentes de vacío")
                .Must(x => x.Length > 5).WithMessage("Las prestaciones deben tener más de 5 caracteres"); 

            RuleFor(x => x.SueldoEmpleo).NotNull().WithMessage("El sueldo no puede ser nulo"); 

            RuleFor(x => x.MunicipioEmpleo).NotNull().NotEmpty().WithMessage("El municipio, debe ser diferente de vacio");

            RuleFor(x => x.DescripcionEmpleo).NotNull().NotEmpty().WithMessage("La descripción, debe ser diferente de vacio")
                .Must(x => x.Length > 1).WithMessage("El Nombre, debe tener más de 1 caracter")
                .Must(x => x.Length < 500).WithMessage("El Nombre, debe tener menos de 500 caracteres");

            RuleFor(x => x.TipoEmpleo).NotNull().NotEmpty().WithMessage("El tipo, debe ser diferente de vacio"); 

            RuleFor(x => x.FotoEmpleo).NotNull().NotEmpty().WithMessage("La foto, debe ser diferente de vacio");

            RuleFor(x => x.IdCategoria).NotNull().NotEmpty().WithMessage("La categoría, debe ser diferente de vacio");

            RuleFor(x => x.IdEmpresa).NotNull().NotEmpty().WithMessage("La empresa, debe ser diferente de vacio");
        }
    }
}