using System;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main
{
    public class MonitorSpiders
    {
        public MonitorSpiders(string spiderName, Action callbackFinish) { Spider = spiderName; _callbackFinish = callbackFinish; }
        public string Spider { get; private set; }
        public int CountItems { get; private set; }
        public int CountTotalRequests { get; private set; }
        public int CountOpenRequests { get; private set; }
        public int CountDuplicateRequests { get; private set; }
        public int CountRequestsError { get; private set; }
        public DateTime StartProcess { get; private set; } = DateTime.Now;

        private DateTime lastUpdateSpider;
        private readonly Action _callbackFinish;

        public bool Finish { get; private set; } = false;

        public void AddCountDuplicateRequest() => CountDuplicateRequests++;
        public void AddCountRequestError() => CountRequestsError++;
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

        private void Print()
        {
            var timeInterval = DateTime.Now - StartProcess;
            var requestsPorMinuto = Math.Round(CountTotalRequests / timeInterval.TotalMinutes);
            var itemsPorMinuto = Math.Round(CountItems / timeInterval.TotalMinutes);

            var message = String.Format("[{0}] - SPIDER [{1}] - REQUESTS {2,-5} - REQUEST/MINUTOS {3,-5} - ITEMS {4,-5} - ITEMS/MINUTOS {5,-5} - REQUESTS/DUPLICADOS {6,-5} - REQUESTS/ERROR {7,-5}",
                                        timeInterval, Spider, CountTotalRequests, requestsPorMinuto, CountItems, itemsPorMinuto, CountDuplicateRequests, CountRequestsError);
            Console.WriteLine(message);
        }

        private void StatisticsCollect()
        {
            Task.Run(async () =>
            {
                while (true && !Finish)
                {
                    Print();
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
                         Console.WriteLine("---------------------------------------------- FINISH ----------------------------------------------");
                        Print();
                        Finish = true;
                        _callbackFinish.Invoke();
                    }
                }
            });
        }

    }
}