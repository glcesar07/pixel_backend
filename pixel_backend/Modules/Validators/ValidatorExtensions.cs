using Application.DTO;
using Application.Validator;

namespace Presentation.Modules.Validators
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddCustomValidators(this IServiceCollection services)
        {
            services.AddTransient<LoginDtoValidator>();

            return services;
        }
    }
}
