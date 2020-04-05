using GlobalHotKey;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AudioReminderUI
{

    public class HotKeyListener
    {
        Thread hotKeyListeningThread;

        HotKeyManager hotKeyManager;
        HotKey hotKey;

        public HotKeyListener()
        {
            Log.Logger.Information("Creating HotKeyListener ");

            hotKeyListeningThread = new Thread(new ThreadStart(StartHotKeyListener));
            hotKeyListeningThread.SetApartmentState(ApartmentState.STA);
            hotKeyListeningThread.Start();
        }

        protected virtual void StartHotKeyListener()
        {
            Log.Logger.Information("Registering the hotkey");

            hotKeyManager = new HotKeyManager();
            hotKeyManager.KeyPressed += HotKeyManagerPressed;
            hotKey = hotKeyManager.Register(Key.D2, ModifierKeys.Control);

            Log.Logger.Information("Registering the hotkey done");
        }

        protected virtual void StopListener()
        {
            Log.Logger.Information("Unregistering the hotkey");

            hotKeyManager.Unregister(hotKey);
            hotKeyManager.Dispose();

            Log.Logger.Information("Unregistering the hotkey");
        }

        protected virtual void HotKeyManagerPressed(object sender, KeyPressedEventArgs e)
        {
            if (IsThisTheHotKey(e))
            {
                Log.Logger.Information("Hotkey pressed");
            }
        }

        private static bool IsThisTheHotKey(KeyPressedEventArgs e)
        {
            return e.HotKey.Key == Key.D2 && e.HotKey.Modifiers == ModifierKeys.Control;
        }
    }
}
