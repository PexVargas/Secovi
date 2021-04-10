using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPexIM.ViewModel
{
    public class FiltroPesquisa
    {
        public List<string> Cidades = new List<string>();
        public List<string> Bairros = new List<string>();
        public List<string> Tipos = new List<string>();
        public int TipoImovel { get; set; }
    }

}
