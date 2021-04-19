using System;
using System.Collections.Generic;
using System.Text;

namespace ImobiliariasCrawler.Main.Model
{
    public class UrlsProcessadas
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime ProcessedAt { get; set; }
        public int Spider { get; set; }
        public string ArgsJson { get; set; }
    }
}
