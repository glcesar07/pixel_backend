using Application.DTO;
using FluentValidation;

namespace Application.Validator
{
    public class DocumentDtoValidator : AbstractValidator<DocumentDto>
    {
        public DocumentDtoValidator()
        {
            RuleFor(u => u.nombre)
                .NotNull().WithMessage("El campo nombre es requerido.")
                .NotEmpty().WithMessage("El campo nombre no puede estar vacío.");

            RuleFor(u => u.archivo)
                .NotNull().WithMessage("El campo archivo es requerido.")
                .NotEmpty().WithMessage("El campo archivo no puede estar vacío.");

            RuleFor(u => u.ubicacion)
                .NotNull().WithMessage("El campo ubicacion es requerido.")
                .NotEmpty().WithMessage("El campo ubicacion no puede estar vacío.");

            RuleFor(u => u.extension)
                .NotNull().WithMessage("El campo extension es requerido.")
                .NotEmpty().WithMessage("El campo extension no puede estar vacío.");

            RuleFor(u => u.extension)
                .NotNull().WithMessage("El campo extension es requerido.")
                .NotEmpty().WithMessage("El campo extension no puede estar vacío.");

            RuleFor(u => u.carpeta)
                .NotNull().WithMessage("El campo carpeta es requerido.")
                .NotEmpty().WithMessage("El campo carpeta no puede estar vacío.");

            RuleFor(u => u.usuario)
                 .NotNull().WithMessage("El campo usuario es requerido.")
                 .NotEmpty().WithMessage("El campo usuario no puede estar vacío.");
        }
    }
}
