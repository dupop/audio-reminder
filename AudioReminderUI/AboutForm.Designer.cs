namespace AudioReminderUI
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.visitProjectWebsiteButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.creditsBox = new System.Windows.Forms.RichTextBox();
            this.versionBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // visitProjectWebsiteButton
            // 
            this.visitProjectWebsiteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitProjectWebsiteButton.AutoSize = true;
            this.visitProjectWebsiteButton.Location = new System.Drawing.Point(319, 391);
            this.visitProjectWebsiteButton.Margin = new System.Windows.Forms.Padding(4);
            this.visitProjectWebsiteButton.Name = "visitProjectWebsiteButton";
            this.visitProjectWebsiteButton.Size = new System.Drawing.Size(181, 28);
            this.visitProjectWebsiteButton.TabIndex = 2;
            this.visitProjectWebsiteButton.Text = "Visit the project website";
            this.visitProjectWebsiteButton.UseVisualStyleBackColor = true;
            this.visitProjectWebsiteButton.Click += new System.EventHandler(this.visitProjectWebsiteButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.backButton.Location = new System.Drawing.Point(507, 391);
            this.backButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 28);
            this.backButton.TabIndex = 3;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // creditsBox
            // 
            this.creditsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.creditsBox.Location = new System.Drawing.Point(12, 12);
            this.creditsBox.Name = "creditsBox";
            this.creditsBox.ReadOnly = true;
            this.creditsBox.Size = new System.Drawing.Size(598, 372);
            this.creditsBox.TabIndex = 0;
            this.creditsBox.Text = resources.GetString("creditsBox.Text");
            this.creditsBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.creditsBox_LinkClicked);
            // 
            // versionBox
            // 
            this.versionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.versionBox.Location = new System.Drawing.Point(12, 394);
            this.versionBox.Name = "versionBox";
            this.versionBox.ReadOnly = true;
            this.versionBox.Size = new System.Drawing.Size(100, 22);
            this.versionBox.TabIndex = 1;
            this.versionBox.Text = "Version 0.3";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.backButton;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.versionBox);
            this.Controls.Add(this.creditsBox);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.visitProjectWebsiteButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "AboutForm";
            this.Text = "Audio Reminder - Credits";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button visitProjectWebsiteButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.RichTextBox creditsBox;
        private System.Windows.Forms.TextBox versionBox;
    }
}