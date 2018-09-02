using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using SuperHero.Domain.Auth;

namespace SuperHero.CrossCutting.Auth
{
	public class HashString : IHashString
    {
        private readonly string _hash;

        public HashString(IConfiguration configuration)
        {
            _hash = configuration.GetSection("AppConfig").GetSection("HashKey").Value;
        }

        public string Generate(string text)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                var data = Encoding.UTF8.GetBytes(_hash + text);
                return Convert.ToBase64String(shaM.ComputeHash(data));
            }
        }
    }
}
