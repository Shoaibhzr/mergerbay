using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.Sectors;
using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBay.Services.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace MergerBay.Services.Services.Seller
{
    public class SellerRepository :GenericRepository, ISellerRepository
    {
        protected readonly MergerBayContext _Context;
        public SellerRepository(MergerBayContext Context):base(Context)
        {
            this._Context = Context;
        }

        //====Additional ISeller Contract Will be implemented here
        public async Task<int> UpdateSellerUserId(SellOutVm seller)
        {
            var res = await _Context.Set<SellOut>().FirstOrDefaultAsync(x => x.SellOut_Id == seller.SellOut_Id);
            if (res == null) return 0;
            res.UserId = seller.UserId;
            res.Status = seller.Status;
            return await _Context.SaveChangesAsync();
        }

        public async Task<bool> StoreSectors(SellOut seller)
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
                catch(Exception ex)
                {

                };
              // return await _Context.SaveChangesAsync()>0 ? true:false;
              

            }
            return true;
        }
    }
}
