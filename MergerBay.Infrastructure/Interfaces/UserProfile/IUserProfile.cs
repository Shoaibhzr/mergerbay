using MergerBay.Domain.Entities.Company;
using MergerBay.Infrastructure.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Infrastructure.Interfaces.UserProfile
{
    public interface IUserProfile: IGenericRepository
    {
        Task<List<CompanyCategory>> GetCategories();
        Task<List<MergerBay.Domain.Entities.UserProfile.UserProfile>> GetProfile(Guid userId);
        Task<int> SaveProfile(MergerBay.Domain.Entities.UserProfile.UserProfile profile);
        Task<int> SaveCompanyProfile(MergerBay.Domain.Entities.UserProfile.CompanyInformation companyInformation);
        Task<List<MergerBay.Domain.Entities.UserProfile.CompanyInformation>> GetCompanyInformation(Guid userId);


    }
}
