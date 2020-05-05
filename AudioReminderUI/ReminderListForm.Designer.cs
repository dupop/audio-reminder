namespace AudioReminderUI
{
    partial class ReminderListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.deleteButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.cloneButton = new System.Windows.Forms.Button();
            this.remindersListBox = new System.Windows.Forms.ListBox();
            this.backButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(15, 14);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 28);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(120, 14);
            this.editButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(100, 28);
            this.editButton.TabIndex = 1;
            this.editButton.TabStop = false;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // cloneButton
            // 
            this.cloneButton.Location = new System.Drawing.Point(225, 14);
            this.cloneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(100, 28);
            this.cloneButton.TabIndex = 2;
            this.cloneButton.TabStop = false;
            this.cloneButton.Text = "Clone";
            this.cloneButton.UseVisualStyleBackColor = true;
            this.cloneButton.Click += new System.EventHandler(this.cloneButton_Click);
            // 
            // remindersListBox
            // 
            this.remindersListBox.AccessibleDescription = "Press Delete key to remove selected reminder, Enter key to modify it, or space ke" +
    "y to clone it. Press Exit key to go back. Press Exit key to go back.";
            this.remindersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remindersListBox.FormattingEnabled = true;
            this.remindersListBox.ItemHeight = 16;
            this.remindersListBox.Location = new System.Drawing.Point(15, 79);
            this.remindersListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.remindersListBox.Name = "remindersListBox";
            this.remindersListBox.Size = new System.Drawing.Size(1036, 308);
            this.remindersListBox.TabIndex = 3;
            this.remindersListBox.Leave += new System.EventHandler(this.remindersListBox_Leave);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.backButton.Location = new System.Drawing.Point(952, 399);
            this.backButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 28);
            this.backButton.TabIndex = 4;
            this.backButton.TabStop = false;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(597, 34);
            this.label1.TabIndex = 5;
            this.label1.Text = "Press Delete key to remove selected reminder, Enter key to modify it, or space ke" +
    "y to clone it.\r\nPress Exit key to go back.";
            // 
            // ReminderListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.backButton;
            this.ClientSize = new System.Drawing.Size(1067, 441);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.remindersListBox);
            this.Controls.Add(this.cloneButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.deleteButton);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ReminderListForm";
            this.Text = "ReminderListForm";
            this.Load += new System.EventHandler(this.ReminderListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cloneButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ListBox remindersListBox;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label label1;
    }
}