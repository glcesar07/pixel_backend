using Microsoft.OpenApi.Models;

namespace Presentation.Modules.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PIXEL Technology Services API Market",
                    Description = "ASP.NET Core Web API",
                    TermsOfService = new Uri("http://localhost"),
                    Contact = new OpenApiContact
                    {
                        Name = "Cesar Guarchaj",
                        Email = "cesarguarchaj0@gmail.com",
                        Url = new Uri("https://github.com/glcesar07")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });

            return services;
        }
    }
}
