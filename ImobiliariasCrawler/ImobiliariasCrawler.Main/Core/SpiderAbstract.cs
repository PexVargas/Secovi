using ImobiliariasCrawler.Main.Model;
using System;
using System.Threading;

namespace ImobiliariasCrawler.Main
{
    public abstract class SpiderAbstract
    {
        public MenageRequest Request { get; set; }
        private readonly MonitorSpiders _logging;

        public SpiderAbstract()
        {
            _logging = new MonitorSpiders(GetType().Name, Close);
            Request = new MenageRequest(_logging);
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
        public abstract void Close();
    }
}
