using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infraestructure.Helpers
{
    public class ValidatePassword
    {
        private readonly ITokenService _tokenService;
        public ValidatePassword(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public ServiceResultEntity VerifyPassword(DataRow data, string pass, IConfiguration configuration)
        {            

            if (!BCrypt.Net.BCrypt.Verify(pass, data["password"].ToString()))
            {
                return new ServiceResultEntity
                {
                    Success = false,
                    Message = "Credenciales no válidas",
                    Data = data["username"].ToString()
                };
            }

            var response = new
            {
                p = data["idPersona"],
                nombreCompleto = data["nombreCompleto"],
                roles = data["rol"],
                username = data["username"],
                password = data["password"],
                rememberToken = data["rememberToken"],
                estado = data["estado"],
            };

            return new ServiceResultEntity
            {
                Success = true,
                Message = "Acceso permitido.",
                Data = new
                {
                    response.p,
                    response.username,
                    response.estado,
                    response.roles,
                    response.nombreCompleto,
                    response.rememberToken,
                    token = _tokenService.GenerateToken(response)
                }
            };
        }
    }
}
