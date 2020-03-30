namespace AudioReminderCore.Interfaces
{
    public interface IAudioReminderService :
        IPersistenceAdapter,
        IReminderCallBack
    {
    }
}