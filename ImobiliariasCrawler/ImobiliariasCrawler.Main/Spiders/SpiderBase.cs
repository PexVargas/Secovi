using ImobiliariasCrawler.Main.Core;
using ImobiliariasCrawler.Main.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ImobiliariasCrawler.Main.Spiders
{
    public abstract class SpiderBase : SpiderAbstract
    {
        protected readonly PexinContext _context;
        private int _bufferInsertItems = 0;

        public SpiderBase(ConfigurationSpider config = null) : base(config ?? new ConfigurationSpider(new TimeSpan(0, 0, 0, 0, 1000), 10))
        {
            _context = new PexinContext();
        }

        public void Save(ImoveiscapturadosDto imoveiscapturados)
        {
            _logging.AddCountItem();
            lock (_context)
            {
                _context.Add(imoveiscapturados.ToImoveiscapturados());
                if (_bufferInsertItems++ % 100 == 0)
                {
                    _bufferInsertItems = 0;
                    try
                    {
                        _context.SaveChanges();
                    }
                    finally { }
                }
            }
        }

        public void UpdateUrlProcessada(UrlsProcessadas urlProcessada)
        {
            lock (_context)
            {
                _context.Update(urlProcessada);
            }
        }

        public override void Close()
        {
            lock (_context)
            {
                _context.SaveChanges();
                _context.Dispose();
            }
        }
    }
}
