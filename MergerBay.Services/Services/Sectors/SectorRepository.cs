using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.Sectors;
using MergerBay.Infrastructure.Interfaces.Sectors;
using MergerBay.Services.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Services.Services.Sectors
{
    public class SectorRepository : GenericRepository, ISectorRepository
    {
        protected readonly MergerBayContext _Context;
        public SectorRepository(MergerBayContext Context) : base(Context)
        {
            this._Context = Context;
        }
        /// <summary>
        /// All Sector Related Services Will be Implemented Here......................
        /// </summary>
        /// <returns></returns>
        public async Task<List<SectorMain>> GetAllSectorsWithSubSectors()
        {
            List<SectorMain> result = new List<SectorMain>();
            SectorMain se;
            SectorDetail subsec;
            SectorDetail_Items subsecItem;
            Task.Delay(1000);
            //Reading all sectorssss
            var sectors = Context.Sectors.Where(d => d.IsActive == true && d.IsDeleted == false).ToList();
            if (sectors.Count > 0)
            {
                //===Set Sector's values
                foreach (var sec in sectors)
                {
                    try
                    {
                        se = new SectorMain();
                        se.SectorName = sec.Name;
                        se.SectorId = sec.Sector_Id;
                        //reading all subsectors of sector i.e sector -1 
                        var subSectors = Context.SubSectors.Where(s => s.Sector_Id == sec.Sector_Id && s.IsDeleted == false && s.IsActive == true).ToList();
                        if (subSectors.Count > 0)
                        {
                            foreach (var subseclst in subSectors)
                            {
                                subsec = new SectorDetail();
                                subsec.SubSectorId = subseclst.SubSector_Id;
                                subsec.SectorId = sec.Sector_Id;
                                subsec.SubSectorName = subseclst.Name;
                                subsec.Status = subseclst.Status;
                                //se.SubSectorArr.Add(subsec);
                                Task.Delay(1000);
                                var subSectorsItems = Context.SubSector_Items.Where(i => i.Sector_Id == sec.Sector_Id && i.SubSector_Id == subseclst.SubSector_Id && i.IsDeleted == false && i.IsActive == true).ToList();
                                if (subSectorsItems.Count > 0)
                                {
                                    foreach (var item in subSectorsItems)
                                    {
                                        subsecItem = new SectorDetail_Items();
                                        subsecItem.SectorId = sec.Sector_Id;
                                        subsecItem.SubSectorId = subseclst.SubSector_Id;
                                        subsecItem.SubSectorItemId = item.Item_Id;
                                        subsecItem.SubSectorItemName = item.Name;
                                        subsecItem.Status = item.Status;
                                        //se.SubSectorItemArr.Add(subsecItem);
                                        subsec.SubSectorItemArr.Add(subsecItem);

                                    }
                                }
                                se.SubSectorArr.Add(subsec);
                            }
                        }
                        result.Add(se);
                    }
                    catch (Exception er)
                    {
                        continue;
                    }

                }
            }
            return result;
        }

    }
}
