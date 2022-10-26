using MergerBay.Infrastructure.Interfaces.Blob;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsignController : ControllerBase
    {
        private readonly IAzureblob _blob;
        public EsignController(IAzureblob blob)
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
    }
}
