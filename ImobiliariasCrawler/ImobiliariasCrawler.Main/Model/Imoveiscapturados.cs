using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ImobiliariasCrawler.Main.Model
{
    public enum SpiderEnum
    {
        DLegend = 5,
        CreditoReal = 6,
    }

    public enum TipoImovelEnum
    {
        Comprar = 1,
        Alugar = 2
    }
    public partial class Imoveiscapturados
    {
        public Imoveiscapturados(SpiderEnum spiderEnum, TipoImovelEnum tipoImovel)
        {
            DataCaptura = DateTime.Now;
            CodImobiliaria = (int)spiderEnum;

            TipoImovel = (int)tipoImovel;
            Finalidade = tipoImovel.ToString();
        }
        protected Imoveiscapturados() { }

        public int? CodImobiliaria { get; private set; }
        public DateTime? DataCaptura { get; private set; }
        public int? Excluido { get; private set; }
        public int CodImovelcapturado { get; private set; }
        public string Finalidade { get; private set; }
        public int? TipoImovel { get; private set; }
        public string Satus { get; private set; }

        public string Tipo { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Valor { get; set; }
        public string Quartos { get; set; }
        public string Suites { get; set; }
        public string Garagens { get; set; }
        public string Churrasqueiras { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }
        public string SiglaEstado { get; set; }
        public string AreaTotal { get; set; }
        public string AreaPrivativa { get; set; }
        public string Imagens { get; set; }
        public string Iptu { get; set; }
        public string Condominio { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Banheiros { get; set; }
        public string Anunciante { get; set; }
        public string Localidade { get; set; }
    }
}
