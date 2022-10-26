using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Model
{
    public class NotificationSettingModel
    {
        public string User_Id { get; set; }
        public bool IsPendingAction { get; set; }
        public bool IsFeatureRequirements { get; set; }
    }
}
