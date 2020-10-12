using System.Data;

namespace ApiPriceRecorder.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
