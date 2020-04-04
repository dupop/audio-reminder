using AudioReminderCore.Interfaces;
using AudioReminderCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace AudioReminderCore.ClientProxies
{
    //TODO: handle service not avilable wiht error message probably
    public class AudioReminderWebServiceClient : ClientBase<IAudioReminderService>, IAudioReminderService
    {
        #region Constructors
        public AudioReminderWebServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public AudioReminderWebServiceClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public AudioReminderWebServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public AudioReminderWebServiceClient(Binding binding, EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }
        #endregion


        #region Proxy methods
        public void Delete(string reminderName)
        {
            Channel.Delete(reminderName);
        }

        public bool IsNameAvailable(string reminderName)
        {
            return Channel.IsNameAvailable(reminderName);
        }

        public ReminderEntity[] Load()
        {
            return Channel.Load();
        }

        public ServiceSettingsDto LoadSettings()
        {
            return Channel.LoadSettings();
        }

        public void RegsiterForReminderCallBack()
        {
            Channel.RegsiterForReminderCallBack();
        }

        public void Save(ReminderEntity createdReminder)
        {
            Channel.Save(createdReminder);
        }

        public void Update(string reminderOldName, ReminderEntity reminder)
        {
            Channel.Update(reminderOldName, reminder);
        }

        public void UpdateSettings(ServiceSettingsDto settings)
        {
            Channel.UpdateSettings(settings);
        }
        #endregion

    }
}
