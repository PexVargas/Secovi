using BootSecovi.Model;
using System;

namespace BootSecovi
{
    class Program
    {
        static void Main(string[] args)
        {
            VivaReal vivaReal = new VivaReal();
            vivaReal.Coletar("RJ", "Rio de Janeiro", 2);


            Console.WriteLine("Processo finalizado com sucesso!");
            Console.ReadKey();
        }
    }
}
