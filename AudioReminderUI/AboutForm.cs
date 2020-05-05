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
        }

        private void visitProjectWebsiteButton_Click(object sender, EventArgs e)
        {
            //TODO: extract somwhere this and the link
            System.Diagnostics.Process.Start("https://github.com/dupop/audio-reminder");
        }

        private void visitSoundsWebsiteButton_Click(object sender, EventArgs e)
        {
            //TODO: extract somwhere this and the link in the contributors description
            System.Diagnostics.Process.Start("https://freesound.org/"); 
        }

        private void creditsBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText); 
        }
    }
}
