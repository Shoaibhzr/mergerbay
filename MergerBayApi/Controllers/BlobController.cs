using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Blob;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBay.Services.Services.Seller;
using MergerBayApi.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IAzureblob _blob;
        public BlobController(IAzureblob blob)
        {
            _blob = blob;
        }

        [HttpPost("upload/filename")]
        public async Task<ActionResult<bool>> uploadfilename(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                return true;
            }
            else
            {
                return BadRequest("Unable To Save Data");
            }
        }

        [HttpPost("upload/filestream")]
        public async Task<ActionResult<bool>> uploadfilestream(FileStream filestream,string filename)
        {
            if (filestream != null)
            {
                var res=await _blob.UploadAsyncViaStream(filestream,filename);
                return true;
            }
            else
            {
                return BadRequest("Unable To Save Data");
            }
        }

        [HttpPost("download/filename")]
        public async Task<ActionResult<bool>> downloadfilename(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                var res = await _blob.DownloadAsyncStream(filename);
                return true;
            }
            else
            {
                return BadRequest("Unable To Save Data");
            }
        }
    }
}
