using System.Threading.Tasks;

namespace ParityFactory.Weather.Data
{
    public interface IDataRepository
    {
        Task BulkInsertAsync<T>(string tableName, T records);
    }
}
