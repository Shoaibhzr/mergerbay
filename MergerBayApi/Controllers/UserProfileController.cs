using Azure;
using MergerBay.Domain.Entities.Company;
using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Entities.User;
using MergerBay.Domain.Entities.UserProfile;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Blob;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBay.Infrastructure.Interfaces.UserProfile;
using MergerBay.Services.Services.Seller;
using MergerBay.Services.Services.UserManagment;
using MergerBay.Utilities.Utilities.Apiresponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Net.WebRequestMethods;


namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfile _userprofile;
        private IWebHostEnvironment _webHostEnvironment;
        private IHttpContextAccessor _contextAccessor;
        private readonly IAzureblob _blob;  
        public UserProfileController(IUserProfile userprofile, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor, IAzureblob blob)
        {
            this._userprofile = userprofile;
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
            _blob = blob;
        }

        [HttpPost("save/profile")]
        public async Task<ActionResult> SaveProfile(UserProfile profile)
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

        [HttpPost("save/companyprofile")]
        public async Task<ActionResult> companyprofile(MergerBay.Domain.Entities.UserProfile.CompanyInformation companyInformation)
        {
            try
            {
                var userProfile = await _userprofile.SaveCompanyProfile(companyInformation);
                return new OkObjectResult(new BaseApiModel { root = "Company Information has been updated successfully.", Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest("Unable To Save Data");
            }
        }

        [HttpPost("get/profile")]
        public async Task<List<UserProfile>> GetProfile([FromBody] UserKey UserInfo)
        {
            try
            {
                Guid userIdGuid = Guid.Parse(UserInfo.User_Id);   
                var userProfile = await _userprofile.GetProfile(userIdGuid);
                return userProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("get/companyprofile")]
        public async Task<List<MergerBay.Domain.Entities.UserProfile.CompanyInformation>> Getcompanyprofile([FromBody] UserKey UserInfo)
        {
            try
            {
                Guid userIdGuid = Guid.Parse(UserInfo.User_Id);
                var userProfile = await _userprofile.GetCompanyInformation(userIdGuid);
                return userProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("save/profilepic")]
        public async Task<ActionResult> ProfilePic(IFormFile? imageFile)
        {
            try
            {
                var file = Request.Form.Files[0];
                var path=Path.Combine(_webHostEnvironment.ContentRootPath,"Uploads",file.FileName);
                using (var fileStream=new FileStream(path,FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    await _blob.UploadAsyncViaStream(fileStream,file.FileName);
                }

                var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + _contextAccessor.HttpContext.Request.PathBase;
                return Ok(new
                {
                    fileName = file.FileName
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("save/cardattachments")]
        public async Task<ActionResult> CardAttachments(IFormFile? imageFile)
        {
            try
            {
                List<string> res = new List<string>();
                var files = Request.Form.Files;
                if(files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        res.Add(file.FileName);
                        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", file.FileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            await _blob.UploadAsyncViaStream(fileStream, file.FileName);
                        }
                    }

                }
                var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + _contextAccessor.HttpContext.Request.PathBase;
                return Ok(new
                {
                    fileName = res
                });

            }
            catch (Exception ex)
            {
                return BadRequest("Unable To Save Data");
            }
        }

        [HttpPost("save/certificateattachments")]
        public async Task<ActionResult> CertificateAttachments(IFormFile? imageFile)
        {
            try
            {
                List<string> res = new List<string>();
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        res.Add(file.FileName);
                        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", file.FileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            await _blob.UploadAsyncViaStream(fileStream, file.FileName);
                        }
                    }

                }
                var baseURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + _contextAccessor.HttpContext.Request.PathBase;
                return Ok(new
                {
                    fileName = res
                }); ;

            }
            catch (Exception ex)
            {
                return BadRequest("Unable To Save Data");
            }
        }


        [HttpPost("get/companycategory")]
        public async Task<List<CompanyCategory>> companycategory()
        {
            try
            {
               return  await _userprofile.GetCategories();
                  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("get/userprofile")]
        public async Task<ActionResult> userprofile(Guid userId)
        {
            try
            {
                var userProfile = await _userprofile.GetProfile(userId);
                return new OkObjectResult(new BaseApiModel { root = "success", Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest("Unable To Save Data");
            }
        }
    }
}
