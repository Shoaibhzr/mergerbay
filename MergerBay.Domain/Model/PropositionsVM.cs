using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Model
{
    public class PropositionsVM
    {
        public Guid FormId { get; set; }
        public string? Title { get; set; }
        public Guid? UserId { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }
        public DateTime Created_Date { get; set; }

    }
}
