
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MergerBay.Infrastructure.Interfaces.Login;
using MergerBay.Domain.Context;
using MergerBay.Infrastructure.Interfaces.Password;
using MergerBay.Domain.Entities.Login;
using System.Data.SqlClient;
using MergerBay.Domain.Model;

namespace MergerBay.Services.Services.Login
{
    public class Login : ILogin
    {
        private readonly MergerBayContext _Context;
        private readonly IPasswordHasher _PasswordHasher;
        public Login(MergerBayContext context, IPasswordHasher PasswordHasher)
        {
            _Context = context;
            _PasswordHasher = PasswordHasher;
        }
        public async Task<UserLoginReponse> UserAuthentication(UserSignIn model)
        {
            try
            {
                UserLoginReponse response = new UserLoginReponse();
                var UsersAddedPassword = _Context.UserPersonalInformation.Where(e => e.User_Joined_Email == model.UserName && e.Is_Active==true && e.Is_Deleted==false).ToList();
                    if (UsersAddedPassword.Count > 0)
                    {
                        var temp = _PasswordHasher.GenerateIdentityV3Hash(model.Password);
                        bool flag = _PasswordHasher.VerifyIdentityV3Hash(model.Password, UsersAddedPassword[0].Password);
                        if (flag)
                        {
                           
                            response.UserName = UsersAddedPassword[0].User_First_Name+" "+ UsersAddedPassword[0].User_Last_Name;
                            response.User_Id = UsersAddedPassword[0].UserId;
                            response.Email_Address = UsersAddedPassword[0].User_Joined_Email;
                            response.Role_ID = UsersAddedPassword[0].Role_ID;
                            return response;
                    }
                        else
                        {
                        response.Message = "Wrong password";
                        return response;
                        }
                    }
                    else
                    {
                    response.Message = "User does not exists";
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<bool> MatchUserPassword(PasswordRequestModel model)
        {
            try
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    var User_Joined_Email = new SqlParameter("@User_Joined_Email", model.User_Joined_Email);
                    List<AlreadyAddedPassword> UsersAddedPassword = await _Context.Set<AlreadyAddedPassword>().FromSqlInterpolated($"EXEC Sp_Get_Encrypted_Password {User_Joined_Email}").ToListAsync();
                    if (UsersAddedPassword.Count > 0)
                    {
                        bool flag = _PasswordHasher.VerifyIdentityV3Hash(model.User_Password, UsersAddedPassword[0].User_Password);
                        return flag;
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
    }

}

