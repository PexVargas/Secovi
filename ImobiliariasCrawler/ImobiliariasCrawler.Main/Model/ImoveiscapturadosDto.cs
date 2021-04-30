using ImobiliariasCrawler.Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImobiliariasCrawler.Main
{
    public class ImoveiscapturadosDto
    {
        public ImoveiscapturadosDto(SpiderEnum spider, TipoImovelEnum tipoImovel)
        {
            Spider = spider;
            TipoImovel = tipoImovel;
        }

        public Imoveiscapturados ToImoveiscapturados()
        {
            return new Imoveiscapturados(Spider, TipoImovel)
            {
                Url = Url,
                Valor = Valor,
                Tipo = Tipo,
                Suites = Suites,
                SiglaEstado = SiglaEstado,
                Rua = Rua,
                Quartos = Quartos,
                Localidade = Localidade,
                Iptu = Iptu,
                Garagens = Garagens,
                Imagens = Imagens,
                Descricao = Descricao,
                AreaPrivativa = AreaPrivativa,
                AreaTotal = AreaTotal,
                Bairro = Bairro,
                Banheiros = Banheiros,
                Cep = Cep,
                Churrasqueiras = Churrasqueiras,
                Cidade = Cidade,
                Condominio = Condominio,
                CodImolvelAPI = CodImolvelAPI
            };
        }

        private SpiderEnum Spider { get; set; }
        private TipoImovelEnum TipoImovel { get; set; }


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
        public string Localidade { get; set; }
        public string CodImolvelAPI { get; set; }

    }
}
