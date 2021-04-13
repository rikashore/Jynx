using Emzi0767.Utilities;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jynx
{
    public class Config
    {
        public string Token { get; set; }
        public List<string> Prefixes { get; set; }
        public string Version { get; set; }
        public string ApiTrackerKey { get; set; }
    }

    public class Configuration
    {
        private string[] _prefixes = null!;
        private string _token = null!;
        private string _version = null!;

        private readonly string _configurationPath = Path.Combine(Environment.CurrentDirectory, "config.json");

        public string[] Prefixes
        {
            get => _prefixes;
            set
            {
                if (value == null)
                    throw new NullReferenceException($"Prefixes must be defined in {_configurationPath}");

                _prefixes = value;
            }
        }

        public string Token
        {
            get => _token;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException($"Token must be defined in {_configurationPath}");

                _token = value;
            }
        }

        public string Version
        {
            get => _version;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException($"Token must be defined in {_configurationPath}");

                _version = value;
            }
        }

        public string ApiTrackerKey { get; set; }

        public Configuration()
        {
            LoadConfiguration();

            if (ApiTrackerKey == null || string.IsNullOrEmpty(ApiTrackerKey))
                Log.Warning("No ApiTrackerKey has been set, commands using this will be unavailable");
        }

        private void LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(_configurationPath)
                .Build();

            Prefixes = config.GetSection(nameof(Prefixes)).Get<string[]>();

            Token = config.GetValue<string>(nameof(Token));
            Version = config.GetValue<string>(nameof(Version));
            ApiTrackerKey = config.GetValue<string>(nameof(ApiTrackerKey));
        }
    }
}
