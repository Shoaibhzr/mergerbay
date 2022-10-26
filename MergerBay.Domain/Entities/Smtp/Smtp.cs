using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Smtp
{
    [Table("Tbl_Smtp")]
    public class Smtp
    {
        [Key]
        public Guid Smtp_Id { get; set; }
        public string From_Email { get; set; }
        public string To_Email { get; set; }
        public string Server { get; set; }
        public string Login_UserName { get; set; }
        public string Login_Password { get; set; }
        public int Port { get; set; }
        public bool Is_SSL { get; set; }
    }
}
