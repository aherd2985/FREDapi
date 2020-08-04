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
            // realtime_start=1776-07-04   realtime_end=9999-12-3   
            var services = ServiceProviderBuilder.GetServiceProvider(args);
            var options = services.GetRequiredService<IOptions<APIKeys>>();

            SampleData.Root gnpcaData = CallAPI("GNPCA", new DateTime(2020, 1, 1), DateTime.Now, options.Value.FRED);

            Console.ReadLine();
        }

        static SampleData.Root CallAPI(string sereisID, DateTime startDt, DateTime endDt, string apiKey)
        {
            string content = string.Empty;
            string url = string.Format("https://api.stlouisfed.org/fred/series/observations?series_id={0}&realtime_start={1}&realtime_start={1}&realtime_end={2}&api_key={3}&file_type=json", sereisID, startDt.ToString("yyy-MM-dd"), endDt.ToString("yyyy-MM-dd"), apiKey);
            WebRequest myReq = WebRequest.Create(url);
            using (WebResponse wr = myReq.GetResponse())
                using (Stream receiveStream = wr.GetResponseStream())
                    using (StreamReader sReader = new StreamReader(receiveStream, Encoding.UTF8))
                        content = sReader.ReadToEnd();
            return JsonConvert.DeserializeObject<SampleData.Root>(content);
        }
    }
}
