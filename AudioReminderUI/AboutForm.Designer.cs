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
            this.visitSoundsWebsiteButton = new System.Windows.Forms.Button();
            this.visitProjectWebsiteButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.creditsBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // visitSoundsWebsiteButton
            // 
            this.visitSoundsWebsiteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitSoundsWebsiteButton.AutoSize = true;
            this.visitSoundsWebsiteButton.Location = new System.Drawing.Point(336, 353);
            this.visitSoundsWebsiteButton.Margin = new System.Windows.Forms.Padding(4);
            this.visitSoundsWebsiteButton.Name = "visitSoundsWebsiteButton";
            this.visitSoundsWebsiteButton.Size = new System.Drawing.Size(191, 28);
            this.visitSoundsWebsiteButton.TabIndex = 1;
            this.visitSoundsWebsiteButton.Text = "Visit freesound.org website";
            this.visitSoundsWebsiteButton.UseVisualStyleBackColor = true;
            this.visitSoundsWebsiteButton.Click += new System.EventHandler(this.visitSoundsWebsiteButton_Click);
            // 
            // visitProjectWebsiteButton
            // 
            this.visitProjectWebsiteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitProjectWebsiteButton.AutoSize = true;
            this.visitProjectWebsiteButton.Location = new System.Drawing.Point(147, 353);
            this.visitProjectWebsiteButton.Margin = new System.Windows.Forms.Padding(4);
            this.visitProjectWebsiteButton.Name = "visitProjectWebsiteButton";
            this.visitProjectWebsiteButton.Size = new System.Drawing.Size(181, 28);
            this.visitProjectWebsiteButton.TabIndex = 2;
            this.visitProjectWebsiteButton.Text = "Visit the project webpage";
            this.visitProjectWebsiteButton.UseVisualStyleBackColor = true;
            this.visitProjectWebsiteButton.Click += new System.EventHandler(this.visitProjectWebsiteButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.backButton.Location = new System.Drawing.Point(533, 353);
            this.backButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 28);
            this.backButton.TabIndex = 10;
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
            this.creditsBox.Size = new System.Drawing.Size(624, 334);
            this.creditsBox.TabIndex = 12;
            this.creditsBox.Text = resources.GetString("creditsBox.Text");
            this.creditsBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.creditsBox_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.backButton;
            this.ClientSize = new System.Drawing.Size(648, 395);
            this.Controls.Add(this.creditsBox);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.visitProjectWebsiteButton);
            this.Controls.Add(this.visitSoundsWebsiteButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AboutForm";
            this.Text = "Credits";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button visitSoundsWebsiteButton;
        private System.Windows.Forms.Button visitProjectWebsiteButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.RichTextBox creditsBox;
    }
}