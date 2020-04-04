namespace AudioUserInterface
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
            this.deleteButton.Location = new System.Drawing.Point(51, 8);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(68, 45);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "Delete reminder";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(176, 8);
            this.editButton.Margin = new System.Windows.Forms.Padding(2);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(68, 45);
            this.editButton.TabIndex = 1;
            this.editButton.TabStop = false;
            this.editButton.Text = "Edit reminder";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // cloneButton
            // 
            this.cloneButton.Location = new System.Drawing.Point(285, 8);
            this.cloneButton.Margin = new System.Windows.Forms.Padding(2);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(68, 45);
            this.cloneButton.TabIndex = 2;
            this.cloneButton.TabStop = false;
            this.cloneButton.Text = "Clone reminder";
            this.cloneButton.UseVisualStyleBackColor = true;
            this.cloneButton.Click += new System.EventHandler(this.cloneButton_Click);
            // 
            // remindersListBox
            // 
            this.remindersListBox.FormattingEnabled = true;
            this.remindersListBox.Location = new System.Drawing.Point(51, 115);
            this.remindersListBox.Margin = new System.Windows.Forms.Padding(2);
            this.remindersListBox.Name = "remindersListBox";
            this.remindersListBox.Size = new System.Drawing.Size(738, 212);
            this.remindersListBox.TabIndex = 3;
            this.remindersListBox.Leave += new System.EventHandler(this.remindersListBox_Leave);
            // 
            // backButton
            // 
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.backButton.Location = new System.Drawing.Point(320, 333);
            this.backButton.Margin = new System.Windows.Forms.Padding(2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(60, 63);
            this.backButton.TabIndex = 4;
            this.backButton.TabStop = false;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(446, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Press Delete key to remove selected reminder, Enter key to modify it, or space ke" +
    "y to clone it.\r\nPress Exit key to go back.";
            // 
            // ReminderListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.backButton;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.remindersListBox);
            this.Controls.Add(this.cloneButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.deleteButton);
            this.KeyPreview = true;
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