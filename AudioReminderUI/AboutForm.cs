using AudioReminderCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioReminderUI
{
    public partial class AboutForm : Form
    {
        const string version = "0.4";
        public AboutForm()
        {
            InitializeComponent();

            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Translate();
        }

        protected virtual void Translate()
        {
            Text = TranslationProvider.Tr("aboutFormTitle");
            versionBox.Text = TranslationProvider.Tr("versionBox") + " " + version;
            visitProjectWebsiteButton.Text = TranslationProvider.Tr("visitpageButton");
            backButton.Text = TranslationProvider.Tr("backButton");
            creditsBox.Text = TranslationProvider.Tr("creditsTextBoxDescription");
        }

        private void visitProjectWebsiteButton_Click(object sender, EventArgs e)
        {
            //TODO: extract somwhere this and the link
            System.Diagnostics.Process.Start("https://github.com/dupop/audio-reminder");
        }
        
        private void creditsBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText); 
        }
    }
}
