using MergerBay.Domain.Entities.Deals;
using MergerBay.Domain.Entities.SearchParams;
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
        Task<bool> StoreSectorsSellOut(SellOut seller);
        Task<bool> StoreSectorsBuyOut(BuyOut buyer);
        Task<IEnumerable<PropositionsVM>> GetPrepositions(Guid userId);
        Task<IEnumerable<SP_SELLOUTS>> GetSellOuts(SearchParams searchParams);
        Task<IEnumerable<SP_BUYOUTS>> GetBuyOuts(SearchParams searchParams);
        Task<IEnumerable<SP_RecommendedDeals>> GetRecommendedDeals(Guid? userId);

    }
}
