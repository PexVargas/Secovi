using BootSecovi.Model;
using System;
using System.Threading.Tasks;

namespace BootSecovi
{
    class Program
    {
        static void Main(string[] args)
        {


            Zap zap = new Zap();
            //zap.Coletar("RJ", "Rio de Janeiro", 1);
            zap.Coletar("RJ", "Rio de Janeiro", 2);

            //VivaReal vivaReal = new VivaReal();
            //vivaReal.Coletar("RJ", "Rio de Janeiro", 1);
            //vivaReal.Coletar("RJ", "Rio de Janeiro", 2);

            Console.WriteLine("Processo finalizado com sucesso!");
             Console.ReadKey();

            //Classificador c = new Classificador();

            //c.Classificar("RJ");


        }
    }
}
