using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.Setups
{
    [Table("Tbl_Notification_Settings")]
    public class NotificationSetting
    {
        [Key]
        public Guid Notification_Setting_Id { get; set; }
        public bool Pending_Action_MergerBay { get; set; }
        public bool Feature_Requirements { get; set; }
        public Guid User_Id { get; set; }
    }
}
