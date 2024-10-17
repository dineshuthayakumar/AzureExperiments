using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BU.BlobTrigger
{
    public class BlobTrigger
    {
        [FunctionName("BlobTriggerForThirdParties")]
        public void Run([BlobTrigger("thirdparties/{name}", Connection = "storageaccountname")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
