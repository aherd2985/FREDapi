using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FredAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider services = ServiceProviderBuilder.GetServiceProvider(args);
            IOptions<APIKeys> options = services.GetRequiredService<IOptions<APIKeys>>();

            SampleData.Root gnpcaData = CallAPI.CallLatestDataAPI("GNPCA", new DateTime(2010, 1, 1), DateTime.Now, options.Value.FRED);

            Console.ReadLine();
        }
    }
}
