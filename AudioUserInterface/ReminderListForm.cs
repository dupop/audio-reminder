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
    public partial class ReminderListForm : Form
    {
        PersistenceAdapter PersistenceAdapter;
        //TODO: BUG - space may trigger focused button, change sortcut and description

        protected ReminderEntity SelectedReminder => remindersListBox.SelectedItem as ReminderEntity;

        public ReminderListForm(PersistenceAdapter persistenceAdapter)
        {
            InitializeComponent();
            this.PersistenceAdapter = persistenceAdapter;
        }

        private void ReminderListForm_Load(object sender, EventArgs e)
        {
            ReminderEntity[] loadedReminders = PersistenceAdapter.Load();
            remindersListBox.Items.AddRange(loadedReminders);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteAction();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(remindersListBox.Focused)
            {
                if (keyData == Keys.Delete)
                {
                    DeleteAction();
                }
                else if (keyData == Keys.Enter)
                {
                    EditAction();
                }
                else if (keyData == Keys.Space)
                {
                    CloneAction();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            EditAction();
        }

        protected virtual void EditAction()
        {
            ReminderEntity reminderToUpdate = SelectedReminder;
            if (reminderToUpdate == null)
            {
                ErrorDialogUtility.ErrorDialog("No reminder selected to edit");
                return;
            }

            //TODO: add Edit dialog
            CreateAndUpdateReminderForm createReminderForm = new CreateAndUpdateReminderForm(PersistenceAdapter, reminderToUpdate);
            createReminderForm.ShowDialog();

            if (createReminderForm.DialogResult != DialogResult.OK)
            {
                Log.Logger.Information($"UpdateReminderForm closed with result NotOk.");
                return;
            }

            var updatedReminder = createReminderForm.CreateOrUpdatedReminder;

            PersistenceAdapter.Update(reminderToUpdate.Name, updatedReminder);

            //using the element in the listbox
            remindersListBox.Items[remindersListBox.SelectedIndex] = updatedReminder;
        }

        private void cloneButton_Click(object sender, EventArgs e)
        {
            CloneAction();
        }

        private void CloneAction()
        {
            if (SelectedReminder == null)
            {
                ErrorDialogUtility.ErrorDialog("No reminder selected to clone");
                return;
            }


            //TODO: add clone dialog
            ErrorDialogUtility.ErrorDialog("Not yet implemented");
            ReminderEntity clone = SelectedReminder;

            PersistenceAdapter.Save(clone);
            remindersListBox.Items.Add(clone);
        }



        protected virtual void DeleteAction()
        {
            ReminderEntity reminderToDelete = SelectedReminder;
            if (reminderToDelete == null)
            {
                ErrorDialogUtility.ErrorDialog("No reminder selected to delete");
                return;
            }

            //TODO: enable this via Del key
            DialogResult confirmationResult = MessageBox.Show($"Confirm deleting: {reminderToDelete.Name}.", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (confirmationResult != DialogResult.OK)
            {
                Log.Logger.Information($"Confirmation not given for deleting {reminderToDelete.Name}");
                return;
            }

            PersistenceAdapter.Delete(reminderToDelete.Name);
            HandleRemovingFromListBox(reminderToDelete);
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

    }
}
