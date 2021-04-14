using ImobiliariasCrawler.Main.DataObjectTransfer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Services
{
    public class Scheduling
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly LoggingPerMinuteDto _logging;
        private readonly TimeSpan _timeIntervalOpenTask;
        public Scheduling(int concurrentTasks, TimeSpan timeIntervalOpenTask, LoggingPerMinuteDto logging)
        {
            _timeIntervalOpenTask = timeIntervalOpenTask;
            _semaphore = new SemaphoreSlim(concurrentTasks, concurrentTasks);
            _logging = logging;
        }

        public async void Add(Action action)
        {
            await _semaphore.WaitAsync();
            await Task.Delay(_timeIntervalOpenTask);
            try{
                action.Invoke();
            }
            catch (Exception ex){
                Console.WriteLine($"Erro ao executar a ação: {ex.Message}");
            }
            finally {
                _semaphore.Release();
            }
        }
    }
}