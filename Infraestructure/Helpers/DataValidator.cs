using Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructure.Helpers
{
    public class DataValidator
    {
        public static ServiceResultEntity ValidateDatabaseData(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet setter = new DataSet();

            try
            {
                adapter.Fill(setter, "tabla");
                if (setter.Tables["tabla"] == null)
                {
                    return new ServiceResultEntity
                    {
                        Success = false,
                        Message = "Proceso no realizado.",                        
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity
                {
                    Success = false,
                    Message = "Proceso no realizado. " + ex.Message,                    
                };
            }

            if (setter?.Tables["tabla"]?.Rows.Count <= 0 || setter?.Tables["tabla"] is null)
            {
                return new ServiceResultEntity
                {
                    Success = false,
                    Message = "No se han encontrado datos relacionados con la búsqueda.",                    
                };
            }

            return new ServiceResultEntity
            {
                Success = true,
                Message = "Proceso realizado con éxito.",
                Data = setter.Tables["tabla"]
            };
        }
    }
}
