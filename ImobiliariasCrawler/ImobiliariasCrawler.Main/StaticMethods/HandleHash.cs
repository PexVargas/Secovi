using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ImobiliariasCrawler.Main
{
    public static class HandleHash
    {
        public static string StringSHA256(string value)
        {
            StringBuilder Sb = new StringBuilder();
            using var hash = SHA256.Create();
            var result = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            foreach (var b in result) Sb.Append(b.ToString("x2"));
            return Sb.ToString();
        }

        public static byte[] BytesSHA256(string value)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.ASCII.GetBytes(value));
        }
}
}
