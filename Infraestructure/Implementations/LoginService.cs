using Application.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructure.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly string _loginStoredProcedure;
        private readonly ValidatePassword _validatePassword;

        public LoginService(IConfiguration configuration, ValidatePassword validatePassword)
        {
            _configuration = configuration;
            _loginStoredProcedure = configuration["DataBase:StoredProcedures:Login"];
            _validatePassword = validatePassword;
        }

        public ServiceResultEntity LoginUser(LoginDto login)
        {
            try
            {
                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(_loginStoredProcedure, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", login.Username);
                        var validationResult = DataValidator.ValidateDatabaseData(cmd);

                        var dataTable = validationResult.Data as DataTable;

                        if (dataTable == null || dataTable.Rows.Count == 0)
                        {
                            return new ServiceResultEntity { Success = false, Message = "No se encontraron datos." };
                        }

                        var passwordValidationResult = _validatePassword.VerifyPassword(dataTable.Rows[0], login.Password, _configuration);

                        if (!passwordValidationResult.Success)
                        {
                            return passwordValidationResult;
                        }

                        return new ServiceResultEntity { Success = true, Message = "Login exitoso", Data = passwordValidationResult.Data };
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
