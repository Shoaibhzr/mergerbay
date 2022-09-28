using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Setups
{
    [Table("Tbl_EstablishmentYears")]
    public class EstablishmentYears
    {

        [Key]
        public Guid Year_Id { get; set; }
        public string Year { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public Guid Created_By { get; set; }
        public Guid Updated_By { get; set; }
    }
}
