namespace Rina;

public interface ISettingsService
{
    public void Update(Settings settings);
    public Settings Read();
}