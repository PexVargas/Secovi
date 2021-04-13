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
    }
}
