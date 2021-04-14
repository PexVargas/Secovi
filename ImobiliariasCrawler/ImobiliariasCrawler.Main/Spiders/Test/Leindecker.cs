using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ImobiliariasCrawler.Main.Model;

namespace ImobiliariasCrawler.Main.Spiders
{
    class Leindecker : SpiderBase
    {

        public override void StartRequest()
        {
            Request.Get("https://www.leindecker.com.br/", callback: Parse);
        }

        public override void Parse(Response response)
        {
            foreach (var tipoImovel in new string[] { "locacao", "venda" })
            {
                foreach (var option in response.Selector.SelectNodes("//div[@class='modal-content']//select[@id='cidade']/option"))
                {
                    var cidade = option.InnerHtml.Replace(" ", ";");
                    var cidadePartialId = option.InnerHtml.Replace(" ", "-").ToLower();
                    foreach (var input in response.Xpath($"//input[contains(@id,'{cidadePartialId}')]"))
                    {
                        var filter = new FilterLeindecker
                        {
                            Bairro = input.GetAttributeValue("value", null),
                            Cidade = cidade,
                            TipoImovel = tipoImovel
                        };
                        Request.Get(filter.Url, callback: ParseResultList, dictArgs: new Dictionary<string, object> { { "filter", filter } });
                    }
                }
            }
        }

        public void ParseResultList(Response response)
        {
            var filter = response.DictArgs["filter"] as FilterLeindecker;
            var listImoveis = response.Xpath("//div[contains(@class,'imovel ')]");
            if (listImoveis.Count > 0)
            {
                filter.NextPage();
                Request.Get(url: filter.Url, callback: ParseResultList, dictArgs: response.DictArgs);
            }

            foreach (var div in listImoveis)
            {
                var url = div.SelectSingleNode(".//a").GetAttributeValue("href", null);
                Request.Get(url: url, callback: ParseImovel, dictArgs: response.DictArgs);
            }
        }

        public void ParseImovel(Response response)
        {
            var filter = (response.DictArgs["filter"] as FilterLeindecker);
            var tipoImovel = filter.TipoImovel == "venda" ? TipoImovelEnum.Comprar : TipoImovelEnum.Alugar;

            var banheiro = response.Selector.SelectSingleNode("//ul[@class='licarac']/li").ReHas("BANHEIRO") ? "1" : "0";
            var imovel = new ImoveiscapturadosDto(SpiderEnum.Leindecker, tipoImovel)
            {
                Url = response.Url,
                Bairro = filter.Bairro,
                Cidade = filter.Cidade,
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
        public int Pagina { get; private set; } = 0;

        public void NextPage() => Pagina += 12;
        public string Url => string.Format("https://www.leindecker.com.br/busca/{0}/cidade/{1}/bairros/{2}/{3}/", TipoImovel, Cidade, Bairro, Pagina);
    }
}
