using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator
{
    public class CatalogueDtoValidator : AbstractValidator<CatalogueDto>
    {
        public CatalogueDtoValidator()
        {
            RuleFor(u => u.nombre)
                .NotNull().WithMessage("El campo nombre es requerido.")
                .NotEmpty().WithMessage("El campo nombre no puede estar vacío.");

            RuleFor(u => u.usuario)
                 .NotNull().WithMessage("El campo usuario es requerido.")
                 .NotEmpty().WithMessage("El campo usuario no puede estar vacío.");
        }
    }
}
