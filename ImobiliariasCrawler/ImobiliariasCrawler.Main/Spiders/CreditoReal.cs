using ImobiliariasCrawler.Main.Model;
using ImobiliariasCrawler.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImobiliariasCrawler.Main.Spiders
{
    public class CreditoReal : SpiderBase
    {
        public string UrlBaseVenda { get; } = "https://www.creditoreal.com.br/Services/RealEstate/JSONP/List.aspx?mode=realties&currentPage={0}&pageSize=28&numberOfImages=1&lancamento=0&tem_foto=1&tipo_negociacao=-2&nt=-2&ordem=0&valor_completo=0&estado={1}&cidade={2}&_1617542582010=";
        public string UrlBaseAluguel { get; } = "https://www.creditoreal.com.br/Services/RealEstate/JSONP/List.aspx?mode=realties&currentPage={0}&pageSize=28&numberOfImages=1&tipo_negociacao=-4&nt=-4&ordem=0&valor_completo=0&estado={1}&cidade={2}&_1617543989530=";
        public override void BeginRequests() => Request.Get("https://www.creditoreal.com.br/", callback: Parse);

        public override void Parse(Response response)
        {
            var cidadeEstadoList = new List<string> { "Porto Alegre - RS", "Curitiba - PR", "Natal - RN", "Águas Claras - RS", "Alpestre - RS", "Alvorada - RS", "Antônio Prado - RS", "Arroio Do Sal - RS", "Atlândida Sul - RS", "Atlântida Sul - RS", "Balneário Pinhal - RS", "Bento Gonçalves - RS", "Boa Vista do Sul - RS", "Butiá - RS", "Cachoeira do Sul - RS", "Cachoeirinha - RS", "Campo Bom - RS", "Candelária - RS", "Canela - RS", "Canoas - RS", "Capão Da Canoa - RS", "Capão Novo - RS", "Capão Novo (Capão da Canoa) - RS", "Capivarita (Pantano Grande) - RS", "Carlos Barbosa - RS", "Caxias Do Sul - RS", "Cidreira - RS", "Coronel Pilar - RS", "Criuva - RS", "Cruz Alta - RS", "Curumim - RS", "Dois Irmãos - RS", "Eldorado do Sul - RS", "Encruzilhada (Maçambará) - RS", "Encruzilhada do Sul - RS", "Estância Velha - RS", "Esteio - RS", "Faria Lemos (Bento Gonçalves) - RS", "Farroupilha - RS", "Fazenda Souza (Caxias do Sul) - RS", "Flores Da Cunha - RS", "Garibaldi - RS", "Garibaldina (Garibaldi) - RS", "Glorinha - RS", "Gramado - RS", "Gramado Dos Loureiros - RS", "Gravataí - RS", "Guaíba - RS", "Imbé - RS", "Imigrante - RS", "IPE - RS", "Itaara - RS", "Ivoti - RS", "Lajeado - RS", "Lajeado Bonito (Cotiporã) - RS", "Mariana Pimentel - RS", "Monte Belo do Sul - RS", "Montenegro - RS", "Morro Reuter - RS", "Morungava (Gravataí) - RS", "Muçum - RS", "Nova Pádua - RS", "Nova Petrópolis - RS", "Nova Santa Rita - RS", "Nova Tramandaí (Tramandaí) - RS", "Novo Hamburgo - RS", "Osório - RS", "Pantano Grande - RS", "Passo do Sobrado - RS", "Pelotas - RS", "Picada Café - RS", "Pinheiro Machado - RS", "Pinto Bandeira - RS", "Portão - RS", "Rainha do Mar - RS", "Rincão Del Rei (Rio Pardo) - RS", "Rio Grande - RS", "Rio Pardinho (Santa Cruz do Su - RS", "Rio Pardo - RS", "Rolante - RS", "Santa Cruz da Concórdia (Taqua - RS", "Santa Cruz do Sul - RS", "Santa Maria - RS", "Santa Tereza - RS", "Santana da Boa Vista - RS", "São Francisco de Paula - RS", "São Geraldo - RS", "São José de Castro (Garibaldi) - RS", "São Leopoldo - RS", "São Lourenço do Sul - RS", "São Marcos - RS", "São Sebastião Do Caí - RS", "São Valentim do Sul - RS", "São Vendelino - RS", "Sapiranga - RS", "Sapucaia Do Sul - RS", "Sobradinho - RS", "Terra de Areia - RS", "Teutônia - RS", "Torres - RS", "Tramandaí - RS", "Triunfo - RS", "Tuiuti (Bento Gonçalves) - RS", "Uruguaiana - RS", "Vale do Rio Cai (Nova Petrópol - RS", "Vale do Sol - RS", "Vale dos Vinhedos (Bento Gonça - RS", "Venâncio Aires - RS", "Vera Cruz - RS", "Veranópolis - RS", "Viamão - RS", "Vila Seca (Caxias do Sul) - RS", "Xangri-Lá - RS", "Xinguara - RS", "Balneário Camboriú - SC", "Bella Torres - SC", "Florianópolis - SC", "Garopaba - SC", "Garuva - SC", "Imbituba - SC", "Itapema - SC", "Jaguaruna - SC", "Laguna - SC", "Palhoça - SC", "Passo de Torres - SC", "São José - SC", "Taubaté - SP", };

            foreach (var cidadeEstado in cidadeEstadoList)
            {
                var cidade = cidadeEstado.Split(" - ")[0];
                var estado = cidadeEstado.Split(" - ")[1];

                var dictCidadeEstado = new Dictionary<string, object> { { "cidade", cidade }, { "estado", estado } };

                var urlAluguel = string.Format(UrlBaseAluguel, 1, estado, cidade);
                Request.Get(urlAluguel, callback: ParseResult, dictArgs: dictCidadeEstado);

                var urlVenda = string.Format(UrlBaseVenda, 1, estado, cidade);
                Request.Get(urlVenda, callback: ParseResult, dictArgs: dictCidadeEstado);

            }
        }

        public async void ParseResult(Response response)
        {
            var contentString = await response.HttpResponse.Content.ReadAsStringAsync();
            var formatedContent = contentString.Replace("vistasoftrest_realties_callback(", "").Replace("realties_callback(", "").Replace(");", "");
            var desserialize = JsonSerializer.Deserialize<JsonImoveis>(formatedContent);


            var urlBase = response.Url.Contains("tipo_negociacao=-2") ? UrlBaseVenda : UrlBaseAluguel;
            if (desserialize.CurrentPage < desserialize.NumberOfPages)
            {
                var urlNextPage = string.Format(urlBase, desserialize.CurrentPage+1, response.DictArgs["estado"].ToString(), response.DictArgs["cidade"]);
                Request.Get(urlNextPage, callback: ParseResult, dictArgs: response.DictArgs);
            }

            foreach (var item in desserialize.Items)
            {
                var tipoEnum = item.CurrentNegotiationTypeTitle == "Venda" ? TipoImovelEnum.Comprar : TipoImovelEnum.Alugar;
                var imovel = new ImoveiscapturadosDto(SpiderEnum.CreditoReal, tipoEnum)
                {
                    Url = $"https://www.creditoreal.com.br/{item.CurrentNegotiationTypeTitle}/{item.ReferenceId}",
                    AreaPrivativa = item.FormattedArea,
                    AreaTotal = item.FormattedArea,
                    Bairro = item.CurrentSpot.Neighborhood,
                    Cep = item.CurrentSpot.ZipCode,
                    Cidade = item.CurrentSpot.City,
                    SiglaEstado = item.CurrentSpot.CurrentStateName,
                    Rua = item.CurrentSpot.CurrentAddress,

                    Tipo = item.CurrentRealtyTypeId,
                    Banheiros = item.Bathrooms.ToString(),
                    Quartos = item.Bedrooms.ToString(),
                    Descricao = item.Description,
                    Imagens = item.Image,
                    Iptu = item.FormattedIPTUValue,
                    Suites = item.Suites.ToString(),
                    Valor = item.FormattedFullPrice,
                    Garagens = item.ParkingSpots.ToString(),
                    Condominio = item.FormattedCondominiumValue,
                    
                };
                Save(imovel);
            }
        }
    }

    public class JsonImoveis
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfPages { get; set; }

        public List<ImoveisCreditoReal> Items { get; set; }

        public class ImoveisCreditoReal
        {
            public string ReferenceId { get; set; }
            public string CurrentRealtyTypeId { get; set; }
            public int Bathrooms { get; set; }
            public int Bedrooms { get; set; }
            public string CurrentNegotiationTypeTitle { get; set; }
            public string Description { get; set; }
            public string FormattedArea { get; set; }
            public string FormattedCondominiumValue { get; set; }
            public string FormattedFullPrice { get; set; }
            public string FormattedIPTUValue { get; set; }
            public string FormattedLotArea { get; set; }
            public string Image { get; set; }
            public int ParkingSpots { get; set; }
            public int Suites { get; set; }

            public CurrentSpotCreditoReal CurrentSpot { get; set; }
            public class CurrentSpotCreditoReal
            {
                public string City { get; set; }
                public string CurrentAddress { get; set; }
                public string CurrentStateName { get; set; }
                public string Neighborhood { get; set; }
                public string ZipCode { get; set; }
            }
        }
    }
}
