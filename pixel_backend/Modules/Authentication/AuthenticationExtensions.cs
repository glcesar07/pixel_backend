using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Helpers;
using System.Security.Claims;
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
                        if (context == null)
                        {
                            throw new ArgumentNullException(nameof(context), "El contexto no puede ser nulo.");
                        }

                        if (context.Principal == null)
                        {
                            throw new ArgumentNullException(nameof(context.Principal), "Principal no puede ser nulo.");
                        }

                        var nameIdentifierClaim = context.Principal.FindFirst(ClaimTypes.NameIdentifier);
                        if (nameIdentifierClaim == null)
                        {
                            throw new ArgumentNullException(nameof(nameIdentifierClaim), "El claim 'nameidentifier' no puede ser nulo.");
                        }
                        
                        var email = nameIdentifierClaim.Value;
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
