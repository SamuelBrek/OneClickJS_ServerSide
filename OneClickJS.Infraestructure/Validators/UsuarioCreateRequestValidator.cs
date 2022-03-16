using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OneClickJS.Domain.Dtos.Request;
using OneClickJS.Domain.Interfaces;

namespace OneClickJS.Infraestructure.Validators
{
    public class UsuarioCreateRequestValidator : AbstractValidator<UsuarioCreateRequest>
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioCreateRequestValidator(IUsuarioRepository repository)
        {
            this._repository = repository;
            RuleFor(x => x.NombreUsuario).NotNull().NotEmpty().WithMessage("El Nombre, debe ser diferente de vacio")
                .Must(x => x.Length > 3).WithMessage("El Nombre, debe tener más de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Nombre, debe tener menos de 51 caracteres");


            RuleFor(x => x.ApellidoUsuario).NotNull().NotEmpty().WithMessage("El Apellido, debe ser diferente de vacio")
                .Must(x => x.Length > 3).WithMessage("El Apellido, de tener mas de 3 caracteres")
                .Must(x => x.Length < 51).WithMessage("El Apellido, debe tener menos de 51 caracteres");


            RuleFor(dest => dest.CorreoUsuario).NotNull().NotEmpty().Length(10, 100).EmailAddress();
            RuleFor(x => x.CorreoUsuario).Must(NotEmailExist).WithMessage("El correo electrónico ya está registrado");

            RuleFor(dest => dest.ContraseñaUsuario).NotNull().NotEmpty().Length(4,8);


            RuleFor(dest => dest.NivelUsuario).NotNull().NotEmpty().Equals("Usuario");
        }

        public bool NotEmailExist(string email)
        {
            return !_repository.Exist(u => u.CorreoUsuario == email);
        }
        
    }
}