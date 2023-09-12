using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace Domain.Interfaces
{
    public interface IDataValidator
    {
        JObject ValidateDatabaseData(SqlCommand cmd);
    }
}
