using GlobalHotKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AudioReminder
{
    class HotKeyListener
    {

        Thread hotKeyListeningThread;

        HotKeyManager hotKeyManager;
        HotKey hotKey;

        public HotKeyListener()
        {
            hotKeyListeningThread = new Thread(new ThreadStart(StartHotKeyListener));
            hotKeyListeningThread.SetApartmentState(ApartmentState.STA);
            StartHotKeyListener();
        }

        private void StartHotKeyListener()
        {
            HotKeyManager hotKeyManager = new HotKeyManager();
            hotKeyManager.KeyPressed += HotKeyManagerPressed;
            hotKey = hotKeyManager.Register(Key.F5, ModifierKeys.Control);
        }

        private void StopListener()
        {
            hotKeyManager.Unregister(hotKey);
            hotKeyManager.Dispose();
        }

        private void HotKeyManagerPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.HotKey.Key == Key.F5)
                MessageBox.Show("Hot key pressed!");
        }
    }
}
