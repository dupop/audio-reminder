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
    public partial class ReminderListForm : Form
    {
        #region Properties
        const Keys DeleteActionKey = Keys.Delete;
        const Keys EditActionKey = Keys.Enter;
        const Keys CloneActionKey = Keys.Space;
        
        PersistenceAdapter PersistenceAdapter;
        
        protected ReminderEntity SelectedReminder => remindersListBox.SelectedItem as ReminderEntity;
        #endregion


        #region Constructor and events
        public ReminderListForm(PersistenceAdapter persistenceAdapter)
        {
            InitializeComponent();
            
            PersistenceAdapter = persistenceAdapter;
            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
        }

        private void ReminderListForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteAction();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            EditAction();
        }

        private void cloneButton_Click(object sender, EventArgs e)
        {
            CloneAction();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (remindersListBox.Focused)
            {
                ProcessKeyPressedOnListBox(keyData);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void remindersListBox_Leave(object sender, EventArgs e)
        {
            ReturnFocusToListBox();
        }
        #endregion


        #region Logic
        protected virtual void LoadData()
        {
            ReminderEntity[] loadedReminders = PersistenceAdapter.LoadAll();
            remindersListBox.Items.AddRange(loadedReminders);
        }

        protected virtual void ProcessKeyPressedOnListBox(Keys keyData)
        {
            switch (keyData)
            {
                case DeleteActionKey:
                    DeleteAction();
                    break;
                case EditActionKey:
                    EditAction();
                    break;
                case CloneActionKey:
                    CloneAction();
                    break;
            }
        }

        protected virtual void EditAction()
        {
            ReminderEntity reminderToUpdate = SelectedReminder;
            if (reminderToUpdate == null)
            {
                ErrorDialogUtility.ErrorDialog("No reminder selected to edit");
                return;
            }

            CreateAndUpdateReminderForm createReminderForm = new CreateAndUpdateReminderForm(PersistenceAdapter, reminderToUpdate);
            createReminderForm.ShowDialog();

            if (createReminderForm.DialogResult != DialogResult.OK)
            {
                Log.Logger.Information($"UpdateReminderForm closed with result NotOk.");
                return;
            }

            var updatedReminder = createReminderForm.CreateOrUpdatedReminder;

            bool result = PersistenceAdapter.Update(reminderToUpdate.Name, updatedReminder);

            if(result == true)
            {
                //using the element in the listbox
                remindersListBox.Items[remindersListBox.SelectedIndex] = updatedReminder;
            }
            else
            {
                ErrorDialogUtility.ErrorDialog("Can't modify snoozed reminders. Snoozed reminders will be started now.");
            }
            
        }

        protected virtual void CloneAction()
        {
            if (SelectedReminder == null)
            {
                ErrorDialogUtility.ErrorDialog("No reminder selected to clone");
                return;
            }


            //TODO: add clone dialog
            ErrorDialogUtility.ErrorDialog("Not yet implemented");
            ReminderEntity clone = (ReminderEntity) SelectedReminder.Clone();

            //TODO: missing validation of backend causes creating duplicate name
            //PersistenceAdapter.Save(clone);
            //remindersListBox.Items.Add(clone);
        }

        protected virtual void DeleteAction()
        {
            ReminderEntity reminderToDelete = SelectedReminder;
            if (reminderToDelete == null)
            {
                ErrorDialogUtility.ErrorDialog("No reminder selected to delete");
                return;
            }

            DialogResult confirmationResult = MessageBox.Show($"Confirm deleting: {reminderToDelete.Name}.", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (confirmationResult != DialogResult.OK)
            {
                Log.Logger.Information($"Confirmation not given for deleting {reminderToDelete.Name}");
                return;
            }

            bool result = PersistenceAdapter.Delete(reminderToDelete.Name);
            if (result == true)
            {
                HandleRemovingFromListBox(reminderToDelete);
            }
            else
            {
                ErrorDialogUtility.ErrorDialog("Can't delete snoozed reminders. Snoozed reminders will be started now.");
            }
        }

        /// <summary>
        /// Removes row and selects other appropiate row
        /// </summary>
        protected virtual void HandleRemovingFromListBox(ReminderEntity reminderToDelete)
        {

            int oldSlectedIndex = remindersListBox.SelectedIndex;
            remindersListBox.Items.Remove(reminderToDelete);
            int newNumberOfItem = remindersListBox.Items.Count;

            if (oldSlectedIndex < newNumberOfItem)
            {
                remindersListBox.SelectedIndex = oldSlectedIndex;
                Log.Logger.Information($"Non-last row removed. Moving selection to same index. Index is still {oldSlectedIndex}.");
            }
            else if (newNumberOfItem > 0)
            {
                remindersListBox.SelectedIndex = newNumberOfItem - 1;
                Log.Logger.Information($"Last row was removed. Moving selection to index which is now the last, i.e. {newNumberOfItem - 1}.");
            }
            else
            {
                Log.Logger.Information($"List is empty, not updating selection.");
            }

        }

        protected virtual void ReturnFocusToListBox()
        {
            Log.Logger.Information($"Forsing focus back to ListBox. Control that have stolen the focus from listbox: {ActiveControl.Name}");
            remindersListBox.Focus();
        }
        #endregion


    }
}
