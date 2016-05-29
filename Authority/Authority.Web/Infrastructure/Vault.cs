using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace IdentityServer.Web.Infrastructure
{
    public static class Vault
    {
        private const string VaultFilePathKey = "VaultFilePath";

        private static readonly Dictionary<string, string> _values = new Dictionary<string, string>(); 

        public static void Init()
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(VaultFilePathKey))
            {
                return;
            }

            string vaultFilePath = ConfigurationManager.AppSettings["VaultFilePath"];

            if (string.IsNullOrEmpty(vaultFilePath) || !File.Exists(vaultFilePath))
            {
                return;
            }

            foreach (string line in File.ReadAllLines(vaultFilePath))
            {
                string[] data = line.Split(';');
                _values.Add(data[0], data[1]);
            }
        }

        public static string Keys(string key)
        {
            if (!_values.Any())
            {
                return ConfigurationManager.AppSettings[key];
            }

            if (!_values.ContainsKey(key))
            {
                throw new ArgumentException("Invalid key");
            }

            return _values[key];
        }
    }
}