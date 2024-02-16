using Quartz;
using System.Threading.Tasks;
using System;
using System.Linq;
using NLog;

namespace QST.Scheduling
{
    [DisallowConcurrentExecution]
    public class MainJob : IJob
    {

        public MainJob()
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Write Service Methods to be called 
            //throw new NotImplementedException();

            await Task.CompletedTask;
        }
    }
}