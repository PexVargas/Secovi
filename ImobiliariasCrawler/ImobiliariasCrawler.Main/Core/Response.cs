using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;

namespace ImobiliariasCrawler
{
    public class Response
    {
        public HttpResponseMessage HttpResponse { get; set; }
        public HttpContent Content { get; set; }
        public HtmlNode Selector { get; set; }
        public Dictionary<string, object> DictArgs { get; set; }
        public string Url { get; set; }
        public HtmlNodeCollection Xpath(string expression) => Selector.SelectNodes(expression) ?? new HtmlNodeCollection(null);
    }
}
