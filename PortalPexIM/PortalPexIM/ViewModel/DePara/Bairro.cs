using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.ViewModels.DePara
{
    public class Bairro
    {
        public string NomeBairro { get; set; }
        public int CodBairro { get; set; }
        public string Cidade { get; set; }
        public string BairroStr { get; set; }
        public int? CodCidade { get; set; }
    }
}