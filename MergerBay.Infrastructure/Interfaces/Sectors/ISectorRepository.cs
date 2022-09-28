using MergerBay.Infrastructure.Interfaces.Common;
using MergerBay.Domain.Entities;
using MergerBay.Domain.Entities.Sectors;

namespace MergerBay.Infrastructure.Interfaces.Sectors
{
    public interface ISectorRepository : IGenericRepository
    {
        //====Additional Sectors Module Related Methods will be defined here
        Task<List<SectorMain>> GetAllSectorsWithSubSectors();
    }
}
