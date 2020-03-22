namespace AudioUserInterface
{
    partial class SettignsForm
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
            this.components = new System.ComponentModel.Container();
            this.remindInterval = new System.Windows.Forms.TextBox();
            this.EventsGrid = new System.Windows.Forms.DataGridView();
            this.eventsDataSet = new AudioUserInterface.EventsDataSet();
            this.eventsDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eventsTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scheduledTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repeatWeeklyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.repeatYearlyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.repeatWeeklyDaysDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheduledDayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EventsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // remindInterval
            // 
            this.remindInterval.AccessibleName = "Remind internal";
            this.remindInterval.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AudioUserInterface.Properties.Settings.Default, "RemindInterval", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.remindInterval.Location = new System.Drawing.Point(12, 12);
            this.remindInterval.Name = "remindInterval";
            this.remindInterval.Size = new System.Drawing.Size(100, 20);
            this.remindInterval.TabIndex = 0;
            this.remindInterval.Text = global::AudioUserInterface.Properties.Settings.Default.RemindInterval;
            // 
            // EventsGrid
            // 
            this.EventsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EventsGrid.AutoGenerateColumns = false;
            this.EventsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EventsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.scheduledTimeDataGridViewTextBoxColumn,
            this.repeatWeeklyDataGridViewCheckBoxColumn,
            this.repeatYearlyDataGridViewCheckBoxColumn,
            this.repeatWeeklyDaysDataGridViewTextBoxColumn,
            this.sheduledDayDataGridViewTextBoxColumn});
            this.EventsGrid.DataSource = this.eventsTableBindingSource;
            this.EventsGrid.Location = new System.Drawing.Point(12, 38);
            this.EventsGrid.Name = "EventsGrid";
            this.EventsGrid.Size = new System.Drawing.Size(325, 275);
            this.EventsGrid.TabIndex = 1;
            // 
            // eventsDataSet
            // 
            this.eventsDataSet.DataSetName = "EventsDataSet";
            this.eventsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // eventsDataSetBindingSource
            // 
            this.eventsDataSetBindingSource.DataSource = this.eventsDataSet;
            this.eventsDataSetBindingSource.Position = 0;
            // 
            // eventsTableBindingSource
            // 
            this.eventsTableBindingSource.DataMember = "EventsTable";
            this.eventsTableBindingSource.DataSource = this.eventsDataSet;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // scheduledTimeDataGridViewTextBoxColumn
            // 
            this.scheduledTimeDataGridViewTextBoxColumn.DataPropertyName = "ScheduledTime";
            this.scheduledTimeDataGridViewTextBoxColumn.HeaderText = "ScheduledTime";
            this.scheduledTimeDataGridViewTextBoxColumn.Name = "scheduledTimeDataGridViewTextBoxColumn";
            // 
            // repeatWeeklyDataGridViewCheckBoxColumn
            // 
            this.repeatWeeklyDataGridViewCheckBoxColumn.DataPropertyName = "RepeatWeekly";
            this.repeatWeeklyDataGridViewCheckBoxColumn.HeaderText = "RepeatWeekly";
            this.repeatWeeklyDataGridViewCheckBoxColumn.Name = "repeatWeeklyDataGridViewCheckBoxColumn";
            // 
            // repeatYearlyDataGridViewCheckBoxColumn
            // 
            this.repeatYearlyDataGridViewCheckBoxColumn.DataPropertyName = "RepeatYearly";
            this.repeatYearlyDataGridViewCheckBoxColumn.HeaderText = "RepeatYearly";
            this.repeatYearlyDataGridViewCheckBoxColumn.Name = "repeatYearlyDataGridViewCheckBoxColumn";
            // 
            // repeatWeeklyDaysDataGridViewTextBoxColumn
            // 
            this.repeatWeeklyDaysDataGridViewTextBoxColumn.DataPropertyName = "RepeatWeeklyDays";
            this.repeatWeeklyDaysDataGridViewTextBoxColumn.HeaderText = "RepeatWeeklyDays";
            this.repeatWeeklyDaysDataGridViewTextBoxColumn.Name = "repeatWeeklyDaysDataGridViewTextBoxColumn";
            // 
            // sheduledDayDataGridViewTextBoxColumn
            // 
            this.sheduledDayDataGridViewTextBoxColumn.DataPropertyName = "SheduledDay";
            this.sheduledDayDataGridViewTextBoxColumn.HeaderText = "SheduledDay";
            this.sheduledDayDataGridViewTextBoxColumn.Name = "sheduledDayDataGridViewTextBoxColumn";
            // 
            // SettignsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 325);
            this.Controls.Add(this.EventsGrid);
            this.Controls.Add(this.remindInterval);
            this.Name = "SettignsForm";
            this.Text = "Audio Reminder - Settings";
            ((System.ComponentModel.ISupportInitialize)(this.EventsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox remindInterval;
        private System.Windows.Forms.DataGridView EventsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scheduledTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn repeatWeeklyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn repeatYearlyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn repeatWeeklyDaysDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheduledDayDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource eventsTableBindingSource;
        private EventsDataSet eventsDataSet;
        private System.Windows.Forms.BindingSource eventsDataSetBindingSource;
    }
}

