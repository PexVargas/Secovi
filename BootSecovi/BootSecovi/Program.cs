using BootSecovi.Model;
using System;

namespace BootSecovi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Zap zap = new Zap();
            // zap.Coletar("RJ", "Rio de Janeiro", 2);

            //VivaReal vivaReal = new VivaReal();
            //vivaReal.Coletar("RJ", "Rio de Janeiro", 2);

            //Classificador c = new Classificador();

            //c.Classificar("RJ");

            GeradorCSV.GeradorZapRJ();
        }
    }
}
