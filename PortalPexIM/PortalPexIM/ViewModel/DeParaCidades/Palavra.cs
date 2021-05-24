using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.ViewModels.DeParaCidades
{
    public class Palavra
    {
        public int CodPalavra { get; set; }
        public string NomePalavra { get; set; }
        public string Estado { get; set; }
        public string SiglaEstado { get; set; }
        public string Cidade { get; set; }
    }
}