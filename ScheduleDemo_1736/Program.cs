using Quartz.Impl;
using Quartz;

namespace ScheduleDemo_1736
{
    internal class Program
    {
        //使用async是因為啟動排程器 (scheduler.Start())、排程作業 (scheduler.ScheduleJob())
        //和關閉排程器 (scheduler.Shutdown())需要一定時間如果沒使用async異步會導致程式阻塞
        static async Task Main(string[] args)
        {
            // 讓使用者輸入每隔 N 秒執行的間隔時間
            Console.Write("請輸入每隔 N 秒執行的間隔時間：");
           
            int intervalInSeconds;//儲存使用者輸入的變數
            while (true)
            {
                // 嘗試解析使用者輸入的秒數
                //int.TryParse(Console.ReadLine(), out intervalInSeconds)為將使用者輸入的數字轉換成整數
                //並丟到 intervalInSeconds變數中
                if (int.TryParse(Console.ReadLine(), out intervalInSeconds) && intervalInSeconds > 0)
                {
                    break;  // 如果成功並且大於0，跳出循環
                }
                else
                {
                    // 輸入無效時，提示錯誤並要求重新輸入
                    Console.WriteLine("請輸入有效的正整數秒數！");
                    Console.Write("請輸入每隔 N 秒執行的間隔時間：");
                }
            }

            // 讓使用者輸入指定時間（格式：yyyy-MM-dd HH:mm:ss）
            DateTime specifiedTime;
            while (true)
            {
                Console.Write("請輸入指定的時間 (yyyy-MM-dd HH:mm:ss)：");
                string userInput = Console.ReadLine();

                // 嘗試解析使用者輸入的時間
                //DateTime.TryParseExact為驗證時間格式的方法()
                //第一個參數userInput為使用者所輸入的時間
                //第二哥參數"yyyy-MM-dd HH:mm:ss"為設定時間隔式
                //第三個參數null為用來指定解析過程中的地區設置，null表示使用系統的默認設置來解析日期時間
                //第四個參數 out specifiedTime將輸入的時間存到此變數中
                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out specifiedTime))
                {
                    break;  // 如果解析成功，跳出循環
                }
                else
                {
                    // 輸入無效時，提示錯誤並要求重新輸入
                    Console.WriteLine("輸入的時間格式無效！請使用 yyyy-MM-dd HH:mm:ss 格式");
                }
            }

            Console.WriteLine($"您輸入的間隔時間為：{intervalInSeconds} 秒");
            Console.WriteLine($"您輸入的指定時間為：{specifiedTime}");


            // 建立 Scheduler 為調度器，是用來負責啟動和停止任務的執行，根據觸發器來安排任務的執行
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            //StdSchedulerFactory.GetDefaultScheduler()為創建一個預設的排程器
            
            await scheduler.Start();
            // scheduler.Start()啟動排程器，排程器啟動後，它會等待任務觸發並執行。

            //創建一個第一個任務，<Job> 裡面有定義要做什麼工作
            //JobDetail為告訴排程器作業的類型
            IJobDetail job1 = JobBuilder.Create<Job>().Build();
            
           //創建觸發器，觸發器為定義何時執行任務與執行間隔
            ITrigger trigger1 = TriggerBuilder.Create()
                //指定任務從現在開始
                .StartNow()
               
                //WithIntervalInSeconds(intervalInSeconds)這個方法為設置任務每次的執行間隔
                //RepeatForever()為無限重複執行
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(intervalInSeconds).RepeatForever())
                .Build();

            //scheduler.ScheduleJob(job1, trigger1)為將這個任務和觸發器註冊到排程器中，並開始執行這個任務。
            await scheduler.ScheduleJob(job1, trigger1);

            // 設定第二個排程 - 指定時間點執行
            IJobDetail job2 = JobBuilder.Create<PermanentJob>().Build();
            ITrigger trigger2 = TriggerBuilder.Create()
                .StartAt(specifiedTime)
                .Build();
            await scheduler.ScheduleJob(job2, trigger2);

            Console.WriteLine("排程已設定完成。按任意鍵結束...");
            // Console.ReadKey();當使用者輸入任意建會暫停程式碼
            Console.ReadKey();
            //關閉排程器
            await scheduler.Shutdown();

        }
    }
}
