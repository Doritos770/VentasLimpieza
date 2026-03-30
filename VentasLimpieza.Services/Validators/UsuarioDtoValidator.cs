using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VentasLimpieza.core.Dtos;

namespace VentasLimpieza.Services.Validators
{
    public  class UsuarioDtoValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidator()
         {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("EL ID ES OBLIGATORIO Y DEBE SER MAYOR A 0")
                .NotEmpty().WithMessage("El id usuario no puede ser vacio");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("EL EMAIL ES OBLIGATORIO");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de usuario no puede ser vacio");
            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El nombre de usuario no puede ser vacio");
            
         }
    }
}
