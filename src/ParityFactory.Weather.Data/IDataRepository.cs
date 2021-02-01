using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ParityFactory.Weather.Data
{
    public interface IDataRepository
    {
        void BulkInsert<T>(string destinationTable, List<T> rows,
            List<SqlBulkCopyColumnMapping> columnMappings = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        Task ExecuteStoredProcedureAsync(string sprocName, object parameters);

        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
    }
}