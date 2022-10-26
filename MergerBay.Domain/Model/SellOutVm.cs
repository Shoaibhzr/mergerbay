using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Domain.Model
{
    public class SellOutVm
    {
        public Guid? FormId { get; set; }
        public Guid? UserId { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsFeatured { get; set; }

    }
}
