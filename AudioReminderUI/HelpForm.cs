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
    public partial class helpForm : Form
    {
        public helpForm()
        {
            InitializeComponent();

            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Translate();
        }

        protected virtual void Translate()
        {
            Text = TranslationProvider.Tr("helpFormTitle");
            label1.Text = TranslationProvider.Tr("userManualLabel");
            visitProjectWebsiteButton.Text = TranslationProvider.Tr("visitPageButton");
            backButton.Text = TranslationProvider.Tr("backButton");
        }

        private void visitProjectWebsiteButton_Click(object sender, EventArgs e)
        {
            //TODO: extract somwhere this and the link
            System.Diagnostics.Process.Start("https://github.com/dupop/audio-reminder");
        }
        
    }
}
