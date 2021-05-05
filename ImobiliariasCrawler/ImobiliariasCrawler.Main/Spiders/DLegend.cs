using System;
using System.Collections.Generic;
using ImobiliariasCrawler.Main.Extensions;
using System.Linq;
using ImobiliariasCrawler.Main.Model;
using ImobiliariasCrawler.Main.Core;
using System.Text.RegularExpressions;

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

        public override void BeginRequests()
        {

            var urlComprar = "https://www.dlegend.com.br/api/search?page=0&perPage=1000000&maps=&order=&goal=&free=&status=comprar&values[]=&values[]=&footage[]=&footage[]=&boundless=false";
            Request.Get(urlComprar, Parse, dictArgs: new Dictionary<string, object> { { "tipoImovel", TipoImovelEnum.Comprar } });

            var urlAlugar = "https://www.dlegend.com.br/api/search?page=0&perPage=1000000&maps=&order=&goal=&free=&status=alugar&values[]=&values[]=&footage[]=&footage[]=&boundless=false";
            Request.Get(urlAlugar, Parse, dictArgs: new Dictionary<string, object> { { "tipoImovel", TipoImovelEnum.Alugar } });
        }

        public override void Parse(Response response)
        {
            var tipoImovel = (TipoImovelEnum)response.DictArgs["tipoImovel"];

            var dlegendJsonList = response.Content.ReadAsStringAsync().Result.DeserializeSnakeCase<List<DlegendJson>>();
            foreach (var item in dlegendJsonList)
            {
                var bairro = item.AddressDistrict;
                var rua = item.Address;
                var url = $"https://www.dlegend.com.br/{tipoImovel}/{item.CategoryName}/{bairro}/{rua}/{item.Code}".RemoveAccents().Replace(" ", "-").ToLower();

                var imagens = string.Join(", ", item.Images.DeserializeCamelCase<List<DLegendImageJson>>().Take(5).Select(i =>
                    $"https://www.dlegend.com.br/vista.imobi/fotos/{item.Code}/{i.File}"
                ));


                var imovel = new ImoveiscapturadosDto(SpiderEnum.DLegend, tipoImovel)
                {
                    Url = url,
                    Bairro = item.AddressDistrict,
                    Cep = item.AddressCep,
                    Cidade = item.AddressCity,
                    SiglaEstado = item.AddressUf,
                    Rua = $"{item.Address}, {item.AddressAdditional}",

                    AreaPrivativa = item.PrivativeArea,
                    AreaTotal = item.PrivativeAreaMax,

                    Quartos = item.Bedroom,
                    Garagens = item.Parking,
                    Descricao = item.Description,
                    Tipo = item.CategoryDescription,
                    Valor = item.SalesValue,
                    Suites = item.Description.ReValue(@"\d? su[íi]tes?"),
                    Banheiros = item.Description.ReValue(@"\d? banheiros?"),
                    Churrasqueiras = item.Description.ReValue("churrasqueira") is null ? "0" : "1",
                    Imagens = imagens,
                    CodImolvelAPI = item.Code,
                };
                Save(imovel);
            }
        }
    }

    public sealed class DLegendImageJson
    {
        public string File { get; set; }
    }

    public sealed class DlegendJson
    {
        public string RentTotalUnits { get; set; }
        public string SalesTotalUnits { get; set; }
        public string Address { get; set; }
        public string AddressType { get; set; }
        public string AddressNumber { get; set; }
        public string AddressAdditional { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressCity { get; set; }
        public string AddressUf { get; set; }
        public string AddressCep { get; set; }
        public string AddressCountry { get; set; }
        public string AddressReference { get; set; }
        public string Confidential { get; set; }
        public string DisplayRent { get; set; }
        public string DisplayBuy { get; set; }
        public string Bedroom { get; set; }
        public string Parking { get; set; }
        public string ParkingMax { get; set; }
        public string Launch { get; set; }
        public string LaunchSales { get; set; }
        public string Code { get; set; }
        public string Cover { get; set; }
        public string Featured { get; set; }
        public string PrivativeArea { get; set; }
        public string Goal { get; set; }
        public string Slogan { get; set; }
        public string Description { get; set; }
        public string RentalValue { get; set; }
        public string SalesValue { get; set; }
        public string Images { get; set; }
        public string Files { get; set; }
        public string RentalOrder { get; set; }
        public string SalesOrder { get; set; }
        public string Order { get; set; }
        public string Status { get; set; }
        public string PrivativeAreaMax { get; set; }
        public string RentalValueMax { get; set; }
        public string SalesValueMax { get; set; }
        public string PromotionalValue { get; set; }
        public string IsIncome { get; set; }
        public string IncomeValue { get; set; }
        public string Furnished { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}
