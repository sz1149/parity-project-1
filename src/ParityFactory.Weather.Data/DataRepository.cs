using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using ParityFactory.Weather.Data.Extensions;

namespace ParityFactory.Weather.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                return results;
            }
        }

        public Task ExecuteStoredProcedureAsync(string sprocName, object parameters)
        {
            return ExecuteAsync(sprocName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var results = await connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
                //IDisposable will automatically close the connection. **Testing Async calls**
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                return results;
            }
        }

        public void BulkInsert<T>(string destinationTable, List<T> rows, List<SqlBulkCopyColumnMapping> columnMappings = null)
        {
            using (var destinationConnection = new SqlConnection(_connectionString))
            {
                destinationConnection.Open();
                using (var bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    bulkCopy.DestinationTableName = destinationTable;
                    if (columnMappings != null)
                    {
                        foreach (var mapping in columnMappings)
                            bulkCopy.ColumnMappings.Add(mapping);
                    }
                    bulkCopy.WriteToServer(rows.ToDataTable());
                }
            }
        }

    }
}