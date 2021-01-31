using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ParityFactory.Weather.Data
{
    public class SqlLiteDataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public SqlLiteDataRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
        }

        // If I were using a production system I would use BulkInsert, but SQL Lite has a limitation
        // Reference: https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/bulk-insert
        //
        // So why SQL Lite?
        // Lightweight for POC purpose.
        // Implementation can easily be swapped to SQL Server, MySQL with DI change.
        // Alternatively a strategy pattern, but running out of time.
        public virtual async Task BulkInsertAsync<T>(string tableName, T records)
        {
            await using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            await using var transaction = await connection.BeginTransactionAsync();

            var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO {tableName} VALUES ($value)";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "$value";
            parameter.Value = records;
            command.Parameters.Add(parameter);
            
            await command.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }
    }
}
