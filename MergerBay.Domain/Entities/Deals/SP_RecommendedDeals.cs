using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Deals
{
    public class SP_RecommendedDeals
    {

        public Guid? FormId { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }
        public string? Description { get; set; }
        public string? UserName { get; set; }
        public string? BusinessLocation { get; set; }
        public string? Property { get; set; }
        public string? Role { get; set; }
        public string? City { get; set; }
        public string? OwnerShipPreference { get; set; }
        public string? TransactionType { get; set; }
        public string? RevenuePreference { get; set; }
        public string? Sectors { get; set; }
        public string? Countries { get; set; }
        public string? Revenue { get; set; }
        public string? Ebitda { get; set; }
        public decimal? PropertyValue { get; set; }
        public decimal? AnnualGrossIncome { get; set; }
        public DateTime? Created_Date { get; set; }
    }
}
