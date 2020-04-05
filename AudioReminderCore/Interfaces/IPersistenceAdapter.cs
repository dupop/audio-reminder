using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    //TODO: this class has no specific reason to exist alone, remove it by inlining these interfaces

    [ServiceContract]
    public interface IPersistenceAdapter :
        IReminderEntityProvider,
        IServiceSettingsEntityProvider
    {
    }
}