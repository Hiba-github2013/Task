using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Kronos.TestTask.AzureFunctions
{
    public class KronosAPiCaller
    {
        [FunctionName("KronosAPiCaller")]
         public async Task Run([TimerTrigger("0 */1 * * * *",RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Environment.GetEnvironmentVariable("url"));
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var html = reader.ReadToEnd();
                log.LogInformation(html);
            }
        }

        public String GetURlFromappSetting(IConfiguration configuration)
        {
            string url = configuration.GetValue<string>("url");
            return url;
        } 
    }
}
