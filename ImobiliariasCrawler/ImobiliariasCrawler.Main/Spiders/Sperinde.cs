using ImobiliariasCrawler.Main.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class Sperinde : SpiderBase
    {
        public override void BeginRequests()
        {
            Request.Get("https://www.sperinde.com", callback: Parse);
        }

        public override void Parse(Response response)
        {

            foreach (var tipoImovel in new []{"aluguel", "venda" })
            {
                foreach (var cidade in response.Xpath("//select[contains(@class,'cityselect')]/option[not(@disabled='disabled')]").Select(o => o.TextOrNull()))
                {
                    var selectorBairros = response.Selector.SelectSingleNode("//div[@class='modal-dialog bairro']//div[@class='modal-body']");

                    var cidadeLabel = cidade.RemoveAccents().Replace(" ", "-").ToLower();
                    var cidadeFormated = cidade.Replace(" ", ";");

                    var bairrosList = string.Join(";", selectorBairros.SelectNodes($"//label[contains(@for,'{cidadeLabel}')]").Select(n => n.TextOrNull()));
                    var url = $"https://www.sperinde.com/busca/site/caxias/modo/{tipoImovel}/cidade/{cidadeFormated}/bairros/{bairrosList}/hv/S/0/";

                    Request.Get(url: url, callback: ParseResultList, dictArgs: new Dictionary<string, object> { { "tipoImovel", tipoImovel }, { "cidade", cidade } });
                }
            }
        }

        public void ParseResultList(Response response)
        {
            var nextPage = response.Xpath("//ul[@class='pg']/li[last()]/a").FirstOrDefault();
            if (nextPage != null)
            {
                var urlNextPage = nextPage.GetAttributeValue("href", null);
                if (urlNextPage != null && !urlNextPage.Contains("javascript:void(0);"))
                {
                    Request.Get(url: urlNextPage, callback: ParseResultList, dictArgs: response.DictArgs);
                }
            }

            var urlList = response.Xpath("//div[@class='container']/div[contains(@class,'fleft100')]/a");
            foreach (var a in urlList)
            {
                var url = a.GetAttributeValue("href", null);
                Request.Get(url, callback: ParseImovel, dictArgs: response.DictArgs);
            }
        }

        public void ParseImovel(Response response)
        {
            var tipoImovel = response.DictArgs["tipoImovel"] as string;
            var cidade = response.DictArgs["cidade"] as string;

            var tipoImovelEnum = tipoImovel == "aluguel" ? TipoImovelEnum.Alugar : TipoImovelEnum.Comprar;

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


            var imovel = new ImoveiscapturadosDto(SpiderEnum.Sperinde, tipoImovelEnum)
            {
                Url = response.Url,
                SiglaEstado = "RS",
                Cidade = cidade,
                Bairro = response.Selector.SelectSingleNode("//h3[contains(text(),'LOCALIZAÇÃO')]/../span").TextOrNull(),
                Rua = response.Selector.SelectSingleNode("//h6[@class='fw-600 t-up']").TextOrNull() ?? dictKeyValue["imovelEndereco"],
                Cep = dictKeyValue["imovelCep"],

                AreaPrivativa = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600']").TextOrNull(),
                AreaTotal = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600']").TextOrNull(),

                Tipo = response.Selector.SelectSingleNode("//h4[@class='fleft100 cl-red fw-600 t-up']").TextOrNull(),
                Descricao = response.Selector.SelectSingleNode("//p[@class='ft-size17 fleft100']").TextOrNull(),

                Imagens = response.Selector.SelectSingleNode("//figure[contains(@class,'lazy-img')]//img").GetAttributeValue("data-url", null) ?? dictKeyValue["imovelUrl"],
                Suites = response.Selector.SelectSingleNode("//ul[@class='iconsdesc']/li[not(@class='naotem')]/span[contains(text(),'Suíte')]").TextOrNull(),
                Quartos = dictKeyValue["imovelDormitorios"],
                Garagens = dictKeyValue["imovelVagas"],
                Valor = dictKeyValue["imovelAluguel"],
                Condominio = dictKeyValue["imovelCondominio"],
                Iptu = dictKeyValue["imovelIptu"],
            };
            if (imovel.Suites == "Suíte") imovel.Suites = "1";
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
