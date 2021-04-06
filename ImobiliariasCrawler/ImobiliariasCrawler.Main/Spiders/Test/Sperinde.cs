﻿using ImobiliariasCrawler.Main.DataObjectTransfer;
using ImobiliariasCrawler.Main.Extensions;
using ImobiliariasCrawler.Main.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class Sperinde : SpiderBase
    {

        public class FilterSperinde : Filter
        {
            public string Site { get; set; }
            public string MountUrl() => $"https://www.sperinde.com/busca/site/{Site}/modo/{TipoImovel}/cidade/{Cidade.Replace(" ", ";")}/bairros/{Bairro}/hv/S/{Page}/";
        }

        public async override Task StartRequest() => await Request.Get("https://www.sperinde.com/", callback: Parse);

        public override async void Parse(Response response)
        {
            foreach (var tipoImovel in new []{"aluguel", "venda" })
            {
                var site = "poa";
                foreach (var option in response.Selector.SelectNodes("//select[@id='cidade']/option"))
                {
                    var isDisable = option.GetAttributeValue("disabled", null);
                    if (isDisable == "disabled")
                    {
                        if (option.ReHas("Caxias e Região"))
                            site = "caxias";
                        continue;
                    }
                    var cidade = option.GetAttributeValue("value", null);
                    var cidadeFormatada = cidade.RemoveAccents().ToLower();

                    foreach (var input in response.Selector.SelectNodes($"//div[@class='citys'][contains(@id,'{cidadeFormatada}')]//input"))
                    {
                        var bairro = input.GetAttributeValue("value", null);
                        var filter = new FilterSperinde
                        {
                            Cidade = cidade,
                            Bairro = bairro,
                            Estado = "RS",
                            TipoImovel = tipoImovel,
                            Site = site,
                        };
                        var url = filter.MountUrl();
                        await Request.Get(url, callback: ParseResultList, dictArgs: new Dictionary<string, object> { { "filter", filter } });
                    }
                }
            }
        }

        public async void ParseResultList(Response response)
        {
            var urlList = response.Selector.SelectNodes("//div[@class='container']/div[contains(@class,'fleft100')]/a");
            if (urlList != null)
            {
                var filter = response.DictArgs["filter"] as FilterSperinde;
                filter.NextPage(12);
                var nextUrl = filter.MountUrl();
                await Request.Get(nextUrl, callback: ParseResultList, dictArgs: response.DictArgs);

                foreach (var a in urlList)
                {
                    var url = a.GetAttributeValue("href", null);
                    await Request.Get(url, callback: ParseImovel, dictArgs: response.DictArgs);
                }
            }
        }

        public void ParseImovel(Response response)
        {
            var filter = response.DictArgs["filter"] as FilterSperinde;
            var tipoImovel = filter.TipoImovel == "aluguel" ? TipoImovelEnum.Alugar : TipoImovelEnum.Comprar;

            var jsonText = response.Selector.SelectSingleNode("//script[contains(text(),'SOH.Exec')]").TextOrNull().Replace("SOH.Exec(", "").Replace(");", "");
            var fixQuotes = jsonText.Replace("'", "\"").Replace("href=\"", "href='").Replace("void(0\"", "void(0'");
            var fixComma = new Regex(",", RegexOptions.RightToLeft).Replace(fixQuotes, "", 1);
            var imovelSperinde = JsonSerializer.Deserialize<ImovelSperinde>(fixComma, RequestService.JsonOptions);

            var imovel = new ImoveiscapturadosDto(SpiderEnum.Sperinde, tipoImovel)
            {
                Url = response.Url,
                SiglaEstado = "RS",
                Cidade = filter.Cidade,
                Bairro = filter.Bairro,
                Rua = response.Selector.SelectSingleNode("//h6[@class='fw-600 t-up']").TextOrNull() ?? imovelSperinde.ImovelEndereco,
                Cep = imovelSperinde.ImovelCep,

                AreaPrivativa = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600']").TextOrNull(),
                AreaTotal = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600']").TextOrNull(),

                Tipo = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600 t-up']").TextOrNull(),
                Descricao = response.Selector.SelectSingleNode("//p[@class='//p[@class='ft-size17 fleft100']']").TextOrNull(),

                Imagens = response.Selector.SelectSingleNode("//figure[contains(@class,'lazy-img')]//img").GetAttributeValue("data-url", null) ?? imovelSperinde.ImovelUrl,
                Quartos = imovelSperinde.ImovelDormitorios,
                Garagens = imovelSperinde.ImovelVagas,
                Valor = imovelSperinde.ImovelAluguel,
                Condominio = imovelSperinde.ImovelCondominio,
                Iptu = imovelSperinde.ImovelIptu,
            };
            Save(imovel);
        }
    }

    public class ImovelSperinde
    {
        public string ImovelId { get; set; }
        public string ImovelUrl { get; set; }
        public string ImovelEndereco { get; set; }
        public string ImovelNumero { get; set; }
        public string ImovelComplemento { get; set; }
        public string ImovelBairro { get; set; }
        public string ImovelCidade { get; set; }
        public string ImovelEstado { get; set; }
        public string ImovelDormitorios { get; set; }
        public string ImovelVagas { get; set; }
        public string ImovelCep { get; set; }
        public string ImovelAluguel { get; set; }
        public string ImovelCondominio { get; set; }
        public string ImovelIptu { get; set; }
    }
}