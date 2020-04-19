using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IPersistenceAdapter :
        IReminderEntityProvider,
        IServiceSettingsEntityProvider
    {
    }
}