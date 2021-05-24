using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PortalPexIM.Model
{
    public partial class Imoveisclassificados
    {
        public int CodImovelclassificado { get; set; }
        public string Tipo { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public decimal? Valor { get; set; }
        public int? Quartos { get; set; }
        public int? Suites { get; set; }
        public int? Garagens { get; set; }
        public int? Churrasqueiras { get; set; }
        public string Satus { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }
        public string SiglaEstado { get; set; }
        public int? TipoImovel { get; set; }
        public decimal? AreaTotal { get; set; }
        public decimal? AreaPrivativa { get; set; }
        public string Imagens { get; set; }
        public int? CodImobiliaria { get; set; }
        public DateTime? DataClassificacao { get; set; }
        public int? Excluido { get; set; }
        public string Iptu { get; set; }
        public string Condominio { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Banheiros { get; set; }
        public string Anunciante { get; set; }
        public string Finalidade { get; set; }
        public string Localidade { get; set; }
        public string CidadeCapturada { get; set; }
        public string BairroCapturado { get; set; }
        public string TipoCapturado { get; set; }
        public string Apto { get; set; }
        public string PalavraExclusao { get; set; }
    }
}
