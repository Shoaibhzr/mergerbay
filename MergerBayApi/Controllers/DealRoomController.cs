using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.UserProfile;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBay.Utilities;
using MergerBay.Utilities.Utilities.Apiresponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealRoomController : ControllerBase
    {

        public DealRoomController()
        {

        }

        [HttpPost("save/dealroomattachments")]
        public async Task<ActionResult> dealroomattachments(UserProfile profile)
        {
            try
            {
                var userProfile = await _userprofile.SaveProfile(profile);
                return new OkObjectResult(new BaseApiModel { root = "Profile has been updated successfully.", Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest("Unable To Save Data");
            }
        }


    }
}
