namespace Presentation.Modules.Cors
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            string myPolicy = "pixelCors";

            services.AddCors(options =>
            {
                options.AddPolicy(myPolicy, builder =>
                {
                    builder.WithOrigins("http://localhost:3000",
                                        "http://localhost")
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .WithMethods("GET", "POST", "PUT", "DELETE")
                           .WithExposedHeaders("Content-Disposition");
                });
            });

            return services;
        }
    }
}
