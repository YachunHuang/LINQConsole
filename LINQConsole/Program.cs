using LINQConsole.service;
using Microsoft.Extensions.DependencyInjection;

namespace LINQConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IListService, ListService>()
                .AddSingleton<Startup>()
                .BuildServiceProvider();

            var startup = serviceProvider.GetRequiredService<Startup>();
            startup.Run();

            Console.ReadLine();
        }
    }
}
