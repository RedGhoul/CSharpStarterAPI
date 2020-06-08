using Microsoft.Extensions.Configuration;
using System;

namespace Application.Configuration
{
    public class ConfigManager : IConfigManager
    {
        readonly IConfiguration _Configuration;
        public ConfigManager(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string GetAppSettingsValue(string name)
        {
            if (!bool.Parse(_Configuration.GetSection("AppSettings")["DockerEnv"]))
            {
                try
                {
                    var value = _Configuration.GetSection("AppSettings")[name];
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"Could not find {name} in the Configuration");
                }

            }
            return Environment.GetEnvironmentVariable(name);

        }

        public string GetConnectionString(string name)
        {
            if (!bool.Parse(_Configuration.GetSection("AppSettings")["DockerEnv"]))
            {
                try
                {
                    var value = _Configuration.GetConnectionString(name);
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"Could not find {name} in the Configuration");
                }

            }
            return Environment.GetEnvironmentVariable(name);

        }
    }
}
