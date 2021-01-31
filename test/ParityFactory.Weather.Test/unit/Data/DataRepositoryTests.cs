using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Data;

namespace ParityFactory.Weather.Test.unit.Data
{
    [TestClass]
    public class DataRepositoryTests
    {
        private readonly string _connectionString;
        private readonly SqlLiteDataRepository _dataRepository;

        public DataRepositoryTests()
        {
            _connectionString = "Data Source=:memory:";
            Environment.SetEnvironmentVariable("DB_CONNECTION", _connectionString);
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE Data(test TEXT)";
            command.ExecuteNonQuery();
            
            _dataRepository = new SqlLiteDataRepository();
        }

        [TestMethod]
        public async Task Test_BulkInsertAsync()
        {
            _dataRepository.BulkInsertAsync();
        }
    }
}