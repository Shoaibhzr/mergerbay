using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.Deals;
using MergerBay.Domain.Entities.SearchParams;
using MergerBay.Domain.Entities.Sectors;
using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBay.Services.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Text;

namespace MergerBay.Services.Services.Seller
{
    public class SellerRepository : GenericRepository, ISellerRepository
    {
        protected readonly MergerBayContext _Context;
        public SellerRepository(MergerBayContext Context) : base(Context)
        {
            this._Context = Context;
        }

        //====Additional ISeller Contract Will be implemented here
        public async Task<int> UpdateSellerUserId(SellOutVm seller)
        {
            var res = await _Context.Set<SellOut>().FirstOrDefaultAsync(x => x.SellOut_Id == seller.FormId);
            if (res == null) return 0;
            res.UserId = seller.UserId;
            res.Status = seller.Status;
            return await _Context.SaveChangesAsync();
        }

        public async Task<bool> StoreSectorsSellOut(SellOut seller)
        {
            SectorMain SM;
            SectorDetail SD;
            SectorDetail_Items SDI;
            foreach (var s in seller.SectorsArray)
            {
                try
                {
                    SM = new SectorMain();
                    SM.SectorMainId = Guid.NewGuid();
                    SM.FormId = seller.SellOut_Id;
                    SM.SectorId = s.SectorId;
                    SM.SectorName = s.SectorName;
                    await _Context.AddAsync<SectorMain>(SM);
                    await _Context.SaveChangesAsync();
                    foreach (var sub in s.SubSectorArr)
                    {
                        SD = new SectorDetail();
                        SD.SectorDetailId = Guid.NewGuid();
                        SD.SectorMainId = SM.SectorMainId;
                        SD.SectorId = s.SectorId;
                        SD.SubSectorId = sub.SectorId;
                        SD.SubSectorName = sub.SubSectorName;
                        SD.Status = sub.Status;
                        await _Context.AddAsync<SectorDetail>(SD);
                        await _Context.SaveChangesAsync();
                        foreach (var Item in sub.SubSectorItemArr)
                        {
                            SDI = new SectorDetail_Items();
                            SDI.SectorDetailItemId = Guid.NewGuid();
                            SDI.SectorDetailId = SD.SectorDetailId;
                            SDI.SectorId = s.SectorId;
                            SDI.SubSectorId = sub.SubSectorId;
                            SDI.SubSectorItemId = Item.SubSectorItemId;
                            SDI.SubSectorItemName = Item.SubSectorItemName;
                            SDI.Status = Item.Status;
                            await _Context.AddAsync<SectorDetail_Items>(SDI);
                            await _Context.SaveChangesAsync();

                        }
                    }
                }
                catch (Exception ex)
                {

                };
                // return await _Context.SaveChangesAsync()>0 ? true:false;


            }
            return true;
        }
        public async Task<bool> StoreSectorsBuyOut(BuyOut buyer)
        {
            SectorMain SM;
            SectorDetail SD;
            SectorDetail_Items SDI;
            foreach (var s in buyer.SectorsArray)
            {
                try
                {
                    SM = new SectorMain();
                    SM.SectorMainId = Guid.NewGuid();
                    SM.FormId = buyer.BuyOut_Id;
                    SM.SectorId = s.SectorId;
                    SM.SectorName = s.SectorName;
                    await _Context.AddAsync<SectorMain>(SM);
                    await _Context.SaveChangesAsync();
                    foreach (var sub in s.SubSectorArr)
                    {
                        SD = new SectorDetail();
                        SD.SectorDetailId = Guid.NewGuid();
                        SD.SectorMainId = SM.SectorMainId;
                        SD.SectorId = s.SectorId;
                        SD.SubSectorId = sub.SectorId;
                        SD.SubSectorName = sub.SubSectorName;
                        SD.Status = sub.Status;
                        await _Context.AddAsync<SectorDetail>(SD);
                        await _Context.SaveChangesAsync();
                        foreach (var Item in sub.SubSectorItemArr)
                        {
                            SDI = new SectorDetail_Items();
                            SDI.SectorDetailItemId = Guid.NewGuid();
                            SDI.SectorDetailId = SD.SectorDetailId;
                            SDI.SectorId = s.SectorId;
                            SDI.SubSectorId = sub.SubSectorId;
                            SDI.SubSectorItemId = Item.SubSectorItemId;
                            SDI.SubSectorItemName = Item.SubSectorItemName;
                            SDI.Status = Item.Status;
                            await _Context.AddAsync<SectorDetail_Items>(SDI);
                            await _Context.SaveChangesAsync();

                        }
                    }
                }
                catch (Exception ex)
                {

                };
                // return await _Context.SaveChangesAsync()>0 ? true:false;
            }
            return true;
        }

