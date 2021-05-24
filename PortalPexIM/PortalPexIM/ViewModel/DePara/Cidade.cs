using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.ViewModels.DePara
{
    public class Cidade
    {
        public string NomeCidade { get; set; }
        public string SiglaEstadoCidade { get; set; }
        public int? CodCidade { get; set; }
        public List<Bairro> ListaBairros { get; set; }
        public List<Tipo> ListaTipos { get; set; }
        public int CodEstado { get; set; }
    }
}