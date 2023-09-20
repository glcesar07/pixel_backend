using Application.DTO;
using FluentValidation;

namespace Application.Validator
{
    public class ParagraphDtoValidator : AbstractValidator<ParagraphDtoReq>
    {
        public ParagraphDtoValidator()
        {
            RuleFor(u => u.carpeta)
                .NotNull().WithMessage("El campo carpeta es requerido.")
                .NotEmpty().WithMessage("El campo carpeta no puede estar vacío.");

            RuleFor(u => u.nombreDocumento)
                .NotNull().WithMessage("El campo nombreDocumento es requerido.")
                .NotEmpty().WithMessage("El campo nombreDocumento no puede estar vacío.");

            RuleFor(u => u.data)
                .NotNull().WithMessage("El campo data es requerido.")
                .NotEmpty().WithMessage("El campo data no puede estar vacío.");           

            RuleFor(u => u.usuario)
                 .NotNull().WithMessage("El campo usuario es requerido.")
                 .NotEmpty().WithMessage("El campo usuario no puede estar vacío.");
        }
    }
}
