using System;
using System.Reflection;
using ImobiliariasCrawler.Main.Spiders;

namespace ImobiliariasCrawler.Main
{

    class Program
    {
        static void Main(string[] args)
        {
            var typeSpider = Type.GetType($"ImobiliariasCrawler.Main.Spiders.{args[0]}");
            var assemblySpiders = Activator.CreateInstance(typeSpider) as SpiderBase;
            assemblySpiders.Init();

            Console.Read();
        }
    }
}
