using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Setups
{
    [Table("Tbl_City")]
    public class City
    {
        [Key]
        public Guid City_Id { get; set; }
        public Guid Country_Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        public Guid? Created_By { get; set; }
        public Guid? Updated_By { get; set; }
    }
}
