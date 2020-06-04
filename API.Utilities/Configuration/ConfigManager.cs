using Microsoft.Extensions.Configuration;
using System;

namespace API.Utilities.Configuration
{
    public static class ConfigManager
    {
        public static string GetAppSettingsValue(IConfiguration Configuration, string name)
        {
            if (!bool.Parse(Configuration.GetSection("AppSettings")["DockerEnv"]))
            {
                try
                {
                    var value = Configuration.GetSection("AppSettings")[name];
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

        public static string GetConnectionString(IConfiguration Configuration, string name)
        {
            if (!bool.Parse(Configuration.GetSection("AppSettings")["DockerEnv"]))
            {
                try
                {
                    var value = Configuration.GetConnectionString(name);
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
