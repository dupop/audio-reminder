using AudioReminderCore.Model;

namespace AudioReminderCore.Interfaces
{
    public interface IServiceSettingsEntityProvider
    {
        void UpdateSettings(ServiceSettingsDto settings);
        ServiceSettingsDto LoadSettings();
    }
}