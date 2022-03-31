using Microsoft.Extensions.Configuration;
using WritableJsonConfiguration;

namespace Rina;

public class SettingsService : ISettingsService
{
    private readonly IConfigurationRoot _appConfig;
    
    public SettingsService()
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        this._appConfig = configurationBuilder.Add<WritableJsonConfigurationSource>(
            (Action<WritableJsonConfigurationSource>)(s =>
            {
                s.FileProvider = null;
                s.Path = "appsettings.json";
                s.Optional = false;
                s.ReloadOnChange = true;
                s.ResolveFileProvider();
            })).Build();
    }
    
    public void Update(Settings settings)
    {
        this._appConfig["userId"] = settings.UserId?.ToString();
    }

    public Settings Read()
    {
        Settings settings = new Settings();
        settings.ConnectionString = _appConfig.GetConnectionString("dbName");
        settings.UserId = _appConfig["UserId"] is not null ? Convert.ToInt32(_appConfig["UserId"]) : null;
        return settings;
    }
}