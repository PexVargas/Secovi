using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.DataObjectTransfer
{
    public class LoggingPerMinuteDto
    {
        public LoggingPerMinuteDto(string spiderName, Action callbackFinish) { Spider = spiderName; _callbackFinish = callbackFinish; }
        public string Spider { get; private set; }
        public int CountItems { get; private set; }
        public int CountTotalRequests { get; private set; }
        public int CountOpenRequests { get; private set; }
        public DateTime StartProcess { get; private set; } = DateTime.Now;

        private DateTime lastUpdateSpider;
        private readonly Action _callbackFinish;

        public bool Finish { get; private set; } = false;

        public void AddCountItem() {
            lastUpdateSpider = DateTime.Now;
            CountItems++;
        }
        public void CloseRequest() => CountOpenRequests--;

        public void AddCountRequest()
        {
            lastUpdateSpider = DateTime.Now;
            CountTotalRequests++;
            CountOpenRequests++;
        }

        public void Init()
        {
            lastUpdateSpider = DateTime.Now;
            StatisticsCollect();
            MonitoringFinishSpider();
        }

        private void StatisticsCollect()
        {
            Task.Run(async () =>
            {
                Console.Clear();
                while (true && !Finish)
                {
                    var timeInterval = DateTime.Now - StartProcess;
                    var requestsPorMinuto = Math.Round(CountTotalRequests / timeInterval.TotalMinutes);
                    var itemsPorMinuto = Math.Round(CountItems / timeInterval.TotalMinutes);

                    var message = String.Format("[{0}] - SPIDER [{1}] - REQUESTS {2,-5} - REQUEST/MINUTOS {3,-5} - ITEMS {4,-5} - ITEMS/MINUTOS {5,-5}",
                                                timeInterval, Spider, CountTotalRequests, requestsPorMinuto, CountItems, itemsPorMinuto);
                    Console.WriteLine(message);
                    await Task.Delay(60 * 1000);
                }
            });
        }


        private void MonitoringFinishSpider()
        {
            Task.Run(async () =>
            {
                while (true && !Finish)
                {
                    await Task.Delay(10 * 1000);
                    if ((DateTime.Now - lastUpdateSpider).TotalSeconds > 30 && !Finish)
                    {
                        Finish = true;
                        _callbackFinish.Invoke();
                    }
                }
            });
        }

    }
}
