using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Seller
{
    [Table("Tbl_Form")]
    public class Form
    {
        [Key]
        public Guid Form_Id { get; set; }
        public string? ProjectName { get; set; }
        public Guid? BusinessLocation_Id { get; set; }
        public string? Subsidiary_Ids { get; set; } //Comma Seperated Subsidiary_Id
        public Guid? City_Id { get; set; }
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
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public Guid Created_By { get; set; }
        public Guid Updated_By { get; set; }

    }
}
