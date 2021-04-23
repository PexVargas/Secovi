using System;
using System.Collections.Generic;
using ImobiliariasCrawler.Main.Extensions;
using System.Linq;
using ImobiliariasCrawler.Main.Model;
using ImobiliariasCrawler.Main.Core;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class DLegend : SpiderBase
    {

        public DLegend() : base(config: new ConfigurationSpider(
                downloadDelay: TimeSpan.FromSeconds(0),
                concurretnRequests: 1
            ))
        {
        }

        public override void BeginRequests() => Request.Get("https://www.dlegend.com.br", Parse);

        public override void Parse(Response response)
        {
            var headers = new Dictionary<string, string> { { "x-requested-with", "XMLHttpRequest" } };
            var searchMap = new SearchMapsFilter();
            Request.FormPost("https://www.dlegend.com.br/search/search_maps", ParseGoals, searchMap, headers);
        }

        public void ParseGoals(Response response)
        {
            var headers = new Dictionary<string, string> { { "x-requested-with", "XMLHttpRequest" } };
            var deserialize = response.Selector.Deserialize<SearchMapResult>();
            var searchListCard = new SearchListCard(deserialize.Imoveis.Select(i => i.Id.ToString()).ToList());
            var dictArgs = new Dictionary<string, object> { { "form", searchListCard } };
            Request.FormPost("https://www.dlegend.com.br/search/searchListCard", ParseResult, searchListCard, headers, dictArgs: dictArgs);
        }

        public void ParseResult(Response response)
        {
            var listUrls = response.Xpath("//div[contains(@class,'btn')]/a");

            if (listUrls != null)
            {
                var headers = new Dictionary<string, string> { { "x-requested-with", "XMLHttpRequest" } };
                var searchListCard = response.DictArgs["form"] as SearchListCard;
                searchListCard.NextPage();
                Console.WriteLine($"Pagina {searchListCard.Per_page}");
                Request.FormPost("https://www.dlegend.com.br/search/searchListCard", ParseResult, searchListCard, headers, dictArgs: response.DictArgs);

                foreach (var a in listUrls)
                {
                    Request.Get(a.GetAttr("href"), ParseImovel);
                }
            }
        }

        public void ParseImovel(Response response)
        {
            var erro = response.Selector.SelectSingleNode("//h1").TextOrNull();
            if (erro != null && erro.Contains("Ocorreu um erro de banco de dados"))
                Console.WriteLine($"Site informando: {erro}");
            else
            {
                var ul = response.Selector.SelectSingleNode("//ul[@class='property-info-list']");
                var descriptionSelector = response.Selector.SelectSingleNode("//section[@class='property-description wrap']/div/p");

                var tipoEnum = response.Url.Contains("locacao") ? TipoImovelEnum.Alugar : TipoImovelEnum.Comprar;
                var imob = new ImoveiscapturadosDto(SpiderEnum.DLegend, tipoEnum)
                {
                    Url = response.Url,
                    Cidade = "Porto Alegre",
                    SiglaEstado = "RS",

                    Tipo = response.Url.Split("/")[5],
                    AreaPrivativa = ul.SelectSingleNode(".//use/@*[contains(.,'#svg-metreage')]/../../..").ReFirst("(.*?) -"),
                    AreaTotal = ul.SelectSingleNode(".//use/@*[contains(.,'#svg-metreage')]/../../..").ReFirst("- (.*)"),
                    Quartos = ul.SelectSingleNode("./li[@class='dormitorio-icon']/div[2]").TextOrNull(),
                    Banheiros = ul.SelectSingleNode(".//use/@*[contains(.,'#svg-bathroom')]/../../..").TextOrNull(),
                    Garagens = ul.SelectSingleNode(".//use/@*[contains(.,'#svg-garage')]/../../..").TextOrNull(),
                    Churrasqueiras = descriptionSelector.ReHas("churrasqueira") ? "1" : "0",
                    Imagens = response.Selector.SelectSingleNode("//div[@class='b-lazy img-background']").GetAttr("data-src"),
                    Valor = response.Selector.SelectSingleNode("//p[@class='price-por']/span[@class='value']").TextOrNull(),
                    Rua = response.Selector.SelectSingleNode("//h1[@class='property-title']").TextOrNull(),
                    Descricao = descriptionSelector.TextOrNull(),
                    
                };
                var ruaBairro = response.Selector.SelectSingleNode("//h3[@class='property-subtitle']").TextOrNull();
                if (ruaBairro != null) imob.Bairro = ruaBairro.Split("-").Last();

                Save(imob);
            }
        }
    }

    public class SearchMapsFilter
    {
        public string Free { get; set; } 
        public List<string> Goal { get; set; } = new List<string> { "venda", "locacao" };
        public List<string> Price { get; set; } = new List<string> { "0", "100.000.000" };
        public List<string> Footage { get; set; } = new List<string> { "0", "14000" };
    }


    public class SearchListCard
    {
        public string Goal { get; set; } = "3";
        public string Orderby { get; set; } = "destaque-desc";

        public List<string> Code { get; set; }
        public int Per_page { get; private set; }

        public void NextPage() => Per_page += 10;
        public SearchListCard(List<string> codes)
        {
            Per_page = 0;
            Code = codes;
        }
    }

    public class SearchMapResult
    {
        public List<ImoveisDLegend> Imoveis { get; set; }
    }
    public class ImoveisDLegend
    {
        public string Id { get; set; }
    }
}
