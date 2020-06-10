namespace AudioReminderRinging
{
    partial class reminderRingingForm
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
            this.snoozeButton = new System.Windows.Forms.Button();
            this.dismissButton = new System.Windows.Forms.Button();
            this.reminderDescriptionTextbox = new System.Windows.Forms.TextBox();
            this.eventDescriptionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // snoozeButton
            // 
            this.snoozeButton.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.snoozeButton.Location = new System.Drawing.Point(304, 394);
            this.snoozeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.snoozeButton.Name = "snoozeButton";
            this.snoozeButton.Size = new System.Drawing.Size(150, 28);
            this.snoozeButton.TabIndex = 0;
            this.snoozeButton.Text = "Snooze";
            this.snoozeButton.UseVisualStyleBackColor = true;
            this.snoozeButton.Click += new System.EventHandler(this.snoozeButton_Click);
            // 
            // dismissButton
            // 
            this.dismissButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.dismissButton.Location = new System.Drawing.Point(460, 394);
            this.dismissButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dismissButton.Name = "dismissButton";
            this.dismissButton.Size = new System.Drawing.Size(150, 28);
            this.dismissButton.TabIndex = 2;
            this.dismissButton.Text = "Dismiss";
            this.dismissButton.UseVisualStyleBackColor = true;
            this.dismissButton.Click += new System.EventHandler(this.dismissButton_Click);
            // 
            // reminderDescriptionTextbox
            // 
            this.reminderDescriptionTextbox.AccessibleName = "Event description";
            this.reminderDescriptionTextbox.Enabled = false;
            this.reminderDescriptionTextbox.Location = new System.Drawing.Point(13, 71);
            this.reminderDescriptionTextbox.Margin = new System.Windows.Forms.Padding(4);
            this.reminderDescriptionTextbox.Multiline = true;
            this.reminderDescriptionTextbox.Name = "reminderDescriptionTextbox";
            this.reminderDescriptionTextbox.Size = new System.Drawing.Size(584, 69);
            this.reminderDescriptionTextbox.TabIndex = 21;
            this.reminderDescriptionTextbox.Text = "Adding reminder description will be implemented in next version.\r\n";
            // 
            // eventDescriptionLabel
            // 
            this.eventDescriptionLabel.AutoSize = true;
            this.eventDescriptionLabel.Location = new System.Drawing.Point(15, 50);
            this.eventDescriptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.eventDescriptionLabel.Name = "eventDescriptionLabel";
            this.eventDescriptionLabel.Size = new System.Drawing.Size(117, 17);
            this.eventDescriptionLabel.TabIndex = 22;
            this.eventDescriptionLabel.Text = "Event description";
            // 
            // reminderRingingForm
            // 
            this.AcceptButton = this.dismissButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.snoozeButton;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.reminderDescriptionTextbox);
            this.Controls.Add(this.eventDescriptionLabel);
            this.Controls.Add(this.dismissButton);
            this.Controls.Add(this.snoozeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "reminderRingingForm";
            this.Text = "Reminder ringing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReminderRingingForm_FormClosing);
            this.Load += new System.EventHandler(this.ReminderRingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button snoozeButton;
        private System.Windows.Forms.Button dismissButton;
        private System.Windows.Forms.TextBox reminderDescriptionTextbox;
        private System.Windows.Forms.Label eventDescriptionLabel;
    }
}