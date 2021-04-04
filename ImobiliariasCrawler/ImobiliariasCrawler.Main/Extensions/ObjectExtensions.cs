using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ImobiliariasCrawler.Main.Extensions
{
    public static class ObjectExtensions
    {
     
        public static List<KeyValuePair<string, string>> ToKeyValue(this object source)
        {
            if (source == null) ThrowExceptionWhenSourceArgumentIsNull();

            var keyValuePairsList = new List<KeyValuePair<string, string>>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                var value = property.GetValue(source);
                if (IsOfType<List<string>>(value))
                {
                    foreach (var v in (List<string>)value)
                    {
                        var element = new KeyValuePair<string, string>($"{property.Name}[]".ToLower(), v);
                        keyValuePairsList.Add(element);
                    }
                }
                else if (IsOfType<int>(value))
                    keyValuePairsList.Add(new KeyValuePair<string, string>(property.Name.ToLower(), value.ToString()));
                else
                    keyValuePairsList.Add(new KeyValuePair<string, string>(property.Name.ToLower(), (string)value));

            }
            return keyValuePairsList;
        }

        private static bool IsOfType<T>(object value) => value is T;

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new NullReferenceException("Unable to convert anonymous object to a dictionary. The source anonymous object is null.");
        }

        //public static IDictionary<string, string> ToKeyValue(this object metaToken)
        //{
        //    if (metaToken == null)
        //    {
        //        return null;
        //    }

        //    var serializer = new JsonSerializer { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        //    var token = metaToken as JToken;
        //    if (token == null)
        //    {
        //        return ToKeyValue(JObject.FromObject(metaToken, serializer));
        //    }

        //    if (token.HasValues)
        //    {
        //        var contentData = new Dictionary<string, string>();
        //        foreach (var child in token.Children().ToList())
        //        {
        //            var childContent = child.ToKeyValue();
        //            if (childContent != null)
        //            {
        //                contentData = contentData.Concat(childContent)
        //                                         .ToDictionary(k => k.Key.ToLower(), v => v.Value);
        //            }
        //        }

        //        return contentData;
        //    }

        //    var jValue = token as JValue;
        //    if (jValue?.Value == null)
        //    {
        //        return null;
        //    }

        //    var value = jValue?.Type == JTokenType.Date ?
        //                    jValue?.ToString("o", CultureInfo.InvariantCulture) :
        //                    jValue?.ToString(CultureInfo.InvariantCulture);

        //    return new Dictionary<string, string> { { token.Path, value } };
        //}

    }
}
