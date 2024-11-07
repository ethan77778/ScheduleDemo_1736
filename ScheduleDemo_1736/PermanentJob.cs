using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleDemo_1736
{
    /// <summary>
    /// 固定工作
    /// </summary>
    public class PermanentJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"SpecifiedTimeJob 在指定時間執行: {DateTime.Now}");
            return Task.CompletedTask;
        }

    }
}
