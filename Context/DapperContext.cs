using Microsoft.Data.SqlClient;
using System.Data;

namespace VisualSoftAspCoreApi.Context
{
    public class DapperContext
    {
       private readonly IConfiguration _config;
       private readonly string _connString;

       public DapperContext(IConfiguration config)
       {
        _config = config;
        _connString = _config.GetConnectionString("SqlConnection");
       }

       public IDbConnection CreateConnection() => new SqlConnection(_connString);

    }
}