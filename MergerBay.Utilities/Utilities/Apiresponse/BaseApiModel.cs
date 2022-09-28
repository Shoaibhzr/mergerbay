using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Utilities.Utilities.Apiresponse
{
    public class BaseApiModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int total { get; set; }
        public object root { get; set; }
        public int? MessageType { get; set; }
    }
}
