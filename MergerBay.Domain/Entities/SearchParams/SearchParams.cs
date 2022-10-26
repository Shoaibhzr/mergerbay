using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Entities.SearchParams
{
    public class SearchParams
    {

        public Guid? FormId { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? Type { get; set; }

    }
}
