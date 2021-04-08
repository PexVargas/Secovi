using System;
using ImobiliariasCrawler.Main.Spiders;

namespace ImobiliariasCrawler.Main
{

    class Program
    {
        static void Main(string[] args)
        {
            //var dLegend = new DLegend();
            //dLegend.Init();
            //Console.ReadKey();

            //var creditoReal = new CreditoReal();
            //creditoReal.Init();
            //Console.ReadKey();

            //var garcia = new Garcia();
            //garcia.Init();
            //Console.ReadKey();

            //var guarita = new Guarita();
            //guarita.Init();
            //Console.ReadKey();

            var leindecker = new Leindecker();
            leindecker.Init();
            Console.ReadKey();

        }
    }
}
