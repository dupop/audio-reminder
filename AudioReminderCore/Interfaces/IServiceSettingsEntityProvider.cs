using AudioReminderCore.Model;
using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IServiceSettingsEntityProvider
    {
        [OperationContract]
        void UpdateSettings(ServiceSettingsDto settings);
        
        [OperationContract]
        ServiceSettingsDto LoadSettings();
    }
}