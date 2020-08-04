using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FredAPI
{
    public class ServiceProviderBuilder
    {
        public static IServiceProvider GetServiceProvider(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .AddUserSecrets(typeof(Program).Assembly)
                .AddCommandLine(args)
                .Build();
            ServiceCollection services = new ServiceCollection();

            services.Configure<APIKeys>(configuration.GetSection(typeof(APIKeys).FullName));

            ServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
        
    }
}
