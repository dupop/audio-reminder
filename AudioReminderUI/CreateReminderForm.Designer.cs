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
            this.repeatWeeklyCheckBox = new System.Windows.Forms.CheckBox();
            this.repeatYearlyCheckBox = new System.Windows.Forms.CheckBox();
            this.scheduledDatePicker = new System.Windows.Forms.DateTimePicker();
            this.repeatMonthlyCheckBox = new System.Windows.Forms.CheckBox();
            this.repeatWeeklyCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.hoursNumericBox = new System.Windows.Forms.NumericUpDown();
            this.minuteNumbericBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumericBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumbericBox)).BeginInit();
            this.SuspendLayout();
            // 
            // reminderNameStringBox
            // 
            this.reminderNameStringBox.AccessibleName = "Reminder name";
            this.reminderNameStringBox.Location = new System.Drawing.Point(8, 31);
            this.reminderNameStringBox.Name = "reminderNameStringBox";
            this.reminderNameStringBox.Size = new System.Drawing.Size(317, 20);
            this.reminderNameStringBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reminder name";
            // 
            // repeatWeeklyCheckBox
            // 
            this.repeatWeeklyCheckBox.AutoSize = true;
            this.repeatWeeklyCheckBox.Location = new System.Drawing.Point(5, 17);
            this.repeatWeeklyCheckBox.Name = "repeatWeeklyCheckBox";
            this.repeatWeeklyCheckBox.Size = new System.Drawing.Size(97, 17);
            this.repeatWeeklyCheckBox.TabIndex = 2;
            this.repeatWeeklyCheckBox.Text = "Repeat weekly";
            this.repeatWeeklyCheckBox.UseVisualStyleBackColor = true;
            // 
            // repeatYearlyCheckBox
            // 
            this.repeatYearlyCheckBox.AutoSize = true;
            this.repeatYearlyCheckBox.Location = new System.Drawing.Point(176, 147);
            this.repeatYearlyCheckBox.Name = "repeatYearlyCheckBox";
            this.repeatYearlyCheckBox.Size = new System.Drawing.Size(91, 17);
            this.repeatYearlyCheckBox.TabIndex = 6;
            this.repeatYearlyCheckBox.Text = "Repeat yearly";
            this.repeatYearlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // scheduledTimePicker
            // 
            this.scheduledDatePicker.AccessibleName = "Date of first occurance";
            this.scheduledDatePicker.Location = new System.Drawing.Point(13, 77);
            this.scheduledDatePicker.Name = "scheduledTimePicker";
            this.scheduledDatePicker.Size = new System.Drawing.Size(200, 20);
            this.scheduledDatePicker.TabIndex = 1;
            // 
            // repeatMonthlyCheckBox
            // 
            this.repeatMonthlyCheckBox.AutoSize = true;
            this.repeatMonthlyCheckBox.Location = new System.Drawing.Point(176, 124);
            this.repeatMonthlyCheckBox.Name = "repeatMonthlyCheckBox";
            this.repeatMonthlyCheckBox.Size = new System.Drawing.Size(100, 17);
            this.repeatMonthlyCheckBox.TabIndex = 5;
            this.repeatMonthlyCheckBox.Text = "Repeat monthly";
            this.repeatMonthlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // repeatWeeklyCheckedListBox
            // 
            this.repeatWeeklyCheckedListBox.FormattingEnabled = true;
            this.repeatWeeklyCheckedListBox.Items.AddRange(new object[] {
            "Mon",
            "Tue",
            "Wed",
            "Thr",
            "Fri",
            "Sat",
            "Sun"});
            this.repeatWeeklyCheckedListBox.Location = new System.Drawing.Point(4, 38);
            this.repeatWeeklyCheckedListBox.Margin = new System.Windows.Forms.Padding(2);
            this.repeatWeeklyCheckedListBox.Name = "repeatWeeklyCheckedListBox";
            this.repeatWeeklyCheckedListBox.Size = new System.Drawing.Size(81, 109);
            this.repeatWeeklyCheckedListBox.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.repeatWeeklyCheckBox);
            this.groupBox1.Controls.Add(this.repeatWeeklyCheckedListBox);
            this.groupBox1.Location = new System.Drawing.Point(8, 124);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(144, 158);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Repeet weekly";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Date of first occurance\r\n";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(119, 306);
            this.okButton.Margin = new System.Windows.Forms.Padding(2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(50, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(185, 306);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(50, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // hoursNumericBox
            // 
            this.hoursNumericBox.AccessibleName = "Hours";
            this.hoursNumericBox.Location = new System.Drawing.Point(223, 77);
            this.hoursNumericBox.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hoursNumericBox.Name = "hoursNumericBox";
            this.hoursNumericBox.Size = new System.Drawing.Size(44, 20);
            this.hoursNumericBox.TabIndex = 2;
            this.hoursNumericBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // minuteNumbericBox
            // 
            this.minuteNumbericBox.AccessibleName = "Minutes";
            this.minuteNumbericBox.Location = new System.Drawing.Point(273, 77);
            this.minuteNumbericBox.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minuteNumbericBox.Name = "minuteNumbericBox";
            this.minuteNumbericBox.Size = new System.Drawing.Size(44, 20);
            this.minuteNumbericBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Time";
            // 
            // CreateReminderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(352, 333);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minuteNumbericBox);
            this.Controls.Add(this.hoursNumericBox);
            this.Controls.Add(this.repeatMonthlyCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.scheduledDatePicker);
            this.Controls.Add(this.repeatYearlyCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reminderNameStringBox);
            this.Name = "CreateReminderForm";
            this.Text = "CreateReminderForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumericBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumbericBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox reminderNameStringBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox repeatWeeklyCheckBox;
        private System.Windows.Forms.CheckBox repeatYearlyCheckBox;
        private System.Windows.Forms.DateTimePicker scheduledDatePicker;
        private System.Windows.Forms.CheckBox repeatMonthlyCheckBox;
        private System.Windows.Forms.CheckedListBox repeatWeeklyCheckedListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown hoursNumericBox;
        private System.Windows.Forms.NumericUpDown minuteNumbericBox;
        private System.Windows.Forms.Label label2;
    }
}