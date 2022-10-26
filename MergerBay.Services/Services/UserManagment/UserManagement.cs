

using MergerBay.Utilities.Services.SMTP_Services;
using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.Login;
using MergerBay.Domain.Entities.User;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Token;
using MergerBay.Infrastructure.Interfaces.UserManagment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MergerBay.Domain.Entities.Setups;

namespace MergerBay.Services.Services.UserManagment
{
    public class UserManagement : IUserManagement
    {
        private readonly MergerBayContext _context;
        private readonly IToken _tokenService;
        public UserManagement(IToken tokenService, MergerBayContext context)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<UserSignup> Add_User_Personal_Information(UserPersonalInformation model)
        {
            try
            {
                UserSignup u = new UserSignup();
                var IsExist = _context.UserPersonalInformation.Where(f => f.User_Joined_Email == model.Email && f.Is_Deleted==false && f.Is_Active==true).ToList();
                if(IsExist.Count==0)
                {
                    Tbl_User_Management tu = new Tbl_User_Management();
                    tu.UserId = Guid.NewGuid();
                    tu.User_First_Name = model.FirstName;
                    tu.User_Last_Name = model.LastName;
                    tu.User_Joined_Email = model.Email;
                    tu.User_Contact_Number = model.PhoneNumber;
                    tu.Password = model.HashPassword;
                    tu.City = model.City;
                    tu.Country = model.Country;
                    tu.TimeZone = "not recorded";
                    tu.Business_Name = "not recorded";
                    tu.Business_Email = "not recorded";
                    tu.Address = "not recorded";
                    tu.Business_Phone_Number = "not recorded";
                    tu.City = "not recorded";
                    tu.State = "not recorded";
                    tu.Zip = "not recorded";
                    tu.Designation = "not recorded";
                    tu.Website = "not recorded";
                    tu.Designation = "not recorded";
                    tu.Is_Deleted = false;
                    tu.Is_Active = true;
                    tu.Is_Company_Account = true;
                    tu.Created_Date = DateTime.UtcNow;
                    _context.UserPersonalInformation.Add(tu);
                    _context.SaveChanges();
                    u.message = "User has been registered successfully";
                }
                else
                {
                    u.message = "User already exists";
                }
              
                return u;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChangePassword> ChangePassword(ChangePassword model)
        {
            try
            {
                Guid userKey = Guid.Parse(model.User_Id);
                var IsExist = _context.UserPersonalInformation.FirstOrDefault(f => f.UserId == userKey && f.Is_Deleted == false && f.Is_Active == true);
                if (IsExist !=null)
                {
                    IsExist.Password = model.Password;
                    _context.SaveChanges();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> NotificationSetting(NotificationSettingModel model)
        {
            try
            {
                Guid UserKey = Guid.Parse(model.User_Id);
                var ExistingUser = _context.NotificationSetting.FirstOrDefault(u => u.User_Id == UserKey);
                if(ExistingUser != null)
                {
                    ExistingUser.Pending_Action_MergerBay = model.IsPendingAction;
                    ExistingUser.Feature_Requirements = model.IsFeatureRequirements;
                   await _context.SaveChangesAsync();
                }
                else
                {
                    NotificationSetting ns = new NotificationSetting();
                    ns.User_Id = Guid.Parse(model.User_Id);
                    ns.Notification_Setting_Id = Guid.NewGuid();
                    ns.Pending_Action_MergerBay = model.IsPendingAction;
                    ns.Feature_Requirements = model.IsFeatureRequirements;
                    _context.NotificationSetting.Add(ns);
                   await _context.SaveChangesAsync();
                }
               
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NotificationSettingModel> GetNotificationSetting(string User_Id)
        {
            try
            {
                NotificationSettingModel ns = new NotificationSettingModel();
                Guid UserKey = Guid.Parse(User_Id);
                var ExistingUser = _context.NotificationSetting.FirstOrDefault(u => u.User_Id == UserKey);
                if (ExistingUser != null)
                {
                   ns.IsPendingAction = ExistingUser.Pending_Action_MergerBay;
                    ns.IsFeatureRequirements = ExistingUser.Feature_Requirements;
                }
                return ns;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string Email_Body_For_Email_Verification(string Name, string Url)
        {
            string body;
            StringBuilder sb = new StringBuilder();
            body = sb.Append("<h4>Hello, " + Name + ",</h4>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<p>You are receiving this email as you signed up for Meggafone, or an admin on Meggafone has invited you to the app. To confirm your email address or invite, you'll need to click on the following link and complete your sign up:</p>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<h4>Please Click <a href='" + Url + "' target='_blank'>Here</a> for the email verification</h4>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<p>If you need help, please see our FAQs and Help Section at:<br/>" +
                "<a href='https://www.meggafone.com/support' target='_blank'>https://www.meggafone.com/support</a><br/><br/>" +
                "Sincerely,<br/>" +
                "Meggafone<br/>---------------------------------------------------------------------<br/>" +
                "Please do not reply to this email. This mailbox is not monitored and you will not receive a response.<br/>" +
                "---------------------------------------------------------------------<br/>" +
                "PROTECT YOUR PASSWORD<br/>NEVER give your password to anyone. Protect yourself against fraudulent websites by opening a new web browser and typing in the Meggafone URL every time you log in to your account.<br/>" +
                "---------------------------------------------------------------------</p>").ToString();
            return body;
        }
        public async Task<Email_Confirmed_Response> Confirm_Email(ConfirmEmail request)
        {
            try
            {
                Email_Confirmed_Response Response_Obj = new Email_Confirmed_Response();
                using (SqlCommand comm = new SqlCommand())
                {
                    var Confirmation_Email = new SqlParameter("@Confirmation_Email", request.Confirmation_Email);
                    var Token = new SqlParameter("@Token", request.Token);
                    var resp = await _context.Set<Email_Confirmed>().FromSqlInterpolated($"EXEC sp_Check_For_Email_Verification {Confirmation_Email},{Token} ").ToListAsync();
                    if (resp.Count > 0)
                    {
                        if (!resp[0].Is_Active)
                        {

                            int response = await _context.Database.ExecuteSqlRawAsync($"EXEC sp_confirmed_email @Token", Token);
                            if (response > 0)
                            {
                                Response_Obj.Is_Active = true;
                                Response_Obj.Response_Message = "Email Verified Successfully";
                                return Response_Obj;
                            }
                            else
                            {
                                Response_Obj.Is_Active = false;
                                Response_Obj.Response_Message = "Email is not verified. Please contact to support";
                                return Response_Obj;
                            }
                        }
                        else
                        {
                            Response_Obj.Is_Active = false;
                            Response_Obj.Response_Message = "Email activation link is expired";
                            return Response_Obj;
                        }
                    }
                    else
                    {
                        Response_Obj.Is_Active = false;
                        Response_Obj.Response_Message = "Invalid email address";
                        return Response_Obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CompanyInformation> Get_Company_Information(long Company_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var CompanyID = new SqlParameter("@Company_ID", Company_ID);
                    List<CompanyInformation> CompanyInformationResponse = await _context.Set<CompanyInformation>().FromSqlInterpolated($"EXEC sp_Settings_Get_Company_Information {CompanyID}").ToListAsync();
                    return CompanyInformationResponse[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update_Company_Information(CompanyInformation request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var CompanyID = new SqlParameter("@Company_ID", request.Company_ID);
                    var CompanyName = new SqlParameter("@Company_Name", request.Company_Name);
                    var Address = new SqlParameter("@Address", request.Address);
                    var Address1 = new SqlParameter("@Address1", request.Address1 ?? (object)DBNull.Value);
                    var EmailAddress = new SqlParameter("@Email_Address", request.Email_Address);
                    var ContactNumber = new SqlParameter("@Contact_Number", request.Contact_Number);
                    var ModifiedBy = new SqlParameter("@Modified_By", request.Modified_By);
                    var City = new SqlParameter("@City", request.City);
                    var State = new SqlParameter("@State", request.State);
                    var Zip = new SqlParameter("@Zip", request.Zip);
                    var Attachment_Path = new SqlParameter("@Attachment_Path", request.Attachment_Path ?? (object)DBNull.Value);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC sp_Settings_Update_Company_Information @Company_ID,@Company_Name,@Address,@Address1,@Email_Address,@Contact_Number,@Modified_By,@City,@State,@Zip,@Attachment_Path",
                        CompanyID, CompanyName, Address, Address1, EmailAddress, ContactNumber, ModifiedBy, City, State, Zip, Attachment_Path);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<ConnectedChannels>> Get_Connected_Channels(long Company_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var CompanyID = new SqlParameter("@Company_ID", Company_ID);
                    List<ConnectedChannels> getConnectedChannels = await _context.Set<ConnectedChannels>().FromSqlInterpolated($"EXEC Sp_Settings_Get_Connected_Channels {CompanyID}").ToListAsync();
                    return getConnectedChannels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AllCompanyActiveUsers>> Get_All_Active_Company_Users(long Company_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var CompanyID = new SqlParameter("@Company_ID", Company_ID);
                    List<AllCompanyActiveUsers> getAllCompanyActiveUsers = await _context.Set<AllCompanyActiveUsers>().FromSqlInterpolated($"EXEC Sp_Settings_Get_All_Company_Active_Users {CompanyID}").ToListAsync();
                    if (getAllCompanyActiveUsers.Count > 0)
                    {
                        getAllCompanyActiveUsers[0].Is_Super_admin = true;
                        return getAllCompanyActiveUsers;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GetPersonalInformationForEdition> Get_Personal_Information(long User_ID, long Company_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    GetPersonalInformationForEdition GetPersonalInformation = new GetPersonalInformationForEdition();
                    var UserID = new SqlParameter("@User_ID", User_ID);
                    var CompanyID = new SqlParameter("@Company_ID", Company_ID);
                    List<GetPersonalInformation> getPersonalInformation = await _context.Set<GetPersonalInformation>().FromSqlInterpolated($"EXEC Sp_Settings_Get_User_Personal_Information {UserID}").ToListAsync();
                    List<GetUsersConnectedChannels> getAssignedChannels = await _context.Set<GetUsersConnectedChannels>().FromSqlInterpolated($"EXEC Sp_Get_Users_connected_Channels {UserID},{CompanyID}").ToListAsync();
                    List<Get_All_Active_Teams_And_Its_Members> GetAllActiveTeamsAndItsMembers = await _context.Set<Get_All_Active_Teams_And_Its_Members>().FromSqlInterpolated($"EXEC SP_Settings_Get_All_Teams_With_User_Assignmet_new {UserID}").ToListAsync();
                    GetPersonalInformation.GetPersonalInformation = getPersonalInformation[0];
                    GetPersonalInformation.UserChannels = getAssignedChannels;
                    GetPersonalInformation.AssigedTeams = GetAllActiveTeamsAndItsMembers;
                    return GetPersonalInformation;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update_Personal_Information(UpdatePersonalInformation request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var UserID = new SqlParameter("@User_ID", request.User_ID);
                    var FirstName = new SqlParameter("@User_First_Name", request.User_First_Name);
                    var LastName = new SqlParameter("@User_last_Name", request.User_last_Name);
                    var PhoneNumber = new SqlParameter("@PhoneNumber", request.PhoneNumber);
                    var NewPassword = new SqlParameter("@New_Password", request.New_Password ?? (object)DBNull.Value);
                    var User_Time = new SqlParameter("@User_Time_Zone", request.User_Time);
                    var Attachment_Path = new SqlParameter("@Attachment_Path", request.Attachment_Path ?? (object)DBNull.Value);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC sp_Settings_Update_Personal_Information @User_ID,@User_First_Name,@User_last_Name,@PhoneNumber,@New_Password,@User_Time_Zone,@Attachment_Path",
                        UserID, FirstName, LastName, PhoneNumber, NewPassword, User_Time, Attachment_Path);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GetUserNotificatipnsSettings> Get_Setting_Notifications(long User_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var UserID = new SqlParameter("@User_ID", User_ID);
                    List<GetUserNotificatipnsSettings> getUserNotificatipnsSettings = await _context.Set<GetUserNotificatipnsSettings>().FromSqlInterpolated($"EXEC Sp_Settings_Get_Notifications {UserID}").ToListAsync();
                    if (getUserNotificatipnsSettings.Count > 0)
                        return getUserNotificatipnsSettings[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Get_Setting_Notifications(GetUserNotificatipnsSettings request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    var Notification_Settings_ID = new SqlParameter("@Notification_Settings_ID", request.Notification_Settings_ID);
                    var UserID = new SqlParameter("@User_ID", request.User_ID);
                    var Task = new SqlParameter("@Task", request.Task);
                    var Keywords = new SqlParameter("@Keywords", request.Keywords);
                    var Users = new SqlParameter("@Users", request.Users);
                    var Approvals = new SqlParameter("@Approvals", request.Approvals);
                    var Archived_Delete_Years_Count = new SqlParameter("@Archived_Delete_Years_Count", request.Archived_Delete_Years_Count);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC sp_Settings_Add_Users_Notifiactions @User_ID,@Task,@Keywords,@Users,@Approvals,@Archived_Delete_Years_Count,@Notification_Settings_ID",
                        UserID, Task, Keywords, Users, Approvals, Archived_Delete_Years_Count, Notification_Settings_ID);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Check_For_Valid_Email(string request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Email = new SqlParameter("@Email", request.Trim());
                    List<Email_Check> CheckEmailAlreadyExits = await _context.Set<Email_Check>().FromSqlInterpolated($"EXEC Sp_Check_Email_Already_Exists {Email}").ToListAsync();
                    if (CheckEmailAlreadyExits.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Reset_Password_Generate_link(Reset_Password request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Email = new SqlParameter("@Email", request.UserEmail);
                    List<Email_Check> CheckEmailAlreadyExits = await _context.Set<Email_Check>().FromSqlInterpolated($"EXEC Sp_Check_Email_Already_Exists {Email}").ToListAsync();
                    if (CheckEmailAlreadyExits.Count > 0)
                    {
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, request.UserEmail)
                        };
                        var new_token = _tokenService.GenerateAccessToken(claims);
                        var Token = new SqlParameter("@TOKEN", new_token);
                        var EMAIL = new SqlParameter("@EMAIL", request.UserEmail);
                        int Token_Updation = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_TOKEN_FOR_ACCOUNT_VERIFICATION @TOKEN,@EMAIL", Token, EMAIL);

                        //sendEmail(new_token);
                        request.EmailURL = request.EmailURL + "user/ForgetPassword?Email=" + request.UserEmail + "&Token=" + new_token;
                        EmailServices.Send_Email(request.UserEmail, "Reset Password", Email_Body_For_Reset_Password(CheckEmailAlreadyExits[0].User_Full_Name, request.EmailURL));
                        //EmailServices.sendEmail(Email_Body(model.EmailUrl,new_token), model.Email);
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string Email_Body_For_Reset_Password(string Name, string Url)
        {
            string body;
            StringBuilder sb = new StringBuilder();
            body = sb.Append("<h4>Hello " + Name + ",</h4>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<p>You'll need to click on the following link and complete your reset password process:</p>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<h4>Please Click <a href='" + Url + "' target='_blank'>Here</a> for your reset password</h4>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<p>If you need help, please see our FAQs and Help Section at:<br/>" +
                "<a href='https://www.meggafone.com/support' target='_blank'>https://www.meggafone.com/support</a><br/>" +
                "Sincerely,<br/>" +
                "Meggafone<br/>" +
                "---------------------------------------------------------------------<br/>" +
                "Please do not reply to this email. This mailbox is not monitored and you will not receive a response.<br/>" +
                "---------------------------------------------------------------------<br/>" +
                "PROTECT YOUR PASSWORD<br/>" +
                "NEVER give your password to anyone. Protect yourself against fraudulent websites by opening a new web browser and typing in the Meggafone URL every time you log in to your account.<br/>" +
                "---------------------------------------------------------------------</p>").ToString();
            return body;
        }
        public async Task<bool> Check_For_Valid_Reset_Password_Link(ConfirmEmail request)
        {
            try
            {
                Email_Confirmed_Response Response_Obj = new Email_Confirmed_Response();
                using (SqlCommand comm = new SqlCommand())
                {
                    var Confirmation_Email = new SqlParameter("@Confirmation_Email", request.Confirmation_Email);
                    var Token = new SqlParameter("@Token", request.Token);
                    var response = await _context.Set<Email_Confirmed>().FromSqlInterpolated($"EXEC sp_Check_For_Reset_Password {Confirmation_Email},{Token}").ToListAsync();
                    if (response.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Reset_Password(Reset_Password_After_Verification request)
        {
            try
            {
                Email_Confirmed_Response Response_Obj = new Email_Confirmed_Response();
                using (SqlCommand comm = new SqlCommand())
                {
                    var Password = new SqlParameter("@Password", request.Password);
                    var Email = new SqlParameter("@EMAIL", request.UserEmail);
                    var response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_USER_PASSWORD @Password,@EMAIL", Password, Email);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Get_All_Active_Teams_And_Its_Members>> Get_User_Assiged_And_All_Company_Active_Team(long User_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var UserID = new SqlParameter("@User_ID", User_ID);
                    List<Get_All_Active_Teams_And_Its_Members> getAllActiveTeamsAndItsMembers = await _context.Set<Get_All_Active_Teams_And_Its_Members>().FromSqlInterpolated($"EXEC SP_Settings_Get_All_Teams_With_User_Assignmet {UserID}").ToListAsync();
                    if (getAllActiveTeamsAndItsMembers.Count > 0)
                        return getAllActiveTeamsAndItsMembers;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Get_All_Active_Teams_And_Its_Members>> Get_User_Assiged_And_All_Company_Active_Team_New(long User_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var UserID = new SqlParameter("@User_ID", User_ID);
                    List<Get_All_Active_Teams_And_Its_Members> getAllActiveTeamsAndItsMembers = await _context.Set<Get_All_Active_Teams_And_Its_Members>().FromSqlInterpolated($"EXEC SP_Settings_Get_All_Teams_With_User_Assignmet_New {UserID}").ToListAsync();
                    if (getAllActiveTeamsAndItsMembers.Count > 0)
                        return getAllActiveTeamsAndItsMembers;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Save_Team_Assignment(List<Team_Assignment_Request> request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var response = 0;
                    for (int i = 0; i < request.Count; i++)
                    {
                        var Users_Team_ID = new SqlParameter("@Users_Team_ID", request[i].users_Team_ID);
                        var Is_Assigned = new SqlParameter("@Is_Assigned", !request[i].Is_Assigned);
                        var Team_ID = new SqlParameter("@Team_ID", request[i].Team_ID);
                        var User_ID = new SqlParameter("@User_ID", request[0].User_ID);
                        var Assigned_User_ID = new SqlParameter("@Assigned_User_ID", request[0].Selected_User_ID_Team_Assignment);
                        response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_TEAM_ASSIGNMENT @Users_Team_ID,@Is_Assigned,@Team_ID,@User_ID,@Assigned_User_ID",
                            Users_Team_ID, Is_Assigned, Team_ID, User_ID, Assigned_User_ID);
                    }
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public async Task<Publishing_Queue_Details> Get_Publishing_Queue_Details(long Channel_ID)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var ChannelID = new SqlParameter("@Channel_ID", Channel_ID);
                    List<Publishing_Queue_Details> getPublishingQueueDetails = await _context.Set<Publishing_Queue_Details>().FromSqlInterpolated($"EXEC SP_Get_Publishing_Queue_Details {ChannelID}").ToListAsync();
                    if (getPublishingQueueDetails.Count > 0)
                        return getPublishingQueueDetails[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Publishing_Queue_Details> Get_Publishing_Queue_Details_Post(Publishing_Queue_Details_Request model)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var ChannelID = new SqlParameter("@Channel_ID", model.Channel_ID);
                    var timezone = new SqlParameter("@timezone", model.timezone);
                    List<Publishing_Queue_Details> getPublishingQueueDetails = await _context.Set<Publishing_Queue_Details>().FromSqlInterpolated($"EXEC SP_Get_Publishing_Queue_Details_Post {ChannelID},{timezone}").ToListAsync();
                    if (getPublishingQueueDetails.Count > 0)
                        return getPublishingQueueDetails[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Publishing_Queue_Details> Get_Publishing_Queue_Details_Post_Queue(Publishing_Queue_Details_Request model)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var ChannelID = new SqlParameter("@Channel_ID", model.Channel_ID);
                    var timezone = new SqlParameter("@timezone", model.timezone);
                    var schdate = new SqlParameter("@start_date", model.schdate);
                    List<Publishing_Queue_Details> getPublishingQueueDetails = await _context.Set<Publishing_Queue_Details>().FromSqlInterpolated($"EXEC SP_Get_Publishing_Queue_Details_Post_new {ChannelID},{timezone},{schdate}").ToListAsync();
                    if (getPublishingQueueDetails.Count > 0)
                        return getPublishingQueueDetails[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<string>> Invited_People(List<Invited_People_Email> Request)
        {
            try
            {
                // Encryption e = new Encryption();
                // string EncryptionKey = Encryption.GenerateEncryptionKey();
                List<string> ExistEmails = new List<string>();
                using (SqlCommand comm = new SqlCommand())
                {

                    for (int i = 0; i < Request.Count; i++)
                    {
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, Request[i].Email)
                        };

                        var new_token = _tokenService.GenerateAccessToken(claims);
                        Request[i].EmailUrl = Request[i].EmailUrl + "user/invitedUser?Email=" + Request[i].Email + "&Token=" + new_token;

                        var Token = new SqlParameter("@TOKEN", new_token);
                        var EMAIL = new SqlParameter("@EMAIL", Request[i].Email.Trim());
                        var Invited_By = new SqlParameter("@Invited_By", Request[i].User_ID);
                        var Company_ID = new SqlParameter("@Company_ID", Request[i].Company_ID);
                        int Token_Updation = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_Add_TOKEN_And_Details_FOR_ACCOUNT_VERIFICATION @TOKEN,@EMAIL,@Invited_By,@Company_ID",
                           Token, EMAIL, Invited_By, Company_ID);
                       // if (Token_Updation != 0)
                        if (Token_Updation == 2)
                        {
                            EmailServices.Send_Email(Request[i].Email, "Invitation for Meggafone", Email_Body_For_Invited_User(Request[i].EmailUrl));
                        }
                        else
                        {
                            ExistEmails.Add(Request[i].Email.Trim());

                        }
                        //sendEmail(new_token);

                    }
                    //EmailServices.sendEmail(Email_Body(model.EmailUrl,new_token), model.Email);
                    return ExistEmails;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string Email_Body_For_Invited_User(string Url)
        {
            string body;
            StringBuilder sb = new StringBuilder();
            body = sb.Append("<h4>Hello,</h4>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<p>You are receiving this email as you signed up for Meggafone, or an admin on Meggafone has invited you to the app. To confirm your email address or invite, you'll need to click on the following link and complete your sign up:</p>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<h4>Please Click <a href='" + Url + "' target='_blank'>Here</a> for the email verification</h4>").ToString();
            sb = new StringBuilder();
            body = body + sb.Append("<p>If you need help, please see our FAQs and Help Section at:<br/>" +
                "<a href='https://www.meggafone.com/support' target='_blank'>https://www.meggafone.com/support</a><br/><br/>" +
                "Sincerely,<br/>" +
                "Meggafone<br/>---------------------------------------------------------------------<br/>" +
                "Please do not reply to this email. This mailbox is not monitored and you will not receive a response.<br/>" +
                "---------------------------------------------------------------------<br/>" +
                "PROTECT YOUR PASSWORD<br/>NEVER give your password to anyone. Protect yourself against fraudulent websites by opening a new web browser and typing in the Meggafone URL every time you log in to your account.<br/>" +
                "---------------------------------------------------------------------</p>").ToString();
            return body;
        }
        public async Task<Invited_User_ID> Invited_User_Verification(ConfirmEmail request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Confirmation_Email = new SqlParameter("@Confirmation_Email", request.Confirmation_Email);
                    var Token = new SqlParameter("@Token", request.Token);
                    List<Invited_User_ID> getInvitedUserID = await _context.Set<Invited_User_ID>().FromSqlInterpolated($"EXEC SP_Check_Invitation_link_V1 {Confirmation_Email},{Token}").ToListAsync();
                    if (getInvitedUserID.Count > 0)
                        return getInvitedUserID[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> User_Personal_Information_Invited_People(UserPersonalInformation request)
        {
            try
            {
                // Encryption e = new Encryption();
                // string EncryptionKey = Encryption.GenerateEncryptionKey();
                using (SqlCommand comm = new SqlCommand())
                {
                    var UserID = new SqlParameter("@User_ID", request.Invited_User_ID);
                    var FirstName = new SqlParameter("@User_First_Name", request.FirstName);
                    var LastName = new SqlParameter("@User_last_Name", request.LastName);
                    var UserEmail = new SqlParameter("@User_Email", request.Email.Trim());
                    var PhoneNumber = new SqlParameter("@PhoneNumber", request.PhoneNumber);
                    var NewPassword = new SqlParameter("@New_Password", request.HashPassword);
                    var TimeZone = new SqlParameter("@TimeZone", request.TimeZone);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC sp_Settings_Update_Personal_Information_Invited_User @User_ID,@User_First_Name,@User_last_Name,@User_Email,@PhoneNumber,@New_Password,@TimeZone",
                        UserID, FirstName, LastName, UserEmail, PhoneNumber, NewPassword, TimeZone);

                    if (response > 0)
                    {
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, request.Email)
                        };
                        var new_token = _tokenService.GenerateAccessToken(claims);
                        var Token = new SqlParameter("@TOKEN", new_token);
                        var EMAIL = new SqlParameter("@EMAIL", request.Email);
                        int Token_Updation = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_TOKEN_FOR_ACCOUNT_VERIFICATION @TOKEN,@EMAIL", Token, EMAIL);
                        //sendEmail(new_token);
                        request.EmailUrl = request.EmailUrl + "user/ConfirmEmail?Email=" + request.Email + "&Token=" + new_token;
                        EmailServices.Send_Email(request.Email, "Email Confirmation for Meggafone", Email_Body_For_Email_Verification(request.FirstName + " " + request.LastName, request.EmailUrl));
                        //EmailServices.sendEmail(Email_Body(model.EmailUrl,new_token), model.Email);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Disabled_User(Deleted_User request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Deleted_User_ID = new SqlParameter("@Deleted_User_ID", request.Deleted_User_ID);
                    var User_ID = new SqlParameter("@User_ID", request.User_ID);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_Disabled_User @Deleted_User_ID,@User_ID", Deleted_User_ID, User_ID);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<GET_ACTIVE_COMPANY_TEAMS_AND_MEMBERS>> Get_All_Company_Teams(GetPersonal_Information request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var COMPANY_ID = new SqlParameter("@COMPANY_ID", request.Company_ID);
                    var User_ID = new SqlParameter("@User_ID", request.User_ID);
                    List<GET_ACTIVE_COMPANY_TEAMS_AND_MEMBERS> getActiveCompanyTeamsAndMembers = await _context.Set<GET_ACTIVE_COMPANY_TEAMS_AND_MEMBERS>().FromSqlInterpolated($"EXEC SP_GET_ACTIVE_COMPANY_TEAMS_AND_MEMBERS_V1 {COMPANY_ID},{User_ID}").ToListAsync();
                    if (getActiveCompanyTeamsAndMembers.Count > 0)
                        return getActiveCompanyTeamsAndMembers;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Create_Team(Create_Team_And_Team_Members request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    int response = 0;
                    var COMPANY_ID = new SqlParameter("@COMPANY_ID", request.Company_ID);
                    var USER_ID = new SqlParameter("@USER_ID", request.User_ID);
                    var TEAM_NAME = new SqlParameter("@TEAM_NAME", request.Team_Name);
                    var Background_Color = new SqlParameter("@Background_Color", request.Background_Color);

                    List<Last_Inserted_Team_ID> getLastInsertedTeamID = await _context.Set<Last_Inserted_Team_ID>().FromSqlInterpolated($"EXEC SP_CREATE_TEAMS {COMPANY_ID},{USER_ID},{TEAM_NAME},{Background_Color}").ToListAsync();
                    if (getLastInsertedTeamID.Count > 0)
                        response = 1;
                    if (request.Assigned_Team_To_User.Count > 0)
                    {
                        for (int i = 0; i < request.Assigned_Team_To_User.Count; i++)
                        {
                            var User_ID = new SqlParameter("@User_ID", request.User_ID);
                            var TEAM_ID = new SqlParameter("@TEAM_ID", getLastInsertedTeamID[0].TEAM_ID);
                            var Assigned_User_ID = new SqlParameter("@Assigned_User_ID", request.Assigned_Team_To_User[i].Assigned_User_ID);
                            response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_TEAMS_ASSIGNMENTS @USER_ID,@Team_ID,@Assigned_User_ID",
                                USER_ID, TEAM_ID, Assigned_User_ID);
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Get_All_Users_With_Respect_To_Team>> Get_All_Users_With_Respect_To_Team(TeamUsersRequestModel request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Team_ID = new SqlParameter("@Team_ID", request.TeamId);
                    var User_ID = new SqlParameter("@User_ID", request.UserId);
                    List<Get_All_Users_With_Respect_To_Team> GetAllUsersWithRespectToTeam = await _context.Set<Get_All_Users_With_Respect_To_Team>().FromSqlInterpolated($"EXEC SP_GET_ALL_USERS_WITH_TEAM_ASSIGNMENT_CHECK {Team_ID},{User_ID }").ToListAsync();
                    if (GetAllUsersWithRespectToTeam.Count > 0)
                        return GetAllUsersWithRespectToTeam;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Save_Team_Assigment_To_Multiple_Users(List<All_Users_With_Team_Assignment> request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    for (int i = 0; i < request.Count; i++)
                    {
                        var User_ID = new SqlParameter("@User_ID", request[i].User_ID);
                        var Team_ID = new SqlParameter("@Team_ID", request[i].Team_ID);
                        var Modified_By = new SqlParameter("@Modified_By", request[i].Modified_By);
                        var Is_Assigned = new SqlParameter("@Is_Assigned", !request[i].Is_Assigned);
                        int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_AND_INSERT_TEAM_ASSIGNMENT @User_ID,@Team_ID,@Modified_By,@Is_Assigned",
                            User_ID, Team_ID, Modified_By, Is_Assigned);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> Delete_Teams(All_Users_With_Team_Assignment request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var User_ID = new SqlParameter("@User_ID", request.User_ID);
                    var Team_ID = new SqlParameter("@Team_ID", request.Team_ID);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_DELETE_TEAM @User_ID,@Team_ID",
                        User_ID, Team_ID);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> Update_Team(Update_Team_And_Team_Members request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    int response = 0;
                    var COMPANY_ID = new SqlParameter("@COMPANY_ID", request.Company_ID);
                    var USER_ID = new SqlParameter("@USER_ID", request.User_ID);
                    var TEAM_NAME = new SqlParameter("@TEAM_NAME", request.Team_Name);
                    var Team_ID = new SqlParameter("@Team_ID", request.Team_ID);
                    response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_Update_TEAM @COMPANY_ID,@USER_ID,@Team_ID,@TEAM_NAME",
                               COMPANY_ID, USER_ID, Team_ID, TEAM_NAME);
                    if (response > 0)
                        response = 1;
                    if (request.Assigned_Team_To_User.Count > 0)
                    {
                        for (int i = 0; i < request.Assigned_Team_To_User.Count; i++)
                        {
                            var User_ID = new SqlParameter("@User_ID", request.Assigned_Team_To_User[i].User_ID);
                            var TeamID = new SqlParameter("@Team_ID", request.Assigned_Team_To_User[i].Team_ID);
                            var Modified_By = new SqlParameter("@Modified_By", request.Assigned_Team_To_User[i].Modified_By);
                            var Is_Assigned = new SqlParameter("@Is_Assigned", !request.Assigned_Team_To_User[i].Is_Assigned);
                            response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_AND_INSERT_TEAM_ASSIGNMENT @User_ID,@Team_ID,@Modified_By,@Is_Assigned",
                                User_ID, TeamID, Modified_By, Is_Assigned);
                        }
                    }
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<GetUserAssignedChannels>> Get_User_Connected_Channels_And_Details(Update_Team_And_Team_Members request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var User_ID = new SqlParameter("@User_ID", request.User_ID);
                    var Company_ID = new SqlParameter("@Company_ID", request.Company_ID);
                    List<GetUserAssignedChannels> GetUserAssignedChannels = await _context.Set<GetUserAssignedChannels>().FromSqlInterpolated($"EXEC sp_get_users_assigned_channels {User_ID},{Company_ID}").ToListAsync();
                    if (GetUserAssignedChannels.Count > 0)
                        return GetUserAssignedChannels;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<GET_ACTIVE_COMPANY_TEAMS_AND_With_Channels_Asssigned>> Get_All_Company_Teams_with_Channel_Assignment(Assigned_Teams_To_Channels request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Company_ID = new SqlParameter("@COMPANY_ID", request.Company_ID);
                    var Channel_ID = new SqlParameter("@CHANNEL_ID", request.Channel_ID);

                    List<GET_ACTIVE_COMPANY_TEAMS_AND_With_Channels_Asssigned> getActiveCompanyTeamsAndMembers = await _context.Set<GET_ACTIVE_COMPANY_TEAMS_AND_With_Channels_Asssigned>().FromSqlInterpolated($"EXEC SP_GET_ACTIVE_COMPANY_TEAMS_AND_ASSIGNED_CHANNELS {Company_ID},{Channel_ID}").ToListAsync();
                    if (getActiveCompanyTeamsAndMembers.Count > 0)
                        return getActiveCompanyTeamsAndMembers;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Save_Team_Assigment_To_Channels(List<Update_Team_To_Channels> request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    for (int i = 0; i < request.Count; i++)
                    {
                        var Team_ID = new SqlParameter("@Team_ID", request[i].Team_ID);
                        var Channel_ID = new SqlParameter("@Channel_ID", request[i].Channel_ID);
                        var Modified_By = new SqlParameter("@Modified_By", request[i].Modified_By);
                        var Is_Assigned = new SqlParameter("@Is_Assigned", !request[i].Is_Assigned);
                        int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_AND_INSERT_TEAM_ASSIGNMENT_TO_CHANNELS @Team_ID,@Channel_ID,@Modified_By,@Is_Assigned",
                            Team_ID, Channel_ID, Modified_By, Is_Assigned);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<GetChannelspublishingQueue>> Get_Connected_Channesl_Publishing_Queue(Update_Team_And_Team_Members request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var User_ID = new SqlParameter("@User_ID", request.User_ID);
                    var Company_ID = new SqlParameter("@Company_ID", request.Company_ID);
                    List<GetChannelspublishingQueue> getChannelspublishingQueue = await _context.Set<GetChannelspublishingQueue>().FromSqlInterpolated($"EXEC SP_Get_Channesl_Publishing_Queue {User_ID},{Company_ID}").ToListAsync();
                    if (getChannelspublishingQueue.Count > 0)
                        return getChannelspublishingQueue;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Edit_Personal_Inforamtion(UpdateUserInformation request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var User_ID = new SqlParameter("@User_ID", request.User_ID);
                    var Modified_By = new SqlParameter("@Modified_By", request.Modified_By);
                    var Role_ID = new SqlParameter("@Role_ID", request.Role_ID);
                    var Is_Active = new SqlParameter("@Is_Active", request.Is_Active);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_PERSONAL_INFORMATION @User_ID,@Modified_By,@Role_ID,@Is_Active",
                        User_ID, Modified_By, Role_ID, Is_Active);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PackageLimitsResponse> GetPackageRemainingLimits(PackageSettingRequest request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Company_Id = new SqlParameter("@Channel_Id", request.CompanyId);
                    List<PackageLimitsResponse> CompanyLimits = await _context.Set<PackageLimitsResponse>().FromSqlInterpolated($"EXEC SP_GET_PACKGE_REMAINING_LIMITS {Company_Id}").ToListAsync();
                    User_Information_And_Permission response = new User_Information_And_Permission();
                    List<GetUsersConnectedChannelsForFeeds> getAssignedChannels = new List<GetUsersConnectedChannelsForFeeds>();
                    var User_ID = new SqlParameter("@User_ID", request.UserId);
                    //AllChannelsDetails.AllChannels = AllChannels;
                    getAssignedChannels = await _context.Set<GetUsersConnectedChannelsForFeeds>().FromSqlInterpolated($"EXEC Sp_Get_Users_connected_Channels_For_Feeds {User_ID}").ToListAsync();
                    GetAllUsersConnectedChannelsForFeeds.GetAssignedChannels = getAssignedChannels;
                    GetAllUsersConnectedChannelsForFeeds.Company_ID = (long)request.CompanyId;
                    if (CompanyLimits.Count > 0)

                        return CompanyLimits[0];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<PackageDetails>> GetAllPackageDetails()
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    List<PackageDetails> PackageDetails = await _context.Set<PackageDetails>().FromSqlInterpolated($"EXEC SP_GET_ALL_PACKAGE_DETAILS ").ToListAsync();
                    if (PackageDetails.Count > 0)
                        return PackageDetails;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BillingInformation> Billing_Inforamtion(long request)
        {
            try
            {
                BillingInformation billingInformation = new BillingInformation();
                var Company_ID = new SqlParameter("@Company_ID", request);
                List<PackageLimitsResponse> CompanyLimits = await _context.Set<PackageLimitsResponse>().FromSqlInterpolated($"EXEC SP_GET_PACKGE_REMAINING_LIMITS {Company_ID}").ToListAsync();
                List<GetBillingInformation> getBillingInformation = await _context.Set<GetBillingInformation>().FromSqlInterpolated($"EXEC SP_GET_BILLING_INFORAMTION {Company_ID}").ToListAsync();
                billingInformation.PackageLimitsResponse = CompanyLimits;
                billingInformation.GetBillingInformation = getBillingInformation;
                return billingInformation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update_Billing_History(PaymentDetailsRequest request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Package_ID = new SqlParameter("@Package_ID", request.Package_ID);
                    var Package_Price = new SqlParameter("@Package_Price", request.Package_Price);
                    var Company_ID = new SqlParameter("@Company_ID", request.Company_ID);
                    var Created_By = new SqlParameter("@Created_By", request.Created_By);
                    var Stripe_Customer_ID = new SqlParameter("@Stripe_Customer_ID", request.Customer_ID);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_ADD_UPDATE_BILLING_INFORMATION_V1 @Package_ID,@Package_Price,@Company_ID,@Created_By,@Stripe_Customer_ID",
                        Package_ID, Package_Price, Company_ID, Created_By, Stripe_Customer_ID);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> GetStipePackagePriceID(int request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Package_ID = new SqlParameter("@Package_ID", request);
                    List<StripeProductID> response = await _context.Set<StripeProductID>().FromSqlInterpolated($"EXEC SP_GET_STRIPE_PRODUCT_PRICE_ID {Package_ID}").ToListAsync();
                    if (response.Count > 0)
                        return response[0].Stripe_Product_ID;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> logout(long request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                   
                    var User_ID = new SqlParameter("@User_ID", request);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_Logout_User @User_ID",
                        User_ID);
                    var Note = new SqlParameter("@Note", "Logged out");
                    await _context.Database.ExecuteSqlRawAsync($"EXEC Sp_SaveUserLogs @User_ID,@Note", User_ID, Note);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<WidgetTourSetting>> GetTourSetting(long request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var User_Id = new SqlParameter("@User_Id", request);
                    List<WidgetTourSetting> getWidgetTourSetting = await _context.Set<WidgetTourSetting>().FromSqlInterpolated($"EXEC SP_GET_WIDGET_TOUR_Setting {User_Id}").ToListAsync();
                    if (getWidgetTourSetting.Count > 0)
                        return getWidgetTourSetting;
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update_Widget_Tour_Setting(WidgetTourSettingRequest request)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var Widgets_Id = new SqlParameter("@Widgets_Id", request.Widgets_Id);
                    var User_Id = new SqlParameter("@User_Id", request.User_id);
                    var is_viewed = new SqlParameter("@is_viewed", request.is_viewed);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_UPDATE_WIDGET_TOUR_Setting @Widgets_Id,@User_Id,@is_viewed",
                        Widgets_Id, User_Id, is_viewed);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update_Auto_Billing_History(string CustomerID, string json)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var JSON = new SqlParameter("@JSON", json);
                    var Stripe_Customer_ID = new SqlParameter("@Stripe_Customer_ID", CustomerID);
                    int response = await _context.Database.ExecuteSqlRawAsync($"EXEC SP_ADD_UPDATE_AUTO_BILLING_INFORMATION @JSON,@Stripe_Customer_ID",
                        JSON, Stripe_Customer_ID);
                    if (response > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
