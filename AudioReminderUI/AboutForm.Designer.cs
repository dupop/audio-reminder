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
            this.label1 = new System.Windows.Forms.Label();
            this.visitSoundsWebsiteButton = new System.Windows.Forms.Button();
            this.visitProjectWebsiteButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 130);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // visitSoundsWebsiteButton
            // 
            this.visitSoundsWebsiteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitSoundsWebsiteButton.AutoSize = true;
            this.visitSoundsWebsiteButton.Location = new System.Drawing.Point(279, 287);
            this.visitSoundsWebsiteButton.Name = "visitSoundsWebsiteButton";
            this.visitSoundsWebsiteButton.Size = new System.Drawing.Size(116, 23);
            this.visitSoundsWebsiteButton.TabIndex = 1;
            this.visitSoundsWebsiteButton.Text = "Visit freeSFX website";
            this.visitSoundsWebsiteButton.UseVisualStyleBackColor = true;
            this.visitSoundsWebsiteButton.Click += new System.EventHandler(this.visitSoundsWebsiteButton_Click);
            // 
            // visitProjectWebsiteButton
            // 
            this.visitProjectWebsiteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitProjectWebsiteButton.AutoSize = true;
            this.visitProjectWebsiteButton.Location = new System.Drawing.Point(137, 287);
            this.visitProjectWebsiteButton.Name = "visitProjectWebsiteButton";
            this.visitProjectWebsiteButton.Size = new System.Drawing.Size(136, 23);
            this.visitProjectWebsiteButton.TabIndex = 2;
            this.visitProjectWebsiteButton.Text = "Visit the project webpage";
            this.visitProjectWebsiteButton.UseVisualStyleBackColor = true;
            this.visitProjectWebsiteButton.Click += new System.EventHandler(this.visitProjectWebsiteButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.backButton.Location = new System.Drawing.Point(400, 287);
            this.backButton.Margin = new System.Windows.Forms.Padding(2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.backButton;
            this.ClientSize = new System.Drawing.Size(486, 321);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.visitProjectWebsiteButton);
            this.Controls.Add(this.visitSoundsWebsiteButton);
            this.Controls.Add(this.label1);
            this.Name = "AboutForm";
            this.Text = "Credits";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button visitSoundsWebsiteButton;
        private System.Windows.Forms.Button visitProjectWebsiteButton;
        private System.Windows.Forms.Button backButton;
    }
}