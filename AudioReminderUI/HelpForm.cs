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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();

            Icon = AudioReminderCore.Properties.Resources.AudioReminderIcon;
            Translate();
        }

        protected virtual void Translate()
        {
            //TODO: implement
        }

        private void visitProjectWebsiteButton_Click(object sender, EventArgs e)
        {
            //TODO: extract somwhere this and the link
            System.Diagnostics.Process.Start("https://github.com/dupop/audio-reminder");
        }
        
    }
}
