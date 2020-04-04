using System.ServiceModel;

namespace AudioReminderCore.Interfaces
{
    [ServiceContract]
    public interface IReminderCallBack
    {
        [OperationContract]
        void RegsiterForReminderCallBack();
    }
}