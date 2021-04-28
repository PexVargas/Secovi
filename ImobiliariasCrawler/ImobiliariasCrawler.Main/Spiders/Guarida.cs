using ImobiliariasCrawler.Main.Extensions;
using ImobiliariasCrawler.Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class Guarida : SpiderBase
    {
        public override void BeginRequests()
        {
            Request.Get(url: "https://www.guarida.com.br/alugueis/0/selecione-uma-cidade?endereco=&items=15&order=codigo-desc&page=1", callback: Parse);
            Request.Get(url: "https://www.guarida.com.br/vendas/0/selecione-uma-cidade?endereco=&items=15&order=codigo-desc&page=1", callback: Parse);
        }

        public override void Parse(Response response)
        {
            var codCidadeList = response.Xpath("//option[contains(@class,'sources-filtro-cidade')]").Select(e => e.GetAttr("value")).ToList();
            foreach (var codCidade in codCidadeList)
            {
                var tipo = response.Url.Contains("alugueis") ? "alugar" : "vender";
                var url = response.Url.Contains("alugueis") ? "https://www.guarida.com.br/alugueis/index/filtro" : "https://www.guarida.com.br/vendas/index/buscaImovel";
                var dictForm = new Dictionary<string, string>()
                {
                    { "opcao-filtro", tipo },
                    { "page", "1" },
                    { "cidade", codCidade },
                    { "valor", "0-24000000" },
                    { "area", "0-10000" },
                    { "order", "codigo-desc" },
                    { "url", url }
                };
                var newDictForm = new Dictionary<string, object>() { { "form", dictForm } };
                Request.FormPost(url: url, dictBody: dictForm, callback: ParseAluguelVenda, dictArgs: newDictForm);
            }
        }

        public void ParseAluguelVenda(Response response)
        {

            var dictForm = response.DictArgs["form"] as Dictionary<string, string>;
            var url = dictForm["url"];
            var desserialize = response.Selector.Deserialize<JsonImovelGuarita>();
            var currentPage = int.Parse(desserialize.Paginacao.Current);

            if (desserialize.Paginacao.Pages > 1)
            {
                foreach (var pag in Enumerable.Range(0, desserialize.Paginacao.Pages-1))
                {
                    dictForm["page"] = (++currentPage).ToString();
                    var newDictForm = new Dictionary<string, object>() { { "form", dictForm } };
                    Request.FormPost(url: url.ToString(), dictBody: dictForm, callback: ParseImovelList, dictArgs: newDictForm);
                }
            }
            ParseImovelList(response);
        }

        public void ParseImovelList(Response response)
        {
            var dictForm = response.DictArgs["form"] as Dictionary<string, string>;
            var desserialize = response.Selector.Deserialize<JsonImovelGuarita>();
            var currentPage = int.Parse(desserialize.Paginacao.Current);

            var tipoImovel = dictForm["opcao-filtro"] == "vender" ? TipoImovelEnum.Comprar : TipoImovelEnum.Alugar;

            foreach (var item in desserialize.Imoveis)
            {
                var imovel = new ImoveiscapturadosDto(SpiderEnum.Guarida, tipoImovel)
                {
                    AreaPrivativa = item.Area_util,
                    AreaTotal = item.Area_total,
                    Bairro = item.Bairro,
                    Banheiros = item.Banheiros,
                    Cidade = item.Cidade,
                    Valor = item.Valor ?? item.Preco,
                    Descricao = item.Descricao_interna,
                    Garagens = item.Vagas,
                    Imagens = item.Imagem,
                    Quartos = item.Quartos,
                    Url = "https://www.guarida.com.br" + item.Url,
                    Rua = item.Endereco,
                    SiglaEstado = item.Estado == "Rio Grande do Sul" ? "RS" : item.Estado,
                    Tipo = item.Tipo,
                    
                };
                Request.Get(url: imovel.Url, callback: ParseImovel, dictArgs: new Dictionary<string, object> { { "imovel", imovel } });
            }
        }

        private void ParseImovel(Response response)
        {
            var imovel = response.DictArgs["imovel"] as ImoveiscapturadosDto;
            imovel.Condominio = response.Selector.SelectSingleNode("//h6[contains(text(),'Condomínio')]/../h5").TextOrNull();
            imovel.Iptu = response.Selector.SelectSingleNode("//h6[contains(text(),'IPTU')]/../h5").TextOrNull();
            if (string.IsNullOrWhiteSpace(imovel.Descricao)){
                try
                {
                    imovel.Descricao = response.Selector.SelectSingleNode("//h5[text()='Sobre este imóvel']/..").InnerText.Split("Sobre este imóvel")[1].Split("O que eu preciso para alugar este imóvel?")[0];
                    imovel.Suites = imovel.Descricao.ReValue(@"\d? su[íi]tes?");
                }
                catch { }
            }
        }
    }

    public class JsonImovelGuarita
    {
        public List<ImoveisGuarita> Imoveis { get; set; }
        public Paginacao Paginacao { get; set; }
    }

    public class Paginacao
    {
        public string Current { get; set; }
        public int Pages { get; set; }
    }

    public class ImoveisGuarita
    {
        public string Imagem { get; set; }
        public string Descricao_interna { get; set; }
        public string Valor { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Tipo { get; set; }
        public string Quartos { get; set; }
        public string Banheiros { get; set; }
        public string Area_total { get; set; }
        public string Area_util { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Vagas { get; set; }
        public string Preco { get; set; }
        public string Url { get; set; }

    }
}
