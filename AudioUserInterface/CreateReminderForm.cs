using AudioReminderCore.Interfaces;
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

namespace AudioUserInterface
{
    public partial class CreateAndUpdateReminderForm : Form
    {
        public ReminderEntity CreateOrUpdatedReminder { get; set; }
        private ReminderEntity oldValueOfReminderToBeUpdated;
        private PersistenceAdapter nameChecker;

        public CreateAndUpdateReminderForm(PersistenceAdapter nameChecker)
        {
            InitializeComponent();
            this.nameChecker = nameChecker;
            Text = "Create reminder";
        }
        public CreateAndUpdateReminderForm(PersistenceAdapter nameChecker, ReminderEntity reminderToUpdate)
        {
            InitializeComponent();
            this.nameChecker = nameChecker;
            Text = "Update reminder";
            DisplayReminderToUpdate(reminderToUpdate);
            oldValueOfReminderToBeUpdated = reminderToUpdate;
        }

        protected virtual void DisplayReminderToUpdate(ReminderEntity reminderToUpdate)
        {
            reminderNameStringBox.Text = reminderToUpdate.Name;

            scheduledTimePicker.Value = reminderToUpdate.ScheduledTime.Date;
            hoursNumericBox.Value = reminderToUpdate.ScheduledTime.Hour;
            minuteNumbericBox.Value = reminderToUpdate.ScheduledTime.Minute;

            repeatWeeklyCheckBox.Checked = reminderToUpdate.RepeatWeekly;

            for (int i = 0; i < 7; i++)
            {
                repeatWeeklyCheckedListBox.SetItemCheckState(i, reminderToUpdate.RepeatWeeklyDays[i] ? CheckState.Checked : CheckState.Unchecked);
            }

            repeatMonthlyCheckBox.Checked = reminderToUpdate.RepeatMonthly;
            
            repeatYearlyCheckBox.Checked = reminderToUpdate.RepeatYearly;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            CreateOrUpdatedReminder = CreateReminderEntity();
            DialogResult = DialogResult.OK;

            Log.Logger.Information($"Closing CreateReminderForm with result OK.");
        }

        protected virtual ReminderEntity CreateReminderEntity()
        {
            DateTime scheduledDateTime = scheduledTimePicker.Value + new TimeSpan((int)hoursNumericBox.Value, (int)minuteNumbericBox.Value, 0);
            
            bool[] repeatWeeklyDays = new bool[7];
            if (repeatWeeklyCheckBox.Checked)
            {
                foreach (int checkedIndex in repeatWeeklyCheckedListBox.CheckedIndices)
                {
                    repeatWeeklyDays[checkedIndex] = true;
                }
            }

            var reminderEntity = new ReminderEntity()
            {
                Name = reminderNameStringBox.Text,
                ScheduledTime = scheduledDateTime,
                RepeatWeekly = repeatWeeklyCheckBox.Checked,
                RepeatWeeklyDays = repeatWeeklyDays,
                RepeatMonthly = repeatMonthlyCheckBox.Checked,
                RepeatYearly = repeatYearlyCheckBox.Checked
            };
            
            return reminderEntity;
        }

        protected virtual bool ValidateInput()
        {
            string reminderName = reminderNameStringBox.Text;
            bool reminderNameIsEmpty =  string.IsNullOrWhiteSpace(reminderName);

            if (reminderNameIsEmpty)
            {
                ErrorDialogUtility.ErrorDialog("Reminder name is missing");
                return false;
            }

            bool nameSameAsBefore = oldValueOfReminderToBeUpdated?.Name == reminderName;
            if(!nameSameAsBefore && !IsNameAvialable(reminderName))
            {
                ErrorDialogUtility.ErrorDialog("Reminder name already exists");
                return false;
            }

            Log.Logger.Information($"Reminder '{reminderName}' valid.");
            return true;
        }

        protected virtual bool IsNameAvialable(string reminderName)
        {

            bool nameAvialable = nameChecker.Load(reminderName) == null;
            
            Log.Logger.Information($"Checking if reminder name '{reminderName}' is avialable done. Result is {nameAvialable}");
            return nameAvialable;
        }

    }
}
