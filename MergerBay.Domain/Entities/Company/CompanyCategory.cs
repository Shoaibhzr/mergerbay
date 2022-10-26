
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Company
{
 
    [Table("Tbl_Company_Category")]
    public class CompanyCategory
    {
        [Key]
        public Guid Company_Category_ID { get; set; }
        public string Category_Name { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public Guid Created_by { get; set; }
        public DateTime Created_Date { get; set; }
        public Guid Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }
    }

    [Table("Tbl_Company_Category_Store")]
    public class CompanyCategoryStore
    {
        [Key]
        public Guid Category_Store_Id { get; set; }
        public Guid Category_Id { get; set; }
        public Guid User_Id { get; set; }
        public bool Is_active { get; set; }
        public bool Is_Deleted { get; set; }
        public Guid Created_by { get; set; }
        public DateTime Created_Date { get; set; }
        public Guid Modified_by { get; set; }

        public DateTime Modified_Date { get; set; }
    }
}
