using AzureStorageAccessPolicySample.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzureStorageAccessPolicySample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("images");

            var blobs = new List<BlobImage>();

            // var sas = GetSasToken(storageAccount);
            var sas = GetSasTokenFromPolicy(container);

            foreach (var blob in container.ListBlobs())
            {
                if (blob.GetType() == typeof(CloudBlockBlob))
                {
                    blobs.Add(new BlobImage { BlobUri = blob.Uri.ToString() + sas });
                }
            }

            return View(blobs);
        }

        /// <summary>
        /// Generates a SAS Token.
        /// </summary>
        /// <param name="storageAccount"></param>
        /// <returns></returns>
        public static string GetSasToken(CloudStorageAccount storageAccount)
        {
            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Object,
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30),
                Protocols = SharedAccessProtocol.HttpsOnly
            };

            return storageAccount.GetSharedAccessSignature(policy);
        }

        /// <summary>
        /// Uses the MySAP Access Policy in the blob container to generate the Sas token
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static string GetSasTokenFromPolicy(CloudBlobContainer container)
        {
            return container.GetSharedAccessSignature(null, "MySAP");
        }
    }
}