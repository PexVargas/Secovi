using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPexIM.ViewModel
{
    public class ImovelOutlier
    {
        public int? CodImovelClassificado { get; set; }
        public double valor { get; set; }
        public decimal? valorDecimal { get; set; }
        public bool outlier { get; set; }
        public decimal? Area { get; set; }
    }
}
