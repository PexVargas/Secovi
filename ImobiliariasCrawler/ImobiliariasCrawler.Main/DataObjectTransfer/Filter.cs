using System;
using System.Collections.Generic;
using System.Text;

namespace ImobiliariasCrawler.Main.DataObjectTransfer
{
    public abstract class Filter
    {
        public string TipoImovel { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Localidade { get; set; }
        public int Page { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }

        public void NextPage(int sumCurrentPage) => Page += sumCurrentPage;
    }
}
