using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BootZap
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("0", "50000");
            //dic1.Add("50001", "100000");
            //dic1.Add("100001", "150000");
            foreach (var item in dic1)
            {
                ColetarURL("rs", "venda", item.Key,item.Value);
            }

            
        }
        public static void ColetarURL(string siglaEstado, string tipoImovel,  string precoMinimo, string precoMaximo)
        {
            var client = new RestClient("https://www.zapimoveis.com.br/"+tipoImovel+"/imoveis/"+siglaEstado+ "/?tipo=Im%C3%B3vel%20usado&precoMaximo="+precoMaximo+"&precoMinimo="+precoMinimo+"&transacao=" + tipoImovel+"");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=d31ca02fdbd62dccdfba994bbb900bd741614625939; __cfruid=7c6b4d94b486b5ce2c1640d765a1e71cb5fc110d-1614625941");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response.Content);

            var htmlLinkNoticias = htmlDoc.DocumentNode.SelectNodes("//div[@class='Chamada']/h2/a/@href");

        }
    }

}
