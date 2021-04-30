using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPexIM.ViewModel
{
    public class FiltroPesquisa
    {
        //public List<string> Cidades = new List<string>();
        //public List<string> Bairros = new List<string>();
        //public List<string> Tipos = new List<string>();
        public string[] Cidades { get; set; }
        public string[] Bairros { get; set; }
        public string[] Tipos { get; set; }

        public int TipoImovel { get; set; }
        public string DtReferencia { get; set; }
    }

}
