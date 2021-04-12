using HtmlAgilityPack;
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
        public List<ImoveiscapturadosDto> Items { get; set; }
        public RequestService Request { get; set; }
        private readonly PexinContext _context;

        public SpiderBase()
        {
            Items = new List<ImoveiscapturadosDto>();
            Request = new RequestService(new HttpClient());
            _context = new PexinContext();
        }
        public void Init() {
            StartRequest();
        }

        public abstract void StartRequest();
        public abstract void Parse(Response response);

        public void Save(ImoveiscapturadosDto imoveiscapturados)
        {
            lock (_context)
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine(imoveiscapturados.Url);
                _context.Imoveiscapturados.AddRange(Items.Select(i => i.ToImoveiscapturados()));
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            Request.Dispose();
            Console.WriteLine("Salvado");
        }
    }
}
