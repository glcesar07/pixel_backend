using Application.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Infraestructure.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IConfiguration _configuration;
        private readonly string _commandText;

        public UsuarioService(IConfiguration configuration)
        {
            _configuration = configuration;
            _commandText = configuration["DataBase:StoredProcedures:Usuario"];
        }

        public ServiceResultEntity ChangePassword(ChangePasswordDto request)
        {
            try
            {
                if (request.newPassword != request.confirmPassword)
                {
                    return new ServiceResultEntity { Success = false, Message = "La nueva contraseña y la confirmación no coinciden." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    // Paso 1: Consulta a la Opción 8 del SP para obtener la contraseña actual
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 8);
                        cmd.Parameters.AddWithValue("@persona", request.persona);

                        var result = DataValidator.ValidateDatabaseData(cmd);
                        ServiceResultEntity validationResult = DataValidator.ValidateDatabaseData(cmd);

                        if (!validationResult.Success)
                        {
                            return validationResult;
                        }

                        DataTable? table = validationResult.Data as DataTable;

                        if (table == null)
                        {
                            return new ServiceResultEntity { Success = false, Message = "No se puede pudo consultar los datos." };
                        }
                        
                        var currentHashedPassword = table.Rows[0]["password"].ToString();

                        if (!BCrypt.Net.BCrypt.Verify(request.oldPassword, currentHashedPassword))
                        {
                            return new ServiceResultEntity { Success = false, Message = "La contraseña actual no es correcta." };
                        }
                        
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@opcion", 7);
                        cmd.Parameters.AddWithValue("@persona", request.persona);
                        cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(request.newPassword));

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

    }
}
