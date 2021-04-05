using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ImobiliariasCrawler.Main
{
    public class Response
    {
        public HttpResponseMessage HttpResponse { get; set; }
        public HtmlNode Selector { get; set; }
        public Dictionary<string, object> DictArgs { get; set; }
        public string Url { get; set; }
    }
}
