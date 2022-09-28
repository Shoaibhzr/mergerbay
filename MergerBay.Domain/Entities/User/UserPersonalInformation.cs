
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MergerBay.Domain.Entities.User
{
    [NotMapped]
    public class UserPersonalInformation
    {
        [Key]
        public Guid User_ID { get; set; }
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string TimeZone { get; set; }
        public string HashPassword { get; set; }
        public bool Is_Company_Account { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string BusinessEmail { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string EmailUrl { get; set; }
        public string Country { get; set; }
        public long Invited_User_ID { get; set; }


    }
    [NotMapped]
    public class CompanyInformation
    {
        [Key]
        public long Company_ID { get; set; }
        public string Company_Name { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Contact_Number { get; set; }
        public string Email_Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Attachment_Path { get; set; }
        [NotMapped]
        public long Modified_By { get; set; }
        [NotMapped]
        public AttachmentModel Attachment { get; set; }
        [NotMapped]
        public bool IsAttachmentUpdated { get; set; }

    }
    [NotMapped]
    public class AttachmentModel
    {
        public string attachmentName { get; set; }
        public string attachmentData { get; set; }
    }
    [NotMapped]
    public class ConnectedChannels
    {
        [Key]
        public long Channel_ID { get; set; }
        public string User_Name { get; set; }
        [NotMapped]
        public int Social_Media_Type { get; set; }
        public long Social_Media_ID { get; set; }
        public string Channel_Name { get; set; }
        public string Social_Media_Title { get; set; }
    }
    [NotMapped]
    public class AllCompanyActiveUsers
    {
        [Key]
        public long User_ID { get; set; }
        public string User_Joined_Email { get; set; }
        public string user_name { get; set; }
        public long Role_ID { get; set; }
        public string Role_Title { get; set; }
        public string All_Role { get; set; }
        public string All_Role_ID { get; set; }
        public string All_Assign_Team_ID { get; set; }
        public string All_Assign_Team_Title { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public string Attachment_Path { get; set; }
        public string Team_Background_Color { get; set; }
        public bool Is_Invite_Accepted { get; set; }
        [NotMapped]
        public bool Is_Super_admin { get; set; }


    }
    [NotMapped]
    public class GetPersonalInformation
    {
        [Key]
        public long User_ID { get; set; }
        public string User_First_Name { get; set; }
        public string User_Last_Name { get; set; }
        public string User_Joined_Email { get; set; }
        public string User_Contact_Number { get; set; }
        public string User_Time_Zone { get; set; }
        public string Attachment_Path { get; set; }
    }
    [Table("Tbl_Users_Management_Channels_Management")]
    public class Tbl_Users_Management_Channels_Management
    {
        [Key]
        public long Channel_ID { get; set; }
        public long Social_Media_ID { get; set; }
        public string Channel_Identifier { get; set; }
        public string Channel_Name { get; set; }
        public string Channel_Category { get; set; }
        public string Channel_Username { get; set; }
        public string Channel_Auth_Token { get; set; }
        public string Channel_Token_Secret { get; set; }
        public string Channel_Link { get; set; }
        public string Channel_Picture { get; set; }
        public int? Social_Media_Service { get; set; }
        public long? User_ID { get; set; }
        public long Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Is_Deleted { get; set; }
        public long? Modified_By { get; set; }
        public DateTime? Modified_Date { get; set; }

    }
    public class UpdatePersonalInformation
    {
        public long User_ID { get; set; }
        public string User_First_Name { get; set; }
        public string User_last_Name { get; set; }
        public string PhoneNumber { get; set; }
        public string User_Email { get; set; }
        public string New_Password { get; set; }
        public string User_Time { get; set; }
        [NotMapped]
        public AttachmentModel Attachment { get; set; }
        [NotMapped]
        public string Attachment_Path { get; set; }
        [NotMapped]
        public string CurrentPassword { get; set; }
        [NotMapped]
        public bool IsAttachmentUpdated { get; set; }
    }
    [NotMapped]
    public class GetUserNotificatipnsSettings
    {
        [Key]
        public long Notification_Settings_ID { get; set; }
        public long User_ID { get; set; }
        public bool Task { get; set; }
        public bool Keywords { get; set; }
        public bool Users { get; set; }
        public bool Approvals { get; set; }
        public int Archived_Delete_Years_Count { get; set; }
    }
    public class ConfirmEmail
    {
        public string Token { get; set; }
        public string Confirmation_Email { get; set; }
    }
    public class Email_Confirmed
    {
        public bool Is_Active { get; set; }
    }
    public class Email_Confirmed_Response
    {
        public bool Is_Active { get; set; }
        public string Response_Message { get; set; }
    }
    public class Email_Check
    {
        public long User_ID { get; set; }
        public string User_Full_Name { get; set; }
    }
    public class Reset_Password
    {
        public string EmailURL { get; set; }
        public string UserEmail { get; set; }
    }
    public class Reset_Password_After_Verification
    {
        public string Password { get; set; }
        public string UserEmail { get; set; }
    }
    public class Get_All_Active_Teams_And_Its_Members
    {
        public long Team_ID { get; set; }
        public string Team_Name { get; set; }
        public string Background_Color { get; set; }
        public long Users_Team_ID { get; set; }
        public string All_Assigned_User_ID { get; set; }
        public string All_Assigned_User_Name { get; set; }
        public bool Is_Assigned { get; set; }
        public string All_Assigned_User_Attachment { get; set; }


    }
    public class Team_Assignment_Request
    {
        public int users_Team_ID { get; set; }
        public int User_ID { get; set; }
        public int Team_ID { get; set; }
        public bool Is_Assigned { get; set; }
        public int Selected_User_ID_Team_Assignment { get; set; }


    }
    public class PublishingQueue
    {
        public int Created_by { get; set; }
        public int Channel_ID { get; set; }
        public bool Queue_Status { get; set; }
        public List<PublishingQueueDays> Days { get; set; }
        public List<PublishingQueuePostTime> PostTime { get; set; }

        public int timezone { get; set; }
    }
    public class PublishingQueueDays
    {
        public int Channel_ID { get; set; }
        public int Day { get; set; }
    }
    public class PublishingQueuePostTime
    {
        public int Channel_ID { get; set; }
        public string PostTime { get; set; }

    }
    public class Publishing_Queue_Details
    {
        public long Publishing_Queue_ID { get; set; }
        public string Day_Number { get; set; }
        public string Post_Time { get; set; }

    }
    public class Publishing_Queue_Details_Request
    {
        public long Channel_ID { get; set; }
        public int timezone { get; set; }
        public DateTime schdate { get; set; }
    }
    public class Invited_People_Email
    {
        public string Email { get; set; }
        public string EmailUrl { get; set; }
        public int User_ID { get; set; }
        public int Company_ID { get; set; }
    }
    public class Invited_User_ID
    {
        public long User_ID { get; set; }
        public long Company_ID { get; set; }
    }
    public class GetPersonalInformationForEdition
    {
        [Key]
        public GetPersonalInformation GetPersonalInformation { get; set; }
        public List<GetUsersConnectedChannels> UserChannels { get; set; }
        public List<Get_All_Active_Teams_And_Its_Members> AssigedTeams { get; set; }
    }
    public class GetUsersConnectedChannels
    {

        public long Social_Media_ID { get; set; }
        public string Channel_Name { get; set; }
        public string Channel_Picture { get; set; }
    }
    public class DaysNameList
    {

        public long DayNumber { get; set; }
        public string DayName { get; set; }
        public int PostCount { get; set; }
        public int Day { get; set; }
    }

    public class ChannelkeyWithChannelInd
    {

        public long ChannelPrimaryId { get; set; }
     
    }
    public class GetUsersConnectedChannelsForFeeds
    {
        public string Channel_Identifier { get; set; }
        public long Social_Media_ID { get; set; }
        public string Channel_Name { get; set; }
        public string Channel_Picture { get; set; }
        public Int32 Social_Media_Service { get; set; }
        public long User_Id { get; set; }
        public long Channel_ID { get; set; }
        public long Company_ID { get; set; }
        [NotMapped]
        public string Connection_ID { get; set; }
    }
    public static class GetAllUsersConnectedChannelsForFeeds
    {
        public static long Company_ID { get; set; }
        public static List<GetUsersConnectedChannelsForFeeds> GetAssignedChannels { get; set; }
    }
    public class GetPersonal_Information
    {
        public int User_ID { get; set; }
        public int Company_ID { get; set; }
    }
    public class Deleted_User
    {
        public int Deleted_User_ID { get; set; }
        public int User_ID { get; set; }
    }
    public class GET_ACTIVE_COMPANY_TEAMS_AND_MEMBERS
    {
        public long TEAM_ID { get; set; }
        public string TEAM_NAME { get; set; }
        public string TEAM_MEMBERS { get; set; }
        public string TEAM_MEMBERS_ATTACHMENT { get; set; }
        public string Background_Color { get; set; }
        public bool Is_Assigned { get; set; }

    }
    public class Create_Team_And_Team_Members
    {
        public string Team_Name { get; set; }
        public long User_ID { get; set; }
        public long Company_ID { get; set; }
        public long Team_ID { get; set; }
        public string Background_Color { get; set; }
        public List<Assigned_Team_To_User> Assigned_Team_To_User { get; set; }

    }
    public class Assigned_Team_To_User
    {
        public long Assigned_User_ID { get; set; }

    }
    public class Last_Inserted_Team_ID
    {
        public long TEAM_ID { get; set; }

    }
    public class Get_All_Users_With_Respect_To_Team
    {
        public long User_ID { get; set; }
        public string USER_FULL_NAME { get; set; }
        public string User_Joined_Email { get; set; }
        public bool Is_Assigned { get; set; }
        public string Attachment_Path { get; set; }


    }
    public class All_Users_With_Team_Assignment
    {
        public long User_ID { get; set; }
        public bool Is_Assigned { get; set; }
        public long Team_ID { get; set; }
        public long Modified_By { get; set; }
    }
    public class Update_Team_And_Team_Members
    {
        public long Team_ID { get; set; }
        public string Team_Name { get; set; }
        public long User_ID { get; set; }
        public long Company_ID { get; set; }
        public List<All_Users_With_Team_Assignment> Assigned_Team_To_User { get; set; }

    }
    public class GetUserAssignedChannels
    {
        public long Channel_ID { get; set; }
        public int? Social_Media_Service { get; set; }
        public long User_ID { get; set; }
        public long Social_Media_ID { get; set; }
        public string Channel_Identifier { get; set; }
        public string Channel_Name { get; set; }
        public string Channel_Category { get; set; }
        public string Channel_Auth_Token { get; set; }
        public string Channel_Token_Secret { get; set; }
        public string Channel_Link { get; set; }
        public string Channel_Picture { get; set; }
        public long Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Is_Deleted { get; set; }
        public long? Modified_By { get; set; }
        public DateTime? Modified_Date { get; set; }
        public string All_Teams { get; set; }
        public string All_Team_Members { get; set; }
        public bool Is_Admin { get; set; }
        public string All_Team_Members_Attachment { get; set; }
        public string Teams_Background_Color { get; set; }


    }
    public class Assigned_Teams_To_Channels
    {
        public long Company_ID { get; set; }
        public long Channel_ID { get; set; }
    }
    public class GET_ACTIVE_COMPANY_TEAMS_AND_With_Channels_Asssigned
    {
        public long TEAM_ID { get; set; }
        public string TEAM_NAME { get; set; }
        public string TEAM_MEMBERS { get; set; }
        public bool Is_Assigned { get; set; }
        public string Attachment_Path { get; set; }
        public string Background_Color { get; set; }

    }
    public class Update_Team_To_Channels
    {
        public long Channel_ID { get; set; }
        public bool Is_Assigned { get; set; }
        public long Team_ID { get; set; }
        public long Modified_By { get; set; }

    }
    public class GetChannelspublishingQueue
    {
        public string Channel_Picture { get; set; }
        public string Channel_Name { get; set; }
        public long Channel_ID { get; set; }
        public long Social_Media_ID { get; set; }
        public bool Is_Active { get; set; }
    }
    public class UpdateUserInformation
    {
        public long User_ID { get; set; }
        public long Modified_By { get; set; }
        public long Role_ID { get; set; }
        public bool Is_Active { get; set; }
    }
    public class PackageDetails
    {
        public long Package_ID { get; set; }
        public string Package_Category { get; set; }
        public string Package_Tagline { get; set; }
        public decimal Package_Price { get; set; }
        public string Component_Main_Title { get; set; }
        public string Component_Sub_Title { get; set; }
        public string Component_Status { get; set; }
        public string Component_Type { get; set; }
        public string Package_MainComponent_limit { get; set; }
        public string Package_SubComponent_limit { get; set; }
        public string Package_MainComponent_HasQuantity { get; set; }
        public long Package_Users { get; set; }

    }
    public class PackageLimitsResponse
    {
        public string Package_Name { get; set; }
        public int Remaning_Invites { get; set; }
        public int Remaning_Admins { get; set; }
        public int Remaning_Managers { get; set; }
        public int Remaning_Users { get; set; }
        public int Remaning_Channels { get; set; }
        public int Remaning_Teams { get; set; }
        public decimal Percentage_Used { get; set; }
        public int Remaining_Days { get; set; }
    }
    public class GetBillingInformation
    {
        public long Purchased_ID { get; set; }
        public long Package_ID { get; set; }
        public string Package_Category { get; set; }
        public string Package_Tagline { get; set; }
        public string Component_Title { get; set; }
        public string Package_Component_limit { get; set; }
        public string Expiration_Date { get; set; }
        public decimal Package_Price { get; set; }
        public string Package_Component_Price { get; set; }
        public string Package_Component_Total_Price { get; set; }


    }
    public class TeamUsersRequestModel
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
    }
    public class StripeProductID
    {
        public string Stripe_Product_ID { get; set; }
    }
    public class PaymentDetailsRequest
    {
        public string Email { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVC { get; set; }
        public float Package_Price { get; set; }
        public long Created_By { get; set; }
        public long Company_ID { get; set; }
        public long Package_ID { get; set; }
        public string Discounted_Price { get; set; }
        public string DiscountCode { get; set; }
        public string Original_Price { get; set; }
        public string Token { get; set; }
        public string Customer_ID { get; set; }
        public int PackageID { get; set; }

    }
    public class UsersCompanyID
    {
        public long company_ID { get; set; }
    }
    public class WidgetTourSetting
    {
        public int Widgets_Id { get; set; }
        public string Widgets_Name { get; set; }
        public int is_viewed { get; set; }

    }
    public class WidgetTourSettingRequest
    {
        public int Widgets_Id { get; set; }
        public int User_id { get; set; }
        public int is_viewed { get; set; }

    }
    public class PackageSettingRequest
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }

    }
    public class ApplyDiscount
    {
        public string DiscountCode { get; set; }
        public double CurrentAmount { get; set; }
    }
    public class DiscountResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public double RemainingAmount { get; set; }
    }
    public class BillingInformation
    {
        public List<PackageLimitsResponse> PackageLimitsResponse { get; set; }
        public List<GetBillingInformation> GetBillingInformation { get; set; }
    }

    public class Rootobject
    {
        public int created { get; set; }
        public bool livemode { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string _object { get; set; }
        public object request { get; set; }
        public int pending_webhooks { get; set; }
        public string api_version { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        [JsonProperty("object")]
        public Object _object { get; set; }
    }

    public class Object
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int amount { get; set; }
        public int amount_captured { get; set; }
        public int amount_refunded { get; set; }
        public object application { get; set; }
        public object application_fee { get; set; }
        public object application_fee_amount { get; set; }
        public string balance_transaction { get; set; }
        public Billing_Details billing_details { get; set; }
        public string calculated_statement_descriptor { get; set; }
        public bool captured { get; set; }
        public int created { get; set; }
        public string currency { get; set; }
        public string customer { get; set; }
        public string description { get; set; }
        public bool disputed { get; set; }
        public object failure_code { get; set; }
        public object failure_message { get; set; }
        public Fraud_Details fraud_details { get; set; }
        public string invoice { get; set; }
        public bool livemode { get; set; }
        public Metadata metadata { get; set; }
        public object on_behalf_of { get; set; }
        public object order { get; set; }
        public Outcome outcome { get; set; }
        public bool paid { get; set; }
        public string payment_intent { get; set; }
        public string payment_method { get; set; }
        public Payment_Method_Details payment_method_details { get; set; }
        public object receipt_email { get; set; }
        public object receipt_number { get; set; }
        public string receipt_url { get; set; }
        public bool refunded { get; set; }
        public Refunds refunds { get; set; }
        public object review { get; set; }
        public object shipping { get; set; }
        public object source_transfer { get; set; }
        public object statement_descriptor { get; set; }
        public object statement_descriptor_suffix { get; set; }
        public string status { get; set; }
        public object transfer_data { get; set; }
        public object transfer_group { get; set; }
    }

    public class Billing_Details
    {
        public Address address { get; set; }
        public object email { get; set; }
        public string name { get; set; }
        public object phone { get; set; }
    }

    public class Address
    {
        public object city { get; set; }
        public object country { get; set; }
        public object line1 { get; set; }
        public object line2 { get; set; }
        public object postal_code { get; set; }
        public object state { get; set; }
    }

    public class Fraud_Details
    {
    }

    public class Metadata
    {
    }

    public class Outcome
    {
        public string network_status { get; set; }
        public object reason { get; set; }
        public string risk_level { get; set; }
        public int risk_score { get; set; }
        public string seller_message { get; set; }
        public string type { get; set; }
    }

    public class Payment_Method_Details
    {
        public Card card { get; set; }
        public string type { get; set; }
    }

    public class Card
    {
        public string brand { get; set; }
        public Checks checks { get; set; }
        public string country { get; set; }
        public int exp_month { get; set; }
        public int exp_year { get; set; }
        public string fingerprint { get; set; }
        public string funding { get; set; }
        public object installments { get; set; }
        public string last4 { get; set; }
        public string network { get; set; }
        public object three_d_secure { get; set; }
        public object wallet { get; set; }
    }

    public class Checks
    {
        public object address_line1_check { get; set; }
        public object address_postal_code_check { get; set; }
        public string cvc_check { get; set; }
    }

    public class Refunds
    {
        public string _object { get; set; }
        public object[] data { get; set; }
        public bool has_more { get; set; }
        public string url { get; set; }
    }

}
