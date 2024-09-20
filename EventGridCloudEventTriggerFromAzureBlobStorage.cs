using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Messaging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DU.BlobCreated
{
    public class StorageDiagnostics
    {
        public string BatchId { get; set; }
    }

    public class AzureBlobCloudEventData
    {
        public string Api { get; set; }
        public string ClientRequestId { get; set; }
        public string RequestId { get; set; }
        public string ETag { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string BlobType { get; set; }
        public string AccessTier { get; set; }
        public string Url { get; set; }
        public string Sequencer { get; set; }
        public StorageDiagnostics StorageDiagnostics { get; set; }       
    }

    public static class EventGridCloudEventTriggerFromAzureBlobStorage
    {
        [FunctionName("EventGridCloudEventTriggerFromAzureBlobStorage")]
        public static async Task Run([EventGridTrigger]CloudEvent cloudEvent, ILogger log)
        //public static void Run([EventGridTrigger]CloudEvent cloudEvent, ILogger log)
        {
            var azureBlobCloudEventDataObject = JsonConvert.DeserializeObject<AzureBlobCloudEventData>(cloudEvent.Data.ToString());
            log.LogInformation("URL received {url}", azureBlobCloudEventDataObject.Url);

            var url = "logic-app-https-listener-url";
            var payload = "{\"url\": \"" + azureBlobCloudEventDataObject.Url + "\"}";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, new StringContent(payload));

            //log.LogInformation("Event received {type} {subject}: {data}", cloudEvent.Type, cloudEvent.Subject, cloudEvent.Data.ToString());
        }
    }
}
