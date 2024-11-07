using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleDemo_1736
{
    /// <summary>
    /// N秒執行
    /// </summary>
    public class Job:IJob
    {
        public Task Execute(IJobExecutionContext context)
        //Execute 方法作用是被排程器在指定時間內呼叫並執行一次指定的工作。
        {

            Console.WriteLine($"IntervalJob 執行時間: {DateTime.Now}");
            return Task.CompletedTask;
            //返回 Task.CompletedTask 意思是Console.WriteLine($"IntervalJob 執行時間: {DateTime.Now}")已經執行完成
        }
    }
}
