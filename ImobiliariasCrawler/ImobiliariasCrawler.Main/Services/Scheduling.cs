using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Services
{
    public class Scheduling
    {
        private SemaphoreSlim _semaphore;
        public Scheduling(int concurrentTasks)
        {
            _semaphore = new SemaphoreSlim(concurrentTasks);
            Print();
        }

        public async void Print()
        {
            //while (true)
            //{
            //    lock (_processing)
            //    {
            //        var listThreads = _processing.Values.ToList().Select(p => $"Task [{p.Id}] - Completed [{p.IsCompleted}]");
            //        Console.SetCursorPosition(0, 0);
            //        Console.WriteLine($"PROCESSING [{_processing.Count}] - WAITING [{_waiting.Count}]");
            //        Console.WriteLine(string.Join("\n", listThreads));
            //    }
            //    await Task.Delay(3000);
            //}
        }

     
        public async void Add(Action action)
        {
            try
            {
                await _semaphore.WaitAsync();
                action.Invoke();
            }
            finally
            {
                _semaphore.Release();
            }

        }

        public void Remove(string url)
        {
            _processing.Remove(url);
            if (_waiting.Count > 0)
                Add(_waiting.Dequeue(), url);
        }
    }
}