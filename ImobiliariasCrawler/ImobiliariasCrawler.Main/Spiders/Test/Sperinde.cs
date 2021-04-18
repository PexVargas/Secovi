﻿using ImobiliariasCrawler.Main.DataObjectTransfer;
using ImobiliariasCrawler.Main.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class Sperinde : SpiderBase
    {

        public class FilterSperinde : Filter
        {
            public string Site { get; set; }
            public string MountUrl() => $"https://www.sperinde.com/busca/site/{Site}/modo/{TipoImovel}/cidade/{Cidade.Replace(" ", ";")}/bairros/{Bairro}/hv/S/{Page}/";
        }

        public override void StartRequest() => Request.Get("https://www.sperinde.com/", callback: Parse);

        public override void Parse(Response response)
        {
            foreach (var tipoImovel in new []{"aluguel", "venda" })
            {
                var site = "poa";
                foreach (var option in response.Xpath("//select[@id='cidade']/option"))
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

                    foreach (var input in response.Xpath($"//div[@class='citys'][contains(@id,'{cidadeFormatada}')]//input"))
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
                        Request.Get(url, callback: ParseResultList, dictArgs: new Dictionary<string, object> { { "filter", filter } });
                    }
                }
            }
        }

        public void ParseResultList(Response response)
        {
            var urlList = response.Xpath("//div[@class='container']/div[contains(@class,'fleft100')]/a");
            if (urlList.Count > 0)
            {
                var filter = response.DictArgs["filter"] as FilterSperinde;
                filter.NextPage(12);
                var nextUrl = filter.MountUrl();
                Request.Get(nextUrl, callback: ParseResultList, dictArgs: response.DictArgs);

                foreach (var a in urlList)
                {
                    var url = a.GetAttributeValue("href", null);
                    Request.Get(url, callback: ParseImovel, dictArgs: response.DictArgs);
                }
            }
        }

        public void ParseImovel(Response response)
        {
            var filter = response.DictArgs["filter"] as FilterSperinde;
            var tipoImovel = filter.TipoImovel == "aluguel" ? TipoImovelEnum.Alugar : TipoImovelEnum.Comprar;

            var jsonText = response.Selector.SelectSingleNode("//script[contains(text(),'SOH.Exec')]").TextOrNull().Replace("SOH.Exec(", "").Replace(");", "");
            var fixComma = new Regex(",\n.*?}", RegexOptions.RightToLeft).Replace(jsonText, "}", 1);
            var removeChares = fixComma.Replace("\"", "").Replace("{", "").Replace("}", "");

            var dictKeyValue = new Dictionary<string,string>();
            foreach (var linha in removeChares.Split(","))
            {
                var keyValue = linha.Split(":");
                try { dictKeyValue.Add(keyValue[0].Trim(), keyValue[1].Trim()); }
                catch { dictKeyValue.Add(keyValue[0].Trim(), ""); }
            }


            var imovel = new ImoveiscapturadosDto(SpiderEnum.Sperinde, tipoImovel)
            {
                Url = response.Url,
                SiglaEstado = "RS",
                Cidade = filter.Cidade,
                Bairro = filter.Bairro,
                Rua = response.Selector.SelectSingleNode("//h6[@class='fw-600 t-up']").TextOrNull() ?? dictKeyValue["imovelEndereco"],
                Cep = dictKeyValue["imovelCep"],

                AreaPrivativa = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600']").TextOrNull(),
                AreaTotal = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600']").TextOrNull(),

                Tipo = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600 t-up']").TextOrNull(),
                Descricao = response.Selector.SelectSingleNode("//p[@class='ft-size17 fleft100']").TextOrNull(),

                Imagens = response.Selector.SelectSingleNode("//figure[contains(@class,'lazy-img')]//img").GetAttributeValue("data-url", null) ?? dictKeyValue["imovelUrl"],
                Quartos = dictKeyValue["imovelDormitorios"],
                Garagens = dictKeyValue["imovelVagas"],
                Valor = dictKeyValue["imovelAluguel"],
                Condominio = dictKeyValue["imovelCondominio"],
                Iptu = dictKeyValue["imovelIptu"],
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
