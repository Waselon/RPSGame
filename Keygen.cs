using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RPSGame
{
    class Keygen
    {
        public string GetHMAC(string text, string key)
        {
            var enc = Encoding.Default;
            HMACSHA256 hmac = new HMACSHA256(enc.GetBytes(key));
            hmac.Initialize();
            byte[] buffer = enc.GetBytes(text);
            return BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
}
