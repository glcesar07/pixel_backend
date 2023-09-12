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
                codigo = data["id"],
                username = data["username"],
                estado = data["estado"],
                roles = data["roles"],
                nombreCompleto = data["nombreCompleto"]
            };

            return new ServiceResultEntity
            {
                Success = true,
                Message = "Acceso permitido.",
                Data = new
                {
                    response.codigo,
                    response.username,
                    response.estado,
                    response.roles,
                    response.nombreCompleto,
                    token = _tokenService.GenerateToken(response)
                }
            };
        }
    }
}
