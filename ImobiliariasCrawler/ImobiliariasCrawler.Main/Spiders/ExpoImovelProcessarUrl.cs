using ImobiliariasCrawler.Main.Core;
using ImobiliariasCrawler.Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class ExpoImovelProcessarUrl : SpiderBase
    {
        public ExpoImovelProcessarUrl() : base(
                new ConfigurationSpider(
                        concurretnRequests: 5,
                        downloadDelay: new TimeSpan(0, 0, 0, 0, 1000)
                    ))
        {

        }

        public override void BeginRequests() =>
            Request.Get(url: "https://www.expoimovel.com/", callback: Parse);

        public override void Parse(Response response)
        {
            var tipoList = response.Xpath("//select[@name='tipoImovel']/option").Select((t, i) => new { nome = t.TextOrNull(), cod = t.GetAttributeValue("value", null) });
            var header = new Dictionary<string, string> { { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0" } };

            foreach (var tipo in tipoList)
                foreach (var codPretensao in new string[] { "0", "1;4" })
                    foreach (var codEstado in new string[] { "1" })
                    {
                        var estado = codEstado == "1" ? "PE" : "MG";
                        var tipoImovelEnum = codPretensao == "0" ? TipoImovelEnum.Alugar : TipoImovelEnum.Comprar;

                        var pagina = 1;
                        var url = MountUrl(pagina, codEstado, codPretensao, tipo.cod);

                        var args = new Dictionary<string, object> {
                            { "estado", estado },
                            { "tipoImovelEnum", tipoImovelEnum },
                            { "tipo", tipo.nome },
                            { "pagina", pagina },
                            { "codPretensao", codPretensao },
                            { "codTipo", tipo.cod },
                            { "codEstado", codEstado },
                        };

                        Request.Get(url: url, callback: ParseResultList, dictArgs: args, headers: header);
                    }
        }

        public void ParseResultList(Response response)
        {
            var pagina = (int)response.DictArgs["pagina"];
            response.DictArgs["pagina"] = ++pagina;

            var codEstado = response.DictArgs["codEstado"] as string;
            var codPretensao = response.DictArgs["codPretensao"] as string;
            var codTipo = response.DictArgs["codTipo"] as string;

            Console.WriteLine(response.Selector.SelectSingleNode("//div[@class='texto-resul']").TextOrNull());
            Console.WriteLine(response.Selector.SelectSingleNode("//div[@class='num-pg-ativ']").TextOrNull());

            var header = new Dictionary<string, string> { { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0" } };
            var urlList = response.Xpath("//div[@class='desc']/a").Select(a => a.GetAttributeValue("href", null));
            if (urlList.Count() > 0)
            {
                var nextUrl = MountUrl(pagina, codEstado, codPretensao, codTipo);
                Request.Get(url: nextUrl, callback: ParseResultList, dictArgs: response.DictArgs, headers: header);
            }

            foreach (var url in urlList)
            {
                var jsonArgs = JsonSerializer.Serialize(response.DictArgs);
                using var context = new PexinContext();
                var urlProcessada = new UrlsProcessadas
                {
                    Url = url,
                    ProcessedAt = DateTime.Now.AddDays(-60),
                    Spider = (int)SpiderEnum.ExpoImovel,
                    ArgsJson = jsonArgs
                };
                try
                {
                    context.Add(urlProcessada);
                    context.SaveChanges();
                }
                catch
                {
                    Console.WriteLine($"Url duplicada: {url}");
                }
            }
        }

        public static string MountUrl(int pagina, string codEstado, string codPretensao, string codTipo)
        {
            return @$"https://www.expoimovel.com/imovel.do?method=list&
                    origem=expoimovel&
                    numColunas=2&
                    opcao=busca&
                    lingua=pt&
                    pais=BR&
                    estado={codEstado}&
                    cidade=0&
                    pretensao={codPretensao}&
                    tipoImovel={codTipo}&
                    inicio={(pagina - 1) * 48}&
                    quantidadeImoveis=undefined".Replace("\r", "").Replace("\n", "").Replace(" ", "");
        }
    }
}

