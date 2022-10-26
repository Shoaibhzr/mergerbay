using MergerBay.Domain.Entities.Sectors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Seller
{
    public class SP_SELLOUTS
    {

        public Guid? SellOut_Id { get; set; }
        public string? ProjectName { get; set; }
        public Guid? BusinessLocation_Id { get; set; }
        public string? Subsidiary_Ids { get; set; } //Comma Seperated Subsidiary_Id
        public string? Sector_Ids { get; set; } //Comma Seperated Subsidiary_Id
        public string? SubSector_Ids { get; set; } //Comma Seperated Subsidiary_Id
        public string? Year_Id { get; set; }
        public string? Revenue { get; set; } //4 Years comma seperated
        public string? EBITAY { get; set; }  //4 Years comma seperated 
        public string? NetProfit { get; set; }
        public int? SellingValue { get; set; }
        public int? FixedAssets { get; set; }
        public int? InventoryValue { get; set; }
        public bool? IsBankDebit { get; set; }
        public bool? IsAuditFinancialStatement { get; set; }
        public bool? isValuationReport { get; set; }
        public Guid? TransactionRole_Id { get; set; }
        public bool? isMendate { get; set; }
        public Guid? UserId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }

        //=====Commercial Property  values
        public Guid? Property_Id { get; set; }
        public Guid? Duration_Id { get; set; }
        public int? PropertyValue { get; set; }
        public int? LandArea { get; set; }
        public int? BuiltUpArea { get; set; }
        public bool? HasContract { get; set; }
        public int? AnnualGrossIncome { get; set; }


        public bool? IsPublic { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsFeatured { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public Guid? Created_By { get; set; }
        public Guid? Updated_By { get; set; }

        //====Addditional Properties

        public string? Property { get; set; }
        public string? Role { get; set; }
        public string? BusinessLocation { get; set; }
        public string? UserName { get; set; }
        public string? Sectors { get; set; }
        public string? CountriesInterested { get; set; }


        [NotMapped]
        public List<SectorMain> SectorsArray { get; set; } = new List<SectorMain>();
    }

}
