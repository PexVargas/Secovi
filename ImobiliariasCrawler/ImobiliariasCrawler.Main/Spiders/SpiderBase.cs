using HtmlAgilityPack;
using ImobiliariasCrawler.Main.Model;
using ImobiliariasCrawler.Main.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
        protected readonly PexinContext _context;

        public SpiderBase()
        {
            Items = new List<Imoveiscapturados>();
            Request = new RequestService(new HttpClient());
            _context = new PexinContext();
        }

        public void Init() {
            StartRequest();
        }

        public abstract Task StartRequest();
        public abstract void Parse(Response response);


        public void Dispose()
        {
            _context.Dispose();
            Request.Dispose();
            Console.WriteLine("Salvado");
        }
    }
}
