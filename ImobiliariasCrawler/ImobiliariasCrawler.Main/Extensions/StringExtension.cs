using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ImobiliariasCrawler.Main.Extensions
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy SnakeCase { get; } = new SnakeCaseNamingPolicy();
        public override string ConvertName(string name) => name.ToSnakeCase();
    }


    public static class StringExtension
    {

        public static Dictionary<string, Regex> MapExpression = new Dictionary<string, Regex>();


        public static string ToSnakeCase(this string str) => string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();

        public readonly static JsonSerializerOptions JsonOptionsCamelCase = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        public readonly static JsonSerializerOptions JsonOptionsSnakeCase = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.SnakeCase
        };

        public static string Truncate(this string value, int start, int end)
        {
            if (start >= end && start < value.Length) return value[start..];
            if (value.Length > end) return value[start..end];
            if (value.Length > start) return value[start..];
            return value;
        }

        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
        public static T DeserializeCamelCase<T>(this string text) => JsonSerializer.Deserialize<T>(text, JsonOptionsCamelCase);
        public static T DeserializeSnakeCase<T>(this string text) => JsonSerializer.Deserialize<T>(text, JsonOptionsSnakeCase);

        public static string ReValue(this string value, string pattern, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            if (value is null || pattern is null) return null;
            if (!MapExpression.TryGetValue(pattern, out Regex re))
            {
                re = new Regex(pattern, regexOptions);
                MapExpression.Add(pattern, re);
            }
            return re.Match(value).Groups.Values.ToList().Last().Value;
        }
    }
}
