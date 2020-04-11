using AudioReminderCore.Model;
using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IServiceSettingsEntityProvider
    {
        [OperationContract]
        void UpdateSettings(ServiceSettingsEntity settings);
        
        [OperationContract]
        ServiceSettingsEntity LoadSettings();

        [OperationContract]
        void TestRinging();

        [OperationContract]
        void TestBeeper();
    }
}