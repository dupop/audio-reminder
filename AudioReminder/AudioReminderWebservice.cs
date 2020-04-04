using AudioReminderCore.Interfaces;
using AudioReminderCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminder
{
    class AudioReminderWebservice : IAudioReminderService
    {
        public void Delete(string reminderName)
        {
            throw new NotImplementedException();
        }

        public bool IsNameAvailable(string reminderName)
        {
            throw new NotImplementedException();
        }

        public ReminderEntity[] Load()
        {
            throw new NotImplementedException();
        }

        public ServiceSettingsDto LoadSettings()
        {
            throw new NotImplementedException();
        }

        public void RegsiterForReminderCallBack()
        {
            throw new NotImplementedException();
        }

        public void Save(ReminderEntity createdReminder)
        {
            throw new NotImplementedException();
        }

        public void Update(string reminderOldName, ReminderEntity reminder)
        {
            throw new NotImplementedException();
        }

        public void UpdateSettings(ServiceSettingsDto settings)
        {
            throw new NotImplementedException();
        }
    }
}
