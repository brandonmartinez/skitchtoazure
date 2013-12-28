using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SkitchToAzure.Azure
{
    public class BlobClient
    {
        #region Fields

        private readonly CloudBlobContainer _container;

        #endregion

        #region Constructors

        public BlobClient(string connectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            _container = blobClient.GetContainerReference(containerName);
            _container.CreateIfNotExists();
            _container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
        }

        #endregion

        #region public

        public void Delete(string id)
        {
            var blob = _container.GetBlockBlobReference(id);
            blob.Delete();
        }

        public IEnumerable<string> Get()
        {
            var blobs = _container.ListBlobs();

            return blobs.Cast<CloudBlockBlob>().OrderByDescending(x => x.Properties.LastModified ?? new DateTimeOffset()).Select(x => x.Name);
        }

        public byte[] Get(string id)
        {
            // Get blob reference
            var blob = _container.GetBlockBlobReference(id);

            // Download to stream
            var memoryStream = new MemoryStream();
            blob.DownloadToStream(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Return
            return memoryStream.ToArray();
        }

        public void Upload(string id, Stream stream)
        {
            // Create a block blob reference
            var blob = _container.GetBlockBlobReference(id);

            // Upload stream to blob
            blob.UploadFromStream(stream);
        }

        #endregion
    }
}