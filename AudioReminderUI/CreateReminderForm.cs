using AudioReminderCore;
using AudioReminderCore.Interfaces;
using AudioReminderCore.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
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
            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Text = "Audio Reminder - Create reminder";
            scheduledDatePicker.Value = GetTomorrowLocalDateTime();

            Translate();
        }

        protected virtual void Translate()
        {
            eventNameLabel.Text = TranslProvider.Tr("reminderNameLabel");
            dateLabel.Text = TranslProvider.Tr("dateOfFirstOccuranceLabelForDatetimePicker");
            eventDetailsGroupBox.Text = TranslProvider.Tr("eventDetailsGroupBox");
            eventDescriptionLabel.Text = TranslProvider.Tr("eventDescriptionLabel");
            scheduledTimeGroupBox.Text = TranslProvider.Tr("scheduledTimeGroupBox");
            timeLabel.Text = TranslProvider.Tr("timeLabelForNumericBox");
            repeatPeriodGroupBox.Text = TranslProvider.Tr("repeatPeriodGroupBox");
            repeatDaysLabel.Text = TranslProvider.Tr("repeatWeeklyLabelForCheckedListBox");
            repeatMonthlyCheckBox.Text = TranslProvider.Tr("repeatMonthlyCheckBox");
            repeatYearlyCheckBox.Text = TranslProvider.Tr("repeatYearlyCheckBox");
            okButton.Text = TranslProvider.Tr("okButton");
            cancelButton.Text = TranslProvider.Tr("cancelButton");
            reminderNameStringBox.AccessibleName = TranslProvider.Tr("reminderNameLabel");
            reminderDescriptionTextbox.AccessibleName = TranslProvider.Tr("eventDescriptionLabel");
            scheduledDatePicker.AccessibleDescription = TranslProvider.Tr("scheduledDatePickerAccDes");
            scheduledDatePicker.AccessibleName = TranslProvider.Tr("dateOfFirstOccuranceLabelForDatetimePicker");
            hoursNumericBox.AccessibleName = TranslProvider.Tr("hoursNumericBoxAccName");
            hoursNumericBox.AccessibleDescription = TranslProvider.Tr("hoursNumericBoxAccDes");
            minuteNumbericBox.AccessibleName = TranslProvider.Tr("minuteNumbericBoxAccName");
            minuteNumbericBox.AccessibleDescription = TranslProvider.Tr("minuteNumbericBoxAccDes");
            Text = TranslProvider.Tr("createReminderFormTitle");
            repeatWeeklyCheckedListBox.T = TranslProvider.Tr("createReminderFormTitle");
        }

        public CreateAndUpdateReminderForm(PersistenceAdapter nameChecker, ReminderEntity reminderToUpdate)
        {
            InitializeComponent();
            this.nameChecker = nameChecker;
            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Text = "Audio Reminder - Update reminder";
            DisplayReminderToUpdate(reminderToUpdate);
            oldValueOfReminderToBeUpdated = reminderToUpdate;
        }

        protected virtual DateTime GetTomorrowLocalDateTime()
        {
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
            DateTime tomorrow = DateTime.Now.Date + oneDay;

            return tomorrow;
        }

        protected virtual void DisplayReminderToUpdate(ReminderEntity reminderToUpdate)
        {
            reminderNameStringBox.Text = reminderToUpdate.Name;

            DateTime scheduledLocalTime = ConvertFromUtcToLocal(reminderToUpdate.ScheduledTime);

            scheduledDatePicker.Value = scheduledLocalTime.Date;
            hoursNumericBox.Value = scheduledLocalTime.Hour;
            minuteNumbericBox.Value = scheduledLocalTime.Minute;

            for (int i = 0; i < 7; i++)
            {
                repeatWeeklyCheckedListBox.SetItemCheckState(i, reminderToUpdate.RepeatWeeklyDays[i] ? CheckState.Checked : CheckState.Unchecked);
            }

            repeatMonthlyCheckBox.Checked = reminderToUpdate.RepeatPeriod == RepeatPeriod.Monthly;

            repeatYearlyCheckBox.Checked = reminderToUpdate.RepeatPeriod == RepeatPeriod.Yearly;
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
            DateTime scheduledDateTime = GetDateTimeFromUI();
            DateTime scheduledDateTimeUtc = ConvertFromLocalToUtc(scheduledDateTime);

            //create bool array from the checkbox list
            bool[] repeatWeeklyDays = new bool[7];
            foreach (int checkedIndex in repeatWeeklyCheckedListBox.CheckedIndices)
            {
                repeatWeeklyDays[checkedIndex] = true;
            }

            //disable weekly flag if 0 days are selected
            bool repeatWeekly = AnyChecked(repeatWeeklyDays);

            RepeatPeriod repeatPeriod = GetRepeatPeriod(repeatWeekly);

            var reminderEntity = new ReminderEntity()
            {
                Name = SanitizeReminderName(reminderNameStringBox.Text),
                ScheduledTime = scheduledDateTimeUtc,
                RepeatPeriod = repeatPeriod,
                RepeatWeeklyDays = repeatWeeklyDays
            };

            return reminderEntity;
        }

        private DateTime GetDateTimeFromUI()
        {
            return scheduledDatePicker.Value.Date + new TimeSpan((int)hoursNumericBox.Value, (int)minuteNumbericBox.Value, 0);
        }


        //TODO: remove this temporary fix when Reminder IDs are used instead of name + test support for cyrilic and other unicode characters
        #region Reminder name sanitization

        private string SanitizeReminderName(string userInput)
        {
            return new string(userInput.Select(SanitizeLetter).ToArray());
        }

        private static char SanitizeLetter(char character)
        {

            //ASCII alpahnumeric chars are safe for program arguments
            bool isLowerCaseAsciiLetter = character >= 'a' && character <= 'z';
            bool isUpperCaseAsciiLetter = character >= 'A' && character <= 'Z';
            bool isDigit = character >= '0' && character <= '9';
            if (isLowerCaseAsciiLetter || isUpperCaseAsciiLetter || isDigit)
            {
                return character;
            }

            //space may be translated to underscore
            if (character == ' ' || character == '_')
            {
                return '_';
            }

            //other character will become question mark
            return '?';
        }

        #endregion


        /// <summary>
        /// Determines if any value in the array is true
        /// </summary>
        public static bool AnyChecked(bool[] repeatWeeklyDays)
        {
            return Array.Exists(repeatWeeklyDays, element => element == true);
        }

        protected virtual RepeatPeriod GetRepeatPeriod(bool repeatWeekly)
        {
            RepeatPeriod repeatPeriod;

            if (repeatYearlyCheckBox.Checked)
            {
                repeatPeriod = RepeatPeriod.Yearly;
            }
            else if (repeatMonthlyCheckBox.Checked)
            {
                repeatPeriod = RepeatPeriod.Monthly;
            }
            else if (repeatWeekly)
            {
                repeatPeriod = RepeatPeriod.Weekly;
            }
            else
            {
                repeatPeriod = RepeatPeriod.NoRepeat;
            }

            return repeatPeriod;
        }

        protected virtual bool ValidateInput()
        {
            //TODO DP->SI: add warning if user attempts to create recuring event in future so that one or more occurence of reminder are skipped between now and the scheduled time.
            //Such a reminder would in some way be a contradiction because user violates his own rules. No need to keep track of such an edge case for now.
            //TODO: maybe add validation against special characters in reminder name that would interfere with xml persistence although CDATA elemtns should have some protection already

            DateTime scheduledDateTime = GetDateTimeFromUI();
            if (scheduledDateTime < DateTime.Now)
            {
                ErrorDialogUtility.ErrorDialog(TranslProvider.Tr("warningReminderElapsed"));
                return false;
            }    

            string reminderName = reminderNameStringBox.Text;
            bool reminderNameIsEmpty = string.IsNullOrWhiteSpace(reminderName);

            if (reminderNameIsEmpty)
            {
                ErrorDialogUtility.ErrorDialog(TranslProvider.Tr("warningMissingReminderName"));
                return false;
            }

            bool nameSameAsBefore = oldValueOfReminderToBeUpdated?.Name == reminderName;
            if (!nameSameAsBefore && !IsNameAvialable(reminderName))
            {
                ErrorDialogUtility.ErrorDialog(TranslProvider.Tr("warningReminderNameAlreadyExists"));
                return false;
            }

            bool multiplePeriodsChecked = DetermineIfMultiplePeriodsChecked();
            if (multiplePeriodsChecked)
            {
                ErrorDialogUtility.ErrorDialog(TranslProvider.Tr("warningMultipleReminderPeriodsSelected"));
                return false;
            }

            Log.Logger.Information($"Reminder '{reminderName}' valid.");
            return true;
        }

        protected virtual bool DetermineIfMultiplePeriodsChecked()
        {
            bool repeatWeekly = DetermineIfAtLeastOneDayInWeekIsChecked();

            int numberOfPeriodsChecked = 0;
            if (repeatWeekly)
            {
                numberOfPeriodsChecked++;
            }
            if (repeatMonthlyCheckBox.Checked)
            {
                numberOfPeriodsChecked++;
            }
            if (repeatYearlyCheckBox.Checked)
            {
                numberOfPeriodsChecked++;
            }

            bool multiplePeriodsChecked = numberOfPeriodsChecked > 1;
            return multiplePeriodsChecked;
        }

        protected virtual bool DetermineIfAtLeastOneDayInWeekIsChecked()
        {
            bool[] repeatWeeklyDays = new bool[7];
            foreach (int checkedIndex in repeatWeeklyCheckedListBox.CheckedIndices)
            {
                repeatWeeklyDays[checkedIndex] = true;
            }

            bool repeatWeekly = AnyChecked(repeatWeeklyDays);
            return repeatWeekly;
        }

        protected virtual bool IsNameAvialable(string reminderName)
        {

            bool nameAvialable = nameChecker.Load(reminderName) == null;

            Log.Logger.Information($"Checking if reminder name '{reminderName}' is avialable done. Result is {nameAvialable}");
            return nameAvialable;
        }


        /// <summary>
        /// Returns SchduledTime as a UTC time.
        /// </summary>
        /// <param name="scheduledLocalTime">Local time. The kind property does not need to be spcified.</param>
        /// <returns>UTC Time with Kind=UTC.</returns>
        protected virtual DateTime ConvertFromLocalToUtc(DateTime scheduledLocalTime)
        {
            //ToUniversalTime() needs the Kind of input time to be Local, so we ensure this here
            DateTime scheduledLocalTimeWithExplicitKind = new DateTime(scheduledLocalTime.Ticks, DateTimeKind.Local);

            DateTime scheduledTimeUtc = scheduledLocalTimeWithExplicitKind.ToUniversalTime();

            return scheduledTimeUtc;
        }

        /// <summary>
        /// Returns SchduledTime as a local time.
        /// </summary>
        /// <param name="scheduledUtcTime">UTC time. The kind property does not need to be spcified.</param>
        /// <returns>Local time in  with Kind=Local.</returns>
        public DateTime ConvertFromUtcToLocal(DateTime scheduledUtcTime)
        {
            //ToLocalTime() needs the Kind of input time to be UTC (or Unspecified as it is also treated as UTC), so we ensure this here
            DateTime scheduledUtcTimeWithExplicitKind = new DateTime(scheduledUtcTime.Ticks, DateTimeKind.Utc);

            DateTime scheduledLocalTime = scheduledUtcTimeWithExplicitKind.ToLocalTime();

            return scheduledLocalTime;
        }
    }
}
