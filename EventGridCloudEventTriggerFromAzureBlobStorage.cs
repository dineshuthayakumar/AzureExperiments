using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Azure.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DU.BlobCreated
{
    public class StorageDiagnostics
    {
        public string batchId { get; set; }
    }

    public class AzureBlobCloudEventData
    {
        public string api { get; set; }
        public string clientRequestId { get; set; }
        public string requestId { get; set; }
        public string eTag { get; set; }
        public string contentType { get; set; }
        public long contentLength { get; set; }
        public string blobType { get; set; }
        public string accessTier { get; set; }
        public string url { get; set; }
        public string sequencer { get; set; }
        public StorageDiagnostics storageDiagnostics { get; set; }       
    }

    public static class EventGridCloudEventTriggerFromAzureBlobStorage
    {
        [FunctionName("EventGridCloudEventTriggerFromAzureBlobStorage")]
        public static async Task Run([EventGridTrigger]CloudEvent cloudEvent, ILogger log)
        {
            var azureBlobCloudEventData = System.Text.Json.JsonSerializer.Deserialize<AzureBlobCloudEventData>(cloudEvent.Data.ToString());
            log.LogInformation("URL deserialized {url}", azureBlobCloudEventData.url);

            JsonObject jsonObject = new JsonObject();
            jsonObject.Add("url", azureBlobCloudEventData.url);
            var content = new StringContent(jsonObject.ToJsonString(), Encoding.UTF8, "application/json");

            var logicAppHttpRequestListenerUrl = "copy-past-url-from-logic-app-http-request-listener";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(logicAppHttpRequestListenerUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            log.LogInformation("Response received {response}", responseString);
            //log.LogInformation("Event received {type} {subject}: {data}", cloudEvent.Type, cloudEvent.Subject, cloudEvent.Data.ToString());
        }
    }
}
