using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImobiliariasCrawler.Main.Extensions;
using ImobiliariasCrawler.Main.Model;

namespace ImobiliariasCrawler.Main.Spiders
{
    class Garcia : SpiderBase
    {
       
        public override void StartRequest()
        {
            var dictVenda = new Dictionary<string, object> { { "TipoImovel", "1" } };
            Request.Get("http://www.garciaimoveisrs.com.br/imoveis.php?busca=venda&finalidade=venda&cidade=", callback: Parse, dictArgs: dictVenda);
            Request.Get("http://www.garciaimoveisrs.com.br/imoveis.php?busca=lancamentos&finalidade=venda&lancamentos=true&cidade=", callback: Parse, dictArgs: dictVenda);

            var dictAluguel = new Dictionary<string, object> { { "TipoImovel", "2" } };
            Request.Get("http://www.garciaimoveisrs.com.br/imoveis.php?busca=aluguel&finalidade=aluguel&cidade=", callback: Parse, dictArgs: dictAluguel);

        }

        public override void Parse(Response response)
        {
            var partialNextUrl = response.Selector.SelectSingleNode("//i[@class='fa fa-chevron-right']/..").GetAttributeValue("href", null);
            if (partialNextUrl != null)
            {
                var nextUrl = "http://www.garciaimoveisrs.com.br/imoveis.php" + partialNextUrl;
                Request.Get(nextUrl, callback: Parse, dictArgs: response.DictArgs);
            }

            foreach (var div in response.Selector.SelectNodes("//div[@class='imoveis clearfix']//div[@class='row']/div"))
            {
                var partialUrl = div.SelectSingleNode(".//a").GetAttributeValue("href", null);
                var url = "http://www.garciaimoveisrs.com.br/" + partialUrl;

                Request.Get(url, callback: ParseResult, dictArgs: response.DictArgs);
            }
        }

        public void ParseResult(Response response)
        {
            var tipoImovel = response.DictArgs["TipoImovel"].ToString() == "1" ? TipoImovelEnum.Comprar : TipoImovelEnum.Alugar;
            var cidade = response.Selector.SelectSingleNode("//head/title").ReFirst("Imobil.*?- (.*) [A-Z]{2}");
            var bairro = response.Selector.SelectSingleNode("//div[@class='sobre']/h3").TextOrNull();
            if (bairro != null) bairro = bairro.Replace(cidade, "");
            var imovel = new ImoveiscapturadosDto(SpiderEnum.Garcia, tipoImovel)
            {
                Cidade = cidade,
                SiglaEstado = response.Selector.SelectSingleNode("//head/title").ReFirst("Imobil.*?- .* ([A-Z]{2})"),
                Tipo = response.Selector.SelectSingleNode("//div[@class='sobre']/h2").TextOrNull(),
                Bairro = bairro,
                Rua = response.Selector.SelectSingleNode("//div[@id='detalhes']//i/..").TextOrNull(),

                Url = response.Url,
                Descricao = response.Selector.SelectSingleNode("//div[@id='detalhes']//p[2]").TextOrNull(),
                Quartos = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Dormitório(s)' )]/strong").TextOrNull(),
                Garagens = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Vaga(s)' )]/strong").TextOrNull(),
                AreaTotal = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Total' )]/strong").ReFirst("(.*?m)"),
                AreaPrivativa = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Útil' )]/strong").ReFirst("(.*?m)"),

                Valor = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Aluguel' )]/strong").TextOrNull(),
                Condominio = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Condomínio' )]/strong").TextOrNull(),
                Iptu = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'IPTU' )]/strong").TextOrNull(),
                Imagens = response.Selector.SelectSingleNode("//div[@id='dfoto']//img").GetAttributeValue("src", null),
            };
            if (imovel.AreaPrivativa is null)
                imovel.AreaPrivativa = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Privativo' )]/strong").ReFirst("(.*?m)");
            if (imovel.Valor is null)
                imovel.Valor = response.Selector.SelectSingleNode("//ul[@class='infos']/li[contains(text(),'Venda' )]/strong").TextOrNull();

            Save(imovel);
        }
    }
}
