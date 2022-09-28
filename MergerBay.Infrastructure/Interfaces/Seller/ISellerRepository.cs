using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Model;

using MergerBay.Infrastructure.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Infrastructure.Interfaces.Seller
{
    public interface ISellerRepository : IGenericRepository
    {
        //====Additional Seller Module Related Methods will be defined here

        Task<int> UpdateSellerUserId(SellOutVm seller);
        Task<bool> StoreSectors(SellOut seller);
    }
}
