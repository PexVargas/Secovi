using ImobiliariasCrawler.Main.DataObjectTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Services
{
    public class Scheduling
    {
        private readonly SemaphoreSlim _semaphore;
        private LoggingPerMinuteDto _logging;
        public Scheduling(int concurrentTasks, LoggingPerMinuteDto logging)
        {
            _semaphore = new SemaphoreSlim(concurrentTasks, concurrentTasks);
            _logging = logging;
            LoggingProcesser();
        }

        public async void LoggingProcesser()
        {
            while (true)
            {
                var timeInterval = DateTime.Now - _logging.StartProcess;
                var requestsPorMinuto = Math.Round(_logging.CounRequests / timeInterval.TotalMinutes);
                var itemsPorMinuto = Math.Round(_logging.CountItems / timeInterval.TotalMinutes);

                Console.WriteLine("SPIDER [{0}]: TEMPO TOTAL DECORRIDO [{1,10}] - REQUESTS {2,-5} - REQUEST/MINUTOS {3,-5} - ITEMS {4,-5} - ITEMS/MINUTOS {5,-5}",
                    _logging.Spider, timeInterval, _logging.CounRequests, requestsPorMinuto, _logging.CountItems, itemsPorMinuto);
                await Task.Delay(30 * 1000);
            }
        }

        public async void Add(Action action)
        {
            _logging.CounRequests++;
            await _semaphore.WaitAsync();
            var t = Task.Run(() =>{
                try{
                    action.Invoke();
                }
                catch(Exception ex){
                    Console.WriteLine($"Erro ao executar a ação: {ex.Message}");
                }
                finally {
                    _semaphore.Release();
                }
            });
        }
    }
}