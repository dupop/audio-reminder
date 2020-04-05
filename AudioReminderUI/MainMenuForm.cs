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

            
        }

        private void createReminderButton_Click(object sender, EventArgs e)
        {
            CreateAndUpdateReminderForm createReminderForm = new CreateAndUpdateReminderForm(PersistenceAdapter);
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
            ReminderListForm form = new ReminderListForm(PersistenceAdapter);
            form.ShowDialog();
        }

        private void Settings_Click(object sender, EventArgs e)
        {

            SettingsForm settingsForm = new SettingsForm(PersistenceAdapter);
            settingsForm.ShowDialog();
        }

    }
}
