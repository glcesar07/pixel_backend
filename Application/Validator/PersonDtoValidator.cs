using Application.DTO;
using FluentValidation;

namespace Application.Validator
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(u => u.nombre)
                .NotNull().WithMessage("El campo nombre es requerido.")
                .NotEmpty().WithMessage("El campo nombre no puede estar vacío.");

            RuleFor(u => u.email)
                .NotNull().WithMessage("El campo email es requerido.")
                .NotEmpty().WithMessage("El campo email no puede estar vacío.");

            RuleFor(u => u.fechaNacimiento)
                .NotNull().WithMessage("El campo fechaNacimiento es requerido.")
                .NotEmpty().WithMessage("El campo fechaNacimiento no puede estar vacío.");

            RuleFor(u => u.ciudad)
                .NotNull().WithMessage("El campo ciudad es requerido.")
                .NotEmpty().WithMessage("El campo ciudad no puede estar vacío.");

            RuleFor(u => u.tipoPersona)
                .NotNull().WithMessage("El campo tipoPersona es requerido.")
                .NotEmpty().WithMessage("El campo tipoPersona no puede estar vacío.");

            RuleFor(u => u.genero)
                .NotNull().WithMessage("El campo genero es requerido.")
                .NotEmpty().WithMessage("El campo genero no puede estar vacío.");

            //RuleFor(u => u.usuario)
            //     .NotNull().WithMessage("El campo usuario es requerido.")
            //     .NotEmpty().WithMessage("El campo usuario no puede estar vacío.");

            //RuleFor(u => u.rol)
            //     .NotNull().WithMessage("El campo rol es requerido.")
            //     .NotEmpty().WithMessage("El campo rol no puede estar vacío.");

            RuleFor(u => u.proveedorAutenticacion)
                 .NotNull().WithMessage("El campo proveedorAutenticacion es requerido.")
                 .NotEmpty().WithMessage("El campo proveedorAutenticacion no puede estar vacío.");
        }
    }
}
