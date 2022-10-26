using MergerBay.Infrastructure.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Infrastructure.Interfaces.ApplicationLogs
{
    public interface ILogRepository : IGenericRepository
    {
        Task LogExceptionAsync(Exception ex);
    }
}
