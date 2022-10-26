using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Logs
{
    [Table("Tbl_Logs")]
    public class LogEntity
    {
        [Key]
        public Guid LogId { get; set; }
        public string? LogType { get; set; }
        public string? Message { get; set; }
        public string? ExceptionObject { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        public Guid? Created_By { get; set; }
        public Guid? Updated_By { get; set; }
    }
}
