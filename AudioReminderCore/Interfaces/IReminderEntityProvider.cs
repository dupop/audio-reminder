using AudioReminderCore.Model;
using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IReminderEntityProvider
    {
        [OperationContract]
        void Save(ReminderEntity createdReminder);
        
        [OperationContract]
        ReminderEntity[] LoadAll();

        [OperationContract]
        ReminderEntity Load(string reminderName);

        [OperationContract]
        bool Update(string reminderOldName, ReminderEntity reminder);
        
        [OperationContract]
        bool Delete(string reminderName);
    }
}