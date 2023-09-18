using Application.DTO;
using FluentValidation;

namespace Application.Validator
{
    public class FolderDtoValidator : AbstractValidator<FolderDto>
    {
        public FolderDtoValidator()
        {
            RuleFor(u => u.nombre)
                .NotNull().WithMessage("El campo nombre es requerido.")
                .NotEmpty().WithMessage("El campo nombre no puede estar vacío.");

            RuleFor(u => u.persona)
                .NotNull().WithMessage("El campo persona es requerido.")
                .NotEmpty().WithMessage("El campo persona no puede estar vacío.");

            RuleFor(u => u.usuario)
                 .NotNull().WithMessage("El campo usuario es requerido.")
                 .NotEmpty().WithMessage("El campo usuario no puede estar vacío.");
        }
    }
}
