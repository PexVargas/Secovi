using ImobiliariasCrawler.Main.Core;
using ImobiliariasCrawler.Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class ExpoImovel : SpiderBase
    {

        private PexinContext _contextAux;
        public ExpoImovel() : base(
                new ConfigurationSpider(
                        concurretnRequests: 5,
                        downloadDelay: new TimeSpan(0,0,0,0,1000)
                    ))
        {
            _contextAux = new PexinContext();
        }


        public override void BeginRequests()
        {
            var header = new Dictionary<string, string> { { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0" } };

            var lastProcessed = DateTime.Now.AddDays(-30);
            var urlList = _contextAux.UrlsProcessadas
                .Where(u => u.ProcessedAt < lastProcessed)
                .Where(u => u.Spider == (int)SpiderEnum.ExpoImovel).ToList();

            foreach (var urlParaProcessar in urlList)
            {
                var dictArgs = JsonSerializer.Deserialize<Dictionary<string, object>>(urlParaProcessar.ArgsJson);
                dictArgs.Add("urlParaProcessar", urlParaProcessar);
                Request.Get(url: urlParaProcessar.Url, callback: Parse, headers: header, dictArgs: dictArgs);
            }
        }

        public override void Parse(Response response)
        {
            var tipoImovel = response.DictArgs["tipoImovelEnum"] as string;
            var tipoImovelEnum = tipoImovel == "0" ? TipoImovelEnum.Alugar : TipoImovelEnum.Comprar;

            var estado = response.DictArgs["estado"] as string;
            var tipo = response.DictArgs["tipo"] as string;

            var imovel = new ImoveiscapturadosDto(SpiderEnum.ExpoImovel, tipoImovelEnum)
            {
                Url = response.Url,
                SiglaEstado = estado,
                Rua = response.Selector.SelectSingleNode("//div[@class='lista-detalhe']/h3").TextOrNull(),

                Tipo = tipo,
                Valor = response.Selector.SelectSingleNode("//div[@class='precAptDetExp']").TextOrNull(),
                Quartos = response.Selector.SelectSingleNode("//div[@class='boxInforDetTopInt' ]//div[contains(text(),'Quartos')]/../div[3]").TextOrNull(),
                Suites = response.Selector.SelectSingleNode("//div[@class='boxInforDetTopInt' ]//div[contains(text(),'suítes')]/../div[3]").TextOrNull(),
                Banheiros = response.Selector.SelectSingleNode("//div[@class='boxInforDetTopInt' ]//div[contains(text(),'banheiros sociais')]/../div[3]").TextOrNull(),
                AreaTotal = response.Selector.SelectSingleNode("//div[@class='boxInforDetTopInt' ]//div[contains(text(),'Área')]/../div[3]").TextOrNull(),
                AreaPrivativa = response.Selector.SelectSingleNode("//div[@class='boxInforDetTopInt' ]//div[contains(text(),'área privativa')]/../div[3]").TextOrNull(),
                Garagens = response.Selector.SelectSingleNode("//div[@class='boxInforDetTopInt' ]//div[contains(text(),'Vagas')]/../div[3]").TextOrNull(),
                Descricao = response.Selector.SelectSingleNode("//div[@class='textDetExpo']").TextOrNull(),
                Imagens = response.Selector.SelectSingleNode("//div[@id='geralGaleria']//img").GetAttr("data-src"),
                Condominio = response.Selector.SelectSingleNode("//div[@class='noxSubNomCond']").TextOrNull()
            };
            var cidadeBairro = response.Selector.SelectSingleNode("//div[@id='verMapa']/p").TextOrNull();
            if (cidadeBairro != null)
            {
                cidadeBairro = cidadeBairro.Split(",").Last();
                var bairro = cidadeBairro.Split("-").FirstOrDefault().Trim();
                var cidade = cidadeBairro.Split("-").Skip(1).First().Split("/").FirstOrDefault().Trim();
                imovel.Bairro = bairro;
                imovel.Cidade = cidade;
            }

            if (imovel.AreaTotal != null)
                imovel.AreaTotal = imovel.AreaTotal.Replace("\n", "").Replace("\t", "");
            if (imovel.AreaPrivativa != null)
                imovel.AreaPrivativa = imovel.AreaPrivativa.Replace("\n", "").Replace("\t", "");

            Save(imovel);
            lock (_contextAux)
            {
                var urlParaProcessar = response.DictArgs["urlParaProcessar"] as UrlsProcessadas;
                urlParaProcessar.ProcessedAt = DateTime.Now;
                _contextAux.Update(urlParaProcessar);
                _contextAux.SaveChanges();
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
                        inicio={(pagina-1)*48}&
                        quantidadeImoveis=undefined".Replace("\r", "").Replace("\n", "").Replace(" ", "");
        }
    }
}
