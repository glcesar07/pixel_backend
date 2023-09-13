using Application.DTO;
using FluentValidation;

namespace Application.Validator
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.Username)
                .NotNull().WithMessage("El campo Username es requerido.")
                .NotEmpty().WithMessage("El campo Username no puede estar vacío.");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("El campo Password es requerido.")
                .NotEmpty().WithMessage("El campo Password no puede estar vacío.");
        }
    }
}
