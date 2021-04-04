using System;
using ImobiliariasCrawler.Main.Spiders;

namespace ImobiliariasCrawler.Main
{

    class Program
    {
        static void Main(string[] args)
        {
            var spider = new CreditoReal();
            spider.Init();
            Console.ReadKey();
        }
    }
}
