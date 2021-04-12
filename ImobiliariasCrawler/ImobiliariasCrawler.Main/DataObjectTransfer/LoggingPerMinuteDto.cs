using System;
using System.Collections.Generic;
using System.Text;

namespace ImobiliariasCrawler.Main.DataObjectTransfer
{
    public class LoggingPerMinuteDto
    {
        public string Spider { get; set; }
        public int CountItems { get; set; }
        public int CounRequests { get; set; }
        public DateTime StartProcess { get; set; } = DateTime.Now;
    }
}
