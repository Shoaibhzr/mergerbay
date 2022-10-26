
using MergerBay.Domain.Entities.User;
using MergerBay.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Infrastructure.Interfaces.UserManagment
{
    public interface IUserManagement
    {
        Task<UserSignup> Add_User_Personal_Information(UserPersonalInformation model);
        Task<CompanyInformation> Get_Company_Information(long Company_ID);
        Task<bool> Update_Company_Information(CompanyInformation request);
        Task<GetPersonalInformationForEdition> Get_Personal_Information(long User_ID, long Company_ID);
        Task<bool> Update_Personal_Information(UpdatePersonalInformation request);
        Task<GetUserNotificatipnsSettings> Get_Setting_Notifications(long User_ID);
        Task<bool> Get_Setting_Notifications(GetUserNotificatipnsSettings User_ID);
        Task<Email_Confirmed_Response> Confirm_Email(ConfirmEmail request);
        Task<bool> Check_For_Valid_Email(string request);
        Task<bool> Reset_Password_Generate_link(Reset_Password request);
        Task<bool> Check_For_Valid_Reset_Password_Link(ConfirmEmail request);
        Task<bool> Reset_Password(Reset_Password_After_Verification request);
        Task<ChangePassword> ChangePassword(ChangePassword model);
        Task<bool> NotificationSetting(NotificationSettingModel model);
        Task<List<string>> Invited_People(List<Invited_People_Email> request);
        Task<Invited_User_ID> Invited_User_Verification(ConfirmEmail request);
        Task<NotificationSettingModel> GetNotificationSetting(string User_Id);
        Task<bool> User_Personal_Information_Invited_People(UserPersonalInformation request);
        Task<bool> Disabled_User(Deleted_User request);

      
 
   
        Task<bool> Edit_Personal_Inforamtion(UpdateUserInformation request);
        Task<PackageLimitsResponse> GetPackageRemainingLimits(PackageSettingRequest request);
        Task<List<PackageDetails>> GetAllPackageDetails();
        Task<BillingInformation> Billing_Inforamtion(long request);
        Task<bool> Update_Billing_History(PaymentDetailsRequest request);
        Task<bool> logout(long request);
        Task<List<WidgetTourSetting>> GetTourSetting(long request);
        Task<bool> Update_Widget_Tour_Setting(WidgetTourSettingRequest request);
        Task<string> GetStipePackagePriceID(int request);
        Task<bool> Update_Auto_Billing_History(string CustomerID, string json);
   
        
    }
}
