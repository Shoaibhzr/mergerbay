using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.Deals;
using MergerBay.Domain.Entities.SearchParams;
using MergerBay.Domain.Entities.Seller;
using MergerBay.Domain.Entities.User;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Seller;
using MergerBay.Utilities;
using MergerBay.Utilities.Services.SMTP_Services;
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
        protected readonly MergerBayContext _Context;
        public SellerController(ISellerRepository SellerRepository,MergerBayContext Context)
        {
            _SellerRepository = SellerRepository;
            _Context = Context;
            var smtpDetail= _Context.Smtp.FirstOrDefault();
            Config.From_Email = smtpDetail.From_Email;
            Config.To_Email = smtpDetail.To_Email;
            Config.Port = smtpDetail.Port;
            Config.Server=smtpDetail.Server;
            Config.Is_SSL = smtpDetail.Is_SSL;
            Config.Login_Password=smtpDetail.Login_Password;
            Config.Login_UserName = smtpDetail.Login_UserName;

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
                if(sellOut.SectorsArray!= null && sellOut.SectorsArray.Count > 0) 
                    await _SellerRepository.StoreSectorsSellOut(sellOut);

                //==Sending Mails
                var user =await _SellerRepository.FirstOrDefaultAsync<Tbl_User_Management>(x => x.UserId == result.UserId);
                string emailBody = "Thank You For Making Propositions to Merger Bay \n";
                var sendMail = false;
                if(result.IsBankDebit ==  false)
                {
                    emailBody += "\n We'll help You Making Bank Debts \n";
                    sendMail = true;
                } 
                if(result.isValuationReport ==  false)
                {
                    emailBody += "\n We'll help You Making Valuation Report \n";
                    sendMail = true;
                } if(result.IsAuditFinancialStatement ==  false)
                {
                    emailBody += "\n We'll help You Making Audit Financing Statements \n";
                    sendMail = true;

                }
                try
                {
                   if(sendMail) EmailServices.Send_Email(user.User_Joined_Email,$"Proposition Recevied: {result.Type} | {result.SubType}",emailBody);
                }
                catch (Exception ex)
                {

                }
                return Ok(new SellOutVm()
                        {
                            FormId = result.SellOut_Id,
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
        [HttpPost("Save/Buyer")]
        public async Task<ActionResult<SellOutVm>> SaveBuyOut(BuyOut buyOut)
        {
            //return Ok(sellOut);
            buyOut.BuyOut_Id = Guid.NewGuid();
            buyOut.Created_Date = DateTime.Now;
            buyOut.Updated_Date = DateTime.Now;
            if (buyOut.UserId == Guid.Empty)
            {
                buyOut.Status = StatusTypeEnum.Draft.ToString();
            }
            else
            {
                buyOut.Status = StatusTypeEnum.Pending.ToString();
            }
            await _SellerRepository.AddAsync<BuyOut>(buyOut);
            if (await _SellerRepository.CommitChangesAsync() > 0)
            {
                var result = await _SellerRepository.FirstOrDefaultAsync<BuyOut>(x => x.BuyOut_Id == buyOut.BuyOut_Id);
                if (buyOut.SectorsArray != null && buyOut.SectorsArray.Count > 0)
                    await _SellerRepository.StoreSectorsBuyOut(buyOut);

                //==Sending Mails
                var user = await _SellerRepository.FirstOrDefaultAsync<Tbl_User_Management>(x => x.UserId == result.UserId);
                string emailBody = "Thank You For Making Propositions to Merger Bay \n";
                var sendMail = false;
                if (result.IsFinancingRequired == true)
                {
                    emailBody += "\n We'll Offer You fincancing \n";
                    sendMail = true;
                }
                try
                {
                   if(sendMail) EmailServices.Send_Email(user.User_Joined_Email, $"Proposition Recevied: {result.Type} | {result.SubType}",emailBody);
                }
                catch (Exception ex)
                {

                }

                return Ok(new SellOutVm()
                {
                    FormId = result.BuyOut_Id,
                    UserId = result.UserId,
                    Status = result.Status,
                });
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

        [HttpGet("Get/Propositions/{userId}")]
        public async Task<ActionResult<IEnumerable<SellOut>>> GetPropositions(Guid userId)
        {
            return Ok(await _SellerRepository.GetPrepositions(userId));
        }
        [HttpGet("Get/FeaturedSellOuts")]
        public async Task<ActionResult<IEnumerable<SP_SELLOUTS>>> FeaturedSellOuts([FromQuery] SearchParams searchParams)
        {
            var result = await _SellerRepository.GetSellOuts(searchParams);
            return Ok(result.Where(x => x.IsFeatured == true && x.IsPublic == true).OrderByDescending(x => x.Created_Date).Take(4));
            //return Ok(await _SellerRepository.GetPrepositions(userId));
        }
        [HttpGet("Get/FeaturedBuyOuts")]
        public async Task<ActionResult<IEnumerable<SP_BUYOUTS>>> FeaturedBuyOuts([FromQuery] SearchParams searchParams)
        {
            var result = await _SellerRepository.GetBuyOuts(searchParams);
            return Ok(result.Where(x => x.IsFeatured == true && x.IsPublic == true).OrderByDescending(x => x.Created_Date).Take(4));
        }
        [HttpGet("Get/AllSellOuts")]
        public async Task<ActionResult<IEnumerable<SP_SELLOUTS>>> GetAllSellOuts([FromQuery] SearchParams searchParams)
        {
            var result = await _SellerRepository.GetSellOuts(searchParams);
            return Ok(await Task.FromResult(result.OrderByDescending(x => x.Created_Date)));
        }
        [HttpGet("Get/AllBuyOuts")]
        public async Task<ActionResult<IEnumerable<SP_BUYOUTS>>> GetAllBuyOuts([FromQuery] SearchParams searchParams)
        {
            var result = await _SellerRepository.GetBuyOuts(searchParams);
            return Ok(await Task.FromResult(result.OrderByDescending(x => x.Created_Date)));
        }
        [HttpGet("Get/RecommendedDeals/{userId}")]
        public async Task<ActionResult<IEnumerable<SP_RecommendedDeals>>> RecommendedDeals(Guid? userId)
        {
            var result = await _SellerRepository.GetRecommendedDeals(userId);
            return Ok(result.OrderByDescending(x => x.Created_Date).Take(4));
        }
        [HttpGet("Get/BuyOutData")]
        public async Task<ActionResult<SP_BUYOUTS>> BuyOutData([FromQuery] SearchParams searchParams)
        {
            var result = await _SellerRepository.GetBuyOuts(searchParams);
            return Ok(await Task.FromResult(result.FirstOrDefault()));

        }
        [HttpGet("Get/SellOutData")]
        public async Task<ActionResult<SP_SELLOUTS>> SellOutData([FromQuery] SearchParams searchParams)
        {
            var result = await _SellerRepository.GetSellOuts(searchParams);
            return Ok(await Task.FromResult(result.FirstOrDefault()));

        }
        [HttpPost("Update/SellerByAdmin")]
        public async Task<ActionResult> UpdateSellerByAdmin(SellOutVm[] model)
        {
     
                foreach (var item in model)
                {
                    var sellOut = await _SellerRepository.FirstOrDefaultAsync<SellOut>(x => x.SellOut_Id == item.FormId);
                    if (sellOut == null) return BadRequest("Invalid Form Id");
                    else
                    {
                        //===Data to be updated here=============
                        sellOut.IsPublic = item.IsPublic;
                        sellOut.IsFeatured = item.IsFeatured;
                        sellOut.Status = item.Status;
                        await _SellerRepository.CommitChangesAsync();
                        //await _SellerRepository.Update<BuyOut>(buyOut);
                    }

                }
                 return Ok("Data Updated Sucessfully");
        }



        
        [HttpPost("Update/BuyerByAdmin")]
        public async Task<ActionResult> UpdateBuyerByAdmin(SellOutVm[] model)
        {
            foreach (var item in model)
            {
                var buyOut = await _SellerRepository.FirstOrDefaultAsync<BuyOut>(x => x.BuyOut_Id == item.FormId);
                if (buyOut == null) return BadRequest("Invalid Form Id");
                else
                {
                    //===Data to be updated here=============
                    buyOut.IsPublic = item.IsPublic;
                    buyOut.IsFeatured = item.IsFeatured;
                    buyOut.Status = item.Status;
                    await _SellerRepository.CommitChangesAsync();
                    //await _SellerRepository.Update<BuyOut>(buyOut);
                }

            }
            return Ok("Data Updated Sucessfully");
        }



    }
}
