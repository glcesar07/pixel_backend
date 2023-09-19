using Application.DTO;
using FluentValidation;

namespace Application.Validator
{
    public class OcrDtoValidator : AbstractValidator<OcrDto>
    {
        public OcrDtoValidator()
        {
            RuleFor(u => u.base64Images)
                .NotNull().WithMessage("El campo base64Images es requerido.")
                .NotEmpty().WithMessage("El campo base64Images no puede estar vacío.");

            RuleFor(u => u.language)
                 .NotNull().WithMessage("El campo language es requerido.")
                 .NotEmpty().WithMessage("El campo language no puede estar vacío.");
        }
    }
}
