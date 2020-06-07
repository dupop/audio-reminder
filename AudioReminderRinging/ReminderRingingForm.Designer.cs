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
            this.SuspendLayout();
            // 
            // snoozeButton
            // 
            this.snoozeButton.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.snoozeButton.Location = new System.Drawing.Point(53, 29);
            this.snoozeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.snoozeButton.Name = "snoozeButton";
            this.snoozeButton.Size = new System.Drawing.Size(100, 28);
            this.snoozeButton.TabIndex = 0;
            this.snoozeButton.Text = "Snooze";
            this.snoozeButton.UseVisualStyleBackColor = true;
            this.snoozeButton.Click += new System.EventHandler(this.snoozeButton_Click);
            // 
            // dismissButton
            // 
            this.dismissButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.dismissButton.Location = new System.Drawing.Point(159, 29);
            this.dismissButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dismissButton.Name = "dismissButton";
            this.dismissButton.Size = new System.Drawing.Size(100, 28);
            this.dismissButton.TabIndex = 2;
            this.dismissButton.Text = "Dismiss";
            this.dismissButton.UseVisualStyleBackColor = true;
            this.dismissButton.Click += new System.EventHandler(this.dismissButton_Click);
            // 
            // reminderRingingForm
            // 
            this.AcceptButton = this.dismissButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.snoozeButton;
            this.ClientSize = new System.Drawing.Size(300, 85);
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

        }

        #endregion

        private System.Windows.Forms.Button snoozeButton;
        private System.Windows.Forms.Button dismissButton;
    }
}