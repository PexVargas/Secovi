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
            var codCidadeList = new string[] {"4237", "4402", "3973", "3991", "4399", "4096", "4405", "4397"};
            foreach (var codCidade in codCidadeList)
            {
                foreach (var tipo in new string[] { "alugar" , "vender" })
                {
                    var dictForm = new Dictionary<string, string>()
                    {
                        { "opcao-filtro", tipo },
                        { "page", "1" },
                        { "cidade", codCidade },
                        { "valor", "0-10000" },
                        { "area", "0-10000" },
                        { "order", "codigo-desc" },
                    };
                    var newDictForm = new Dictionary<string, object>() { { "form", dictForm } };
                    Request.FormPost(url: "https://www.guarida.com.br/alugueis/index/filtro", dictBody: dictForm, callback: Parse, dictArgs: newDictForm);
                }
            }
        }

        public override void Parse(Response response)
        {
            var dictForm = response.DictArgs["form"] as Dictionary<string, string>;
            var desserialize = response.Selector.Deserialize<JsonImovelGuarita>();
            var currentPage = int.Parse(desserialize.Paginacao.Current);
            if (currentPage < desserialize.Paginacao.Pages)
            {
                dictForm["page"] = (currentPage+1).ToString();
                Console.WriteLine($"Pagina {currentPage + 1}");
                var newDictForm = new Dictionary<string, object>() { { "form", dictForm } };
                Request.FormPost(url: "https://www.guarida.com.br/alugueis/index/filtro", dictBody: dictForm, callback: Parse, dictArgs: newDictForm);
            }

            foreach (var item in desserialize.Imoveis)
            {
                var tipoImovel = dictForm["opcao-filtro"] == "vender" ? TipoImovelEnum.Comprar : TipoImovelEnum.Alugar;
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
                    SiglaEstado = item.Estado,
                    Tipo = item.Tipo,
                };
                Save(imovel);
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
