using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ParityFactory.Weather.Services.Extensions;


namespace ParityFactory.Weather
{
    public class Program
    {
        [ExcludeFromCodeCoverage]
        private static async Task Main(string[] args)
        {
            if (!InputValidation.IsValid(args))
            {
                PrintHelp();
                return;
            }

            var serviceProvider = BuildServiceProvider();
            var commandHandler = serviceProvider.GetService<ICommandHandler>();
            await commandHandler.ExecuteAsync(args[0]);
        }

        [ExcludeFromCodeCoverage]
        private static void PrintHelp()
        {
            Console.WriteLine("Please run one of the following scripts to run this program:");
            Console.WriteLine("local/download.sh");
            Console.WriteLine("local/import.sh");
            Console.WriteLine("local/aggregate.sh");
            Console.WriteLine("");
            Console.WriteLine("Alternatively provide an argument to the dotnet run command:");
            Console.WriteLine("download.sh");
            Console.WriteLine("import.sh");
            Console.WriteLine("aggregate.sh");
        }
        
        public static ServiceProvider BuildServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<ICommandHandler, CommandHandler>()
                .AddWeatherServices()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}