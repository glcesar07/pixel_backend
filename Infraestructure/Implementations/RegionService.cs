using Application.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infraestructure.Implementations
{
    public class RegionService : IRegionService
    {
        private readonly IConfiguration _configuration;
        private readonly string _commandText;

        public RegionService(IConfiguration configuration)
        {
            _configuration = configuration;
            _commandText = configuration["DataBase:StoredProcedures:Region"];
        }

        public ServiceResultEntity Label(CountryDto request)
        {
            try
            {

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 6);
                        cmd.Parameters.AddWithValue("@pais", request.id);

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
