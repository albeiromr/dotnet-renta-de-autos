using System.Data;

namespace Application.Commons.Interfaces;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
