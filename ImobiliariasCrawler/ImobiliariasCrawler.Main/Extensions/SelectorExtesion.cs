using HtmlAgilityPack;
using ImobiliariasCrawler.Main.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;

namespace ImobiliariasCrawler.Main.Extensions
{
    public static class SelectorExtension
    {
        public static string TextOrNull(this HtmlNode htmlNode)
        {
            if (htmlNode is null) return null;
            return htmlNode.InnerText.Trim();
        }


        public static string ReFirst(this HtmlNode htmlNode, string expression)
        {
            if (htmlNode is null || htmlNode.InnerText is null) return null;
            var re = Regex.Match(htmlNode.InnerText, expression, RegexOptions.Multiline);
            if (!re.Success) return null;
            return re.Groups.Values.ToList().Last().Value;
        }

        public static bool ReHas(this HtmlNode htmlNode, string expression)
        {
            if (htmlNode is null || htmlNode.InnerText is null) return false;
            //var makeRegexOtions = regexOptions is null ? RegexOptions.Multiline : RegexOptions.Multiline & regexOptions;
            return Regex.IsMatch(htmlNode.InnerHtml, expression, RegexOptions.Multiline & RegexOptions.IgnoreCase);
        }

        public static T Deserialize<T>(this HtmlNode htmlNode)
        {
            return JsonSerializer.Deserialize<T>(htmlNode.InnerText, RequestService.JsonOptions);
        }
    }
}
