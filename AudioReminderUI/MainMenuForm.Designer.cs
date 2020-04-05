namespace AudioReminderUI
{
    partial class MainMenuForm
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
            this.createReminderButton = new System.Windows.Forms.Button();
            this.remindersButton = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createReminderButton
            // 
            this.createReminderButton.Location = new System.Drawing.Point(54, 189);
            this.createReminderButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createReminderButton.Name = "createReminderButton";
            this.createReminderButton.Size = new System.Drawing.Size(112, 60);
            this.createReminderButton.TabIndex = 0;
            this.createReminderButton.Text = "Create reminder";
            this.createReminderButton.UseVisualStyleBackColor = true;
            this.createReminderButton.Click += new System.EventHandler(this.createReminderButton_Click);
            // 
            // remindersButton
            // 
            this.remindersButton.Location = new System.Drawing.Point(196, 189);
            this.remindersButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.remindersButton.Name = "remindersButton";
            this.remindersButton.Size = new System.Drawing.Size(112, 58);
            this.remindersButton.TabIndex = 1;
            this.remindersButton.Text = "Reminders";
            this.remindersButton.UseVisualStyleBackColor = true;
            this.remindersButton.Click += new System.EventHandler(this.remindersButton_Click);
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(338, 189);
            this.Settings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(112, 62);
            this.Settings.TabIndex = 2;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 471);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.remindersButton);
            this.Controls.Add(this.createReminderButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainMenuForm";
            this.Text = "Audio Reminder Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createReminderButton;
        private System.Windows.Forms.Button remindersButton;
        private System.Windows.Forms.Button Settings;
    }
}