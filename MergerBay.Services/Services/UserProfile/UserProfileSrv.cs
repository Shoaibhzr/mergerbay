using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.Company;
using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Entities.Setups;
using MergerBay.Domain.Entities.User;
using MergerBay.Domain.Entities.UserProfile;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Common;
using MergerBay.Infrastructure.Interfaces.UserProfile;
using MergerBay.Services.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Services.Services.UserProfile
{
    public class UserProfileSrv : GenericRepository,IUserProfile
    {
        protected readonly MergerBayContext _Context;

        public UserProfileSrv(MergerBayContext Context):base(Context)
        {
            this._Context = Context;
        }

        public async Task<int> SaveProfile(MergerBay.Domain.Entities.UserProfile.UserProfile profile)
        {
            try
            {
                var res = await _Context.Set<Tbl_User_Management>().FirstOrDefaultAsync(x => x.UserId == profile.UserId);
                if (res == null) return 0;
                res.User_First_Name = profile.FirstName;
                res.User_Last_Name = profile.LastName;
                res.Country = profile.Country;
                res.Business_Phone_Number = profile.Phone;
                res.Address = profile.Location;
                res.Is_Company_Account = true;
                Attachments a = new Attachments();
                a.Attachment_ID = Guid.NewGuid();
                a.Created_Date = DateTime.UtcNow;
                a.File_Name = profile.ProfilePictureList;
                a.File_Type = ".PNG";
                a.Source_Type = "ProfilePicture";
                a.Source_ID = profile.UserId;
                var profilePicture = this._Context.AttachmentsGeneral.Where(w => w.Source_ID == profile.UserId && w.Is_Deleted == false && w.Source_Type == "ProfilePicture").ToList();
                foreach (var p in profilePicture)
                {
                    p.Is_Deleted = true;
                    p.Is_Active=false;

                }
                _Context.AttachmentsGeneral.Add(a);
                await _Context.SaveChangesAsync();
                return 1;
            }
            catch(Exception er)
            {
                throw er;
            }
           
        }

        public async Task<int> SaveCompanyProfile(MergerBay.Domain.Entities.UserProfile.CompanyInformation companyInformation)
        {
            var res = await _Context.Set<Tbl_User_Management>().FirstOrDefaultAsync(x => x.UserId == companyInformation.UserId);
            if (res == null) return 0;
            res.Is_Company_Account = true;
            res.Business_Name = companyInformation.Company;
            res.Website = companyInformation.Website;
            res.Designation = companyInformation.Designation;
            foreach (var category in companyInformation.CategoryListStore)
            {
                CompanyCategoryStore cs = new CompanyCategoryStore();
                cs.User_Id = companyInformation.UserId;
                cs.Category_Id =Guid.Parse(category);
                cs.Created_Date = DateTime.UtcNow;
                cs.Created_by = Guid.NewGuid();
                cs.Modified_by = Guid.NewGuid();
                cs.Modified_Date = DateTime.UtcNow;
                cs.Is_active = true;
                cs.Is_Deleted = false;
                this._Context.CompanyCategoryStore.Add(cs);
                await this._Context.SaveChangesAsync();
            }
            foreach (var card in companyInformation.CardAttachment)
            {
                Attachments a = new Attachments();
                a.Attachment_ID = Guid.NewGuid();
                a.Created_Date = DateTime.UtcNow;
                a.File_Name = card;
                a.File_Type = ".PNG";
                a.Source_Type = "ProfileCard";
                a.Source_ID = companyInformation.UserId;
                _Context.AttachmentsGeneral.Add(a);
                await _Context.SaveChangesAsync();
            }
            foreach (var card in companyInformation.CertificateAttachment)
            {
                Attachments a = new Attachments();
                a.Attachment_ID = Guid.NewGuid();
                a.Created_Date = DateTime.UtcNow;
                a.File_Name = card;
                a.File_Type = ".PNG";
                a.Source_Type = "ProfileDocument";
                a.Source_ID = companyInformation.UserId;
                _Context.AttachmentsGeneral.Add(a);
                await _Context.SaveChangesAsync();
            }
            return await _Context.SaveChangesAsync();
        }

        public async Task<List<MergerBay.Domain.Entities.UserProfile.UserProfile>> GetProfile(Guid userId)
        {
            var res = this._Context.UserPersonalInformation.Where(u => u.Is_Deleted == false && u.Is_Active == true && u.UserId==userId).ToList();
            List<MergerBay.Domain.Entities.UserProfile.UserProfile> response = new List<Domain.Entities.UserProfile.UserProfile>();
            MergerBay.Domain.Entities.UserProfile.UserProfile pro;
            CategoryList categoryList;
            if (res.Count > 0)
            {
                foreach (var user in res)
                {
                    pro = new Domain.Entities.UserProfile.UserProfile();
                    pro.UserId = user.UserId;
                    pro.Email = user.User_Joined_Email;
                    pro.FirstName = user.User_First_Name;
                    pro.LastName = user.User_Last_Name;
                    pro.Phone = user.Business_Phone_Number;
                    pro.Country = user.Country;
                    pro.Location = user.Address;
                    var profilePicture = this._Context.AttachmentsGeneral.Where(w => w.Source_ID == user.UserId && w.Is_Deleted == false && w.Source_Type == "ProfilePicture").ToList();
                    foreach (var p in profilePicture)
                    {
                        pro.ProfilePictureList = p.File_Name;
                    }
              
                    response.Add(pro);
                }
            }


            return response;
        }

        public async Task<List<MergerBay.Domain.Entities.UserProfile.CompanyInformation>> GetCompanyInformation(Guid userId)
        {
            var res = this._Context.UserPersonalInformation.Where(u => u.Is_Deleted == false && u.Is_Active == true && u.UserId == userId).ToList();
            List<MergerBay.Domain.Entities.UserProfile.CompanyInformation> response = new List<Domain.Entities.UserProfile.CompanyInformation>();
            MergerBay.Domain.Entities.UserProfile.CompanyInformation pro;
            List<CompanyCategory> CategoryList=new List<CompanyCategory>();
            CompanyCategory cc;
            if (res.Count > 0)
            {
                foreach (var user in res)
                {
                    pro = new Domain.Entities.UserProfile.CompanyInformation();
                    pro.UserId = user.UserId;
                    pro.Company = user.Business_Name;
                    pro.Website = user.Website;
                    pro.Designation = user.Designation;
                    var companyInfo = this._Context.CompanyCategoryStore.Where(w => w.User_Id == user.UserId && w.Is_Deleted == false && w.Is_active == true).ToList();
                    foreach (var cat in companyInfo)
                    {
                        cc = new CompanyCategory();
                        cc.Company_Category_ID = cat.Category_Id;
                        cc.Category_Name = this._Context.CompanyCategory.FirstOrDefault(e => e.Company_Category_ID == cat.Category_Id).Category_Name;
                        CategoryList.Add(cc);
                    }
                    var cardAttachment = this._Context.AttachmentsGeneral.Where(w => w.Source_ID == user.UserId && w.Is_Deleted == false && w.Source_Type== "ProfileCard").ToList();
                    foreach (var card in cardAttachment)
                    {
                        pro.CardAttachment.Add(card.ToString());
                    }
                    var certificateAttachment = this._Context.AttachmentsGeneral.Where(w => w.Source_ID == user.UserId && w.Is_Deleted == false && w.Source_Type == "ProfileDocuments").ToList();
                    foreach (var doc in certificateAttachment)
                    {
                        pro.CertificateAttachment.Add(doc.ToString());  
                    }
                    pro.CategoryList = CategoryList;
                    response.Add(pro);
                }

            }


            return response;
        }
        public async Task<List<CompanyCategory>> GetCategories()
        {
            var res = this._Context.CompanyCategory.Where(w => w.Is_Active == true && w.Is_Deleted == false).ToList();
            return res;
        }
    }
}
