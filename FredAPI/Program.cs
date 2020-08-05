using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FredAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider services = ServiceProviderBuilder.GetServiceProvider(args);
            IOptions<APIKeys> options = services.GetRequiredService<IOptions<APIKeys>>();

            SampleData.Root gnpcaData = CallLatestDataAPI("GNPCA", new DateTime(2010, 1, 1), DateTime.Now, options.Value.FRED);

            Console.ReadLine();
        }

        static SampleData.Root CallLatestDataAPI(string sereisID, DateTime startDt, DateTime endDt, string apiKey)
        {
            string content = string.Empty;
            string url = string.Format("https://api.stlouisfed.org/fred/series/observations?series_id={0}&observation_start={1}&observation_end={2}&api_key={3}&file_type=json", sereisID, startDt.ToString("yyy-MM-dd"), endDt.ToString("yyyy-MM-dd"), apiKey);
            WebRequest myReq = WebRequest.Create(url);
            using (WebResponse wr = myReq.GetResponse())
                using (Stream receiveStream = wr.GetResponseStream())
                    using (StreamReader sReader = new StreamReader(receiveStream, Encoding.UTF8))
                        content = sReader.ReadToEnd();
            return JsonConvert.DeserializeObject<SampleData.Root>(content);
        }
    }
}
