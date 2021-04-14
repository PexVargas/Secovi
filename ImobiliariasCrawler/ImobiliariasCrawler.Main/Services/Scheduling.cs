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
        private readonly TimeSpan _timeIntervalOpenTask;
        private readonly Action _callbackFinish;
        private int _countControlStop = 0;
        public Scheduling(int concurrentTasks, TimeSpan timeIntervalOpenTask, LoggingPerMinuteDto logging, Action callbackFinish)
        {
            _callbackFinish = callbackFinish;
            _timeIntervalOpenTask = timeIntervalOpenTask;
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

                Console.WriteLine("[{0}] - SPIDER [{1}] - REQUESTS {2,-5} - REQUEST/MINUTOS {3,-5} - ITEMS {4,-5} - ITEMS/MINUTOS {5,-5}",
                    timeInterval, _logging.Spider, _logging.CounRequests, requestsPorMinuto, _logging.CountItems, itemsPorMinuto);
                await Task.Delay(60 * 1000);
            }
        }

        public async void Add(Action action)
        {
            await _semaphore.WaitAsync();
            await Task.Delay(_timeIntervalOpenTask);
            _logging.CounRequests++;
            var t = Task.Run(() =>{
                try{
                    _countControlStop++;
                    action.Invoke();
                }
                catch(Exception ex){
                    Console.WriteLine($"Erro ao executar a ação: {ex.Message}");
                }
                finally {
                    _countControlStop--;
                    _semaphore.Release();
                    if (_countControlStop == 0)
                        _callbackFinish.Invoke();
                }
            });
        }
    }
}