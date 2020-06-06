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
        public AboutForm()
        {
            InitializeComponent();

            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Translate();
        }

        protected virtual void Translate()
        {
            Text = TranslProvider.Tr("aboutFormTitle");
            versionBox.Text = TranslProvider.Tr("versionBox");
            visitProjectWebsiteButton.Text = TranslProvider.Tr("visitpageButton");
            backButton.Text = TranslProvider.Tr("backButton");
            creditsBox.Text = TranslProvider.Tr("creditsTextBoxDescription");


            //TODO: implement
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
