using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPexIM.ViewModel.Download
{
    public class ImoveisDados
    {
        public int CodImovelClassificado { get; set; }
        public string CodImovelApi { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Tipo { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string TipoCapturado { get; set; }
        public string CidadeCapturada { get; set; }
        public string BairroCapturado { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> AreaPrivativa { get; set; }
        public Nullable<decimal> AreaTotal { get; set; }
        public Nullable<int> Quartos { get; set; }
        public Nullable<int> Garagens { get; set; }
        public Nullable<int> Suites { get; set; }
        public Nullable<int> Churrasqueiras { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> CodImobiliaria { get; set; }
        public bool Excluido { get; set; }
        public string SiglaEstado { get; set; }
        public int? TipoImovel { get; set; }
        public Nullable<bool> OutlierBairro { get; set; }
        public Nullable<bool> OutlierTipo { get; set; }
        public Nullable<bool> OutlierBairroTipo { get; set; }
        public Nullable<bool> OutlierBairroTipoM2 { get; set; }
        public Nullable<bool> OutlierBairroM2 { get; set; }
        public Nullable<bool> OutlierTipoM2 { get; set; }
        public string Perfil { get; set; }
        public Nullable<bool> OutlierBairroTipoM2Privativa { get; set; }
        public Nullable<bool> OutlierBairroM2Privativa { get; set; }
        public Nullable<bool> OutlierTipoM2Privativa { get; set; }
        public Nullable<System.DateTime> DataImportacao { get; set; }
        public Nullable<bool> OutLierBairroSemTipo { get; set; }
        public Nullable<bool> OutLierBairroM2SemTipo { get; set; }
        public Nullable<bool> OutLierBairroM2PrivativaSemTipo { get; set; }
        public string Imagens { get; set; }
        public Nullable<int> CodCidade { get; set; }
        public Nullable<int> CodBairro { get; set; }
        public string Anunciante { get; set; }
        public Nullable<bool> FgDuplicado { get; set; }
        public string Iptu { get; set; }
        public string Condominio { get; set; }
        public string Endereco { get; set; }
        public string Apto { get; set; }
        public Nullable<int> Dormitorios { get; set; }
    }
}
