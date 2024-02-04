using Application.Commons.Interfaces;
using Npgsql;
using System.Data;

namespace Infrastructure.Databases;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string? _connectionString;

    public SqlConnectionFactory(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        // creando la conexión para dapper
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
