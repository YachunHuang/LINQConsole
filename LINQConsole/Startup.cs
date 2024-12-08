using LINQConsole.service;
using Microsoft.Extensions.DependencyInjection;

namespace LINQConsole
{
    internal class Startup
    {
        private readonly IServiceProvider _serviceProvider;

        public Startup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Run()
        {
            foreach (var service in _serviceProvider.GetServices<IListService>())
            {
                service.Execute();
            }
        }
    }
}
