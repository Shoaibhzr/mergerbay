using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Model
{
    public class UserSignup
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string country { get; set; }
        public string? message { get; set; }
        public long  phone { get; set; }
        public Boolean accepted { get; set; }

    }
}
