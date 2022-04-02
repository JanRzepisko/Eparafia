using System.Data;
using eparafia.Models;
using Npgsql;

namespace eparafia.Helpers;

public class SqlManager : ISqlManager
{
    private const string Host = "194.150.101.246";
    private const string Password = "!Malinka@pass#database";
    private const string Username = "postgres";
    private const string Database = "eparafia";

    private const string ConnectionString = $"Host={Host};Username={Username};Password={Password};Database={Database}";
    private readonly NpgsqlConnection _connection = new NpgsqlConnection(ConnectionString);

    public async Task<List<Dictionary<string, dynamic>>> Reader(string query)
    {
        try
        {
            await _connection.OpenAsync();
            NpgsqlCommand command = new NpgsqlCommand(query, _connection);

            var reader = await command.ExecuteReaderAsync();

            List<Dictionary<string, dynamic>> result = new List<Dictionary<string, dynamic>>();
            while (await reader.ReadAsync())
            {
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var filedName = reader.GetName(i);
                    var value = reader.GetValue(i);

                    data.Add(filedName, value);
                }

                result.Add(data);
            }

            await _connection.CloseAsync();

            return result;
        }
        finally
        {
            if (_connection.State != ConnectionState.Closed)
                await _connection.CloseAsync();
        }
    }

    public async Task Execute(string query)
    {
        var command = new NpgsqlCommand(query, _connection);

        try
        {
            await _connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        finally
        {
            if (_connection.State != ConnectionState.Closed)
                await _connection.CloseAsync();
        }
    }

    public async Task<bool> IsValueExist(string query)
    {
        try
        {
            await _connection.OpenAsync();
            NpgsqlCommand command = new NpgsqlCommand(query, _connection);

            var reader = await command.ExecuteReaderAsync();

            bool result = reader.HasRows;

            await _connection.CloseAsync();
            return result;
        }
        finally
        {
            if (_connection.State != ConnectionState.Closed)
                await _connection.CloseAsync();
        }
    }
}