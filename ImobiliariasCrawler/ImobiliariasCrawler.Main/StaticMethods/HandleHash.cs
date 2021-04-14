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
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.ASCII.GetBytes(value)).ToString();
        }

        public static byte[] BytesSHA256(string value)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.ASCII.GetBytes(value));
        }
}
}
