using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ImobiliariasCrawler.Main.Model;
using ImobiliariasCrawler.Main.Core;
using ImobiliariasCrawler.Main.Extensions;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class Leindecker : SpiderBase
    {

        public Leindecker() : base(new ConfigurationSpider(
                downloadDelay: new TimeSpan(0, 0, 0, 0, 500),
                concurretnRequests: 10
            ))
        {}

        public override void StartRequest()
        {
            Request.Get("https://www.leindecker.com.br/", callback: Parse);
        }

        public override void Parse(Response response)
        {
            foreach (var tipoImovel in new string[] { "locacao", "venda" })
            {
                var urlTipo = $"https://www.leindecker.com.br/busca/{tipoImovel}";
                Request.Get(urlTipo, ParseCidades, dictArgs: new Dictionary<string, object> { { "tipoImovel", tipoImovel } });
            }
        }

        private void ParseCidades(Response response)
        {
            var tipoImovel = response.DictArgs["tipoImovel"] as string;
            foreach (var cidade in response.Xpath("//select[@class='cityselect']/option").Select(o => o.TextOrNull()))
            {
                var selectorBairros = response.Selector.SelectSingleNode("//div[@class='modal-dialog bairro']//div[@class='fleft100']");
                
                var cidadeLabel = cidade.RemoveAccents().Replace(" ", "-").ToLower();
                var cidadeFormated = cidade.Replace(" ", ";");

                var bairrosList = string.Join(";", selectorBairros.SelectNodes($"//label[contains(@for,'{cidadeLabel}')]").Select(n => n.TextOrNull()));
                var url = $"https://www.leindecker.com.br/busca/{tipoImovel}/cidade/{cidadeFormated}/bairros/{bairrosList}/0/";

                Request.Get(url: url, callback: ParseResultList, dictArgs: new Dictionary<string, object> { { "tipoImovel", tipoImovel }, { "cidade", cidade } } );
            }
        }

        public void ParseResultList(Response response)
        {
            var listImoveis = response.Xpath("//div[contains(@class,'imovel im-busca')]");

            var nextPage = response.Xpath("//ul[@class='pg']/li[@class='next']/a").FirstOrDefault();
            if (nextPage != null)
            {
                var urlNextPage = nextPage.GetAttributeValue("href", null);
                if (urlNextPage != null && !urlNextPage.Contains("javascript:void(0);"))
                {
                filter.NextPage();
                    Request.Get(url: urlNextPage, callback: ParseResultList, dictArgs: response.DictArgs);
                }
            }

            foreach (var div in listImoveis)
            {
                var url = div.SelectSingleNode(".//a").GetAttributeValue("href", null);
                Request.Get(url: url, callback: ParseImovel, dictArgs: response.DictArgs);
            }
        }

        public void ParseImovel(Response response)
        {
            var tipoImovel = response.DictArgs["tipoImovel"] as string;
            var cidade = response.DictArgs["cidade"] as string;
            var tipoImovelEnum = tipoImovel == "venda" ? TipoImovelEnum.Comprar : TipoImovelEnum.Alugar;

            var banheiro = response.Selector.SelectSingleNode("//ul[@class='licarac']/li").ReHas("BANHEIRO") ? "1" : "0";
            var imovel = new ImoveiscapturadosDto(SpiderEnum.Leindecker, tipoImovelEnum)
            {
                Url = response.Url,
                Bairro = response.Selector.SelectSingleNode("//h4[contains(text(),'Localização')]/../span").InnerHtml.Split("<br>").FirstOrDefault(),
                Cidade = cidade,
                SiglaEstado = "RS",

                Imagens = response.Selector.SelectSingleNode("//div[@class='item']//img").GetAttributeValue("data-src", null),
                Tipo = response.Selector.SelectSingleNode("//div[contains(@class,'sider-form')]/h5").TextOrNull(),
                Banheiros = banheiro,
                AreaPrivativa = response.Selector.SelectSingleNode("//small[contains(text(),'Área')]/../span").TextOrNull(),
                AreaTotal = response.Selector.SelectSingleNode("//small[contains(text(),'Área')]/../span").TextOrNull(),
                Quartos = response.Selector.SelectSingleNode("//small[contains(text(),'Dorm')]/../span").TextOrNull(),
                Garagens = response.Selector.SelectSingleNode("//small[contains(text(),'Vagas')]/../span").TextOrNull(),
                Valor = response.Selector.SelectSingleNode("//small[contains(text(),'Valor')]/../span").TextOrNull(),
                Descricao = response.Selector.SelectSingleNode("//div[@class='col-12 p-0 p-lg-3 mt-3']/p").TextOrNull(),
                Condominio = response.Selector.SelectSingleNode("//div[contains(@class,'sider-form')]//li[contains(text(),'Cond')]").ReFirst("R.*"),
                Iptu = response.Selector.SelectSingleNode("//div[contains(@class,'sider-form')]//li[contains(text(),'IPTU')]").ReFirst("R.*"),
            };
            Save(imovel);
        }
    }

    public class FilterLeindecker
    {
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string TipoImovel { get; set; }
        public string Url => string.Format("https://www.leindecker.com.br/busca/{0}/cidade/{1}/bairros/{2}/0/", TipoImovel, Cidade, Bairro);
    }
}
