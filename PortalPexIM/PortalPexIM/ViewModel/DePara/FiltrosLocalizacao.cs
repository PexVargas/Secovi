using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.ViewModels.DePara
{
    public class FiltrosLocalizacao
    {

        private List<Cidade> _cidades;

        public List<Cidade> Cidades
        {
            get { return _cidades; }
            set { _cidades = value; }
        }


        private List<Cidade> _cidadesIN;

        public List<Cidade> CidadesIN
        {
            get { return _cidadesIN; }
            set { _cidadesIN = value; }
        }

        private List<Bairro> _bairros;

        public List<Bairro> Bairros
        {
            get { return _bairros; }
            set { _bairros = value; }
        }


        public FiltrosLocalizacao()
        {

        }

        public FiltrosLocalizacao(List<Cidade> cidades)
        {
            _cidades = cidades;
        }

    }
}