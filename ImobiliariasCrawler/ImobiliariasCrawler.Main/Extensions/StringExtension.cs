using System;
using System.Collections.Generic;
using System.Text;

namespace ImobiliariasCrawler.Main.Extensions
{
    public static class StringExtension
    {
        public static string Truncate(this string value, int start, int end)
        {
            if (start >= end && start < value.Length) return value[start..];
            if (value.Length > end) return value[start..end];
            if (value.Length > start) return value[start..];
            return value;
        }
    }
}
