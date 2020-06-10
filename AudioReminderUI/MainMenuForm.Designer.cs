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
            this.settingsButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // createReminderButton
            // 
            this.createReminderButton.AutoSize = true;
            this.createReminderButton.Location = new System.Drawing.Point(13, 118);
            this.createReminderButton.Margin = new System.Windows.Forms.Padding(4);
            this.createReminderButton.Name = "createReminderButton";
            this.createReminderButton.Size = new System.Drawing.Size(150, 28);
            this.createReminderButton.TabIndex = 0;
            this.createReminderButton.Text = "Create reminder";
            this.createReminderButton.UseVisualStyleBackColor = true;
            this.createReminderButton.Click += new System.EventHandler(this.createReminderButton_Click);
            // 
            // remindersButton
            // 
            this.remindersButton.Location = new System.Drawing.Point(13, 154);
            this.remindersButton.Margin = new System.Windows.Forms.Padding(4);
            this.remindersButton.Name = "remindersButton";
            this.remindersButton.Size = new System.Drawing.Size(150, 28);
            this.remindersButton.TabIndex = 1;
            this.remindersButton.Text = "Reminders";
            this.remindersButton.UseVisualStyleBackColor = true;
            this.remindersButton.Click += new System.EventHandler(this.remindersButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(13, 190);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(150, 28);
            this.settingsButton.TabIndex = 2;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.Settings_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(13, 262);
            this.aboutButton.Margin = new System.Windows.Forms.Padding(4);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(150, 28);
            this.aboutButton.TabIndex = 3;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.Enabled = false;
            this.helpButton.Location = new System.Drawing.Point(13, 226);
            this.helpButton.Margin = new System.Windows.Forms.Padding(4);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(150, 28);
            this.helpButton.TabIndex = 4;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AudioReminderUI.Properties.Resources.AudioReminderImage512;
            this.pictureBox1.Location = new System.Drawing.Point(170, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(440, 409);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.remindersButton);
            this.Controls.Add(this.createReminderButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainMenuForm";
            this.Text = "Audio Reminder";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createReminderButton;
        private System.Windows.Forms.Button remindersButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}