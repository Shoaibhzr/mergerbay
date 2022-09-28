using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Setups
{
    [Table("Tbl_SubSector_Items")]
    public class SubSector_Items
    {
        [Key]
        public Guid Item_Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public Guid Sector_Id { get; set; }
        public Guid SubSector_Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public Guid Created_By { get; set; }
        public Guid Updated_By { get; set; }
    }
}
