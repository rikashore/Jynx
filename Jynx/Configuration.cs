using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace Jynx
{
    public class Configuration
    {
        private string[] _prefixes = null!;
        private string _token = null!;
        private string _version = null!;
        private string _dbconnection = null!;

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
                    throw new NullReferenceException($"Version must be defined in {_configurationPath}");

                _version = value;
            }
        }

        public string DbConnection
        {
            get => _dbconnection;
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new NullReferenceException($"DB Connection must be defined in {_configurationPath}");

                _dbconnection = value;
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
            DbConnection = config.GetValue<string>(nameof(DbConnection));
            ApiTrackerKey = config.GetValue<string>(nameof(ApiTrackerKey));
        }
    }
}
