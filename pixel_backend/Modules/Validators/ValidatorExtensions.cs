using Application.DTO;

namespace Presentation.Modules.Validators
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddCustomValidators(this IServiceCollection services)
        {
            services.AddTransient<UserDto>();

            return services;
        }
    }
}
