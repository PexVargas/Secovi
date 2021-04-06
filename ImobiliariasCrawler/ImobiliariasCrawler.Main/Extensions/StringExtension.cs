using System;
using System.Collections.Generic;
using System.Globalization;
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
    }
}
