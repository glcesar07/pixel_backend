using Application.Validator;

namespace Presentation.Modules.Validators
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddCustomValidators(this IServiceCollection services)
        {
            services.AddTransient<LoginDtoValidator>();
            services.AddTransient<CatalogueDtoValidator>();
            services.AddTransient<PersonDtoValidator>();
            services.AddTransient<FolderDtoValidator>();
            services.AddTransient<DocumentDtoValidator>();
            services.AddTransient<OcrDtoValidator>();
            services.AddTransient<ParagraphDtoValidator>();

            return services;
        }
    }
}
