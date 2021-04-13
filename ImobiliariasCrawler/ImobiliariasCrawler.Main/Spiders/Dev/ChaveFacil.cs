using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ImobiliariasCrawler.Main.DataObjectTransfer;
using System.Text.Json;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class ChaveFacil : SpiderBase
    {

        public override void StartRequest()
        {
            foreach (var estado in new[] { "PE", "MG" })
            {
                var filter = new FilterChaveFacil {Estado = estado};
                var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{estado}/municipios";
                Request.Get(url, callback: Parse, dictArgs: new Dictionary<string, object> { { "filter", filter } });
            }
        }

        public override void Parse(Response response)
        {
            
            var cidadeList = response.Selector.Deserialize<List<CidadeIBGE>>();
            var filterBase = response.DictArgs["filter"] as FilterChaveFacil;

            var headers = new Dictionary<string, string> { { "X-Requested-With", "XMLHttpRequest" } };

            foreach (var tipoImovel in new[] { "comprar", "alugar" })
            {
                foreach (var subTipo in new[] {"residencial", "comercial", "rural" })
                {
                    foreach (var cidade in cidadeList)
                    {
                        var filter = new FilterChaveFacil { 
                            Estado = filterBase.Estado,
                            Cidade = cidade.Nome, 
                            TipoImovel = tipoImovel, 
                            SubTipo = subTipo,
                            Url = $"http://chavefacil.com.br/imoveis/{tipoImovel}/{subTipo}/qualquer-tipo-imovel/{filterBase.Estado}/{cidade.Nome.Replace(" ", "-")}"
                        };
                        var urlFormPost = $"http://chavefacil.com.br/imoveis/{tipoImovel}/{subTipo}/qualquer-tipo-imovel/{filter.Estado}/ajax-buscar-imoveis";

                        filter.NextPage(1);
                        var formData = filter.CreateFormPost();

                        var dictArgs = new Dictionary<string, object> { { "filter", filter } };
                        Request.FormPost(urlFormPost, callback: ParseResultList, dictBody: formData, headers: headers, dictArgs: dictArgs);
                    }
                }
            }
        }

        private void ParseResultList(Response response)
        {
            var urlList = response.Selector.SelectNodes("//div[@class='row titulo ir-ficha-imovel']//a").Select(a => a.GetAttributeValue("href", null));
            var filter = response.DictArgs["filter"] as FilterChaveFacil;
            filter.NextPage(1);

            if (urlList != null)
            {
                var formNextPage = filter.CreateFormPost();
                Request.FormPost(response.Url, callback: ParseResultList, dictBody: formNextPage, dictArgs: response.DictArgs);

                foreach (var url in urlList)
                {
                    var formatedUrl = $"http://chavefacil.com.br{url}";
                    Request.Get(formatedUrl, callback: ParseImovel, dictArgs: response.DictArgs);
                }

            }
        }

        private void ParseImovel(Response response)
        {
            var filter = response.DictArgs["filter"] as FilterChaveFacil;
            var tipoImovel = filter.TipoImovel == "comprar" ? TipoImovelEnum.Comprar : TipoImovelEnum.Alugar;

            var imovel = new ImoveiscapturadosDto(SpiderEnum.ChaveFacil, tipoImovel)
            {
                Url = response.Url,
                SiglaEstado = filter.Estado,
                Cidade = filter.Cidade,
                Bairro = response.Selector.SelectSingleNode("//div[contains(@class,'bairro')]").TextOrNull(),
                Rua = response.Selector.SelectSingleNode("//div[contains(@class,'endereco')]").ReFirst(" *(.*) -"),
                Tipo = filter.SubTipo,

                Valor = response.Selector.SelectSingleNode("//span[@id='preco-imovel']").TextOrNull() ?? response.Selector.SelectSingleNode("//span[contains(text(),'Valor')]/../span[2]").TextOrNull(),
                Imagens = response.Selector.SelectSingleNode("//div[@class='item active']//img").GetAttributeValue("src", null),
                Condominio = response.Selector.SelectSingleNode("//span[contains(text(),'Condominio')]/../span[2]").TextOrNull(),
                Iptu = response.Selector.SelectSingleNode("//span[contains(text(),'IPTU')]/../span[2]").TextOrNull(),

                AreaPrivativa = response.Selector.SelectSingleNode("//span[contains(text(),'Área Útil')]/../span[2]").TextOrNull(),
                AreaTotal = response.Selector.SelectSingleNode("//span[contains(text(),'Área Total')]/../span[2]").TextOrNull(),
                Quartos = response.Selector.SelectSingleNode("//span[contains(text(),'Quartos')]/../span[2]").TextOrNull(),
                Suites = response.Selector.SelectSingleNode("//span[contains(text(),'Suites')]/../span[2]").TextOrNull(),
                Banheiros = response.Selector.SelectSingleNode("//span[contains(text(),'Banheiros')]/../span[2]").TextOrNull(),
                Garagens = response.Selector.SelectSingleNode("//span[contains(text(),'Vagas')]/../span[2]").TextOrNull(),
                Descricao = response.Selector.SelectSingleNode("//div[@class='col-md-12 descricao_imovel']").TextOrNull(),
            };
            Save(imovel);
        }



        public class FilterChaveFacil : Filter
        {
            public string Url { get; set; }
            public Dictionary<string, string> CreateFormPost()
            {
                return new Dictionary<string, string>
                {
                    { "url", Url },
                    { "transacao", "1" },
                    { "tipo-imovel", "" },
                    { "ids-tipos-imoveis", "" },
                    { "tipo", "grupo" },
                    { "localidade", $"{Cidade}, {Estado}" },
                    { "valor-minimo", "" },
                    { "valor-maximo", "" },
                    { "area-minima", "" },
                    { "area-maxima", "" },
                    { "dormitorios", "" },
                    { "vagas-garagem", "" },
                    { "id-imobiliaria", "" },
                    { "banheiros", "" },
                    { "pagina", Page.ToString() },
                    { "outras-caracteristicas", "0000000000000000" },
                    { "codigo-buscar", "" },
                    { "somente-codigo", "" },
                    { "controle", "+" },
                    { "filtros", "" },
                    { "coordenadas-mapa", "" },
                    { "ordenacao", "0" },
                    { "visualizacao", "lista" },
                    { "id-log-busca", "21841577" },
                };
            }
        }
    }
}
