using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.ViewModels.DeParaCidades
{
    public class PalavrasListagem
    {
        public List<Palavra> Palavras { get; set; }
        public int CountPalavras { get; set; }
        public int Pagina { get; set; }
        public int NumRegistros { get; set; }
        public int? CodEstado { get; set; }
        public int? CodCidade { get; set; }
    }
}