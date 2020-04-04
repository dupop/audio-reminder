using AudioReminderCore.Model;
using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IReminderEntityProvider : IReminderNameAvailabilityChecker
    {
        [OperationContract]
        void Save(ReminderEntity createdReminder);
        
        [OperationContract]
        ReminderEntity[] Load();
        
        [OperationContract]
        void Update(string reminderOldName, ReminderEntity reminder);
        
        [OperationContract]
        void Delete(string reminderName);
    }

    //TODO: maybe extracct all arguments of each request from this and other interfaces and create e.g. DelteRequest(=string) and DeleteResponse(=void) entities. 
    //we can than send method name (fixed length or announed length) followed by OperationRequest.Than appropruiateOperationReponse would be put in the pipe. 
    //With that approach exception handling would probably need to go thgorugh response and requests would need to be queued, ... how to link responses than.

    //better use WCF or at least basic REST
}