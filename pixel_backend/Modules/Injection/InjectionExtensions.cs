using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Infraestructure.Implementations;
using Infraestructure.Security;
using Presentation.Helpers;

namespace Presentation.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddCustomInjection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            // Configure to Appsettings
            services.Configure<ConnectionStringsConfig>(configuration.GetSection("ConnectionStrings"));
            services.Configure<LoggingConfig>(configuration.GetSection("Logging"));
            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
            services.Configure<DataBaseConfig>(configuration.GetSection("DataBase"));

            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<MainConnection>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, AccessToken>();
            services.AddScoped<LoginService>();
            services.AddScoped<ValidatePassword>();
            services.AddScoped<ICatalogueService, CatalogueService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IOcrService, OcrService>();
            services.AddScoped<IParagraphService, ParagraphService>();

            //services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
