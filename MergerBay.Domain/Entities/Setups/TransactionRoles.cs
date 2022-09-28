using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MergerBay.Domain.Entities.Setups
{
    [Table("Tbl_TransactionRoles")]
    public class TransactionRoles
    {
        [Key]
        public Guid Role_Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public Guid Created_By { get; set; }
        public Guid Updated_By { get; set; }
    }
}
