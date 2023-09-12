using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Helpers;
using System.Text;

namespace Presentation.Modules.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var jwtSettingsSection = builder.Configuration.GetSection("Jwt");
            services.Configure<JwtConfig>(jwtSettingsSection);


            // configure jwt authentication
            var jwtSettings = jwtSettingsSection.Get<JwtConfig>();

            if (jwtSettings is null)
            {
                throw new ArgumentNullException("La configuración de Jwt no puede ser nula.");
            }

            if (jwtSettings.Secret is null)
            {
                throw new ArgumentNullException("El secreto no puede ser nulo.");
            }

            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var Issuer = jwtSettings.Issuer;
            var Audience = jwtSettings.Audience;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if (context?.Principal?.Identity?.Name is null)
                        {
                            throw new ArgumentNullException("El nombre de identidad no puede ser nulo.");
                        }

                        var userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}
