using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using System.Collections.Generic;

namespace DU.HttpTrigger
{
    public static class HttpTrigger
    {
        [FunctionName("SetAzureBlobStorageFolderACL")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string url = data.url;
            string[] folders = url.Replace("/firms/", string.Empty).Split('/');

            string tenantId = "2ac619fb-c474-4b0b-99f5-82b67d197bbc";
            string clientId = "387aacc2-e54a-488f-9a68-b865295b530c";
            string clientSecret = "";
            string securityGroup = "549e84a0-7afb-4027-a576-8adfcbfb3ea4";
            string dfsUri = $"https://dineshuthayakumar.dfs.core.windows.net";
            string containerName = "firms";

            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            DataLakeServiceClient dataLakeServiceClient = new DataLakeServiceClient(new Uri(dfsUri), clientSecretCredential);
            DataLakeFileSystemClient dataLakeFileSystemClient = dataLakeServiceClient.GetFileSystemClient(containerName);

            string path = string.Empty;

            foreach(string folder in folders)
            {
                path = path.Length == 0 ? folder: path + "/" + folder;
                Console.WriteLine(path);
                DataLakeDirectoryClient directoryClient = dataLakeFileSystemClient.GetDirectoryClient(path);
                List<PathAccessControlItem> accessControlList = new List<PathAccessControlItem>();
                accessControlList.Add(new PathAccessControlItem(AccessControlType.Group, folder.Equals("Validated", StringComparison.InvariantCultureIgnoreCase) ? RolePermissions.Write | RolePermissions.Execute : RolePermissions.Execute, false, securityGroup));
                await directoryClient.SetAccessControlListAsync(accessControlList);
            }

            return new OkObjectResult("Permissions updated successfully");
        }
    }
}
