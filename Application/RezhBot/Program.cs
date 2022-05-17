using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RezhBot.Handlers;

namespace RezhBot
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services.AddSingleton<Executor, Executor>().BuildServiceProvider().GetService<Executor>().StartBot();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHandler, Handler>();
            services.AddSingleton<TelegramBot>();
        }
    }
}
