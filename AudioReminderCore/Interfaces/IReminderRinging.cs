using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IReminderRinging
    {
        [OperationContract]
        void SnoozeReminder(string reminderName);
        
        [OperationContract]
        void DismissReminder(string reminderName);
    }
}