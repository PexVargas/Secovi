using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPexIM.ViewModel
{
    public class BaseDados
    {
        public string Chave { get; set; }
        public decimal? Valor { get; set; }
        public int Quantidade { get; set; }
        public double? Minimo { get; set; }
        public double? Maximo { get; set; }
        public decimal? Metragem { get; set; }
        public List<ImovelOutlier> lstImovelOutlier = new List<ImovelOutlier>();

        public double? CV { get; set; }
    }
}
