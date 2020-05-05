namespace AudioReminderUI
{
    partial class CreateAndUpdateReminderForm
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
            this.reminderNameStringBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.repeatYearlyCheckBox = new System.Windows.Forms.CheckBox();
            this.scheduledDatePicker = new System.Windows.Forms.DateTimePicker();
            this.repeatMonthlyCheckBox = new System.Windows.Forms.CheckBox();
            this.repeatWeeklyCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.hoursNumericBox = new System.Windows.Forms.NumericUpDown();
            this.minuteNumbericBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumericBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumbericBox)).BeginInit();
            this.SuspendLayout();
            // 
            // reminderNameStringBox
            // 
            this.reminderNameStringBox.AccessibleName = "Reminder name";
            this.reminderNameStringBox.Location = new System.Drawing.Point(13, 39);
            this.reminderNameStringBox.Margin = new System.Windows.Forms.Padding(4);
            this.reminderNameStringBox.Name = "reminderNameStringBox";
            this.reminderNameStringBox.Size = new System.Drawing.Size(421, 22);
            this.reminderNameStringBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reminder name";
            // 
            // repeatYearlyCheckBox
            // 
            this.repeatYearlyCheckBox.AutoSize = true;
            this.repeatYearlyCheckBox.Location = new System.Drawing.Point(235, 224);
            this.repeatYearlyCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.repeatYearlyCheckBox.Name = "repeatYearlyCheckBox";
            this.repeatYearlyCheckBox.Size = new System.Drawing.Size(118, 21);
            this.repeatYearlyCheckBox.TabIndex = 6;
            this.repeatYearlyCheckBox.Text = "Repeat yearly";
            this.repeatYearlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // scheduledDatePicker
            // 
            this.scheduledDatePicker.AccessibleDescription = "Use up/down and left/right arrows to change date";
            this.scheduledDatePicker.AccessibleName = "Date of first occurance";
            this.scheduledDatePicker.Location = new System.Drawing.Point(13, 95);
            this.scheduledDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.scheduledDatePicker.Name = "scheduledDatePicker";
            this.scheduledDatePicker.Size = new System.Drawing.Size(265, 22);
            this.scheduledDatePicker.TabIndex = 1;
            // 
            // repeatMonthlyCheckBox
            // 
            this.repeatMonthlyCheckBox.AutoSize = true;
            this.repeatMonthlyCheckBox.Location = new System.Drawing.Point(235, 195);
            this.repeatMonthlyCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.repeatMonthlyCheckBox.Name = "repeatMonthlyCheckBox";
            this.repeatMonthlyCheckBox.Size = new System.Drawing.Size(129, 21);
            this.repeatMonthlyCheckBox.TabIndex = 5;
            this.repeatMonthlyCheckBox.Text = "Repeat monthly";
            this.repeatMonthlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // repeatWeeklyCheckedListBox
            // 
            this.repeatWeeklyCheckedListBox.AccessibleName = "Repeat weekly";
            this.repeatWeeklyCheckedListBox.FormattingEnabled = true;
            this.repeatWeeklyCheckedListBox.Items.AddRange(new object[] {
            "Mon",
            "Tue",
            "Wed",
            "Thr",
            "Fri",
            "Sat",
            "Sun"});
            this.repeatWeeklyCheckedListBox.Location = new System.Drawing.Point(16, 195);
            this.repeatWeeklyCheckedListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.repeatWeeklyCheckedListBox.Name = "repeatWeeklyCheckedListBox";
            this.repeatWeeklyCheckedListBox.Size = new System.Drawing.Size(107, 123);
            this.repeatWeeklyCheckedListBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Date of first occurance\r\n";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(249, 368);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 28);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(355, 368);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // hoursNumericBox
            // 
            this.hoursNumericBox.AccessibleDescription = "Use up/down arrows to change time ( hours )";
            this.hoursNumericBox.AccessibleName = "Hours";
            this.hoursNumericBox.Location = new System.Drawing.Point(297, 95);
            this.hoursNumericBox.Margin = new System.Windows.Forms.Padding(4);
            this.hoursNumericBox.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hoursNumericBox.Name = "hoursNumericBox";
            this.hoursNumericBox.Size = new System.Drawing.Size(59, 22);
            this.hoursNumericBox.TabIndex = 2;
            this.hoursNumericBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // minuteNumbericBox
            // 
            this.minuteNumbericBox.AccessibleDescription = "Use up/down arrows to change time ( minutes )";
            this.minuteNumbericBox.AccessibleName = "Minutes";
            this.minuteNumbericBox.Location = new System.Drawing.Point(364, 95);
            this.minuteNumbericBox.Margin = new System.Windows.Forms.Padding(4);
            this.minuteNumbericBox.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minuteNumbericBox.Name = "minuteNumbericBox";
            this.minuteNumbericBox.Size = new System.Drawing.Size(59, 22);
            this.minuteNumbericBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Repeat weekly";
            // 
            // CreateAndUpdateReminderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(469, 410);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.repeatWeeklyCheckedListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minuteNumbericBox);
            this.Controls.Add(this.hoursNumericBox);
            this.Controls.Add(this.repeatMonthlyCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.scheduledDatePicker);
            this.Controls.Add(this.repeatYearlyCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reminderNameStringBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CreateAndUpdateReminderForm";
            this.Text = "CreateReminderForm";
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumericBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumbericBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox reminderNameStringBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox repeatYearlyCheckBox;
        private System.Windows.Forms.DateTimePicker scheduledDatePicker;
        private System.Windows.Forms.CheckBox repeatMonthlyCheckBox;
        private System.Windows.Forms.CheckedListBox repeatWeeklyCheckedListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown hoursNumericBox;
        private System.Windows.Forms.NumericUpDown minuteNumbericBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}