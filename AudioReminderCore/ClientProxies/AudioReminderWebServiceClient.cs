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

        /// <summary>
        /// Creates the client with default binding and endpoint of AudioReminder service
        /// </summary>
        public AudioReminderWebServiceClient() :
                base(WebserviceAdressHelper.GetAudioReminderBinding(), WebserviceAdressHelper.GetAudioReminderEndpoint())
        {
        }
        #endregion


        #region Proxy methods
        public bool Delete(string reminderName)
        {
            return Channel.Delete(reminderName);
        }

        public ReminderEntity Load(string reminderName)
        {
            return Channel.Load(reminderName);
        }

        public ReminderEntity[] LoadAll()
        {
            return Channel.LoadAll();
        }

        public ServiceSettingsEntity LoadSettings()
        {
            return Channel.LoadSettings();
        }

        public void SnoozeReminder(string reminderName)
        {
            Channel.SnoozeReminder(reminderName);
        }

        public void DismissReminder(string reminderName)
        {
            Channel.DismissReminder(reminderName);
        }

        public void Save(ReminderEntity createdReminder)
        {
            Channel.Save(createdReminder);
        }

        public bool Update(string reminderOldName, ReminderEntity reminder)
        {
            return Channel.Update(reminderOldName, reminder);
        }

        public void UpdateSettings(ServiceSettingsEntity settings)
        {
            Channel.UpdateSettings(settings);
        }

        public void TestRinging()
        {
            Channel.TestRinging();
        }

        public void TestBeeper()
        {
            Channel.TestBeeper();
        }
        #endregion

    }
}
