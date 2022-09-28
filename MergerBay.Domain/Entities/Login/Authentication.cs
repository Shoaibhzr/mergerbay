using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MergerBay.Domain.Entities.Login
{
    //user model class
    [NotMapped]
    public class Authentication
    {
        public Guid? User_ID { get; set; }
        public string? Username { get; set; }
        public string? User_Joined_Email { get; set; }
        public string? User_Password { get; set; }
        public string? User_First_Name { get; set; }
        public string? User_Middle_Name { get; set; }
        public string? User_Last_Name { get; set; }
        public string? User_Contact_Number { get; set; }
        public string? User_Invitation_Link { get; set; }
        public string? User_Invitation_Email { get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public long? Company_ID { get; set; }
        public bool Is_Expired { get; set; }
        public Guid Role_ID { get; set; }
        public bool Is_Company_Account { get; set; }
        public bool Is_Active { get; set; }
        public string? User_Profile_Picture { get; set; }
        

        [NotMapped]
        public string User_Token { get; set; }
    }
    public class UserLoginReponse
    {
    
        public string User_Token { get; set; }
        public Guid User_Id { get; set; }
        public Guid Role_ID { get; set; }
        public string UserName { get; set; }
        public string Email_Address { get; set; }
        public string Message { get; set; }
    }
    public class AlreadyAddedPassword
    {
        [Key]
        public string User_Password { get; set; }
    }
    public class Users_Permissions
    {
        public string Permission_Title { get; set; }
    }

    public class PackageExpiry
    {
        public bool IsExpired { get; set; }
    }


    public class User_Information_And_Permission
    {
        public Authentication User_Information { get; set; }
        public List<Users_Permissions> User_Permissions { get; set; }
        public PackageExpiry PackageExpiry { get; set; }
    }

    public class PasswordRequestModel {
        public long User_ID { get; set; }
        public string User_Password { get; set; }
        public string User_Joined_Email { get; set; }
    }
}
