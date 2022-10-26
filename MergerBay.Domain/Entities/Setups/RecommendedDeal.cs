using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MergerBay.Domain.Entities.Setups
{
    [Table("Tbl_RecommendedDeals")]
    public class RecommendedDeal
    {
        [Key]
        public Guid RecommendationId { get; set; }
        public Guid? FormId { get; set; }
        public Guid? UserId { get; set; }
        public string? Type { get; set; }
        public DateTime? Created_Date { get; set; }
    }
}
