using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPexIM.ViewModel
{
    public class Imovel
    {
        public int? CodImovelClassificado { get; set; }
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string Tipo { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public int? Quartos { get; set; }
        public int? Garagem { get; set; }
        public int? Quantidade { get; set; }
        public string GaragemStr { get; set; }
        public int? Suite { get; set; }
        public string SuiteStr { get; set; }
        public int? Churrasqueira { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? AreaPrivativa { get; set; }
        public decimal? AreaTotal { get; set; }
        public decimal? Area { get; set; }
        public bool? OutLierBairro { get; set; }
        public bool? OutLierBairroSemTipo { get; set; }
        public bool? OutLierTipo { get; set; }
        public bool? OutLierBairroTipo { get; set; }
        public bool? OutLierBairroM2 { get; set; }
        public bool? OutLierBairroM2SemTipo { get; set; }
        public bool? OutLierTipoM2 { get; set; }
        public bool? OutLierBairroTipoM2 { get; set; }
        public string Perfil { get; set; }
        public bool? OutLierBairroM2Privativa { get; set; }
        public bool? OutLierBairroM2PrivativaSemTipo { get; set; }
        public bool? OutLierTipoM2Privativa { get; set; }
        public bool? OutLierBairroTipoM2Privativa { get; set; }
        public string Imobiliaria { get; set; }
        public int? CodImobiliaria { get; set; }
        public string TipoImovel { get; set; }
        public string TipoImovelInt { get; set; }
        public string Descricao { get; set; }
        public bool Excluir { get; set; }

    }
}
