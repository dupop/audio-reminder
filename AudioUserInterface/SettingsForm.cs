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

namespace AudioUserInterface
{
    public partial class SettingsForm : Form
    {
        protected PersistenceAdapter PersistenceAdapter;

        public SettingsForm(PersistenceAdapter persistenceAdapter)
        {
            InitializeComponent();
            PersistenceAdapter = persistenceAdapter;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var settings = PersistenceAdapter.LoadSettings();
            DisplaySettingsFromDto(settings);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var settings = CreateServiceSettingsDto();
            
            PersistenceAdapter.UpdateSettings(settings);

            DialogResult = DialogResult.OK;
            Log.Logger.Information($"Closing SettingsForm with result OK.");
        }


        protected virtual ServiceSettingsDto CreateServiceSettingsDto()
        {
           var settings = new ServiceSettingsDto()
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

        protected virtual void DisplaySettingsFromDto(ServiceSettingsDto settings)
        {
            autostartEnabledCheckbox.Checked = settings.AutoStartService;
            serviceEnabledcheckBox.Checked = settings.ServiceEnabled;
            beeperEnabledcheckBox.Checked = settings.BeeperEnabled;
            beepIntervalNumbericBox.Value = settings.BeeperIntervalMinutes;
            snoozeEnabledcheckBox.Checked = settings.SnoozeEnabled;
            snoozeIntervalNumbericBox.Value = settings.SnoozeIntervalMinutes;
        }

    }
}
