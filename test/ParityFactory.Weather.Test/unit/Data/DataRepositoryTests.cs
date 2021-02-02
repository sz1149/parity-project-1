using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Data;

namespace ParityFactory.Weather.Test.unit.Data
{
    [TestClass]
    public class DataRepositoryTests
    {
        private readonly string _connectionString;
        private readonly DataRepository _dataRepository;

        public DataRepositoryTests()
        {
            _connectionString = "Data Source=localhost:1433; Initial Catalog=dbo; User id=sa; Password=LocalDev@Passw0rd>;";
            Environment.SetEnvironmentVariable("DB_CONNECTION", _connectionString);
            
            var dataRepository = new DataRepository();
 
        }

    }
}
