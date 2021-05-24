using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PortalPexIM.Model
{
    public partial class Palavrascidade
    {
        public int CodPalavra { get; set; }
        public string Palavra { get; set; }
        public int? CodCidade { get; set; }
        public string SiglaEstado { get; set; }
        public int? Excluido { get; set; }
    }
}
