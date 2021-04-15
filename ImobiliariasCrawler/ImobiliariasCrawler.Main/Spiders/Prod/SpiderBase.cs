using ImobiliariasCrawler.Main.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ImobiliariasCrawler.Main.Spiders
{
    public abstract class SpiderBase : SpiderAbstract
    {
        public MenageRequest Request { get; set; }
        private readonly PexinContext _context;
        private readonly MonitorSpiders _logging;
        private int _bufferInsertItems = 0;

        public SpiderBase() : base()
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

        public override void Close()
        {
            lock (_context)
            {
                _context.SaveChanges();
                _context.Dispose();
                Console.WriteLine($"FINISH SPIDER [{_logging.Spider}] - Requests [{_logging.CountTotalRequests}] - Items [{_logging.CountItems}]");
            }
        }
    }


    public class Widgets { }

    public abstract class AbstractClass<T>
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public abstract List<T> Items { get; set; }
        public abstract void Add<T>(T item);
    }

    public class Container : AbstractClass<Widgets>
    {
        public override List<Widgets> Items { get; set; }

        public override void Add<T>(T item)
        {
            throw new NotImplementedException();
        }
    }
}
