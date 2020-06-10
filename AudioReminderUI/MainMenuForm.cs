using AudioReminderCore;
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
    public partial class MainMenuForm : Form
    {
        private PersistenceAdapter PersistenceAdapter;
        public MainMenuForm()
        {
            InitializeComponent();

            PersistenceAdapter = new PersistenceAdapter();
            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Translate();
        }

        public virtual void Translate()
        {
            Text = TranslationProvider.Tr("audioReminderMainMenuFormTitle");
            createReminderButton.Text = TranslationProvider.Tr("createReminderButton");
            remindersButton.Text = TranslationProvider.Tr("reminderButton");
            settingsButton.Text = TranslationProvider.Tr("settingsButton");
            helpButton.Text = TranslationProvider.Tr("helpButton");
            aboutButton.Text = TranslationProvider.Tr("aboutButton");
        }

        private void createReminderButton_Click(object sender, EventArgs e)
        {
            var createReminderForm = new CreateAndUpdateReminderForm(PersistenceAdapter);
            createReminderForm.ShowDialog();

            if (createReminderForm.DialogResult != DialogResult.OK)
            {
                Log.Logger.Information($"CreateReminderForm closed with result NotOk.");
                return;
            }

            var createdReminder = createReminderForm.CreateOrUpdatedReminder;
            PersistenceAdapter.Save(createdReminder);
        }

        private void remindersButton_Click(object sender, EventArgs e)
        {
            var form = new reminderListForm(PersistenceAdapter);
            form.ShowDialog();
        }

        private void Settings_Click(object sender, EventArgs e)
        {

            var settingsForm = new SettingsForm(PersistenceAdapter, this);
            settingsForm.ShowDialog();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new helpForm();
            aboutForm.ShowDialog();
        }

    }
}
