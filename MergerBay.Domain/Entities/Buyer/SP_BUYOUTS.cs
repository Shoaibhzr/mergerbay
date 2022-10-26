using MergerBay.Domain.Entities.Sectors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Seller
{
    public class SP_BUYOUTS
    {
        public Guid BuyOut_Id { get; set; }
        public Guid? BusinessLocation_Id { get; set; }
        public string? Country_Ids { get; set; } //Comma Seperated Subsidiary_Id
        public Guid? City_Id { get; set; }
        public Guid? Pref_Id { get; set; }
        public Guid? TransactionType_Id { get; set; }
        public decimal? ValuationPreferenceFrom { get; set; }
        public decimal? ValuationPreferenceTo { get; set; }
        public decimal? InvestmentValueFrom { get; set; }
        public decimal? InvestmentValueTo { get; set; }
        public Guid? Revenue_Id { get; set; }
        public decimal? EBITDA { get; set; }
        public decimal? NetProfit { get; set; }
        public bool? IsFinancingRequired { get; set; }
        public Guid? Property_Id { get; set; }
        public decimal? PropertyValueFrom { get; set; }
        public decimal? PropertyValueTo { get; set; }
        public Guid? TransactionRole_Id { get; set; }
        public bool? isMendate { get; set; }
        public Guid? UserId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsFeatured { get; set; }
        public string? BusinessLocation { get; set; }
        public string? Property { get; set; }
        public string? Role { get; set; }
        public string? City { get; set; }
        public string? OwnerShipPreference { get; set; }
        public string? TransactionType { get; set; }
        public string? RevenuePreference { get; set; }
        public string? UserName { get; set; }
        public string? Sectors { get; set; }
        public string? CountriesInterested { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public Guid Created_By { get; set; }
        public Guid Updated_By { get; set; }

        [NotMapped]
        public List<SectorMain> SectorsArray { get; set; } = new List<SectorMain>();

    }

}
