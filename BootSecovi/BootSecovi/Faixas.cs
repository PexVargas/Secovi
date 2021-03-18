using System;
using System.Collections.Generic;
using System.Text;

namespace BootSecovi
{
    public class Faixas
    {



        public Dictionary<string, string> GetfaixaAluguel()
        {
            Dictionary<string, string> faixaMonitorada = new Dictionary<string, string>();

            faixaMonitorada.Add("0", "100");
            var minimo = 0;
            var maximo = 100;
            var incremento = 100;

            

            for (int i = 0; i < 40; i++)
            {
                if (minimo > 0)
                {
                    faixaMonitorada.Add(minimo.ToString(), maximo.ToString());
                    minimo += incremento;
                    maximo += incremento;
                }
                else
                {
                    minimo = incremento +1;
                    maximo += incremento;
                }
            }

            //apos utiliza milhar
            incremento = 1000;
            maximo = 5000;

            for (int i = 40; i < 50; i++)
            {
                if (minimo > 0)
                {
                    faixaMonitorada.Add(minimo.ToString(), maximo.ToString());
                    minimo += incremento;
                    maximo += incremento;
                }
                else
                {
                    minimo = incremento + 1;
                    maximo += incremento;
                }
            }
            faixaMonitorada.Add(minimo.ToString(), "");

            return faixaMonitorada;
        }

        public Dictionary<string, string> GetFaixaVenda()
        {
            Dictionary<string, string> faixaMonitorada = new Dictionary<string, string>();

            faixaMonitorada.Add("0", "150000");
            var minimo = 0;
            var maximo = 150000;
            var incremento = 5000;


            for (int i = 0; i <= 200; i++)
            {
                if (minimo > 0)
                {
                    faixaMonitorada.Add(minimo.ToString(), maximo.ToString());
                    minimo += incremento;
                    maximo += incremento;
                }
                else
                {
                    minimo = maximo + 1;
                    maximo += incremento;
                }
            }
            //1.150.000
            //apos utiliza milhar
            incremento = 50000;
            maximo = maximo+45000;

            for (int i = 200; i < 250; i++)
            {
                if (minimo > 0)
                {
                    faixaMonitorada.Add(minimo.ToString(), maximo.ToString());
                    minimo += incremento;
                    maximo += incremento;
                }
                else
                {
                    minimo = incremento + 1;
                    maximo += incremento;
                }
            }
            faixaMonitorada.Add(minimo.ToString(), "");

            return faixaMonitorada;
        }
    }


}
