namespace Application.Configuration
{
    public interface IConfigManager
    {
        public string GetAppSettingsValue(string name);

        public string GetConnectionString(string name);

    }
}
