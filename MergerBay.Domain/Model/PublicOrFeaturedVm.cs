using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Model
{
    public class PublicOrFeaturedVm
    {
        public Guid FormId { get; set; }
        public bool IsPublic { get; set; }
        public bool IsFeatured { get; set; }

    }
}
