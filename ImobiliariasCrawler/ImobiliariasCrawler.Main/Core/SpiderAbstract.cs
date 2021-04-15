using ImobiliariasCrawler.Main.Model;
using System;
using System.Threading;

namespace ImobiliariasCrawler.Main
{
    public abstract class SpiderAbstract
    {
        protected ManageRequests Request { get; set; }
        protected readonly MonitorSpiders _logging;

        public SpiderAbstract()
        {
            _logging = new MonitorSpiders(GetType().Name, Close);
            Request = new ManageRequests(_logging);
        }
        public void Init() {
            var thread = new Thread(() =>
            {
                _logging.Init();
                StartRequest();
            });
            thread.Start();
        }

        public abstract void StartRequest();
        public abstract void Parse(Response response);
        public virtual void Close() { }
    }
}
