using MergerBay.Domain.Entities.Login;
using MergerBay.Domain.Entities.Sectors;
using MergerBay.Domain.Entities.Setups;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Common;
using MergerBay.Infrastructure.Interfaces.Sectors;
using MergerBay.Utilities.Services.SMTP_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupsController : ControllerBase
    {
        private readonly IGenericRepository _GenericRepository;
        private readonly ISectorRepository _SectorRepository;

        public SetupsController(IGenericRepository GenericRepository, ISectorRepository SectorRepository)
        {
            _GenericRepository = GenericRepository;
            _SectorRepository = SectorRepository;

        }
        #region GetAll Methods for All Setups
        [HttpGet("Get/Countries")]
        public async Task<IEnumerable<Country>> GetAllCountries() => await _GenericRepository.GetAllAsync<Country>();
        [HttpGet("Get/EstablishmentYears")]
        public async Task<IEnumerable<EstablishmentYears>> GetAllEstablishmentYears() => await _GenericRepository.GetAllAsync<EstablishmentYears>();
        [HttpGet("Get/OwnerShipPrefs")]
        public async Task<IEnumerable<OwnerShipPrefs>> GetAllOnwerShips() => await _GenericRepository.GetAllAsync<OwnerShipPrefs>();

        [HttpGet("Get/TransactionRoles")]
        public async Task<IEnumerable<TransactionRoles>> GetAllTransactionRoles() => await _GenericRepository.GetAllAsync<TransactionRoles>();
        
        [HttpGet("Get/Properties")]
        public async Task<IEnumerable<Property>> GetAllProperties() => await _GenericRepository.GetAllAsync<Property>();
        
        [HttpGet("Get/ContractDurations")]
        public async Task<IEnumerable<ContractDuration>> GetAllContractDurations() => await _GenericRepository.GetAllAsync<ContractDuration>();

        [HttpGet("Get/Cities/{Country_Id}")]
        public Task<IEnumerable<City>> GetAllContractDurations(Guid Country_Id) =>  _GenericRepository.Where<City>(x => x.Country_Id == Country_Id);

        [HttpGet("Get/RevenuePreferences")]
        public async Task<IEnumerable<RevenuePreference>> GetAllRevenuePreferences() => await _GenericRepository.GetAllAsync<RevenuePreference>();

        [HttpGet("Get/TransactionTypes")]
        public async Task<IEnumerable<TransactionType>> GetAllTransactionTypes() => await _GenericRepository.GetAllAsync<TransactionType>();

        [HttpGet("Get/DefaultCurrency")]
        public async Task<Currency> GetAllDefaultCurrency() => await _GenericRepository.FirstOrDefaultAsync<Currency>(x =>x.IsDefault == true);

        [HttpGet("Get/RecommendedDeals")]
        public async Task<IEnumerable<RecommendedDeal>> GetAllRecommendedDeal() => await _GenericRepository.GetAllAsync<RecommendedDeal>();

        [HttpGet("Get/SendEmail")]
        public async Task<ActionResult> SendEmail()
        {
            EmailServices.sendEmail("this is a test mail", "arslan.dev600@gmail.com");
            return Ok("Email Sent");
        }

        #endregion

        #region Sections
        [HttpPost("Get/Sectors")]
        public async Task<List<SectorMain>> Sectors(RequestSectorsItemVm CompanyData)
        {
            return await _SectorRepository.GetAllSectorsWithSubSectors(); 
        }
        [HttpGet("Get/Sectors")]
        public async Task<List<SectorMain>> Sectors()
        {
            return await _SectorRepository.GetAllSectorsWithSubSectors();
        }
        //[HttpGet("Get/SubSectors")]
        //public async Task<IEnumerable<SubSectors>> GetAllSubSectors() => await _GenericRepository.GetAllAsync<SubSectors>();
        //[HttpGet("Get/SubSectorItems")]
        //public async Task<IEnumerable<SubSector_Items>> GetAllSubSectorItems() => await _GenericRepository.GetAllAsync<SubSector_Items>();
        #endregion

        #region Post Methods for All Setups
        [HttpPost("Save/Country")]
        public async Task<ActionResult<Country>> SaveCountry(Country model) {
            model.Country_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            model.Created_By = model.Country_Id;
            model.Updated_By = model.Country_Id;

            await _GenericRepository.AddAsync<Country>(model);
            if(await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<Country>(x => x.Country_Id == model.Country_Id);
            }
            else
            {
               return BadRequest("Having Issue While Saving Data");
            }

        }
        [HttpPost("Save/EstablishmentYear")]
        public async Task<ActionResult<EstablishmentYears>> SaveEstablishmentYear(EstablishmentYears model)
        {
            model.Year_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<EstablishmentYears>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<EstablishmentYears>(x => x.Year_Id == model.Year_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/OwnerShipPref")]
        public async Task<ActionResult<OwnerShipPrefs>> SaveOwnerShipPref(OwnerShipPrefs model)
        {
            model.Pref_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<OwnerShipPrefs>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<OwnerShipPrefs>(x => x.Pref_Id == model.Pref_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/Sectors")]
        public async Task<ActionResult<Sector>> SaveSector(Sector model)
        {
            model.Sector_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<Sector>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<Sector>(x => x.Sector_Id == model.Sector_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/SubSector")]
        public async Task<ActionResult<SubSectors>> SaveSubSector(SubSectors model)
        {
            model.SubSector_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<SubSectors>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<SubSectors>(x => x.SubSector_Id == model.SubSector_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/SubSectorItem")]
        public async Task<ActionResult<SubSector_Items>> SaveSubSectorItem(SubSector_Items model)
        {
            model.Item_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<SubSector_Items>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<SubSector_Items>(x => x.Item_Id == model.Item_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/TransactionRole")]
        public async Task<ActionResult<TransactionRoles>> SaveTransactionRole(TransactionRoles model)
        {
            model.Role_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<TransactionRoles>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<TransactionRoles>(x => x.Role_Id == model.Role_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }       
        [HttpPost("Save/Property")]
        public async Task<ActionResult<Property>> SaveProperty(Property model)
        {
            model.Property_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<Property>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<Property>(x => x.Property_Id == model.Property_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }      
        [HttpPost("Save/ContractDuration")]
        public async Task<ActionResult<ContractDuration>> SaveContractDuration(ContractDuration model)
        {
            model.Duration_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<ContractDuration>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<ContractDuration>(x => x.Duration_Id == model.Duration_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }

        [HttpPost("Save/City")]
        public async Task<ActionResult<City>> SaveCity(City model)
        {
            model.City_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<City>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<City>(x => x.City_Id == model.City_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/RevenuePreference")]
        public async Task<ActionResult<RevenuePreference>> SaveRevenuePreference(RevenuePreference model)
        {
            model.Revenue_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<RevenuePreference>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<RevenuePreference>(x => x.Revenue_Id == model.Revenue_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/TransactionType")]
        public async Task<ActionResult<TransactionType>> SaveTransactionType(TransactionType model)
        {
            model.TransactionType_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<TransactionType>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<TransactionType>(x => x.TransactionType_Id == model.TransactionType_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }
        [HttpPost("Save/Currency")]
        public async Task<ActionResult<Currency>> SaveCurrency(Currency model)
        {
            model.Currency_Id = Guid.NewGuid();
            model.Created_Date = DateTime.Now;
            model.Updated_Date = DateTime.Now;
            await _GenericRepository.AddAsync<Currency>(model);
            if (await _GenericRepository.CommitChangesAsync() > 0)
            {
                return await _GenericRepository.FirstOrDefaultAsync<Currency>(x => x.Currency_Id == model.Currency_Id);
            }
            else
            {
                return BadRequest("Having Issue While Saving Data");
            }
        }

        [HttpPost("Save/RecommendedDeal")]
        public async Task<IActionResult> SaveRecommendedDeal(RecommendedDeal model)
        {
            var deal = await _GenericRepository.FirstOrDefaultAsync<RecommendedDeal>(x => x.FormId == model.FormId && x.UserId == model.UserId);
            if (deal == null)
            {
                model.RecommendationId = Guid.NewGuid();
                model.Created_Date = DateTime.Now;
                await _GenericRepository.AddAsync<RecommendedDeal>(model);
                if (await _GenericRepository.CommitChangesAsync() > 0)
                {
                    return Ok("Added as Reommendation");
                }
                else
                {
                    return BadRequest("Having Issue While Saving Data");
                }
            }
            else
            {
                return Ok("Already Exists");
            }

        }

        #endregion


    }
}
