using Quartz;
using System.Threading.Tasks;
using System;
using System.Linq;
using NLog;

using QST.MicroERP.Service;

namespace QST.Scheduling
{
    [DisallowConcurrentExecution]
    public class MainJob : IJob
    {
        private NTF_NotificationService _ntfSvc;

        public MainJob()
        {
            _ntfSvc = new NTF_NotificationService();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Write Service Methods to be called 
            //throw new NotImplementedException();

            _ntfSvc.SendNotifications();

            await Task.CompletedTask;
        }
    }
}