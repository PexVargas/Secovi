using ImobiliariasCrawler.Main.Core;
using ImobiliariasCrawler.Main.Model;
using System;
using System.Threading;

namespace ImobiliariasCrawler.Main
{
    public abstract class SpiderAbstract
    {
        protected ManageRequests Request { get; set; }
        protected readonly MonitorSpiders _logging;

        public SpiderAbstract(ConfigurationSpider configuration)
        {
            _logging = new MonitorSpiders(GetType().Name, Close);
            Request = new ManageRequests(_logging, configuration);
        }
        public void Init() {
            var thread = new Thread(() =>
            {
                _logging.Init();
                BeginRequests();
            });
            thread.Start();
        }

        public abstract void BeginRequests();
        public abstract void Parse(Response response);
        public virtual void Close() { }
    }
}
