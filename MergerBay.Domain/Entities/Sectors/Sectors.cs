using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MergerBay.Domain.Entities.Sectors
{
    [Table("Tbl_SectorMain")]
    public class SectorMain
    {

        [Key]
        public Guid SectorMainId { get; set; }
        public Guid FormId { get; set; }
        public Guid SectorId { get; set; }
        public string SectorName { get; set; }
        public List<SectorDetail> SubSectorArr { get; set; } =  new List<SectorDetail>();


        //public List<SubSectors> SubSectorArr =new List<SubSectors> { };
        //public List<SubSectorsItem> SubSectorItemArr =new List<SubSectorsItem> { };
    }
    [Table("Tbl_SectorDetail")]
    public class SectorDetail
    {
        [Key]
        public Guid SectorDetailId { get; set; }
        public Guid SectorMainId { get; set; }
        public Guid SectorId { get; set; }
        public Guid SubSectorId { get; set; }
        public string SubSectorName { get; set; }
        public bool Status { get; set; }
   
        public List<SectorDetail_Items> SubSectorItemArr { get; set; } = new List<SectorDetail_Items>();
    }
    [Table("Tbl_SectorDetail_Items")]
    public class SectorDetail_Items
    {
        [Key]
        public Guid SectorDetailItemId { get; set; }
        public Guid SectorDetailId { get; set; }
        public Guid SectorId { get; set; }
        public Guid SubSectorId { get; set; }
        public Guid SubSectorItemId { get; set; }
        public string SubSectorItemName { get; set; }
        public bool Status { get; set; }
    }

    public class RequestSectorsItemVm
    {
        public string CompanyId { get; set; }

    }

    //public class SectorMain
    //{
    //    [Key]
    //    public Guid SectorMainId { get; set; }
    //    public Guid FormId { get; set; }
    //    public Guid SectorId { get; set; }
    //    public string SectorName { get; set; }
    //}

    //public class SectorDetail
    //{
    //    [Key]
    //    public Guid SectorDetailId { get; set; }
    //    public Guid SectorMainId { get; set; }
    //    public Guid SectorId { get; set; }
    //    public Guid SubSectorId { get; set; }
    //    public bool Status { get; set; }
    //    public string SubSectorName { get; set; }
    //}

    //public class SectorDetail_Items
    //{
    //    [Key]
    //    public Guid SectorDetailItemId { get; set; }
    //    public Guid SectorDetailId { get; set; }
    //    public Guid SectorId { get; set; }
    //    public Guid SubSectorId { get; set; }
    //    public Guid SubSectorItemId { get; set; }
    //    public string SubSectorItemName { get; set; }
    //    public bool Status { get; set; }
    //}
}
