using Microsoft.Data.SqlClient;

namespace Gatherly.Application.Abstractions
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
