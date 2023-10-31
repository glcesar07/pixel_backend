using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infraestructure.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly IConfiguration _configuration;
        private readonly string _commandText;

        public CountryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _commandText = configuration["DataBase:StoredProcedures:Pais"];
        }

        public ServiceResultEntity Label()
        {
            try
            {

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 6);

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
