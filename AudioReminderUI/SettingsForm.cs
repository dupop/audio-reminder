using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
{
    public partial class SettingsForm : Form
    {
        protected PersistenceAdapter PersistenceAdapter;

        public SettingsForm(PersistenceAdapter persistenceAdapter)
        {
            InitializeComponent();

            PersistenceAdapter = persistenceAdapter;
            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var settings = PersistenceAdapter.LoadSettings();
            DisplaySettingsFromDto(settings);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var settings = CreateServiceSettingsEntity();
            
            PersistenceAdapter.UpdateSettings(settings);

            DialogResult = DialogResult.OK;
            Log.Logger.Information($"Closing SettingsForm with result OK.");
        }


        protected virtual ServiceSettingsEntity CreateServiceSettingsEntity()
        {
           var settings = new ServiceSettingsEntity()
            {
                AutoStartService = autostartEnabledCheckbox.Checked,
                ServiceEnabled = serviceEnabledcheckBox.Checked,
                BeeperEnabled = beeperEnabledcheckBox.Checked,
                BeeperIntervalMinutes = (int)beepIntervalNumbericBox.Value,
                SnoozeEnabled = snoozeEnabledcheckBox.Checked,
                SnoozeIntervalMinutes = (int)snoozeIntervalNumbericBox.Value
            };

            return settings;
        }

        protected virtual void DisplaySettingsFromDto(ServiceSettingsEntity settings)
        {
            autostartEnabledCheckbox.Checked = settings.AutoStartService;
            serviceEnabledcheckBox.Checked = settings.ServiceEnabled;
            beeperEnabledcheckBox.Checked = settings.BeeperEnabled;
            beepIntervalNumbericBox.Value = settings.BeeperIntervalMinutes;
            snoozeEnabledcheckBox.Checked = settings.SnoozeEnabled;
            snoozeIntervalNumbericBox.Value = settings.SnoozeIntervalMinutes;
        }

        private void testRinging_Click(object sender, EventArgs e)
        {
            PersistenceAdapter.TestRinging();
        }

        private void testBeeper_Click(object sender, EventArgs e)
        {
            PersistenceAdapter.TestBeeper();
        }

    }
}
