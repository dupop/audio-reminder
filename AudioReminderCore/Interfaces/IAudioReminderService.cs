using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IAudioReminderService :
        IPersistenceAdapter,
        IReminderRinging
    {
    }
}