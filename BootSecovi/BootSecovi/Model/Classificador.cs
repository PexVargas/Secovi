using BootSecovi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BootSecovi.Model
{
    class Classificador
    {
       public PexIMContext db = new PexIMContext();
        public void Classificar (string siglaEstado)
        {

            var imoveiscapturados = db.ImoveisCapturados;

            foreach (var item in imoveiscapturados)
            {
                ImoveisClassificados imovel = new ImoveisClassificados();
                Console.WriteLine(item.Cidade);

                imovel.Cidade = item.Cidade == null ? "" : item.Cidade;
                imovel.Bairro = item.Bairro == null ? "" : item.Bairro;
                imovel.Tipo = item.Tipo == null ? "" : item.Tipo;
                imovel.TipoImovel = item.TipoImovel;

                try
                {
                    if (item.Valor != null)
                        imovel.Valor = Convert.ToDecimal(item.Valor);
                }
                catch (Exception)
                {

                }
                using (var banco = new PexIMContext())
                {

                    banco.ImoveisClassificados.Add(imovel);
                    banco.SaveChanges();
                }
                    
            }
        }
    }
}
