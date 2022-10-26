using MergerBay.Domain.Context;
using MergerBay.Domain.Entities.Logs;
using MergerBay.Domain.Enums;
using MergerBay.Infrastructure.Interfaces.ApplicationLogs;
using MergerBay.Services.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Services.Services.ApplicationLogs
{
    public class LogRepository : GenericRepository, ILogRepository
    {
        public LogRepository(MergerBayContext Context) : base(Context)
        {

        }
        public async Task LogExceptionAsync(Exception ex)
        {
            LogEntity log = new LogEntity()
            {
                LogType = LogTypes.Error.ToString(),
                Message = this.ExceptionToMessage(ex),
                ExceptionObject = ex.ToString(),
                IsActive = true,
                Created_By = null,
                Created_Date = DateTime.Now
            };

            await Context.Logs.AddAsync(log);
            await Context.SaveChangesAsync();
        }

        private string ExceptionToMessage(Exception ex)
        {
            var exceptionMessage = ex.Message;
            var innerExceptionMessage = ex.InnerException == null ? null : ex.InnerException.Message;
            var stackTrace = ex.StackTrace;
            //var controllerName = ControllerContext RouteData.Values["controller"].ToString();
            //var actionName = FilterContext.RouteData.Values["action"].ToString();
            string logMessege =
                    "Date: " + DateTime.Now.ToString() + Environment.NewLine +
                    //"Controller: " + controllerName + Environment.NewLine +
                    //"Action: " + actionName + Environment.NewLine +
                    "Exception Message: " + exceptionMessage + Environment.NewLine +
                    "Inner Exception Message: " + innerExceptionMessage + Environment.NewLine +
                    "Stack Trace: " + stackTrace;

            return logMessege;
        }
    }
}
