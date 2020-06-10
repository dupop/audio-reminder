using AudioReminderCore;
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

        public MainMenuForm MainMenuForm { get; }

        public SettingsForm(PersistenceAdapter persistenceAdapter, MainMenuForm mainMenuForm)
        {
            InitializeComponent();

            MainMenuForm = mainMenuForm;
            PersistenceAdapter = persistenceAdapter;
            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Translate();
        }

        protected virtual void Translate()
        {
            selectRingingSoundButton.Text = TranslProvider.Tr("selectRingingSoundButton");
            serviceStatusGroupBox.Text = TranslProvider.Tr("serviceStatusGroupBox");
            autostartEnabledCheckbox.Text = TranslProvider.Tr("autostartServiceCheckBox");
            serviceEnabledcheckBox.Text = TranslProvider.Tr("serviceEnabledCheckBox");
            ringingGroupBox.Text = TranslProvider.Tr("ringingGroupBox");
            currentRingingSoundNameLabel.Text = TranslProvider.Tr("currentRingingSoundNameLabel");
            testRinging.Text = TranslProvider.Tr("testRingingButton");
            snoozeGroupBox.Text = TranslProvider.Tr("snoozeGroupBox");
            snoozeEnabledcheckBox.Text = TranslProvider.Tr("snoozeEnabledCheckBox");
            label3.Text = TranslProvider.Tr("snoozeIntervalLabelForNumericCheckBox");
            snoozeIntervalNumbericBox.AccessibleName = TranslProvider.Tr("snoozeIntervalLabelForNumericCheckBox");
            beeperGroupBox.Text = TranslProvider.Tr("beeperGroupBox");
            beeperEnabledcheckBox.Text = TranslProvider.Tr("beeperEnabledCheckBox");
            label1.Text = TranslProvider.Tr("beepIntervalLabelForNumericCheckBox");
            beepIntervalNumbericBox.AccessibleName = TranslProvider.Tr("beepIntervalLabelForNumericCheckBox");
            selectBeeperSoundButton.Text = TranslProvider.Tr("selectBeeperSoundButton");
            currentBeeperSoundNameLabel.Text = TranslProvider.Tr("currentBeeperSoundNameLabel");
            testBeeper.Text = TranslProvider.Tr("testBeeperButton");
            dataMigrationGroupBox.Text = TranslProvider.Tr("dataMigrationGroupBox");
            exportButton.Text = TranslProvider.Tr("exportButton");
            importButton.Text = TranslProvider.Tr("importButton");
            okButton.Text = TranslProvider.Tr("okButton");
            cancelButton.Text = TranslProvider.Tr("cancelButton");
            Text = TranslProvider.Tr("audioReminderSettingsFormTitle");
            currentRingingSoundNameBox.AccessibleName = TranslProvider.Tr("currentRingingSoundNameLabel");
            currentBeeperSoundNameBox.AccessibleName = TranslProvider.Tr("currentBeeperSoundNameLabel");
            languageGroupBox.Text = TranslProvider.Tr("languageGroupBox");
            languageButton.Text = TranslProvider.Tr("languageButton");

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
                SnoozeIntervalMinutes = (int)snoozeIntervalNumbericBox.Value,
                Language = TranslProvider.Language
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

        private void languageButton_Click(object sender, EventArgs e)
        {
            string nextLanguageName = GetNextLanguageName();

            TranslProvider.LoadNewLanguage(nextLanguageName);
            
            Translate(); //translate current form
            MainMenuForm.Translate();
        }

        /// <summary>
        /// Placeholder implementation to switch languages one by one until a list for selection is implemented
        /// </summary>
        private static string GetNextLanguageName()
        {
            TranslationsLoader translationsLoader = new TranslationsLoader();
            List<string> listOfLanguages = translationsLoader.GetListOfLanguages();

            int currentLanguageIndex = listOfLanguages.IndexOf(TranslProvider.Language);

            int nextLanguageIndex = (currentLanguageIndex + 1) % listOfLanguages.Count; //TODO: handle 0 languages somewhere
            string nextLanguageName = listOfLanguages[nextLanguageIndex];

            return nextLanguageName;
        }
    }
}
