using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBayApi.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerRepository _SellerRepository;
        public SellerController(ISellerRepository SellerRepository)
        {
            _SellerRepository = SellerRepository;
        }

        [HttpPost("Save/Seller")]
        public async Task<ActionResult<SellOutVm>> SaveSellOut(SellOut sellOut)
        {
            //return Ok(sellOut);
            sellOut.SellOut_Id = Guid.NewGuid();
            sellOut.Created_Date = DateTime.Now;
            sellOut.Updated_Date = DateTime.Now;
            if (sellOut.UserId == Guid.Empty)
            {
                sellOut.Status = StatusTypeEnum.Draft.ToString();
            }
            else
            {
                sellOut.Status = StatusTypeEnum.Pending.ToString();
            }
            await _SellerRepository.AddAsync<SellOut>(sellOut);
            if (await _SellerRepository.CommitChangesAsync() > 0)
            {
                var result = await _SellerRepository.FirstOrDefaultAsync<SellOut>(x => x.SellOut_Id == sellOut.SellOut_Id);
                bool res = await _SellerRepository.StoreSectors(sellOut);
                return Ok(new SellOutVm()
                        {
                            SellOut_Id = result.SellOut_Id,
                            UserId = result.UserId,
                            Status = result.Status,
                        }
                    );
            }
            else
            {
                return BadRequest("Unable To Save Data");
            }
        }

        [HttpPut("Update/SellerUserId")]
        public async Task<ActionResult> UpdateSellerUserId(SellOutVm sellOut)
        {
            if (await _SellerRepository.UpdateSellerUserId(sellOut) > 0)
            {
                return Ok("Data Saved Successfully");
            }
            else
            {
                return BadRequest("Unable To Save Data");
            }
        }


    }
}
