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
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void createReminderButton_Click(object sender, EventArgs e)
        {
            CreateReminderForm createReminderForm = new CreateReminderForm();
            createReminderForm.ShowDialog();
        }

        private void remindersButton_Click(object sender, EventArgs e)
        {
            ReminderListForm form = new ReminderListForm();
            form.ShowDialog();
        }

        private void Settings_Click(object sender, EventArgs e)
        {

            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }
    }
}
