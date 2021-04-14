using HtmlAgilityPack;
using ImobiliariasCrawler.Main.DataObjectTransfer;
using ImobiliariasCrawler.Main.Model;
using ImobiliariasCrawler.Main.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Spiders
{

    public abstract class SpiderBase
    {
        public List<Imoveiscapturados> Items { get; set; }
        public RequestService Request { get; set; }
        private readonly PexinContext _context;
        private LoggingPerMinuteDto _logging;
        private int insertItems = 0;

        public SpiderBase()
        {
            _logging = new LoggingPerMinuteDto(GetType().Name, Close);
            Items = new List<Imoveiscapturados>();
            Request = new RequestService(_logging);
            _context = new PexinContext();
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
        public void Save(ImoveiscapturadosDto imoveiscapturados)
        {
            _logging.AddCountItem();
            insertItems++;
            lock (_context)
            {
                _context.Add(imoveiscapturados.ToImoveiscapturados());
                if (insertItems > 30)
                {
                    try{
                        _context.SaveChanges();
                    }
                    finally{
                        insertItems = 0;
                    }
                }
            }
        }

        public virtual void Close()
        {
            lock (_context)
            {
                _context.SaveChanges();
                _context.Dispose();
                Console.WriteLine($"FINISH SPIDER [{_logging.Spider}] - Requests [{_logging.CountTotalRequests}] - Items [{_logging.CountItems}]");
            }
        }
    }
}
