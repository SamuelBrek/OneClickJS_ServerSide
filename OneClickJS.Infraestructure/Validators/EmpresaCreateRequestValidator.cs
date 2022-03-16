using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OneClickJS.Domain.Dtos.Request;

namespace OneClickJS.Infraestructure.Validators
{
    public class EmpresaCreateRequestValidator : AbstractValidator<EmpresaCreateRequest>
    {
        public EmpresaCreateRequestValidator()
        {
            RuleFor(x => x.NombreEmpresa).NotNull().NotEmpty().WithMessage("El Nombre, debe ser diferente de vacio")
                .Must(x => x.Length > 3).WithMessage("El Nombre, debe tener más de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Nombre, debe tener menos de 51 caracteres");

            RuleFor(x => x.DirectorEmpresa).NotNull().NotEmpty().WithMessage("El director, debe ser diferente de vacio")
                .Must(x => x.Length > 3).WithMessage("El director, debe tener más de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El director, debe tener menos de 51 caracteres");

            RuleFor(dest => dest.CalleEmpresa).NotNull().NotEmpty().Length(1, 4).WithMessage("La calle debe tener de 1 a 4 digitos");

            RuleFor(dest => dest.NumeroEmpresa).NotNull().NotEmpty().Length(1, 4).WithMessage("El número debe tener de 1 a 4 digitos");

            RuleFor(x => x.ColoniaEmpresa).NotNull().NotEmpty().WithMessage("La colonia, debe ser diferente de vacio")
                .Must(x => x.Length > 1).WithMessage("La colonia, debe tener más de 1 caracter")
                .Must(x => x.Length < 100).WithMessage("La colonia, debe tener menos de 100 caracteres");

            RuleFor(x => x.TelefonoEmpresa)
                .NotNull()
                .NotEmpty().WithMessage("Teléfono de contacto, debe ser diferente de vacio")
                .Length(10).WithMessage("Teléfono de contacto,  debe tener una longitud de '10' caracteres")
                .Must(x => x != "1111111111" && x != "2222222222"
                    && x != "3333333333" && x != "4444444444"
                    && x != "5555555555" && x != "6666666666"
                    && x != "7777777777" && x != "8888888888"
                    && x != "9999999999").WithMessage("Teléfono de contacto, no tiene un formato valido");

            RuleFor(dest => dest.MunicipioEmpresa).NotNull().NotEmpty().WithMessage("El municipio, debe ser diferente de nulo");

            RuleFor(dest => dest.FotoEmpresa).NotNull().NotEmpty().WithMessage("La foto, debe ser diferente de nulo");

            RuleFor(dest => dest.NivelEmpresa).NotNull().NotEmpty().Equals("Empresa");
        }
    }
}