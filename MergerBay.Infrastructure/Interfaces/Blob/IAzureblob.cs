using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Infrastructure.Interfaces.Blob
{
    public interface IAzureblob
    {
        Task<Boolean> AzureStorage(string FileName);
        Task<bool> UploadAsyncText(string contents, string toPath);

        Task<bool> UploadAsync(string filename);
        Task<bool> UploadAsyncViaStream(Stream fileStream, string filename);
  
        Task<Stream> DownloadAsyncStream(string fileName);
        Task<String> DownloadAsyncText(string fileName);
        Task DownloadAsync(string fileName);
    }
}
