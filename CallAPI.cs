using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace FredAPI
{
    public class CallAPI
    {
        public static SampleData.Root CallLatestDataAPI(string sereisID, DateTime startDt, DateTime endDt, string apiKey)
        {
            SampleData.Root returnObject = new SampleData.Root();
            string content = string.Empty;
            string url = string.Format("https://api.stlouisfed.org/fred/series/observations?series_id={0}&observation_start={1}&observation_end={2}&api_key={3}&file_type=json", sereisID, startDt.ToString("yyy-MM-dd"), endDt.ToString("yyyy-MM-dd"), apiKey);
            WebRequest myReq = WebRequest.Create(url);

            using (WebResponse wr = myReq.GetResponse())
            using (Stream receiveStream = wr.GetResponseStream())
            using (StreamReader sReader = new StreamReader(receiveStream, Encoding.UTF8))
                content = sReader.ReadToEnd();
            return JsonSerializer.Deserialize<SampleData.Root>(content);
        }
    }
}
