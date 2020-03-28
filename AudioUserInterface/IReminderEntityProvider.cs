namespace AudioUserInterface
{
    public interface IReminderEntityProvider: IReminderNameAvailabilityChecker
    {
        void Save(ReminderEntity createdReminder);
        ReminderEntity[] Load();
        void Update(string reminderOldName, ReminderEntity reminder);
        void Delete(string reminderName);
    }
}