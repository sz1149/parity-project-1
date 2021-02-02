using System.Threading.Tasks;

namespace ParityFactory.Weather
{
    public interface ICommandHandler
    {
        Task ExecuteAsync(string command);
    }
}