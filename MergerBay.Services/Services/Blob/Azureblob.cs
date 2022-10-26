
using Azure.Storage.Blobs;
using MergerBay.Infrastructure.Interfaces.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Services.Blob
{
    public class Azureblob:IAzureblob
    {
        private CloudBlobContainer container;

        private string containerName="dealroom";
        private string StorageConnStr;
        public Azureblob()
        {
            this.StorageConnStr = "DefaultEndpointsProtocol=https;AccountName=mergerbayblob;AccountKey=0E45tgRLNzPMqiHr0ol6AWgHtT+xeWFB3FmMLM2uuuHzQ7kEIuyC1rrSIKlFR7d15CQNN53nOWvo+ASt9ETGzw==;EndpointSuffix=core.windows.net";
            this.containerName = containerName;
        }

        private async Task CreateContainer()
        {
            try
            {
                CloudStorageAccount storAcc = CloudStorageAccount.Parse(StorageConnStr);
                CloudBlobClient blobClient = storAcc.CreateCloudBlobClient();
                container = blobClient.GetContainerReference(containerName);
                await container.CreateIfNotExistsAsync();
            }
            catch (Exception r)
            {
                throw r;
            }
        }

        public async Task<bool> UploadAsync(string filename)
        {
            bool rtn = false;
            try
            {
                await CreateContainer();
                var container = new BlobContainerClient(this.StorageConnStr, this.containerName);
                var blob = container.GetBlobClient(filename);
                var stream=File.OpenRead(filename);
                await blob.UploadAsync(stream);
                rtn = true;
            }
            catch (OperationCanceledException ex)
            {
                rtn = false;
                throw new OperationCanceledException("UploadAsync canceled");
            }
            return (rtn);
        }

        public async Task<bool> UploadAsyncText(string contents, string toPath)
        {
            bool rtn = false;
            try
            {
                await CreateContainer();
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(toPath);
                await blockBlob.UploadTextAsync(contents);
                rtn = true;
            }
            catch (OperationCanceledException ex)
            {
                rtn = false;
                throw new OperationCanceledException("UploadAsync canceled");
            }
            return (rtn);
        }

        public async Task<bool> UploadAsyncViaStream(Stream fileStream,string filename)
        {
            bool rtn = false;
            try
            {
                await CreateContainer();
                var container = new BlobContainerClient(this.StorageConnStr, this.containerName);
                var blob = container.GetBlobClient(filename);
                fileStream.Position = 0;
                var res=await blob.UploadAsync(fileStream);
            }
            catch (Exception ex)
            {
                rtn = false;
                throw ex;
            }
            return (rtn);
        }

        public async Task<Stream> DownloadAsyncStream(string fileName)
        {
            try
            {
                Stream returnStream;
                await CreateContainer();
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                using (var mStream = new MemoryStream())
                {

                    await blockBlob.DownloadToStreamAsync(mStream);
                    returnStream = mStream;
                }

                return (returnStream);
            }
            catch (OperationCanceledException ex)
            {
                throw new OperationCanceledException("DownloadAsyncStream canceled");
            }
        }

        public async Task<String> DownloadAsyncText(string fileName)
        {
            Task<String> rtn;
            try
            {
                await CreateContainer();
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                rtn = blockBlob.DownloadTextAsync();

            }
            catch (OperationCanceledException ex)
            {
                throw new OperationCanceledException("DownloadAsync canceled");
            }

            return (await rtn);

        }

        public async Task DownloadAsync(string fileName)
        {
            try
            {
                await CreateContainer();
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            }
            catch (OperationCanceledException ex)
            {
                throw new OperationCanceledException("DownloadAsync canceled");
            }
        }

        public Task<bool> AzureStorage(string FileName)
        {
            throw new NotImplementedException();
        }
    }
}