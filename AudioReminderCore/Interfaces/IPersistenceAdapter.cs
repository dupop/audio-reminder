namespace AudioReminderCore.Interfaces
{
    public interface IPersistenceAdapter :
        IReminderEntityProvider,
        IServiceSettingsEntityProvider
    {
    }
}