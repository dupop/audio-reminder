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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.beepIntervalNumbericBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.testRinging = new System.Windows.Forms.Button();
            this.testBeeper = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.snoozeIntervalNumbericBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beepIntervalNumbericBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Beep interval [minutes]";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(173, 310);
            this.okButton.Margin = new System.Windows.Forms.Padding(2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(257, 310);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // autostartEnabledCheckbox
            // 
            this.autostartEnabledCheckbox.AutoSize = true;
            this.autostartEnabledCheckbox.Checked = true;
            this.autostartEnabledCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autostartEnabledCheckbox.Enabled = false;
            this.autostartEnabledCheckbox.Location = new System.Drawing.Point(4, 16);
            this.autostartEnabledCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.autostartEnabledCheckbox.Name = "autostartEnabledCheckbox";
            this.autostartEnabledCheckbox.Size = new System.Drawing.Size(105, 17);
            this.autostartEnabledCheckbox.TabIndex = 0;
            this.autostartEnabledCheckbox.Text = "Autostart service";
            this.autostartEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // serviceEnabledcheckBox
            // 
            this.serviceEnabledcheckBox.AutoSize = true;
            this.serviceEnabledcheckBox.Checked = true;
            this.serviceEnabledcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.serviceEnabledcheckBox.Location = new System.Drawing.Point(4, 36);
            this.serviceEnabledcheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.serviceEnabledcheckBox.Name = "serviceEnabledcheckBox";
            this.serviceEnabledcheckBox.Size = new System.Drawing.Size(103, 17);
            this.serviceEnabledcheckBox.TabIndex = 1;
            this.serviceEnabledcheckBox.Text = "Service enabled";
            this.serviceEnabledcheckBox.UseVisualStyleBackColor = true;
            // 
            // snoozeIntervalNumbericBox
            // 
            this.snoozeIntervalNumbericBox.AccessibleName = "Snooze interval minutes";
            this.snoozeIntervalNumbericBox.Location = new System.Drawing.Point(11, 44);
            this.snoozeIntervalNumbericBox.Margin = new System.Windows.Forms.Padding(2);
            this.snoozeIntervalNumbericBox.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.snoozeIntervalNumbericBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.snoozeIntervalNumbericBox.Name = "snoozeIntervalNumbericBox";
            this.snoozeIntervalNumbericBox.Size = new System.Drawing.Size(80, 20);
            this.snoozeIntervalNumbericBox.TabIndex = 5;
            this.snoozeIntervalNumbericBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Snooze interval [minutes]";
            // 
            // snoozeEnabledcheckBox
            // 
            this.snoozeEnabledcheckBox.AutoSize = true;
            this.snoozeEnabledcheckBox.Checked = true;
            this.snoozeEnabledcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.snoozeEnabledcheckBox.Location = new System.Drawing.Point(7, 12);
            this.snoozeEnabledcheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.snoozeEnabledcheckBox.Name = "snoozeEnabledcheckBox";
            this.snoozeEnabledcheckBox.Size = new System.Drawing.Size(103, 17);
            this.snoozeEnabledcheckBox.TabIndex = 4;
            this.snoozeEnabledcheckBox.Text = "Snooze enabled";
            this.snoozeEnabledcheckBox.UseVisualStyleBackColor = true;
            // 
            // beeperEnabledcheckBox
            // 
            this.beeperEnabledcheckBox.AutoSize = true;
            this.beeperEnabledcheckBox.Location = new System.Drawing.Point(4, 16);
            this.beeperEnabledcheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.beeperEnabledcheckBox.Name = "beeperEnabledcheckBox";
            this.beeperEnabledcheckBox.Size = new System.Drawing.Size(101, 17);
            this.beeperEnabledcheckBox.TabIndex = 2;
            this.beeperEnabledcheckBox.Text = "Beeper enabled";
            this.beeperEnabledcheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autostartEnabledCheckbox);
            this.groupBox1.Controls.Add(this.serviceEnabledcheckBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(185, 65);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.beepIntervalNumbericBox);
            this.groupBox2.Controls.Add(this.beeperEnabledcheckBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(16, 83);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(185, 83);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Beeper";
            // 
            // beepIntervalNumbericBox
            // 
            this.beepIntervalNumbericBox.AccessibleName = "Beep interval minutes";
            this.beepIntervalNumbericBox.Location = new System.Drawing.Point(7, 46);
            this.beepIntervalNumbericBox.Margin = new System.Windows.Forms.Padding(2);
            this.beepIntervalNumbericBox.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.beepIntervalNumbericBox.Name = "beepIntervalNumbericBox";
            this.beepIntervalNumbericBox.Size = new System.Drawing.Size(80, 20);
            this.beepIntervalNumbericBox.TabIndex = 3;
            this.beepIntervalNumbericBox.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.snoozeIntervalNumbericBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.snoozeEnabledcheckBox);
            this.groupBox3.Location = new System.Drawing.Point(16, 170);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(185, 74);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Snooze";
            // 
            // testRinging
            // 
            this.testRinging.Location = new System.Drawing.Point(16, 249);
            this.testRinging.Name = "testRinging";
            this.testRinging.Size = new System.Drawing.Size(75, 23);
            this.testRinging.TabIndex = 6;
            this.testRinging.Text = "Test ringing";
            this.testRinging.UseVisualStyleBackColor = true;
            this.testRinging.Click += new System.EventHandler(this.testRinging_Click);
            // 
            // testBeeper
            // 
            this.testBeeper.Location = new System.Drawing.Point(97, 249);
            this.testBeeper.Name = "testBeeper";
            this.testBeeper.Size = new System.Drawing.Size(75, 23);
            this.testBeeper.TabIndex = 7;
            this.testBeeper.Text = "Test beeper";
            this.testBeeper.UseVisualStyleBackColor = true;
            this.testBeeper.Click += new System.EventHandler(this.testBeeper_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(343, 344);
            this.Controls.Add(this.testBeeper);
            this.Controls.Add(this.testRinging);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "SettingsForm";
            this.Text = "Audio Reminder - Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.snoozeIntervalNumbericBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beepIntervalNumbericBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown beepIntervalNumbericBox;
        private System.Windows.Forms.Button testRinging;
        private System.Windows.Forms.Button testBeeper;
    }
}

