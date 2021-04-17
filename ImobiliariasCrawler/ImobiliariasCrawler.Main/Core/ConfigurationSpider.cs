using System;
using System.Collections.Generic;
using System.Text;

namespace ImobiliariasCrawler.Main.Core
{
    public class ConfigurationSpider
    {
        // se o concurrent requests estiver em 10 e o download delay em um segundo
        // é provável que o scrpay faça 10 requests por segundo

        public ConfigurationSpider(TimeSpan downloadDelay, int concurretnRequests)
        {
            DownloadDelay = downloadDelay;
            ConcurrentRequests = concurretnRequests;
        }
        public TimeSpan DownloadDelay { get; private set; }
        public int ConcurrentRequests { get; private set; }
    }
}
