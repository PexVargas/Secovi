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
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Spiders
{

    public abstract class SpiderBase : IDisposable
    {
        public List<Imoveiscapturados> Items { get; set; }
        public RequestService Request { get; set; }
        private readonly PexinContext _context;
        private LoggingPerMinuteDto _logging;
        private readonly int _buffer;
        private int countTotalInsert = 0;
        private int lastSkip = 0;

        public SpiderBase(int buffer = 10, int concurrentRequests = 10)
        {
            _buffer = buffer;
            _logging = new LoggingPerMinuteDto() { Spider = GetType().Name };
            Items = new List<Imoveiscapturados>();
            Request = new RequestService(new HttpClient(), _logging, Close);
            _context = new PexinContext();
        }
        public void Init() {
            StartRequest();
        }
    

        public abstract void StartRequest();
        public abstract void Parse(Response response);
        public void Save(ImoveiscapturadosDto imoveiscapturados)
        {
            _logging.CountItems++;
            Items.Add(imoveiscapturados.ToImoveiscapturados());
            InsertItems();
        }

        private void InsertItems(bool close = false)
        {
            if (Items.Count <= _buffer && !close) return;
            lock (_context)
            {
                var newList = Items.Skip(lastSkip).Take(_buffer).ToList();
                countTotalInsert += newList.Count;
                lastSkip += _buffer;
                _context.AddRange(newList);
                _context.SaveChanges();
            }
        }

        public virtual void Close()
        {
            InsertItems();
            Dispose();
            Console.WriteLine($"FINISH SPIDER [{_logging.Spider}] - Inseridos [{countTotalInsert}] - Requests [{_logging.CounRequests}] - Items [{_logging.CountItems}]");
        }

        public void Dispose()
        {
            _context.Dispose();
            Request.Dispose();
        }
    }
}
