namespace AudioReminderUI
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.autostartEnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.serviceEnabledcheckBox = new System.Windows.Forms.CheckBox();
            this.snoozeIntervalNumbericBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.snoozeEnabledcheckBox = new System.Windows.Forms.CheckBox();
            this.beeperEnabledcheckBox = new System.Windows.Forms.CheckBox();
            this.serviceStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.beeperGroupBox = new System.Windows.Forms.GroupBox();
            this.currentBeeperSoundNameLabel = new System.Windows.Forms.Label();
            this.selectBeeperSoundButton = new System.Windows.Forms.Button();
            this.currentBeeperSoundNameBox = new System.Windows.Forms.TextBox();
            this.beepIntervalNumbericBox = new System.Windows.Forms.NumericUpDown();
            this.testBeeper = new System.Windows.Forms.Button();
            this.snoozeGroupBox = new System.Windows.Forms.GroupBox();
            this.testRinging = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.selectRingingSoundButton = new System.Windows.Forms.Button();
            this.dataMigrationGroupBox = new System.Windows.Forms.GroupBox();
            this.ringingGroupBox = new System.Windows.Forms.GroupBox();
            this.currentRingingSoundNameLabel = new System.Windows.Forms.Label();
            this.currentRingingSoundNameBox = new System.Windows.Forms.TextBox();
            this.languageGroupBox = new System.Windows.Forms.GroupBox();
            this.languageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.snoozeIntervalNumbericBox)).BeginInit();
            this.serviceStatusGroupBox.SuspendLayout();
            this.beeperGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beepIntervalNumbericBox)).BeginInit();
            this.snoozeGroupBox.SuspendLayout();
            this.dataMigrationGroupBox.SuspendLayout();
            this.ringingGroupBox.SuspendLayout();
            this.languageGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Beep interval [minutes]";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(396, 392);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 28);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(508, 392);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // autostartEnabledCheckbox
            // 
            this.autostartEnabledCheckbox.AutoSize = true;
            this.autostartEnabledCheckbox.Checked = true;
            this.autostartEnabledCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autostartEnabledCheckbox.Enabled = false;
            this.autostartEnabledCheckbox.Location = new System.Drawing.Point(5, 20);
            this.autostartEnabledCheckbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.autostartEnabledCheckbox.Name = "autostartEnabledCheckbox";
            this.autostartEnabledCheckbox.Size = new System.Drawing.Size(136, 21);
            this.autostartEnabledCheckbox.TabIndex = 0;
            this.autostartEnabledCheckbox.Text = "Autostart service";
            this.autostartEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // serviceEnabledcheckBox
            // 
            this.serviceEnabledcheckBox.AutoSize = true;
            this.serviceEnabledcheckBox.Checked = true;
            this.serviceEnabledcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.serviceEnabledcheckBox.Location = new System.Drawing.Point(5, 44);
            this.serviceEnabledcheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serviceEnabledcheckBox.Name = "serviceEnabledcheckBox";
            this.serviceEnabledcheckBox.Size = new System.Drawing.Size(132, 21);
            this.serviceEnabledcheckBox.TabIndex = 1;
            this.serviceEnabledcheckBox.Text = "Service enabled";
            this.serviceEnabledcheckBox.UseVisualStyleBackColor = true;
            // 
            // snoozeIntervalNumbericBox
            // 
            this.snoozeIntervalNumbericBox.AccessibleName = "Snooze interval minutes";
            this.snoozeIntervalNumbericBox.Location = new System.Drawing.Point(5, 54);
            this.snoozeIntervalNumbericBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.snoozeIntervalNumbericBox.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.snoozeIntervalNumbericBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.snoozeIntervalNumbericBox.Name = "snoozeIntervalNumbericBox";
            this.snoozeIntervalNumbericBox.Size = new System.Drawing.Size(107, 22);
            this.snoozeIntervalNumbericBox.TabIndex = 1;
            this.snoozeIntervalNumbericBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Snooze interval [minutes]";
            // 
            // snoozeEnabledcheckBox
            // 
            this.snoozeEnabledcheckBox.AutoSize = true;
            this.snoozeEnabledcheckBox.Checked = true;
            this.snoozeEnabledcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.snoozeEnabledcheckBox.Location = new System.Drawing.Point(9, 15);
            this.snoozeEnabledcheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.snoozeEnabledcheckBox.Name = "snoozeEnabledcheckBox";
            this.snoozeEnabledcheckBox.Size = new System.Drawing.Size(133, 21);
            this.snoozeEnabledcheckBox.TabIndex = 0;
            this.snoozeEnabledcheckBox.Text = "Snooze enabled";
            this.snoozeEnabledcheckBox.UseVisualStyleBackColor = true;
            // 
            // beeperEnabledcheckBox
            // 
            this.beeperEnabledcheckBox.AutoSize = true;
            this.beeperEnabledcheckBox.Location = new System.Drawing.Point(5, 20);
            this.beeperEnabledcheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.beeperEnabledcheckBox.Name = "beeperEnabledcheckBox";
            this.beeperEnabledcheckBox.Size = new System.Drawing.Size(131, 21);
            this.beeperEnabledcheckBox.TabIndex = 0;
            this.beeperEnabledcheckBox.Text = "Beeper enabled";
            this.beeperEnabledcheckBox.UseVisualStyleBackColor = true;
            // 
            // serviceStatusGroupBox
            // 
            this.serviceStatusGroupBox.Controls.Add(this.autostartEnabledCheckbox);
            this.serviceStatusGroupBox.Controls.Add(this.serviceEnabledcheckBox);
            this.serviceStatusGroupBox.Location = new System.Drawing.Point(21, 12);
            this.serviceStatusGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serviceStatusGroupBox.Name = "serviceStatusGroupBox";
            this.serviceStatusGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serviceStatusGroupBox.Size = new System.Drawing.Size(295, 100);
            this.serviceStatusGroupBox.TabIndex = 0;
            this.serviceStatusGroupBox.TabStop = false;
            this.serviceStatusGroupBox.Text = "Service status";
            // 
            // beeperGroupBox
            // 
            this.beeperGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.beeperGroupBox.Controls.Add(this.currentBeeperSoundNameLabel);
            this.beeperGroupBox.Controls.Add(this.selectBeeperSoundButton);
            this.beeperGroupBox.Controls.Add(this.currentBeeperSoundNameBox);
            this.beeperGroupBox.Controls.Add(this.beepIntervalNumbericBox);
            this.beeperGroupBox.Controls.Add(this.beeperEnabledcheckBox);
            this.beeperGroupBox.Controls.Add(this.testBeeper);
            this.beeperGroupBox.Controls.Add(this.label1);
            this.beeperGroupBox.Location = new System.Drawing.Point(322, 11);
            this.beeperGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.beeperGroupBox.Name = "beeperGroupBox";
            this.beeperGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.beeperGroupBox.Size = new System.Drawing.Size(286, 204);
            this.beeperGroupBox.TabIndex = 3;
            this.beeperGroupBox.TabStop = false;
            this.beeperGroupBox.Text = "Beeper";
            // 
            // currentBeeperSoundNameLabel
            // 
            this.currentBeeperSoundNameLabel.AutoSize = true;
            this.currentBeeperSoundNameLabel.Location = new System.Drawing.Point(9, 117);
            this.currentBeeperSoundNameLabel.Name = "currentBeeperSoundNameLabel";
            this.currentBeeperSoundNameLabel.Size = new System.Drawing.Size(147, 17);
            this.currentBeeperSoundNameLabel.TabIndex = 5;
            this.currentBeeperSoundNameLabel.Text = "Current beeper sound";
            // 
            // selectBeeperSoundButton
            // 
            this.selectBeeperSoundButton.AutoSize = true;
            this.selectBeeperSoundButton.Enabled = false;
            this.selectBeeperSoundButton.Location = new System.Drawing.Point(6, 84);
            this.selectBeeperSoundButton.Name = "selectBeeperSoundButton";
            this.selectBeeperSoundButton.Size = new System.Drawing.Size(161, 27);
            this.selectBeeperSoundButton.TabIndex = 2;
            this.selectBeeperSoundButton.Text = "Select beeper sound...\r\n";
            this.selectBeeperSoundButton.UseVisualStyleBackColor = true;
            // 
            // currentBeeperSoundNameBox
            // 
            this.currentBeeperSoundNameBox.AccessibleName = "Current beeper sound";
            this.currentBeeperSoundNameBox.Location = new System.Drawing.Point(9, 137);
            this.currentBeeperSoundNameBox.Name = "currentBeeperSoundNameBox";
            this.currentBeeperSoundNameBox.ReadOnly = true;
            this.currentBeeperSoundNameBox.Size = new System.Drawing.Size(271, 22);
            this.currentBeeperSoundNameBox.TabIndex = 3;
            this.currentBeeperSoundNameBox.Text = "19.5-Magia2.wav";
            // 
            // beepIntervalNumbericBox
            // 
            this.beepIntervalNumbericBox.AccessibleName = "Beep interval minutes";
            this.beepIntervalNumbericBox.Location = new System.Drawing.Point(9, 57);
            this.beepIntervalNumbericBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.beepIntervalNumbericBox.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.beepIntervalNumbericBox.Name = "beepIntervalNumbericBox";
            this.beepIntervalNumbericBox.Size = new System.Drawing.Size(107, 22);
            this.beepIntervalNumbericBox.TabIndex = 1;
            this.beepIntervalNumbericBox.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // testBeeper
            // 
            this.testBeeper.Location = new System.Drawing.Point(8, 166);
            this.testBeeper.Margin = new System.Windows.Forms.Padding(4);
            this.testBeeper.Name = "testBeeper";
            this.testBeeper.Size = new System.Drawing.Size(150, 28);
            this.testBeeper.TabIndex = 4;
            this.testBeeper.Text = "Test beeper";
            this.testBeeper.UseVisualStyleBackColor = true;
            this.testBeeper.Click += new System.EventHandler(this.testBeeper_Click);
            // 
            // snoozeGroupBox
            // 
            this.snoozeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.snoozeGroupBox.Controls.Add(this.snoozeIntervalNumbericBox);
            this.snoozeGroupBox.Controls.Add(this.label3);
            this.snoozeGroupBox.Controls.Add(this.snoozeEnabledcheckBox);
            this.snoozeGroupBox.Location = new System.Drawing.Point(21, 263);
            this.snoozeGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.snoozeGroupBox.Name = "snoozeGroupBox";
            this.snoozeGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.snoozeGroupBox.Size = new System.Drawing.Size(295, 116);
            this.snoozeGroupBox.TabIndex = 2;
            this.snoozeGroupBox.TabStop = false;
            this.snoozeGroupBox.Text = "Snooze";
            // 
            // testRinging
            // 
            this.testRinging.Location = new System.Drawing.Point(5, 100);
            this.testRinging.Margin = new System.Windows.Forms.Padding(4);
            this.testRinging.Name = "testRinging";
            this.testRinging.Size = new System.Drawing.Size(150, 28);
            this.testRinging.TabIndex = 2;
            this.testRinging.Text = "Test ringing";
            this.testRinging.UseVisualStyleBackColor = true;
            this.testRinging.Click += new System.EventHandler(this.testRinging_Click);
            // 
            // exportButton
            // 
            this.exportButton.AutoSize = true;
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(6, 21);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(137, 27);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Export reminders...\r\n";
            this.exportButton.UseVisualStyleBackColor = true;
            // 
            // importButton
            // 
            this.importButton.AutoSize = true;
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(144, 21);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(136, 27);
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Import reminders...\r\n";
            this.importButton.UseVisualStyleBackColor = true;
            // 
            // selectRingingSoundButton
            // 
            this.selectRingingSoundButton.AutoSize = true;
            this.selectRingingSoundButton.Enabled = false;
            this.selectRingingSoundButton.Location = new System.Drawing.Point(6, 21);
            this.selectRingingSoundButton.Name = "selectRingingSoundButton";
            this.selectRingingSoundButton.Size = new System.Drawing.Size(159, 27);
            this.selectRingingSoundButton.TabIndex = 0;
            this.selectRingingSoundButton.Text = "Select ringing sound...";
            this.selectRingingSoundButton.UseVisualStyleBackColor = true;
            // 
            // dataMigrationGroupBox
            // 
            this.dataMigrationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataMigrationGroupBox.Controls.Add(this.exportButton);
            this.dataMigrationGroupBox.Controls.Add(this.importButton);
            this.dataMigrationGroupBox.Location = new System.Drawing.Point(322, 220);
            this.dataMigrationGroupBox.Name = "dataMigrationGroupBox";
            this.dataMigrationGroupBox.Size = new System.Drawing.Size(286, 60);
            this.dataMigrationGroupBox.TabIndex = 4;
            this.dataMigrationGroupBox.TabStop = false;
            this.dataMigrationGroupBox.Text = "Data migration";
            // 
            // ringingGroupBox
            // 
            this.ringingGroupBox.Controls.Add(this.currentRingingSoundNameLabel);
            this.ringingGroupBox.Controls.Add(this.currentRingingSoundNameBox);
            this.ringingGroupBox.Controls.Add(this.selectRingingSoundButton);
            this.ringingGroupBox.Controls.Add(this.testRinging);
            this.ringingGroupBox.Location = new System.Drawing.Point(21, 122);
            this.ringingGroupBox.Name = "ringingGroupBox";
            this.ringingGroupBox.Size = new System.Drawing.Size(295, 136);
            this.ringingGroupBox.TabIndex = 1;
            this.ringingGroupBox.TabStop = false;
            this.ringingGroupBox.Text = "Ringing";
            // 
            // currentRingingSoundNameLabel
            // 
            this.currentRingingSoundNameLabel.AutoSize = true;
            this.currentRingingSoundNameLabel.Location = new System.Drawing.Point(6, 51);
            this.currentRingingSoundNameLabel.Name = "currentRingingSoundNameLabel";
            this.currentRingingSoundNameLabel.Size = new System.Drawing.Size(145, 17);
            this.currentRingingSoundNameLabel.TabIndex = 3;
            this.currentRingingSoundNameLabel.Text = "Current ringing sound";
            // 
            // currentRingingSoundNameBox
            // 
            this.currentRingingSoundNameBox.AccessibleName = "Current ringing sound";
            this.currentRingingSoundNameBox.Location = new System.Drawing.Point(6, 71);
            this.currentRingingSoundNameBox.Name = "currentRingingSoundNameBox";
            this.currentRingingSoundNameBox.ReadOnly = true;
            this.currentRingingSoundNameBox.Size = new System.Drawing.Size(283, 22);
            this.currentRingingSoundNameBox.TabIndex = 1;
            this.currentRingingSoundNameBox.Text = "LEBER_Zoé_2014_2015_Xylo.mp3\r\n";
            // 
            // languageGroupBox
            // 
            this.languageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.languageGroupBox.Controls.Add(this.languageButton);
            this.languageGroupBox.Location = new System.Drawing.Point(322, 286);
            this.languageGroupBox.Name = "languageGroupBox";
            this.languageGroupBox.Size = new System.Drawing.Size(286, 93);
            this.languageGroupBox.TabIndex = 5;
            this.languageGroupBox.TabStop = false;
            this.languageGroupBox.Text = "Language";
            // 
            // languageButton
            // 
            this.languageButton.AutoSize = true;
            this.languageButton.Location = new System.Drawing.Point(6, 21);
            this.languageButton.Name = "languageButton";
            this.languageButton.Size = new System.Drawing.Size(141, 27);
            this.languageButton.TabIndex = 0;
            this.languageButton.Text = "Choose language...";
            this.languageButton.UseVisualStyleBackColor = true;
            this.languageButton.Click += new System.EventHandler(this.languageButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.languageGroupBox);
            this.Controls.Add(this.ringingGroupBox);
            this.Controls.Add(this.dataMigrationGroupBox);
            this.Controls.Add(this.beeperGroupBox);
            this.Controls.Add(this.snoozeGroupBox);
            this.Controls.Add(this.serviceStatusGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsForm";
            this.Text = "Audio Reminder - Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.snoozeIntervalNumbericBox)).EndInit();
            this.serviceStatusGroupBox.ResumeLayout(false);
            this.serviceStatusGroupBox.PerformLayout();
            this.beeperGroupBox.ResumeLayout(false);
            this.beeperGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beepIntervalNumbericBox)).EndInit();
            this.snoozeGroupBox.ResumeLayout(false);
            this.snoozeGroupBox.PerformLayout();
            this.dataMigrationGroupBox.ResumeLayout(false);
            this.dataMigrationGroupBox.PerformLayout();
            this.ringingGroupBox.ResumeLayout(false);
            this.ringingGroupBox.PerformLayout();
            this.languageGroupBox.ResumeLayout(false);
            this.languageGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox autostartEnabledCheckbox;
        private System.Windows.Forms.CheckBox serviceEnabledcheckBox;
        private System.Windows.Forms.NumericUpDown snoozeIntervalNumbericBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox snoozeEnabledcheckBox;
        private System.Windows.Forms.CheckBox beeperEnabledcheckBox;
        private System.Windows.Forms.GroupBox serviceStatusGroupBox;
        private System.Windows.Forms.GroupBox beeperGroupBox;
        private System.Windows.Forms.GroupBox snoozeGroupBox;
        private System.Windows.Forms.NumericUpDown beepIntervalNumbericBox;
        private System.Windows.Forms.Button testRinging;
        private System.Windows.Forms.Button testBeeper;
        private System.Windows.Forms.Button selectBeeperSoundButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button selectRingingSoundButton;
        private System.Windows.Forms.GroupBox dataMigrationGroupBox;
        private System.Windows.Forms.GroupBox ringingGroupBox;
        private System.Windows.Forms.Label currentBeeperSoundNameLabel;
        private System.Windows.Forms.TextBox currentBeeperSoundNameBox;
        private System.Windows.Forms.Label currentRingingSoundNameLabel;
        private System.Windows.Forms.TextBox currentRingingSoundNameBox;
        private System.Windows.Forms.GroupBox languageGroupBox;
        private System.Windows.Forms.Button languageButton;
    }
}

