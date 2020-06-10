namespace AudioReminderUI
{
    partial class helpForm
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
            this.visitProjectWebsiteButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User manual is not yet written.";
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
            this.visitProjectWebsiteButton.Text = "Visit the project webpage";
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
            this.backButton.TabIndex = 9;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // helpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.backButton;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.visitProjectWebsiteButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "helpForm";
            this.Text = "Audio Reminder - User manual";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button visitProjectWebsiteButton;
        private System.Windows.Forms.Button backButton;
    }
}