using Data;

namespace Rina;

public class Configuration
{
    public ISettingsService SettingsService { get; private set; } = null!;
    public IEmployerService EmployerService { get; private set; } = null!;

    public IEmployerDocumentService EmployerDocumentService { get; private set; } = null!;


    public Configuration()
    {
        this.Setup();
    }

    public void Setup()
    {
        this.SettingsService = new SettingsService();
        var settings = SettingsService.Read();
        this.EmployerService = new EmployerService(settings.ConnectionString);
        this.EmployerDocumentService = new EmployerDocumentService(settings.ConnectionString);
    }
}