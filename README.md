A repository to document my experiments with Microsoft Azure

**Blob Trigger**  
2024-10-17T11:19:22Z   [Information]   Executing 'BlobTriggerForThirdParties' (Reason='New blob detected(LogsAndContainerScan): thirdparties/Inbox/2024-07.csv', Id=2739050f-04aa-400a-87c2-a42b6a0051ae)  
2024-10-17T11:19:22Z   [Information]   Trigger Details: MessageId: 3504684b-f52f-4653-b177-918320e3ef7b, DequeueCount: 1, InsertedOn: 2024-10-17T11:19:22.000+00:00, BlobCreated: 2024-10-17T11:19:21.000+00:00, BlobLastModified: 2024-10-17T11:19:21.000+00:00  
2024-10-17T11:19:22Z   [Information]   C# Blob trigger function Processed blob  
 Name:Inbox/2024-07.csv  
 Size: 16969 Bytes  
2024-10-17T11:19:22Z   [Information]   Executed 'BlobTriggerForThirdParties' (Succeeded, Id=2739050f-04aa-400a-87c2-a42b6a0051ae, Duration=17ms)  
**Azure Function**  
2024-10-17T11:19:22Z   [Information]   Executing 'EventGridCloudEventTriggerFromAzureBlobStorage' (Reason='EventGrid trigger fired at 2024-10-17T11:19:22.2840439+00:00', Id=9e460285-53bb-401a-bcf9-564f06b7fede)  
2024-10-17T11:19:22Z   [Information]   URL deserialized https://dineshuthayakumar.blob.core.windows.net/thirdparties/Inbox/2024-07.csv  
2024-10-17T11:19:22Z   [Information]   Response received  
2024-10-17T11:19:22Z   [Information]   Executed 'EventGridCloudEventTriggerFromAzureBlobStorage' (Succeeded, Id=9e460285-53bb-401a-bcf9-564f06b7fede, Duration=160ms)  