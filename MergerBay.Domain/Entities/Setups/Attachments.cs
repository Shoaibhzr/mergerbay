using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Setups
{
    [Table("Tbl_Attachments")]
    public class Attachments
    {
        [Key]
        public Guid Attachment_ID { get; set; }
        public string File_Name { get; set; }
        public string File_Type { get; set; }
        public DateTime Created_Date { get; set; }
        public Guid Created_by { get; set; }
        public DateTime Modified_Date { get; set; }
        public Guid Modified_by { get; set; }
        public bool Is_Deleted { get; set; }
        public bool Is_Active { get; set; }
        public Guid Source_ID { get; set; }
        public string Source_Type { get; set; }

    }
}
