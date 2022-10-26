using MergerBay.Utilities.Utilities.Apiresponse;
using MergerBay.Domain.Entities.User;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Login;
using MergerBay.Infrastructure.Interfaces.Password;
using MergerBay.Infrastructure.Interfaces.UserManagment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserManagement _UserManagement;
        private readonly ILogin _login;
        public UserManagementController(IPasswordHasher passwordHasher, IUserManagement UserManagement, ILogin login)
        {
            _passwordHasher = passwordHasher;
            _UserManagement = UserManagement;
            _login = login;
        }

        [AllowAnonymous]
        [Route("UserPersonalInformation")]
        [HttpPost]
        public async Task<IActionResult> User_Personal_Information_API([FromBody] UserSignup requestclient)
        {
            try
            {
                UserPersonalInformation request = new UserPersonalInformation();
                request.FirstName = requestclient.firstname;
                request.LastName= requestclient.lastname;
                request.Password = requestclient.password;
                request.Country = requestclient.country;
                request.PhoneNumber = requestclient.phone.ToString();
                request.Email = requestclient.email;
                if (ModelState.IsValid)
                {
                    request.HashPassword = _passwordHasher.GenerateIdentityV3Hash(request.Password);
                    var response = await _UserManagement.Add_User_Personal_Information(request);
                    return new OkObjectResult(new BaseApiModel { root = response.message, Success = true });
                    //return response;
                }
                else
                {
                    return new OkObjectResult(new BaseApiModel { Message = "Operation failed", Success = false });
                }
            }
            catch (System.Exception ex)
            {

                return new OkObjectResult(new BaseApiModel { Message = "Operation failed", Success = false });
            }

        }

        [AllowAnonymous]
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> User_Personal_Information_API([FromBody] ChangePassword requestclient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    requestclient.Password = _passwordHasher.GenerateIdentityV3Hash(requestclient.Password);
                    var response = await _UserManagement.ChangePassword(requestclient);

                    return new OkObjectResult(new BaseApiModel { root = "Password has been changed successfully", Success = true });
                    //return response;
                }
                else
                {
                    return new OkObjectResult(new BaseApiModel { Message = "Operation failed", Success = false });
                }
            }
            catch (System.Exception ex)
            {

                return new OkObjectResult(new BaseApiModel { Message = "Operation failed", Success = false });
            }

        }

        [AllowAnonymous]
        [Route("NotificationSetting")]
        [HttpPost]
        public async Task<IActionResult> Notification_Setting([FromBody] NotificationSettingModel requestclient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _UserManagement.NotificationSetting(requestclient);

                    return new OkObjectResult(new BaseApiModel { root = "Notification setting has been saved successfully", Success = true });
                    //return response;
                }
                else
                {
                    return new OkObjectResult(new BaseApiModel { Message = "Operation failed", Success = false });
                }
            }
            catch (System.Exception ex)
            {

                return new OkObjectResult(new BaseApiModel { Message = "Operation failed", Success = false });
            }

        }

        [AllowAnonymous]
        [Route("get/NotificationSetting")]
        [HttpPost]
        public async Task<NotificationSettingModel> GetNotification_Setting([FromBody] NotificationSettingModel requestclient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    requestclient = await _UserManagement.GetNotificationSetting(requestclient.User_Id);
                }
            }
            catch (System.Exception ex)
            {

                return requestclient;
            }
            return requestclient;

        }
    }
}
