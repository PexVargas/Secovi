﻿using System;
using System.Collections.Generic;
using ImobiliariasCrawler.Main.Extensions;
using System.Linq;
using ImobiliariasCrawler.Main.Model;
using ImobiliariasCrawler.Main.Core;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class DLegend : SpiderBase
    {
        public DLegend() : base(
                new ConfigurationSpider(
                        concurretnRequests: 5,
                        downloadDelay: new TimeSpan(0, 0, 0, 0, 3000)
                    ))
        {

        }
        public override void BeginRequests() => Request.Get("https://www.dlegend.com.br", Parse);

        public override void Parse(Response response)
        {
            var bairrosList = new List<string> { "Auxiliadora", "Bela Vista", "Boa Vista", "Chacara das Pedras", "Higienópolis", "Jardim Europa", "Moinhos de Vento", "Mont Serrat", "Petrópolis", "Rio Branco", "Santa Cecília", "Três Figueiras", "Azenha", "Bom Fim", "Centro", "Centro Histórico", "Cidade Baixa", "Independência", "Menino Deus", "Praia de Belas", "Santana", "Anchieta", "Cristo Redentor", "Farrapos", "Floresta", "Jardim Lindóia", "Jardim Planalto", "Humaitá", "Navegantes", "Passo da Areia", "Passo das Pedras", "Rubem Berta", "Santa Maria Goretti", "Sarandi", "São Geraldo", "São João", "São José", "São Sebastião", "Vila Ipiranga", "Vila Jardim", "Cavalhada", "Cristal", "Teresópolis", "Central Parque", "Agronomia", "Bom Jesus", "Jardim Botânico", "Jardim Carvalho", "Jardim Sabará", "Jardim do Salso", "Lomba do Pinheiro", "Medianeira", "Partenon", "Canoas", "Distrito Industrial", "Esteio", "Gravataí", "Auxiliadora", "Bela Vista", "Boa Vista", "Chacara das Pedras", "Higienópolis", "Jardim Europa", "Moinhos de Vento", "Mont Serrat", "Petrópolis", "Rio Branco", "Santa Cecília", "Três Figueiras", "Azenha", "Bom Fim", "Centro", "Centro Histórico", "Cidade Baixa", "Independência", "Menino Deus", "Praia de Belas", "Santana", "Anchieta", "Cristo Redentor", "Farrapos", "Floresta", "Jardim Lindóia", "Jardim Planalto", "Humaitá", "Navegantes", "Passo da Areia", "Passo das Pedras", "Rubem Berta", "Santa Maria Goretti", "Sarandi", "São Geraldo", "São João", "São José", "São Sebastião", "Vila Ipiranga", "Vila Jardim", "Cavalhada", "Cristal", "Teresópolis", "Central Parque", "Agronomia", "Bom Jesus", "Jardim Botânico", "Jardim Carvalho", "Jardim Sabará", "Jardim do Salso", "Lomba do Pinheiro", "Medianeira", "Partenon", "Canoas", "Distrito Industrial", "Esteio", "Gravataí" };
            var headers = new Dictionary<string, string> { { "x-requested-with", "XMLHttpRequest" } };

            foreach (var bairro in bairrosList)
            {
                var searchMapLocacao = new SearchMapsFilter(goal: "locacao", neighborhood: bairro);
                var dictLocacao = new Dictionary<string, object> { { "bairro", bairro }, { "TipoImovel", "2" } };
                Request.FormPost("https://www.dlegend.com.br/search/search_maps", ParseGoals, searchMapLocacao, headers, dictArgs: dictLocacao);

                var searchMapVenda = new SearchMapsFilter(goal: "venda", neighborhood: bairro);
                var dictVenda = new Dictionary<string, object> { { "bairro", bairro }, { "TipoImovel", "1" } };
                Request.FormPost("https://www.dlegend.com.br/search/search_maps", ParseGoals, searchMapVenda, headers, dictArgs: dictVenda);
            }
        }

        public void ParseGoals(Response response)
        {
            var headers = new Dictionary<string, string> { { "x-requested-with", "XMLHttpRequest" } };
            try
            {
                var deserialize = response.Selector.Deserialize<SearchMapResult>();
                var searchListCard = new SearchListCard(deserialize.Imoveis.Select(i => i.Id.ToString()).ToList(), goal: response.DictArgs["TipoImovel"].ToString());
                response.DictArgs.Add("form", searchListCard);
                Request.FormPost("https://www.dlegend.com.br/search/searchListCard", ParseResult, searchListCard, headers, dictArgs: response.DictArgs);
            }
            catch (Exception)
            {
                Console.WriteLine(response.Selector.InnerHtml);
            }
        }

        public void ParseResult(Response response)
        {
            var listUrls = response.Selector.SelectNodes("//div[@class='property-card']");

            if (listUrls != null)
            {
                var headers = new Dictionary<string, string> { { "x-requested-with", "XMLHttpRequest" } };
                var searchListCard = response.DictArgs["form"] as SearchListCard;
                searchListCard.NextPage();
                Console.WriteLine($"Tipo {searchListCard.Goal} Pagina {searchListCard.Per_page}");
                Request.FormPost("https://www.dlegend.com.br/search/searchListCard", ParseResult, searchListCard, headers, dictArgs: response.DictArgs);

                foreach (var div in response.Selector.SelectNodes("//div[@class='property-card']"))
                {
                    var newDictArgs = new Dictionary<string, object>()
                    {
                        { "Tipo", div.SelectSingleNode(".//p[@class='cod']/small").TextOrNull() },
                        { "bairro", response.DictArgs["bairro"] },
                        { "TipoImovel", response.DictArgs["TipoImovel"] },
                    };

                    var url = div.SelectSingleNode(".//a").GetAttributeValue("href", null);
                    Request.Get(url, ParseImovel, dictArgs: newDictArgs);
                }
            }
        }

        public void ParseImovel(Response response)
        {
            var ul = response.Selector.SelectSingleNode("//ul[@class='property-info-list']");
            var descriptionSelector = response.Selector.SelectSingleNode("//section[@class='property-description wrap']/div/p");
            var tipoImovel = int.Parse(response.DictArgs["TipoImovel"].ToString());

            var tipoEnum = tipoImovel == 1 ? TipoImovelEnum.Comprar : TipoImovelEnum.Comprar;
            var imob = new ImoveiscapturadosDto(SpiderEnum.DLegend, tipoEnum)
            {
                Url = response.Url,
                Bairro = response.DictArgs["bairro"].ToString(),
                Cidade = "Porto Alegre",
                SiglaEstado = "RS",

                Tipo = response.DictArgs["Tipo"].ToString(),
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
            Save(imob);
        }
    }

    public class SearchMapsFilter
    {
        public string Free { get; set; } 

        public List<string> Neighborhood { get; set; }
        public List<string> Goal { get; set; }
        public List<string> Price { get; set; } = new List<string> { "0", "100.000.000" };
        public List<string> Footage { get; set; } = new List<string> { "0", "14000" };

        public SearchMapsFilter(string goal, string neighborhood)
        {
            Goal = new List<string> { goal };
            Neighborhood = new List<string> { neighborhood };
        }
    }


    public class SearchListCard
    {
        public string Goal { get; set; }
        public string Orderby { get; set; } = "destaque-desc";

        public List<string> Code { get; set; }
        public int Per_page { get; private set; }

        public void NextPage() => Per_page += 10;
        public SearchListCard(List<string> codes, string goal)
        {
            Per_page = 0;
            Code = codes;
            Goal = goal;
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
