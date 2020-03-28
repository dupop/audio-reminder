namespace AudioUserInterface
{
    public interface IServiceSettingsEntityProvider
    {
        void UpdateSettings(ServiceSettingsDto settings);
        ServiceSettingsDto LoadSettings();
    }
}