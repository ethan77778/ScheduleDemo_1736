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
        public async Task Execute(IJobExecutionContext context)
        //Execute 方法作用是被排程器在指定時間內呼叫並執行一次指定的工作。
        {
            string Message = $"進行修改{DateTime.Now}\n";
            string FilePath = @"C:\Users\user\Desktop\apple\apple.txt";
            try
            {
                //StreamWriter這是一個用來寫入檔案的類別
                //如果設為 true，資料將會被寫入檔案末尾，而不會覆蓋現有的資料。
                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    await writer.WriteLineAsync(Message);
                    await writer.FlushAsync();
                }
                Console.WriteLine(Message);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            };
            //File.AppendAllText(@"C:\Users\user\Desktop\apple", Message);
            //Console.WriteLine(Message);
            ////Console.WriteLine($"IntervalJob 執行時間: {DateTime.Now}");
            //return Task.CompletedTask;
            ////返回 Task.CompletedTask 意思是Console.WriteLine($"IntervalJob 執行時間: {DateTime.Now}")已經執行完成
            return;
        }

    }

}
