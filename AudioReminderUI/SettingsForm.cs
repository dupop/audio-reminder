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
            selectRingingSoundButton.Text = TranslationProvider.Tr("selectRingingSoundButton");
            serviceStatusGroupBox.Text = TranslationProvider.Tr("serviceStatusGroupBox");
            autostartEnabledCheckbox.Text = TranslationProvider.Tr("autostartServiceCheckBox");
            serviceEnabledcheckBox.Text = TranslationProvider.Tr("serviceEnabledCheckBox");
            ringingGroupBox.Text = TranslationProvider.Tr("ringingGroupBox");
            currentRingingSoundNameLabel.Text = TranslationProvider.Tr("currentRingingSoundNameLabel");
            testRinging.Text = TranslationProvider.Tr("testRingingButton");
            snoozeGroupBox.Text = TranslationProvider.Tr("snoozeGroupBox");
            snoozeEnabledcheckBox.Text = TranslationProvider.Tr("snoozeEnabledCheckBox");
            label3.Text = TranslationProvider.Tr("snoozeIntervalLabelForNumericCheckBox");
            snoozeIntervalNumbericBox.AccessibleName = TranslationProvider.Tr("snoozeIntervalLabelForNumericCheckBox");
            beeperGroupBox.Text = TranslationProvider.Tr("beeperGroupBox");
            beeperEnabledcheckBox.Text = TranslationProvider.Tr("beeperEnabledCheckBox");
            label1.Text = TranslationProvider.Tr("beepIntervalLabelForNumericCheckBox");
            beepIntervalNumbericBox.AccessibleName = TranslationProvider.Tr("beepIntervalLabelForNumericCheckBox");
            selectBeeperSoundButton.Text = TranslationProvider.Tr("selectBeeperSoundButton");
            currentBeeperSoundNameLabel.Text = TranslationProvider.Tr("currentBeeperSoundNameLabel");
            testBeeper.Text = TranslationProvider.Tr("testBeeperButton");
            dataMigrationGroupBox.Text = TranslationProvider.Tr("dataMigrationGroupBox");
            exportButton.Text = TranslationProvider.Tr("exportButton");
            importButton.Text = TranslationProvider.Tr("importButton");
            okButton.Text = TranslationProvider.Tr("okButton");
            cancelButton.Text = TranslationProvider.Tr("cancelButton");
            Text = TranslationProvider.Tr("audioReminderSettingsFormTitle");
            currentRingingSoundNameBox.AccessibleName = TranslationProvider.Tr("currentRingingSoundNameLabel");
            currentBeeperSoundNameBox.AccessibleName = TranslationProvider.Tr("currentBeeperSoundNameLabel");
            languageGroupBox.Text = TranslationProvider.Tr("languageGroupBox");
            languageButton.Text = TranslationProvider.Tr("languageButton");

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
                Language = TranslationProvider.Language
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

            TranslationProvider.LoadNewLanguage(nextLanguageName);
            
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

            int currentLanguageIndex = listOfLanguages.IndexOf(TranslationProvider.Language);

            int nextLanguageIndex = (currentLanguageIndex + 1) % listOfLanguages.Count; //TODO: handle 0 languages somewhere
            string nextLanguageName = listOfLanguages[nextLanguageIndex];

            return nextLanguageName;
        }
    }
}