        public async Task<IEnumerable<PropositionsVM>> GetPrepositions(Guid userId)
        {
            List<PropositionsVM> propositions = new List<PropositionsVM>();
            var sellOutData = await _Context.SellOuts.Where(x => x.UserId == userId).Select(y =>
               new PropositionsVM()
               {
                   UserId = userId,
                   Title = y.ProjectName,
                   Created_Date = y.Created_Date,
                   FormId = y.SellOut_Id,
                   Type = y.Type,
                   SubType = y.SubType,
                   Status = y.Status,
               }).ToListAsync();
            propositions.AddRange(sellOutData);

            var buyOuts = await _Context.BuyOuts.Where(x => x.UserId == userId).ToListAsync();
            foreach (var buyOut in buyOuts)
            {
                string bussinessLocation = string.Empty;
                string countries = string.Empty;
                string propertySelected = string.Empty;
                string sectorsInterested = string.Empty;

                StringBuilder title = new StringBuilder();


                var businessLocation = await _Context.Countries.FirstOrDefaultAsync(x => x.Country_Id == buyOut.BusinessLocation_Id);
                var countryIds = buyOut.Country_Ids?.Split(',');
                //var employeesResult = _Context.Countries.Where(x => countryIds?.Contains();
                if (buyOut.Property_Id != null)
                {
                    var property = await _Context.Properties.FirstOrDefaultAsync(x => x.Property_Id == buyOut.Property_Id);
                    if (property != null) propertySelected = property.Name;
                }
                var sectors = await _Context.SectorMain.Where(x => x.FormId == buyOut.BuyOut_Id).Select(x => x.SectorName).ToListAsync();

                if (sectors.Any())
                {
                    sectorsInterested = string.Join(",", sectors);
                }

                if (!string.IsNullOrEmpty(businessLocation?.Name)) title.Append(businessLocation?.Name);
                if (!string.IsNullOrEmpty(propertySelected)) title.Append("/" + propertySelected);
                if (!string.IsNullOrEmpty(sectorsInterested)) title.Append("/" + sectorsInterested);


                var propositionsVM = new PropositionsVM();
                propositionsVM.UserId = buyOut.UserId;
                propositionsVM.FormId = buyOut.BuyOut_Id;
                propositionsVM.Type = buyOut.Type;
                propositionsVM.SubType = buyOut.SubType;
                propositionsVM.Status = buyOut.Status;
                propositionsVM.Created_Date = buyOut.Created_Date;
                propositionsVM.Title = title.ToString();
                propositions.Add(propositionsVM);

            }

            //var buyOutData = _Context.BuyOuts.Where(x => x.UserId == userId).Select(y =>
            //   new PropositionsVM()
            //   {
            //       UserId = userId,
            //       Title = y.SubType,
            //       Created_Date = y.Created_Date,
            //       FormId = y.BuyOut_Id,
            //       Type = y.Type,
            //       SubType = y.SubType,
            //       Status= y.Status,
            //   });

            //propositions.AddRange(buyOutData);

            return await Task.FromResult(propositions.OrderByDescending(x => x.Created_Date).Take(5));

        }
        public async Task<IEnumerable<SP_SELLOUTS>> GetSellOuts(SearchParams searchParams)
        {

            //SqlParameter[] sp = new SqlParameter[]
            //{
            //    new SqlParameter("@UserId", userId==null?"":userId.ToString()),
            //    new SqlParameter("@FormId", formId==null?"":formId.ToString()),

            //};
            //List<SqlParameter> sp = new List<SqlParameter>();
            //SqlParameterCollection sqlParameter;
            //sqlParameter.Insert

            //if (userId != null) sp.Add(new SqlParameter("@UserId", userId.ToString()));
            //if (formId != null) sp.Add(new SqlParameter("@FormId", formId.ToString()));


            //var result =await _Context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_SELLOUTS @UserId = {p1}, @FormId = {p2}");
            //var result =await _Context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_SELLOUTS @UserId = {p1}, @FormId = {p2}");


            var result = await _Context.SP_SELLOUTS.FromSqlRaw($"EXEC SP_SELLOUTS @UserId='{searchParams.UserId}',@FormId='{searchParams.FormId}'").ToListAsync();
            return await Task.FromResult(result);

        }
        public async Task<IEnumerable<SP_BUYOUTS>> GetBuyOuts(SearchParams searchParams)
        {

            var result = await _Context.SP_BUYOUTS.FromSqlRaw($"EXEC SP_BUYOUTS @UserId='{searchParams.UserId}',@FormId='{searchParams.FormId}'").ToListAsync();
            return await Task.FromResult(result);

        }
        public async Task<IEnumerable<SP_RecommendedDeals>> GetRecommendedDeals(Guid? userId)
        {
            var result = await _Context.SP_RecommendedDeals.FromSqlRaw($"EXEC [dbo].[SP_RecommendedDealsByUserId] @UserId = '{userId}'").ToListAsync();
            return await Task.FromResult(result);

        }

        #region Private Methods 
        #endregion


    }
}
