using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
namespace PPTReport.clases
{
    public class clsStorage
    {
        public string descargaBlob(int id, String strFileName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=cushmanone;AccountKey=MyTHcBbNdSuzD8bCsHt3WQGsxLen/BcDFliFszo72Sflg9b2vdmXOXfAaaN/RuJfJuwOvji6ND0lRGWRXTs9dA==");
            //ConfigurationManager.("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/N" + id);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(strFileName);

            SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(12),
            };

            SharedAccessBlobHeaders headers = new SharedAccessBlobHeaders()
            {
                ContentDisposition = string.Format("attachment;filename=\"{0}\"", strFileName),
            };

            var sasToken = blockBlob.GetSharedAccessSignature(policy, headers);
            return blockBlob.Uri.AbsoluteUri + sasToken;

            // Save blob contents to a file.=
            //using (var fileStream = System.IO.File.OpenWrite(Path.Combine(Server.MapPath("~/App_Data"), strFileName)))
            //using (var fileStream = System.IO.File.OpenWrite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), strFileName)))
            //{
            //blockBlob.DownloadToStream(Response.OutputStream);
            //}
        }

        public static void uploadfile(string ruta)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=cushmanone;AccountKey=MyTHcBbNdSuzD8bCsHt3WQGsxLen/BcDFliFszo72Sflg9b2vdmXOXfAaaN/RuJfJuwOvji6ND0lRGWRXTs9dA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            /// CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("reporte.pdf");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(ruta))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}
